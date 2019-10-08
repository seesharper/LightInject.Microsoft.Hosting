/*********************************************************************************
    The MIT License (MIT)

    Copyright (c) 2019 bernhard.richter@gmail.com

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
******************************************************************************
    LightInject.Microsoft.Hosting version 1.0.0
    http://www.lightinject.net/
    http://twitter.com/bernhardrichter
******************************************************************************/

[module: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1126:PrefixCallsCorrectly", Justification = "Reviewed")]
[module: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:PrefixLocalCallsWithThis", Justification = "No inheritance")]
[module: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Single source file deployment.")]
[module: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1633:FileMustHaveHeader", Justification = "Custom header.")]
[module: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "All public members are documented.")]
[module: System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Performance")]
[module: System.Diagnostics.CodeAnalysis.SuppressMessage("MaintainabilityRules", "SA1403", Justification = "One source file")]
[module: System.Diagnostics.CodeAnalysis.SuppressMessage("DocumentationRules", "SA1649", Justification = "One source file")]
#nullable enable

namespace Microsoft.Extensions.Hosting
{
    using System;
    using LightInject;
    using LightInject.Microsoft.DependencyInjection;

    /// <summary>
    /// Extends the <see cref="IHostBuilder"/> to enable LightInject to be used as the service container.
    /// </summary>
    public static class HostBuilderExtensions
    {
        /// <summary>
        /// Configures the <paramref name="builder"/> to use LightInject as the service container.
        /// </summary>
        /// <param name="builder">The target <see cref="IHostBuilder"/>.</param>
        /// <param name="configureServices">A delegate passing the <see cref="IServiceRegistry"/> used to configure services.</param>
        /// <returns>The <see cref="IHostBuilder"/> configured to use LightInject.</returns>
        public static IHostBuilder UseLightInject(this IHostBuilder builder, Action<IServiceRegistry>? configureServices = null)
        {
            var container = new ServiceContainer(ContainerOptions.Default.Clone().WithMicrosoftSettings().WithAspNetCoreSettings());
            configureServices?.Invoke(container);
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
