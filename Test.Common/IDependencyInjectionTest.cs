using Autofac;

namespace Test.Common
{
    /// <summary>
    ///     Test that resolves its dependencies which IOC container
    /// </summary>
    public interface IDependencyInjectionTest
    {
        IContainer Container { get; set; }

        IContainer BuildContainer();
    }
}