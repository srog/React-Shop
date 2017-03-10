using Autofac;
using ReactShop.Core.Common;
using ReactShop.Core.Data.Cart;
using ReactShop.Core.Data.Categories;
using ReactShop.Core.Data.Customers;
using ReactShop.Core.Data.Orders;
using ReactShop.Core.Data.Products;

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

            builder.RegisterType<GetProducts>().As<IGetProducts>();
            builder.RegisterType<GetCart>().As<IGetCart>();
            builder.RegisterType<GetCartItem>().As<IGetCartItem>();
            builder.RegisterType<SaveCartItem>().As<ISaveCartItem>();
            builder.RegisterType<CreateOrder>().As<ICreateOrder>();
            builder.RegisterType<GetOrders>().As<IGetOrders>();
            builder.RegisterType<GetOrderItems>().As<IGetOrderItems>();
            builder.RegisterType<SaveCartItem>().As<ISaveCartItem>();
            builder.RegisterType<GetCustomer>().As<IGetCustomer>();
            builder.RegisterType<GetCustomerAddress>().As<IGetCustomerAddress>();
            builder.RegisterType<GetPaymentOption>().As<IGetPaymentOption>();
            builder.RegisterType<InitializeDB>().As<IInitializeDB>();
            builder.RegisterType<SaveCustomer>().As<ISaveCustomer>();
            builder.RegisterType<SaveProduct>().As<ISaveProduct>();
            builder.RegisterType<GetCategory>().As<IGetCategory>();
            builder.RegisterType<ConfigManager>().As<IConfigManager>();
            builder.RegisterType<DeleteCustomerAddress>().As<IDeleteCustomerAddress>();
            builder.RegisterType<SaveCustomerAddress>().As<ISaveCustomerAddress>();
            builder.RegisterType<DeletePaymentOption>().As<IDeletePaymentOption>();

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
