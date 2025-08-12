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

            services.AddSingleton(serviceInterface, implType);
        }

        return services;
    }

    /// <summary>
    ///     Add code generators into IOC container
    /// </summary>
    /// <param name="services">service collection</param>
    /// <returns>service collection</returns>
    public static IServiceCollection AddCodeGenerators<TICodeGenerator>(this IServiceCollection services)
    {
        var assembly = Assembly.Load("Arale.CodeGen.Infrastructure");
        var interfaceType = typeof(TICodeGenerator);
        var implTypes = assembly
            .GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false } && interfaceType.IsAssignableFrom(t))
            .ToList();
        foreach (var implType in implTypes) services.AddSingleton(interfaceType, implType);

        return services;
    }
}
