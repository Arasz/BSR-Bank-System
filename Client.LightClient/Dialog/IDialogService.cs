using System;

namespace Client.LightClient.Dialog
{
    public interface IDialogService
    {
        void ShowError(string message, string title);

        void ShowError(Exception exception, string title);

        void ShowError(string message);

        void ShowError(Exception exception);
    }
}