using Pos.Application.Bootstrap;
using Pos.Application.Common.Configuration;
using Pos.Infrastructure.Bootstrap;
using PosApi.Extensions.Swagger;

namespace PosApi.Bootstrapping
{
    public static class Bootstrapper
    {
        public static IServiceCollection Bootstrap(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            configuration.Bind(AppConfigurations.Configuration);
            serviceCollection.AddApplication();
            serviceCollection.AddInfrastructure(configuration);
            serviceCollection.Configure<SwaggerConfig>(configuration.GetSection(nameof(SwaggerConfig)));
            //serviceCollection.BootstrapLogComponents(configuration);
            //serviceCollection.AddApplicationInsightsTelemetry();
            return serviceCollection;
        }


    }
}
