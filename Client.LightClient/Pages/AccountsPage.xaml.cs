using Client.LightClient.ViewModel;
using Data.Core;
using System.Windows.Controls;
using System.Windows.Input;

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

        private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var listItem = sender as ListViewItem;
            if (listItem == null)
                return;
            if (listItem.IsSelected && e.ChangedButton == MouseButton.Left && listItem.IsMouseDirectlyOver)
            {
                var viewModel = DataContext as AccountsViewModel;

                viewModel.SelectAccount((Account)listItem.DataContext);
            }
        }
    }
}