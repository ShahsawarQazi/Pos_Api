using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace PosApi.Extensions.Swagger
{
    public sealed class ConfigureSwaggerOptions : IConfigureOptions<SwaggerOptions>
    {

        #region Private properties

        private readonly SwaggerConfig _swaggerConfig;

        #endregion Private properties
        #region Constructor

        public ConfigureSwaggerOptions(IOptions<SwaggerConfig> swaggerConfig)
        {
            _swaggerConfig = swaggerConfig.Value;
        }

        #endregion Constructor

        public void Configure(SwaggerOptions options)
        {
            options.RouteTemplate = _swaggerConfig.RoutePrefixWithSlash + "{documentName}/swagger.json";
            //options.SerializeAsV2 = true;
        }
    }
}
