using Govno_2._0.View.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Govno_2._0.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainWindows.xaml
    /// </summary>
    public partial class MainWindows : Window
    {
        public MainWindows()
        {
            InitializeComponent();
            App.MainFrame = MainF;
            App.MainFrame.Navigate(new OrdersPage());
        }

        private void ProfilBtn_Click(object sender, RoutedEventArgs e)
        {
            App.MainFrame.Navigate(new ProfilPage());
        }

        private void SupportBtn_Click(object sender, RoutedEventArgs e)
        {
            App.MainFrame.Navigate(new SupportPage());
        }

        private void SettBtn_Click(object sender, RoutedEventArgs e)
        {
            App.MainFrame.Navigate(new SettingsPage());
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            Close();
        }

        private void MenuBar_MouseLeave(object sender, MouseEventArgs e)
        {
            MenuBar.Visibility = Visibility.Collapsed;
            MenuBtn.Visibility = Visibility.Visible;
        }

        private void MenuBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            MenuBar.Visibility = Visibility.Visible;
            MenuBtn.Visibility = Visibility.Collapsed;
        }

        private void OrderBtn_Click(object sender, RoutedEventArgs e)
        {
            MainF.Navigate(new OrdersPage());
        }

        private void OrderListBtn_Click(object sender, RoutedEventArgs e)
        {
            MainF.Navigate(new OrderListPage());
        }

        private void PriceBtn_Click(object sender, RoutedEventArgs e)
        {
            MainF.Navigate(new PriceListPage());
        }

        private void ClientBtn_Click(object sender, RoutedEventArgs e)
        {
            MainF.Navigate(new OrdersPage());
        }

        private void HistoryBtn_Click(object sender, RoutedEventArgs e)
        {
            MainF.Navigate(new OrdersPage());
        }
    }
}
