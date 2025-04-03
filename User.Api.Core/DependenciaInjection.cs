using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace User.Api.Core
{
    public static class DependenciaInjection
    {
        public static void AddCore(this IServiceCollection services)
        {
            services.AddScoped<GetUserBusiness>();
            services.AddScoped<SaveUserBusiness>();
        }   
    }
}
