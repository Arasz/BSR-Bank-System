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
        private Account _selectedAccount;
        private string _username;

        public ICollection<Account> Accounts
        {
            get { return _accounts; }
            set { Set(ref _accounts, value); }
        }

        public string Amount { get; set; }

        public Account SelectedAccount
        {
            get { return _selectedAccount; }
            set { Set(ref _selectedAccount, value); }
        }

        public string TargetAccountNumber { get; set; }

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

        private void UserLogged(User loggedUser)
        {
            Username = loggedUser.Name;

            Accounts = loggedUser.Accounts;
        }
    }
}