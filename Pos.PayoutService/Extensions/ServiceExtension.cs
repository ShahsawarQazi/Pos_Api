using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using PosApi.Bootstrapping;
using PosApi.Extensions.Swagger;

namespace PosApi.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            ConfigureCompression(services);
            ConfigureSwagger(services);
            ConfigureCors(services, configuration);
            ConfigureControllers(services);
            //Register custom services
            bool authenticationEnabled = false; // Set your flag here

            if (authenticationEnabled)
            {
                services.AddAuthentication(options =>
                {
                    options.DefaultScheme = OpenIdConnectDefaults.AuthenticationScheme;
                })
                    .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
                    {
                        options.Authority = "https://yourADFSdomain.com";
                        options.ClientId = "your-client-id";
                        options.ResponseType = OpenIdConnectResponseType.CodeIdToken;
                        options.SaveTokens = true;
                    });
            }

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddHttpContextAccessor();
            services.AddHttpClient();

            services = services.Bootstrap(configuration);

            return services;
        }
        private static void ConfigureControllers(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                // By default, string return types are formatted as text / plain.
                options.OutputFormatters.RemoveType<StringOutputFormatter>();
                // Replace '204 No Content' with Null.
                options.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();

            })
                // Since dictionary with non-string keys is not supported in System.Text.Json yet
                // so using NewtonsoftJson for now.
                // https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-migrate-from-newtonsoft-how-to
                //.AddNewtonsoftJson()
                .AddJsonOptions(options =>
                {
                    var enumConverter = new JsonStringEnumConverter();
                    options.JsonSerializerOptions.Converters.Add(enumConverter);

                });

        }
        private static void ConfigureCors(IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                var allowedDomain = configuration.GetValue<string>("AppConfig:AllowedDomains")
                    ?.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(a => a.Trim()).ToArray() ?? Array.Empty<string>();

                options.AddPolicy(ApiConstants.CorsPolicy,
                    builder =>
                    {
                        builder.WithOrigins(allowedDomain)
                            .SetIsOriginAllowedToAllowWildcardSubdomains()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                    });
            });
        }
        private static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddApiVersionWithExplorer();
            services.AddSwaggerOptions();
            services.AddSwaggerGen();
        }
        private static void ConfigureCompression(IServiceCollection services)
        {
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<GzipCompressionProvider>();
            });
        }
    }
}
