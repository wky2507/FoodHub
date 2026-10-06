using FoodHub.Infrastructure.Model;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Net;
namespace FoodHub.PublicApi.ExceptionHandler
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            // 默认设置为 500 服务器错误
            var statusCode = (int)HttpStatusCode.InternalServerError;
            BaseResponse response;

            switch (exception)
            {
                // 1. 业务逻辑主动抛出的异常
                case BusinessException bizEx:
                    statusCode = (int)HttpStatusCode.BadRequest; // 也可以用 200，取决于前端约定
                    response = BaseResponse.Fail(bizEx.Message);
                    _logger.LogWarning("发生业务拦截: {Message}", bizEx.Message);
                    break;

                // 2. 数据库 / EF Core 写入异常
                case DbUpdateException dbEx:
                    response = BaseResponse.Fail("数据库操作失败，请检查提交数据的完整性。");
                    _logger.LogError(dbEx, "数据库写入异常: {Message}", dbEx.InnerException?.Message ?? dbEx.Message);
                    break;

                // 3. 参数为空异常
                case ArgumentNullException argEx:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    response = BaseResponse.Fail($"缺少必要的请求参数: {argEx.ParamName}");
                    _logger.LogWarning(argEx, "请求参数缺失");
                    break;

                // 4. 其他所有未预料到的系统崩溃异常
                default:
                    response = BaseResponse.Fail($"服务器内部错误，请稍后再试。详细信息:{exception.Message}");
                    _logger.LogError(exception, "捕获到未处理的系统未知异常: {Message}", exception.Message);
                    break;
            }

            // 写入 HTTP 响应
            httpContext.Response.StatusCode = statusCode;
            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

            // 返回 true 代表该异常已被妥善处理，终止异常继续向上抛出
            return true;
        }
    }
}