using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using NV.Beautician.Application.Beautician.Commands.AddBeautician;
using NV.Beautician.Application.Beautician.Commands.UpdateBeautician;

namespace NV.Beautician.Application
{
    public static class AssemblyReferences
    {
        public static IServiceCollection AddBeauticianApplication(this IServiceCollection services)
        {
            services.AddMediatR(x => x.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            //fluent validation
            services.AddScoped<IValidator<AddBeauticianCommand>, AddBeauticianCommandValidator>();
            services.AddScoped<IValidator<UpdateBeauticianCommand>, UpdateBeauticianCommandValidator>();

            return services;
        }

    }
}
