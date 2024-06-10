using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Pos.Application.Common.Behaviours;
using Pos.Application.Common.ValidationDefinitions;

namespace Pos.Application.Bootstrap
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.Load("Pos.Application")));
            services.AddValidatorsFromAssembly(AppDomain.CurrentDomain.Load("Pos.Application"));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            ValidatorOptions.Global.LanguageManager = new CustomValidationErrorCodeAndMessage();
            //CascadeMode.Stop will break processing immediately and throw validation exception
            ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;
            ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;

            AddAppServices(services);
            return services;
        }

        private static void AddAppServices(IServiceCollection services)
        {
            //services.AddTransient(typeof(IClientService), typeof(ClientService));

        }
    }
}
