namespace LightInject.Microsoft.Hosting.Tests
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<IFoo, Foo>();
        }
    }
    public interface IFoo
    {

    }

    public class Foo : IFoo
    {

    }

    public interface IBar
    {

    }

    public class Bar : IBar
    {

    }
}