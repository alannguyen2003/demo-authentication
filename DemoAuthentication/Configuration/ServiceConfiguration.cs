using DemoAuthentication.Data;

namespace DemoAuthentication.Configuration;

public static class ServiceConfiguration
{
    public static IServiceCollection AddDI(this IServiceCollection services)
    {
        
        services.AddDbContext<ApplicationDbContext>();
        return services;
    }
    
    
}