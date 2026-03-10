using Govno_2._0.Models;
using Govno_2._0.View.Windows.Edit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        public OrdersPage()
        {
            InitializeComponent();
            LoadOrders();
        }

        private void LoadOrders()
        {
            try
            {
                var orders = App.context.Orders
                    .Include("Clients")
                    .Include("StatusOrders")
                    .Include("OrdersServices")
                    .Include("OrdersServices.Services")
                    .OrderByDescending(o => o.DateCreate)
                    .ToList();

                OrdersList.ItemsSource = orders;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки заказов: {ex.Message}",
                              "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            EditOrder editOrder = new EditOrder();
            editOrder.ShowDialog();


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

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            App.currentOrder = OrdersList.SelectedItem as Orders;
            EditOrder editOrder = new EditOrder();
            editOrder.ShowDialog();

        }

        private void AcceptBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
