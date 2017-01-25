using System;
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

        public void ShowError(string message, string title)
        {
            MessageBox.Show(_mainWindow, message, title, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void ShowError(Exception exception, string title)
        {
            MessageBox.Show(_mainWindow, exception.Message, title, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void ShowError(string message) => ShowError(message, "Error");

        public void ShowError(Exception exception) => ShowError(exception, "Error");
    }
}