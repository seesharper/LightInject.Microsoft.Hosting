using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using System.Linq;

namespace LightInject.Microsoft.Hosting.Tests;

public class HostTests
{
    [Fact]
    public void ShouldResolveService()
    {
        var host = new HostBuilder().UseLightInject().Build();
        var foo = host.Services.GetRequiredService<IFoo>();
        Assert.NotNull(foo);
    }

    [Fact]
    public void ShouldRegisterAndResolveService()
    {
        var host = new HostBuilder().UseLightInject(r => r.Register<IBar, Bar>()).Build();
        var bar = host.Services.GetRequiredService<IBar>();
        Assert.NotNull(bar);
    }

    [Fact]
    public void ShouldNotHaveVarianceEnabledByDefault()
    {
        var host = new HostBuilder().UseLightInject(r => r.Register<Base>().Register<Derived>()).Build();
        var instances = host.Services.GetServices<Base>();
        Assert.Single(instances);
    }

    [Fact]
    public void ShouldBeAbleToEnableVariance()
    {
        var host = new HostBuilder().UseLightInject(r => r.Register<Base>().Register<Derived>(), options => options.EnableVariance = true).Build();
        var instances = host.Services.GetServices<Base>();
        Assert.Equal(2, instances.Count());
    }

    [Fact]
    public void Issue31()
    {
        var host = new HostBuilder().UseLightInject(r =>
        {
            r
            .Register<IFoo, Foo1>("Foo1")
            .Register<IFoo, Foo2>("Foo2")
            .Register<IFoo>(factory => new Foo3());

        }, options => options.EnableVariance = false).Build();

        var foo = host.Services.GetRequiredService<IFoo>();

        Assert.IsType<Foo3>(foo);
    }
}

public class Base { }

public class Derived : Base { }

public class Foo1 : IFoo { }

public class Foo2 : IFoo { }

public class Foo3 : IFoo { }