using Client.LightClient.Pages;
using Client.Proxy.BankService;
using Data.Core;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using System.ServiceModel;
using System.Windows.Input;

namespace Client.LightClient.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IBankServiceProxy _bankServiceProxy;
        private readonly IDialogService _dialogService;
        private readonly INavigationService _navigationService;
        public ICommand LoginCommand { get; }

        public string Password { get; set; }

        public string Username { get; set; }

        public LoginViewModel(IBankServiceProxy bankServiceProxy, IDialogService dialogService, INavigationService navigationService)
        {
            _bankServiceProxy = bankServiceProxy;
            _dialogService = dialogService;
            _navigationService = navigationService;
            LoginCommand = new RelayCommand(LoginToBankAccountAsync);
        }

        private async void LoginToBankAccountAsync()
        {
            var loggedUser = User.NullUser;

            try
            {
                loggedUser = await _bankServiceProxy.LoginAsync(Username, Password);
            }
            catch (FaultException invalidCredentialException)
            {
                await _dialogService.ShowError(invalidCredentialException, "Login error.", "Ok", null);
            }

            _navigationService.NavigateTo(nameof(AccountsPage));
            Messenger.Default.Send(loggedUser);
        }
    }
}