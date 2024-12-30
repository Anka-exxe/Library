using System.Data;
using System.Security.Claims;

namespace Library.API.Extensions
{
    public static class PolicySetup
    {
        public static IServiceCollection AddPolicies(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
                options.AddPolicy("UserPolicy", policy => policy.RequireRole("Reader", "Admin"));
            });

            return services;
        }
    }
}
