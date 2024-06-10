using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PosApi.Extensions.Swagger
{
    public sealed class ConfigureSwaggerGenOptions : IConfigureOptions<SwaggerGenOptions>
    {
        #region Private properties.

        private readonly IApiVersionDescriptionProvider _versionDescriptionProvider;
        private readonly SwaggerConfig _swaggerSetting;

        #endregion Private properties.

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigureSwaggerGenOptions"/> class.
        /// </summary>
        /// <param name="versionDescriptionProvider"></param>
        /// <param name="swaggerConfig"></param>
        public ConfigureSwaggerGenOptions(IApiVersionDescriptionProvider versionDescriptionProvider, IOptions<SwaggerConfig> swaggerConfig)
        {
            _versionDescriptionProvider = versionDescriptionProvider;
            _swaggerSetting = swaggerConfig.Value;
        }

        #endregion constructor

        #region Configure

        /// <summary>
        /// Configure custom options on Swagger
        /// </summary>
        /// <param name="options"></param>
        public void Configure(SwaggerGenOptions options)
        {
            options.IgnoreObsoleteActions();
            options.IgnoreObsoleteProperties();
            options.SchemaGeneratorOptions.UseInlineDefinitionsForEnums = true;
            options.SchemaGeneratorOptions.UseAllOfForInheritance = true;
            options.SchemaGeneratorOptions.UseAllOfToExtendReferenceSchemas = true;
            //options.SchemaGeneratorOptions.UseOneOfForPolymorphism = false;

            //use fully qualified object names
            options.CustomSchemaIds(schemaIdSelector: x => Regex.Replace(x.FullName ?? $"{x.Namespace}.{x.Name}", "[^a-zA-Z0-9\\.\\-_]", "_"));
            //options.CustomSchemaIds(x=>x.FullName);
            // Makes the namespaces hidden for the schemas
            options.SchemaFilter<NamespaceSchemaFilter>();
            AddSwaggerDocumentForEachDiscoveredApiVersion(options);
            //SetCommentsPathForSwaggerJsonAndUi(options);
            AddSecurityParameters(options);

        }

        public class NamespaceSchemaFilter : ISchemaFilter
        {
            public void Apply(OpenApiSchema schema, SchemaFilterContext context)
            {
                if (schema is null)
                {
                    throw new ArgumentNullException(nameof(schema));
                }

                if (context is null)
                {
                    throw new ArgumentNullException(nameof(context));
                }

                schema.Title = context.Type.Name; // To replace the full name with namespace with the class name only
            }
        }

        #endregion Configure

        #region Api versioning

        /// <summary>
        /// Api version document addition
        /// </summary>
        /// <param name="options"></param>
        private void AddSwaggerDocumentForEachDiscoveredApiVersion(SwaggerGenOptions options)
        {
            foreach (var description in _versionDescriptionProvider.ApiVersionDescriptions)
            {
                _swaggerSetting.Info.Version = description.ApiVersion.ToString();

                if (description.IsDeprecated)
                {
                    _swaggerSetting.Info.Description += " - DEPRECATED";
                }
                options.SwaggerDoc(description.GroupName, _swaggerSetting.Info);
            }
        }

        #endregion Api versioning

        #region Security parameters

        /// <summary>
        /// Adding authorize attribute .. sending api key in header
        /// </summary>
        /// <param name="options"></param>
        private static void AddSecurityParameters(SwaggerGenOptions options)
        {
            //SetSecurityParameterForApiKey(options);
            SetSecurityParameterForAuthenticationToken(options);
        }

        private static void SetSecurityParameterForAuthenticationToken(SwaggerGenOptions options)
        {
            options.AddSecurityDefinition(ApiConstants.AuthenticationToken, new OpenApiSecurityScheme
            {
                Description = "Token needed to access the endpoints",
                In = ParameterLocation.Header,
                Name = ApiConstants.AuthenticationToken,
                Type = SecuritySchemeType.ApiKey
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Name = ApiConstants.AuthenticationToken,
                        Type = SecuritySchemeType.ApiKey,
                        In = ParameterLocation.Header,
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = ApiConstants.AuthenticationToken }
                    },
                    new string[] {}
                }
            });

        }
        // PlaceHolder for Api Key
        private static void SetSecurityParameterForApiKey(SwaggerGenOptions options)
        {
            options.AddSecurityDefinition(ApiConstants.ApiKey, new OpenApiSecurityScheme
            {
                Description = "Api key needed to access the endpoints",
                In = ParameterLocation.Header,
                Name = ApiConstants.ApiKey,
                Type = SecuritySchemeType.ApiKey
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Name = ApiConstants.ApiKey,
                        Type = SecuritySchemeType.ApiKey,
                        In = ParameterLocation.Header,
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = ApiConstants.ApiKey }
                    },
                    new string[] {}
                }
            });
        }

        #endregion Security parameters

        #region Swagger comments

        /// <summary>
        /// Swagger Comments
        /// </summary>
        /// <param name="options"></param>
        //private static void SetCommentsPathForSwaggerJsonAndUi(SwaggerGenOptions options)
        //{
        //    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        //    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        //    options.IncludeXmlComments(xmlPath);
        //    //options.CustomSchemaIds(x => x.FullName);
        //}

        #endregion Swagger comments
    }
}
