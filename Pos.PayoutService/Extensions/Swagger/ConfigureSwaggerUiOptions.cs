using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace PosApi.Extensions.Swagger
{
    public sealed class ConfigureSwaggerUiOptions : IConfigureOptions<SwaggerUIOptions>
    {
        #region Private properties

        private readonly IApiVersionDescriptionProvider _versionDescriptionProvider;
        private readonly SwaggerConfig _swaggerSetting;

        #endregion Private properties

        #region Constructor

        public ConfigureSwaggerUiOptions(IApiVersionDescriptionProvider versionDescriptionProvider, IOptions<SwaggerConfig> swaggerSetting)
        {
            _versionDescriptionProvider = versionDescriptionProvider;
            _swaggerSetting = swaggerSetting.Value;
        }

        #endregion Constructor

        #region Configure

        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="options"></param>
        public void Configure(SwaggerUIOptions options)
        {
            _versionDescriptionProvider
                .ApiVersionDescriptions
                .ToList()
                .ForEach(description =>
                {
                    options.SwaggerEndpoint(
                        $"/{_swaggerSetting.RoutePrefixWithSlash}{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());

                    options.RoutePrefix = _swaggerSetting.RoutePrefix;
                });
        }

        #endregion Configure
    }

}
