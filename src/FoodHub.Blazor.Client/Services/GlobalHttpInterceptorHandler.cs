using Microsoft.AspNetCore.Components;
using System.Net;
using System.Net.Http.Headers;
using FoodHub.Blazor.Client.Authentication;
namespace FoodHub.Blazor.Client.Services
{
    public class GlobalHttpInterceptorHandler   :  DelegatingHandler
    {

        private readonly NavigationManager _navigationManager;

        private readonly TokenStorage _tokenStorage;

        private readonly RefreshAccessTokenService _refreshAccessTokenService;

        //private readonly ToastService _toastService;

        private readonly IServiceProvider _serviceProvider;

        private readonly ILogger<GlobalHttpInterceptorHandler> _logger;

        public GlobalHttpInterceptorHandler(NavigationManager navigationManager, TokenStorage tokenStorage,RefreshAccessTokenService refreshAccessTokenService,ToastService toastService,IServiceProvider serviceProvider,ILogger<GlobalHttpInterceptorHandler> logger)
        {

            _navigationManager = navigationManager;

            _tokenStorage = tokenStorage;

            _refreshAccessTokenService = refreshAccessTokenService;

            _serviceProvider = serviceProvider;

            _logger = logger;
        }


        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {

                await AttachAccessTokenAsync(request);

                var response = await base.SendAsync(request, cancellationToken);

                //接收结果，认证没有被授权
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    //重新尝试拿Token
                    var refreshSuccess = await _refreshAccessTokenService.RefreshAccessTokenAsync();
                    //成功拿到
                    if (refreshSuccess)
                    {
                        response.Dispose();
                        //复制请求
                        var retryRequest = await CloneHttpRequestMessageAsync(request);
                        //添加AccessToken
                        await AttachAccessTokenAsync(retryRequest);
                        //再次发送
                        response = await base.SendAsync(retryRequest, cancellationToken);

                    }

                }

                if (!response.IsSuccessStatusCode)
                {

                    switch (response.StatusCode)
                    {

                        case HttpStatusCode.BadRequest:
                            //弹窗
                            //_navigationManager.NavigateTo("400");
                            return response;
                        //break;

                        case HttpStatusCode.Unauthorized:

                            _navigationManager.NavigateTo("/login");

                            break;

                        case HttpStatusCode.Forbidden:

                            //_navigationManager.NavigateTo("/403");
                            _serviceProvider.GetRequiredService<ToastService>().ShowError("权限不足!", "权限错误");

                            break;

                        case HttpStatusCode.NotFound:
                            _navigationManager.NavigateTo("/404");

                            break;

                        case HttpStatusCode.InternalServerError:
                            //弹窗
                            //_navigationManager.NavigateTo("/500");
                            _serviceProvider.GetRequiredService<ToastService>().ShowError("服务器内部异常，请稍后重试或联系管理员!", "服务器异常");


                            break;
                        default:

                            //_navigationManager.NavigateTo("/Unknow");
                            _serviceProvider.GetRequiredService<ToastService>().ShowError("服务器开小差,请稍后重试!", "服务器异常");
                            break;
                    }
                }

                return response;
            }

            catch (HttpRequestException ex)
            {
                //_toastService.ShowError("网络断开，请检查网络连接", "网络异常");
                _serviceProvider.GetRequiredService<ToastService>().ShowError("网络断开，请检查网络连接!", "网络异常");

                throw new HttpNetworkException("网络异常，检查网络连接或者服务状态!", ex);

            }
            //不是客户端取消请求
            catch (OperationCanceledException ex) when (!cancellationToken.IsCancellationRequested)
            {
                //_toastService.ShowError("服务器响应超时,请稍后充实!", "请求超时");
                _serviceProvider.GetRequiredService<ToastService>().ShowError("服务器响应超时,请稍后充实!", "请求超时");

                throw new HttpCanceledException("请求超时，请稍后重试!", ex);

            }
            //客户端请求取消的直接不用理
            catch (OperationCanceledException ex)
            {

                _logger.LogInformation($"[{ex.Message}]  客户端自己取消的请求");

                throw;
            }
            //catch (DbException ex) { 
            
            //}
            //参数异常自己给它往上抛出，不用拦截，上层自己拦截
            //catch (ArgumentNullException ex)
            //{

            //    throw new ArgumentInvalidException("请求参数为空!", ex);

            //}
            //catch (ArgumentException ex) {

            //    throw new ArgumentInvalidException("请求参数非法!", ex);
            
            //}
        }

        private async Task AttachAccessTokenAsync(HttpRequestMessage request)
        {
            var accessToken = await _tokenStorage.GetAccessTokenAsync();

            if (!string.IsNullOrEmpty(accessToken))
            {

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            }

            var authHeader = request.Headers.Authorization;

            if (authHeader != null) {

                var schema = authHeader.Scheme;

                var token = authHeader.Parameter;

                Console.WriteLine($"请求头的AccessToken{token}");
            }


        }

        private static async Task<HttpRequestMessage> CloneHttpRequestMessageAsync(HttpRequestMessage request)
        {
            var clone = new HttpRequestMessage
                (
                    request.Method, request.RequestUri
                );

            foreach (var header in request.Headers)
            {

                clone.Headers.TryAddWithoutValidation(

                    header.Key,

                    header.Value

                    );
            }

            if (request.Content != null)
            {

                var ms = new MemoryStream();

                await request.Content.CopyToAsync(ms);

                ms.Position = 0;

                clone.Content = new StreamContent(ms);

                foreach (var header in request.Content.Headers)
                {

                    clone.Content.Headers.TryAddWithoutValidation(

                    header.Key,

                    header.Value

                    );

                }

            }

            return clone;

        }
    }
}
