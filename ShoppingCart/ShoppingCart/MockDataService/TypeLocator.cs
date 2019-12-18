using System;
using Autofac;
using Autofac.Core;
using ShoppingCart.DataService;
using ShoppingCart.ViewModels.AboutUs;
using ShoppingCart.ViewModels.Bookmarks;
using ShoppingCart.ViewModels.Catalog;
using ShoppingCart.ViewModels.Detail;
using ShoppingCart.ViewModels.ErrorAndEmpty;
using ShoppingCart.ViewModels.Forms;
using ShoppingCart.ViewModels.Onboarding;
using ShoppingCart.ViewModels.Transaction;

namespace ShoppingCart.MockDataService
{
    public static class TypeLocator
    {
        #region Fields

        private static ILifetimeScope _rootScope;

        #endregion

        #region Methods

        public static T Resolve<T>()
        {
            if (_rootScope == null) throw new Exception("Bootstrapper hasn't been started!");
            return _rootScope.Resolve<T>();
        }

        public static T Resolve<T>(Parameter[] parameters)
        {
            if (_rootScope == null) throw new Exception("Bootstrapper hasn't been started!");
            return _rootScope.Resolve<T>(parameters);
        }

        public static void Start()
        {
            if (_rootScope != null) return;

            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType<CartDataService>().As<ICartDataService>().AsImplementedInterfaces()
                .SingleInstance();
            containerBuilder.RegisterType<CatalogDataService>().As<ICatalogDataService>().AsImplementedInterfaces()
                .SingleInstance();
            containerBuilder.RegisterType<CategoryDataService>().As<ICategoryDataService>().AsImplementedInterfaces()
                .SingleInstance();
            containerBuilder.RegisterType<ProductHomeDataService>().As<IProductHomeDataService>()
                .AsImplementedInterfaces().SingleInstance();
            containerBuilder.RegisterType<WishlistDataService>().As<IWishlistDataService>().AsImplementedInterfaces()
                .SingleInstance();
            containerBuilder.RegisterType<UserDataService>().As<IUserDataService>().AsImplementedInterfaces()
                .SingleInstance();
            // containerBuilder.RegisterType<MyOrdersDataService>().As<IMyOrdersDataService>().AsImplementedInterfaces().SingleInstance();

            containerBuilder.RegisterType<AboutUsViewModel>();
            containerBuilder.RegisterType<CartPageViewModel>();
            containerBuilder.RegisterType<WishlistViewModel>();
            containerBuilder.RegisterType<CatalogPageViewModel>();
            containerBuilder.RegisterType<CategoryPageViewModel>();
            containerBuilder.RegisterType<ProductHomePageViewModel>();
            containerBuilder.RegisterType<DetailPageViewModel>();
            containerBuilder.RegisterType<NoInternetConnectionPageViewModel>();
            containerBuilder.RegisterType<ForgotPasswordViewModel>();
            containerBuilder.RegisterType<LoginPageViewModel>();
            containerBuilder.RegisterType<LoginViewModel>();
            containerBuilder.RegisterType<SignUpPageViewModel>();
            containerBuilder.RegisterType<OnBoardingAnimationViewModel>();
            containerBuilder.RegisterType<CheckoutPageViewModel>();
            containerBuilder.RegisterType<PaymentViewModel>();
            // containerBuilder.RegisterType<MyOrdersPageViewModel>();

            _rootScope = containerBuilder.Build();
        }

        public static void Stop()
        {
            _rootScope.Dispose();
        }

        #endregion
    }
}