using Client.LightClient.Pages;
using Client.Proxy.BankService;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Views;
using System;
using System.ServiceModel;
using System.Windows.Input;
using IDialogService = Client.LightClient.Dialog.IDialogService;

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
            try
            {
                _bankServiceProxy.SetCredentials(Username, Password);

                var loggedUser = await _bankServiceProxy.LoginAsync(Username, Password);
                _navigationService.NavigateTo(nameof(AccountsPage));
                MessengerInstance.Send(loggedUser);
            }
            catch (FaultException invalidCredentialException)
            {
                _dialogService.ShowError(invalidCredentialException, "Login error");
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(exception.Message, "Login error");
            }
        }
    }
}