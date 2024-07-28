using Ejercicio5Modulo3.Models;
using Microsoft.EntityFrameworkCore;

public static class CustomDependency
{
    public static void AddCustomDependency(this IServiceCollection services, ConfigurationManager builder)
    {
        services.AddDbContext<Ejercicio5Modulo3Context>
            (options => options.UseSqlServer(builder.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<GlobalExceptionHandler>();
    }
}