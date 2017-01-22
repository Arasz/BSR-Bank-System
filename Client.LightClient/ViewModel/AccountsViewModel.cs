using Client.LightClient.Message;
using Client.LightClient.Pages;
using Data.Core;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System.Collections.Generic;

namespace Client.LightClient.ViewModel
{
    public class AccountsViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private ICollection<Account> _accounts;
        private string _username;

        public ICollection<Account> Accounts
        {
            get { return _accounts; }
            set { Set(ref _accounts, value); }
        }

        public IList<Account> SelectedAccounts { get; set; }

        public string Username
        {
            get { return _username; }
            set { Set(ref _username, value); }
        }

        public AccountsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            MessengerInstance.Register<User>(this, UserLogged);
        }

        public void SelectAccount(Account selectedAccount)
        {
            _navigationService.NavigateTo(nameof(AccountOperationPage));

            MessengerInstance.Send(new AccountSelectedMessage(selectedAccount.Number, selectedAccount.Balance, Username));
        }

        private void UserLogged(User loggedUser)
        {
            Username = loggedUser.Name;

            Accounts = loggedUser.Accounts;
        }
    }
}