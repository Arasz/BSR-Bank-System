using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System.Windows.Input;

namespace Client.UniversalLightClient.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;
        private readonly INavigationService _navigationService;
        public string Login { get; set; }

        public ICommand LoginCommand { get; }

        public string Password { get; set; }

        public LoginViewModel()
        {
        }

        public LoginViewModel(INavigationService navigationService, IDialogService dialogService, IBankServiceProxy bankServiceProxy)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
        }

        private async void LoginToBank()
        {
        }
    }
}