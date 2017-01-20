using System;
using System.ServiceModel.Description;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Client.LightClient.ServiceClient;
using Service.Contracts;

namespace Client.LightClient.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml 
    /// </summary>
    public partial class LoginPage : Page
    {
        private readonly IBankService _bankService;
        private readonly MainWindow _window;

        public LoginPage(MainWindow window, IBankService bankService)
        {
            _window = window;
            _bankService = bankService;
            InitializeComponent();
        }

        private async void LoginButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var login = LoginTextBox.Text;
                var password = PasswordBox.Password;

                var clientCredentialsUserName = ((BankServiceClient)_bankService).ClientCredentials.UserName;

                clientCredentialsUserName.UserName = login;
                clientCredentialsUserName.Password = password;

                _window.LoggedUser = await Task.Run(() => _bankService.Authentication(login, password));
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}