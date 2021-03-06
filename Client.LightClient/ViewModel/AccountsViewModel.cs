﻿using Client.LightClient.Dialog;
using Client.LightClient.Model;
using Client.Proxy.BankService;
using Data.Core.Entities;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Service.Dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.LightClient.ViewModel
{
    public class AccountsViewModel : ViewModelBase
    {
        private readonly IBankServiceProxy _bankServiceProxy;
        private readonly IDialogService _dialogService;
        private IEnumerable<BankOperation> _accountOperations;
        private ObservableCollection<Account> _accounts;
        private bool _operationInProgress;
        private Account _selectedAccount;
        private string _username;
        public ICommand AccountHistoryCommand { get; }

        public IEnumerable<BankOperation> AccountOperations
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

        public bool OperationInProgress
        {
            get { return _operationInProgress; }
            set { Set(ref _operationInProgress, value); }
        }

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

        private TransferDescription CreateTransferDescription => new TransferDescription(SelectedAccount.Number, TargetAccountNumber, TransferTitle, ParsedAmount);

        private decimal ParsedAmount => decimal.Floor(decimal.Parse(Amount) * 100) / 100;

        public AccountsViewModel(IBankServiceProxy bankServiceProxy, IDialogService dialogService)
        {
            _bankServiceProxy = bankServiceProxy;
            _dialogService = dialogService;

            MessengerInstance.Register<User>(this, UserLogged);

            DepositCommand = new RelayCommand(Deposit, CanExecuteCommand);
            WithdrawCommand = new RelayCommand(Withdraw, CanExecuteCommand);
            ExternalTransferCommand = new RelayCommand(MakeExternalTransfer, CanExecuteCommand);
            TransferCommand = new RelayCommand(MakeTransfer, CanExecuteCommand);
            AccountHistoryCommand = new RelayCommand(GetAccountHistory, CanExecuteCommand);
        }

        private bool CanExecuteCommand() => SelectedAccount != null;

        private async void Deposit()
        {
            var errorTitle = "Deposit error";
            await HandleServiceError(async () =>
            {
                await _bankServiceProxy.DepositAsync(SelectedAccount.Number, ParsedAmount);
            }, errorTitle);
        }

        private async void GetAccountHistory()
        {
            var errorTitle = "Operation history error";
            await HandleServiceError(async () =>
            {
                var operations = await _bankServiceProxy.OperationsHistoryAsync(new AccountHistoryQuery(SelectedAccount.Number));
                AccountOperations = operations.Select(operation => new BankOperation(operation));
            }, errorTitle);
        }

        private async Task HandleServiceError(Func<Task> serviceCall, string errorTitle)
        {
            try
            {
                OperationInProgress = true;
                await serviceCall();
            }
            catch (FaultException faultException)
            {
                _dialogService.ShowError(faultException, errorTitle);
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(exception.Message, errorTitle);
            }
            finally
            {
                OperationInProgress = false;
            }
        }

        private async void MakeExternalTransfer()
        {
            var errorTitle = "External transfer error";
            await HandleServiceError(async () =>
            {
                var transferDescription = CreateTransferDescription;
                await _bankServiceProxy.ExternalTransferAsync(transferDescription);
            }, errorTitle);
        }

        private async void MakeTransfer()
        {
            var errorTitle = "Transfer error";

            await HandleServiceError(async () =>
            {
                var transferDescription = CreateTransferDescription;
                await _bankServiceProxy.InternalTransferAsync(transferDescription);
            }, errorTitle);
        }

        private void UserLogged(User loggedUser)
        {
            Username = loggedUser.Name;

            Accounts = new ObservableCollection<Account>(loggedUser.Accounts);
        }

        private async void Withdraw()
        {
            var errorTitle = "Withdraw error";
            await HandleServiceError(async () =>
            {
                await _bankServiceProxy.WithdrawAsync(SelectedAccount.Number, ParsedAmount);
            }, errorTitle);
        }
    }
}