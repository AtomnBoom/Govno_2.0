using Govno_2._0.Models;
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

namespace Govno_2._0.View.Windows.Edit
{
    /// <summary>
    /// Логика взаимодействия для SelectClientWindow.xaml
    /// </summary>
    public partial class SelectClientWindow : Window
    {
        public Clients SelectedClient { get; private set; }
        private List<Clients> allClients;
        public SelectClientWindow()
        {
            InitializeComponent();
            LoadClients();
        }
        private void LoadClients()
        {
            allClients = App.context.Clients.OrderBy(c => c.FullName).ToList();
            ClientsList.ItemsSource = allClients;
        }
        private void SearchTxt_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            string search = SearchTxt.Text.ToLower();

            var filtered = allClients
                .Where(c => c.FullName.ToLower().Contains(search) ||
                           c.Phone.Contains(search))
                .ToList();

            ClientsList.ItemsSource = filtered;
        }
        private void ClientsList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (ClientsList.SelectedItem is Clients client)
            {
                SelectedClient = client;
                PhoneTbl.Text = client.Phone;
            }
        }
        private void SelectBtn_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedClient == null)
            {
                MessageBox.Show("Выберите клиента из списка!", "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DialogResult = true;
            Close();
        }
        private void AddNewClientBtn_Click(object sender, RoutedEventArgs e)
        {
            var addClientWindow = new AddClientWindow();

            if (addClientWindow.ShowDialog() == true && addClientWindow.NewClient != null)
            {
                SelectedClient = addClientWindow.NewClient;
                DialogResult = true;
                Close();
            }
        }
        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            SelectedClient = null;
            DialogResult = false;
            Close();
        }
    }
}
