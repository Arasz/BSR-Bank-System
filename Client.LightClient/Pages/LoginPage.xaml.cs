using Client.LightClient.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Client.LightClient.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml 
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void ReadPassword(object sender, RoutedEventArgs e)
        {
            // UNSAFE CODE
            var loginViewModel = (LoginViewModel)DataContext;
            loginViewModel.Password = PasswordBox.Password;
        }
    }
}