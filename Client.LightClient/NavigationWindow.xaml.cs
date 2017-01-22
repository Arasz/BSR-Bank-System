using Autofac;
using Client.LightClient.Dialog;
using Client.LightClient.Navigation;
using Client.LightClient.Pages;
using Client.LightClient.ViewModel;
using Client.Proxy.BankService;
using GalaSoft.MvvmLight.Views;
using System.Windows;
using System.Windows.Controls;

namespace Client.LightClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml 
    /// </summary>
    public partial class NavigationWindow : Window
    {
        private IContainer _container;
        private INavigationService _navigationService;

        /// <summary>
        /// Initializes a new instance of the MainWindow class. 
        /// </summary>
        public NavigationWindow()
        {
            InitializeComponent();

            Closing += (s, e) => ViewModelLocator.Cleanup();
        }

        private void BuildContainer()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterType<DialogService>()
                .WithParameter(new TypedParameter(typeof(Window), this))
                .AsImplementedInterfaces()
                .SingleInstance();

            containerBuilder.RegisterType<BankServiceProxy>()
                .AsImplementedInterfaces()
                .SingleInstance();

            containerBuilder.Register(context =>
            {
                var componentContext = context.Resolve<IComponentContext>();

                return new NavigationService(this, componentContext);
            })
            .AsImplementedInterfaces()
            .SingleInstance();

            containerBuilder.RegisterType<LoginPage>()
                .Named<Page>(nameof(LoginPage))
                .AsSelf();

            containerBuilder.RegisterType<LoginViewModel>()
                .AsSelf();

            containerBuilder.RegisterType<AccountsPage>()
                .Named<Page>(nameof(AccountsPage))
                .AsSelf();

            containerBuilder.RegisterType<AccountsViewModel>()
                .AsSelf();

            containerBuilder.RegisterType<AccountOperationPage>()
                .Named<Page>(nameof(AccountOperationPage))
                .AsSelf();

            containerBuilder.RegisterType<AccountOperationViewModel>()
                .AsSelf();

            _container = containerBuilder.Build();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            BuildContainer();

            ViewModelLocator.ComponentContext = _container.Resolve<IComponentContext>();

            NavigateToFirstPage();
        }

        private void NavigateToFirstPage()
        {
            _navigationService = _container.Resolve<INavigationService>();
            _navigationService.NavigateTo(nameof(Pages.LoginPage));
        }
    }
}