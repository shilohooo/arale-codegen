using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Arale.CodeGen.Extensions;

/// <summary>
///     Extension methods for service collection
/// </summary>
public static class ServiceExtensions
{
    /// <summary>
    ///     Add services into IOC container from assembly
    /// </summary>
    /// <param name="services">service collection</param>
    /// <param name="assembly">services assembly to add</param>
    /// <returns>service collection</returns>
    public static IServiceCollection AddServicesFromAssembly(this IServiceCollection services, Assembly assembly)
    {
        var implTypes = assembly.GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false })
            .ToList();
        var serviceInterfaces = assembly.GetTypes()
            .Where(t => t.IsInterface)
            .ToList();
        foreach (var serviceInterface in serviceInterfaces)
        {
            var implType = implTypes.FirstOrDefault(impl => serviceInterface.IsAssignableFrom(impl));
            if (implType is null) continue;

            Console.WriteLine(
                $"{nameof(ServiceExtensions)} - Added {serviceInterface.Name}:{implType.Name} to IOC container");
            services.AddSingleton(serviceInterface, implType);
        }

        return services;
    }
}
