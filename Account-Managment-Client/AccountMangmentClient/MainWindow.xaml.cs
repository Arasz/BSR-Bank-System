using System;
using System.Windows;
using Client.LightClient.Pages;
using Client.LightClient.ServiceClient;
using Data.Core;
using Service.Contracts;

namespace Client.LightClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml 
    /// </summary>
    public partial class MainWindow : Window
    {
        public IBankService BankService { get; }

        public User LoggedUser { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            BankService = new BankServiceClient();

            MainFrame.NavigationService.Navigate(new LoginPage(this, BankService));
        }

        private void Frame_OnContentRendered(object sender, EventArgs e)
        {
        }
    }
}