using Data.Core;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Views;
using System.Collections.Generic;
using System.Windows.Input;

namespace Client.LightClient.ViewModel
{
    public class AccountsViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private ICollection<Account> _accounts;
        private Account _selectedAccount;
        private string _username;

        public ICommand AccountHistoryCommand { get; }

        public ICollection<Account> Accounts
        {
            get { return _accounts; }
            set { Set(ref _accounts, value); }
        }

        public string Amount { get; set; }

        public ICommand DepositCommand { get; }

        public ICommand ExternalTransferCommand { get; }

        public Account SelectedAccount
        {
            get { return _selectedAccount; }
            set { Set(ref _selectedAccount, value); }
        }

        public string TargetAccountNumber { get; set; }

        public ICommand TransferCommand { get; }

        public string Username
        {
            get { return _username; }
            set { Set(ref _username, value); }
        }

        public ICommand WithdrawCommand { get; }

        public AccountsViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            MessengerInstance.Register<User>(this, UserLogged);

            DepositCommand = new RelayCommand(Deposit);
            WithdrawCommand = new RelayCommand(Withdraw);
            ExternalTransferCommand = new RelayCommand(MakeExternalTransfer);
            TransferCommand = new RelayCommand(MakeTransfer);
            AccountHistoryCommand = new RelayCommand(GetAccountHistory);
        }

        private void Deposit()
        {
        }

        private void GetAccountHistory()
        {
        }

        private void MakeExternalTransfer()
        {
        }

        private void MakeTransfer()
        {
        }

        private void UserLogged(User loggedUser)
        {
            Username = loggedUser.Name;

            Accounts = loggedUser.Accounts;
        }

        private void Withdraw()
        {
        }
    }
}