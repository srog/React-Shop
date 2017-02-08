using Autofac;

namespace ReactShop.Core
{
    public class AutoFacHelper
    {
        static ContainerBuilder builder;
        static Autofac.IContainer container;

        public static void Initialize(string serverFilePath)
        {
            builder = new ContainerBuilder();
            builder.RegisterType<CheckoutManager>()
                .As<ICheckoutManager>()
                .WithParameter("serverFilePath", serverFilePath);
            container = builder.Build();
        }

        public static T Resolve<T>()
        {
            using (var scope = container.BeginLifetimeScope())
            {
                return scope.Resolve<T>();
            }
        }
    }
}
