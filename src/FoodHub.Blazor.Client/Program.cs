using Blazored.LocalStorage;
using Blazored.SessionStorage;
using FoodHub.Blazor.Client.Component;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Options;
using FoodHub.Blazor.Client.Security;
using FoodHub.Blazor.Client.Services;
using FoodHub.Blazor.Client.Authentication;
using FoodHub.Blazor.Client.Interfaces;
using FoodHub.Blazor.Client.ApiService.Merchant;
using FoodHub.Blazor.Client.Helper;

namespace FoodHub.Blazor.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.RootComponents.Add<App>("#app");
            
            builder.RootComponents.Add<HeadOutlet>("head::after");

            var configuration = builder.Configuration.GetRequiredSection(BaseUrlConfiguration.CONFIG_NAME);

            builder.Services.Configure<BaseUrlConfiguration>(configuration);

            //builder.Services.AddScoped(sp =>
            //{

            //    var config = sp.GetRequiredService<IOptions<BaseUrlConfiguration>>().Value;

            //    return new HttpClient() { BaseAddress = new Uri(config.ApiBase) };

            //});

            builder.Services.AddScoped<RegisterService>();

            builder.Services.AddScoped<AuthService>();

            builder.Services.AddScoped<HttpService>();

            builder.Services.AddBlazoredLocalStorage();

            builder.Services.AddBlazoredSessionStorage();

            builder.Services.AddScoped<CustomAuthenticationStateProvider>();

            builder.Services.AddAuthorizationCore();

            builder.Services.AddCascadingAuthenticationState();

            builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<CustomAuthenticationStateProvider>());

            builder.Services.AddScoped<ICurrentUser,CurrentUser>();

            builder.Services.AddScoped<TokenStorage>();

            builder.Services.AddScoped<RefreshAccessTokenService>();

            builder.Services.AddScoped<ToastService>();

            builder.Services.AddScoped<ApplicantService>();

            builder.Services.AddScoped<IDraftService, DraftService>();

            builder.Services.AddScoped<ApplyManageMentService>();

            builder.Services.AddScoped<MerChantService>();

            builder.Services.AddScoped<PathTools>();

            builder.Services.AddScoped<IImageTools,ImageTools>();

            //加鉴权，每个页面默认需要登录
            // 1. 注册底层授权核心（WASM唯一可用）
            builder.Services.AddAuthorizationCore(options =>
            {
                // 全局兜底：无特性页面强制登录
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            });

            builder.Services.AddTransient<GlobalHttpInterceptorHandler>();
           
            builder.Services.AddHttpClient<ServerApiClient>(client =>
            {
                string apiBase = builder.Configuration["BaseUrls:ApiBase"] ?? throw new InvalidOperationException("未找到配置");

                client.BaseAddress = new Uri(apiBase);

            }).AddHttpMessageHandler<GlobalHttpInterceptorHandler>();

            builder.Services.AddHttpClient<HttpService>(client =>
            {

                string apiBase = builder.Configuration["BaseUrls:ApiBase"] ?? throw new InvalidOperationException("未找到配置");

                client.BaseAddress = new Uri(apiBase);

            }).AddHttpMessageHandler<GlobalHttpInterceptorHandler>();

            await builder.Build().RunAsync();

    }
    }
}
