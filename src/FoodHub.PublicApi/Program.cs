using FoodHub.BlazorShared;
using FoodHub.Domain.Entity.BuyerAggregate;
using FoodHub.Domain.Entity.Identity;
using FoodHub.Domain.Entity.MerchantApplicationAggregate;
using FoodHub.Domain.Entity.OrderAggregate;
using FoodHub.Domain.Interface;
using FoodHub.Infrastructure.Data;
using FoodHub.Infrastructure.Services;
using FoodHub.Infrastructure.Services.Jwt;
using FoodHub.PublicApi.Config;
using FoodHub.PublicApi.ExceptionHandler;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text; // 添加此行

namespace FoodHub.PublicApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSwaggerGen();

            builder.Services.AddControllers();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi


            var cns = builder.Configuration.GetConnectionString("CatalogConnection");

            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(cns,
                                    b => b.MigrationsAssembly(typeof(AppIdentityDbContext).Assembly.FullName)
                ));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                            .AddEntityFrameworkStores<AppIdentityDbContext>()
                            .AddDefaultTokenProviders()
                            .AddRoles<IdentityRole>()
                            .AddErrorDescriber<ChineseIdentityErrorDescriber>();

            builder.Host.UseSerilog((ctx, lc) => lc
                        .ReadFrom.Configuration(ctx.Configuration)
                        .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)

                );

            builder.Services.AddScoped(typeof(IRepository<>), typeof(EfReponsitory<>));

            builder.Services.AddScoped(typeof(IReadRepository<>), typeof(EfReponsitory<>));

            builder.Services.AddScoped<IdentityTokenService>();

            builder.Services.AddScoped<IOrderRepository, OrderRepository>();

            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            //CORS(跨域)
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            //var key = Encoding.ASCII.GetBytes(AuthorizationConstants.JWT_SECRET_KEY);

            builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));

            builder.Services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(
                config =>
                {
                    config.RequireHttpsMetadata = false;

                    config.SaveToken = true;

                    config.MapInboundClaims = false;

                    var jwtOpt = builder.Configuration.GetSection("Jwt").Get<JwtOptions>()!;

                    var key = Encoding.ASCII.GetBytes(jwtOpt.SecretKey);

                    config.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,

                        IssuerSigningKey = new SymmetricSecurityKey(key),

                        ValidateIssuer = false,

                        ValidIssuer = jwtOpt.Issuer,

                        ValidateAudience = false,

                        ValidAudience = jwtOpt.Audience,

                        //要和生成AccessToken的role位置匹配起来
                        RoleClaimType = "role",

                        NameClaimType = "sub",

                    };

                    config.Events = new JwtBearerEvents
                    {
                        OnChallenge = context =>
                        {
                            context.HandleResponse();

                            context.Response.StatusCode = 401;

                            context.Response.ContentType = "application/json";

                            var response = new BaseApiResponse<object>
                            {

                                Code = 401,

                                Message = "身份证失败",

                                Data = null

                            };

                            return context.Response.WriteAsJsonAsync(response);

                        },

                        OnAuthenticationFailed = context =>
                        {
                            Console.WriteLine("[JWT 认证失败原因]:" + context.Exception.Message);

                            return Task.CompletedTask;

                        }

                    };

                });

            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

            builder.Services.AddProblemDetails();

            builder.Services.Configure<FileStorageOptions>(builder.Configuration.GetSection("FileStorage"));

            // 清空默认的 Claim 映射字典，避免长 short key 转换导致的匹配失灵
            //System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            var app = builder.Build();

            app.UseExceptionHandler();

            app.UseStaticFiles();

            app.Logger.LogInformation("Api App Created");

            using (var scope = app.Services.CreateScope())
            {

                var scopedProvider = scope.ServiceProvider;

                try
                {
                    var identityContext = scopedProvider.GetRequiredService<AppIdentityDbContext>();

                    var userManager = scopedProvider.GetRequiredService<UserManager<ApplicationUser>>();

                    var roleManager = scopedProvider.GetRequiredService<RoleManager<IdentityRole>>();

                    var merchantApply = scopedProvider.GetRequiredService<IRepository<MerchantApplication>>();

                    var buyerManager = scopedProvider.GetRequiredService<IRepository<Buyer>>();

                    var orderManager = scopedProvider.GetRequiredService<IRepository<Order>>();

                    var productManager = scopedProvider.GetRequiredService<IRepository<Product>>();

                    await AppIdentityDbContextSeed.SeedAsync(identityContext, userManager, roleManager, merchantApply,buyerManager,orderManager,productManager);

                    //await SeedFactory.SeedOrderAsync(orderManager, productManager);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }

            // Configure the HTTP request pipeline.

            if (builder.Environment.IsDevelopment())
            {
                app.UseHttpsRedirection();
            }

            app.UseRouting();

            app.UseCors("AllowAll");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSwagger();

            //app.UseSwaggerUI(c => { c.SwaggerEndpoint("swagger/123/swagger.json", "FoodHub Api V1"); });

            app.UseSwaggerUI();

            app.MapControllers();

            app.Run();
        }
    }
}
