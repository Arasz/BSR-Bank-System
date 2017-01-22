/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:Client.LightClient.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>

  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using Autofac;

namespace Client.LightClient.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the application and provides
    /// an entry point for the bindings.
    /// <para> See http://www.mvvmlight.net </para>
    /// </summary>
    public class ViewModelLocator
    {
        public static IComponentContext ComponentContext { get; set; }

        public AccountsViewModel Accounts => ComponentContext.Resolve<AccountsViewModel>();

        public LoginViewModel Login => ComponentContext.Resolve<LoginViewModel>();

        /// <summary>
        /// Cleans up all the resources. 
        /// </summary>
        public static void Cleanup()
        {
        }
    }
}