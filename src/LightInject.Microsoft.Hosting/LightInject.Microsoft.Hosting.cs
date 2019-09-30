using System;
using LightInject.Microsoft.DependencyInjection;
using Microsoft.Extensions.Hosting;

#nullable enable

namespace LightInject.Microsoft.Hosting
{
    /// <summary>
    /// Extends the <see cref="IHostBuilder"/> to enable LightInject to be used as the service container.
    /// </summary>
    public static class HostBuilderExtensions
    {
        public static IHostBuilder UseLightInject(this IHostBuilder builder, Action<IServiceRegistry>? action = null)
        {
            var container = new ServiceContainer(ContainerOptions.Default.Clone().WithMicrosoftSettings().WithAspNetCoreSettings());
            action?.Invoke(container);
            return builder.UseServiceProviderFactory(new LightInjectServiceProviderFactory(container));
        }
    }

    /// <summary>
    /// Extends the <see cref="ContainerOptions"/> class.
    /// </summary>
    public static class ContainerOptionsExtensions
    {
        /// <summary>
        /// Sets up the <see cref="ContainerOptions"/> to be compliant with the conventions used in Microsoft.Extensions.DependencyInjection.
        /// </summary>
        /// <param name="options">The target <see cref="ContainerOptions"/>.</param>
        /// <returns><see cref="ContainerOptions"/>.</returns>
        public static ContainerOptions WithAspNetCoreSettings(this ContainerOptions options)
        {
            options.EnableVariance = false;
            return options;
        }
    }
}
