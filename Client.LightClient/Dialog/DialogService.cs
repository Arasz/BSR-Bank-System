using GalaSoft.MvvmLight.Views;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace Client.LightClient.Dialog
{
    public class DialogService : IDialogService
    {
        private readonly Window _mainWindow;

        public DialogService(Window mainWindow)
        {
            _mainWindow = mainWindow;
        }

        public Task ShowError(string message, string title, string buttonText, Action afterHideCallback)
        {
            return Task.Run(() =>
            {
                MessageBox.Show(_mainWindow, message, title, MessageBoxButton.OK, MessageBoxImage.Error);
                afterHideCallback?.Invoke();
            });
        }

        public Task ShowError(Exception error, string title, string buttonText, Action afterHideCallback)
        {
            MessageBox.Show(_mainWindow, error.Message, title, MessageBoxButton.OK, MessageBoxImage.Error);
            afterHideCallback?.Invoke();
            return Task.CompletedTask;
        }

        public Task ShowMessage(string message, string title)
        {
            throw new NotImplementedException();
        }

        public Task ShowMessage(string message, string title, string buttonText, Action afterHideCallback)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ShowMessage(string message, string title, string buttonConfirmText, string buttonCancelText,
            Action<bool> afterHideCallback)
        {
            throw new NotImplementedException();
        }

        public Task ShowMessageBox(string message, string title)
        {
            throw new NotImplementedException();
        }
    }
}