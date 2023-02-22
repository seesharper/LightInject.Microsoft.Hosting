This package enables **LightInject** to be used as the IoC container in a Asp.Net Core application.

### Installing 

```shell
dotnet add package LightInject.Microsoft.Hosting
```

### Usage 

```c#
public static IHostBuilder CreateHostBuilder(string[] args) =>
	Host.CreateDefaultBuilder(args)
  	.UseLightInject(services => services.RegisterFrom<CompositionRoot>())
    .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
              
```

Or from ASPNET 6 and onwards with minimal `Program.cs` setup:

```c#
var builder = WebApplication.CreateBuilder(args);
builder.Host.UseLightInject(sr => sr.RegisterFrom<CompositionRoot>());
```

The `CompositionRoot` class is usually placed in the host project (the project containing `Program.cs`) and is used to register services that are specific to **LightInject**

```c#
public class HostCompositionRoot : ICompositionRoot
{
	public void Compose(IServiceRegistry registry)
	{
		registry.RegisterScoped<IFoo, Foo>();			
	}
}
```

### LifeTime

In general we could say that there are usually just two different lifetimes used in web applications. Services are either `Scoped` or `Singletons`. Scoped services are services that lives for the lifetime of the web request. An example would `IDbConnection` which gets created when the scope starts (web request start) and disposed when the scope ends (web request end). We register these services using the `RegisterScoped` method. Scoped services are only created once per scope even if injected into several other services during the web request. Singleton services are shared across web request and must be 100% thread safe. An example here could be a cache that is used for all web requests. 





