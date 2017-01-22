using Client.LightClient.ViewModel;
using Data.Core;
using System.Windows.Controls;

namespace Client.LightClient.Pages
{
    /// <summary>
    /// Interaction logic for AccountPage.xaml 
    /// </summary>
    public partial class AccountsPage : Page
    {
        public AccountsPage()
        {
            InitializeComponent();
        }

        private void AccountsList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedAccount = (sender as ListView)?.SelectedItem as Account;
            if (selectedAccount == null)
                return;

            var viewModel = DataContext as AccountsViewModel;

            viewModel.SelectAccount(selectedAccount);
        }
    }
}