using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace LightInject.Microsoft.Hosting.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void ShouldResolveService()
        {
            var host = new HostBuilder().UseLightInject().Build();
            var foo = host.Services.GetRequiredService<IFoo>();
        }

        [Fact]
        public void ShouldRegisterAndResolveService()
        {
            var host = new HostBuilder().UseLightInject(r => r.Register<IBar, Bar>()).Build();
            var foo = host.Services.GetRequiredService<IFoo>();
        }
    }
}
