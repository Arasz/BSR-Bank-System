using Client.Proxy.BankService;
using Data.Core;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Views;
using Service.Dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.Windows.Input;

namespace Client.LightClient.ViewModel
{
    public class AccountsViewModel : ViewModelBase
    {
        private readonly IBankServiceProxy _bankServiceProxy;
        private readonly IDialogService _dialogService;
        private readonly INavigationService _navigationService;
        private IEnumerable<Operation> _accountOperations;
        private ObservableCollection<Account> _accounts;
        private Account _selectedAccount;
        private string _username;
        public ICommand AccountHistoryCommand { get; }

        public IEnumerable<Operation> AccountOperations
        {
            get { return _accountOperations; }
            set { Set(ref _accountOperations, value); }
        }

        public ObservableCollection<Account> Accounts
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

        public string TransferTitle { get; set; }

        public string Username
        {
            get { return _username; }
            set { Set(ref _username, value); }
        }

        public ICommand WithdrawCommand { get; }

        public AccountsViewModel(INavigationService navigationService, IBankServiceProxy bankServiceProxy, IDialogService dialogService)
        {
            _navigationService = navigationService;
            _bankServiceProxy = bankServiceProxy;
            _dialogService = dialogService;

            MessengerInstance.Register<User>(this, UserLogged);

            DepositCommand = new RelayCommand(Deposit, CanExecuteCommand);
            WithdrawCommand = new RelayCommand(Withdraw, CanExecuteCommand);
            ExternalTransferCommand = new RelayCommand(MakeExternalTransfer, CanExecuteCommand);
            TransferCommand = new RelayCommand(MakeTransfer, CanExecuteCommand);
            AccountHistoryCommand = new RelayCommand(GetAccountHistory, CanExecuteCommand);
        }

        private bool CanExecuteCommand()
        {
            return SelectedAccount != null;
        }

        private async void Deposit()
        {
            var errorTitle = "Deposit error";
            try
            {
                var parsedAmount = decimal.Parse(Amount);
                await _bankServiceProxy.DepositAsync(SelectedAccount.Number, parsedAmount);
                SelectedAccount.Balance += parsedAmount;
                RaisePropertyChanged(nameof(Accounts));
            }
            catch (FaultException faultException)
            {
                await _dialogService.ShowError(faultException, errorTitle, "", null);
            }
            catch (Exception exception)
            {
                await _dialogService.ShowError(exception.Message, errorTitle, "", null);
            }
        }

        private async void GetAccountHistory()
        {
            var errorTitle = "Operation history error";
            try
            {
                AccountOperations = await _bankServiceProxy.OperationsHistoryAsync(new AccountHistoryQuery(SelectedAccount.Number));
            }
            catch (FaultException faultException)
            {
                await _dialogService.ShowError(faultException, errorTitle, "", null);
            }
            catch (Exception exception)
            {
                await _dialogService.ShowError(exception.Message, errorTitle, "", null);
            }
        }

        private async void MakeExternalTransfer()
        {
            var errorTitle = "External transfer error";
            try
            {
                var parsedAmount = decimal.Parse(Amount);
                await _bankServiceProxy.ExternalTransferAsync(new TransferDescription(SelectedAccount.Number, TargetAccountNumber, TransferTitle, parsedAmount));
                SelectedAccount.Balance -= parsedAmount;
                RaisePropertyChanged(nameof(Accounts));
            }
            catch (FaultException faultException)
            {
                await _dialogService.ShowError(faultException, errorTitle, "", null);
            }
            catch (Exception exception)
            {
                await _dialogService.ShowError(exception.Message, errorTitle, "", null);
            }
        }

        private async void MakeTransfer()
        {
            var errorTitle = "Transfer error";
            try
            {
                var parsedAmount = decimal.Parse(Amount);
                await _bankServiceProxy.InternalTransferAsync(new TransferDescription(SelectedAccount.Number, TargetAccountNumber, TransferTitle, parsedAmount));
                SelectedAccount.Balance -= parsedAmount;
                RaisePropertyChanged(nameof(Accounts));
            }
            catch (FaultException faultException)
            {
                await _dialogService.ShowError(faultException, errorTitle, "", null);
            }
            catch (Exception exception)
            {
                await _dialogService.ShowError(exception.Message, errorTitle, "", null);
            }
        }

        private void UserLogged(User loggedUser)
        {
            Username = loggedUser.Name;

            Accounts = new ObservableCollection<Account>(loggedUser.Accounts);
        }

        private async void Withdraw()
        {
            var errorTitle = "Withdraw error";
            try
            {
                var parsedAmount = decimal.Parse(Amount);
                await _bankServiceProxy.WithdrawAsync(SelectedAccount.Number, parsedAmount);
                SelectedAccount.Balance -= parsedAmount;
                RaisePropertyChanged(nameof(Accounts));
            }
            catch (FaultException faultException)
            {
                await _dialogService.ShowError(faultException, errorTitle, "", null);
            }
            catch (Exception exception)
            {
                await _dialogService.ShowError(exception.Message, errorTitle, "", null);
            }
        }
    }
}