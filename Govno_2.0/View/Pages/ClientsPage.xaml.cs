using Govno_2._0.Models;
using Govno_2._0.View.Windows.Edit;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Govno_2._0.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для ClientsPage.xaml
    /// </summary>
    public partial class ClientsPage : Page
    {
        public ClientsPage()
        {
            InitializeComponent();
            LoadClients();
        }

        private void LoadClients()
        {
            try
            {
                var clients = App.context.Clients.OrderBy(c => c.FullName).ToList();
                int number = 1;
                foreach (var client in clients)
                    client.Number = number++;
                ClientsList.ItemsSource = clients;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки клиентов: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void HideAdd_MouseEnter(object sender, MouseEventArgs e)
        {
            VisibleAdd.Visibility = Visibility.Visible;
            HideAdd.Visibility = Visibility.Collapsed;
        }

        private void VisibleAdd_MouseLeave(object sender, MouseEventArgs e)
        {
            VisibleAdd.Visibility = Visibility.Collapsed;
            HideAdd.Visibility = Visibility.Visible;
        }

        private void AddClientBtn_Click(object sender, RoutedEventArgs e)
        {
            AddClientWindow addClientWindow = new AddClientWindow();
            if (addClientWindow.ShowDialog() == true)
            {
                MessageBox.Show("Клиент добавлен");
                LoadClients();
            }
        }
        private void EditClientBtn_Click(object sender, RoutedEventArgs e)
        {
            var client = (sender as Button)?.Tag as Clients;
            if (client == null) return;

            var editWindow = new AddClientWindow(client);
            if (editWindow.ShowDialog() == true)
            {
                MessageBox.Show("Изменения сохранены");
                LoadClients();
            }
        }
    }
}