using Exams.Application.Interfaces;
using Exams.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Exams.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPasswordHasherService, PasswordHasherService>();

            return services;
        }
    }
}
