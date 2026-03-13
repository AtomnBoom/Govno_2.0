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
using System.Windows.Markup;
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
                    .Include("OrdersServices")
                    .Include("OrdersServices.Services")
                    .Where(o => o.Status == 1)
                    .OrderBy(o => o.DateCreate)
                    .ToList();

                OrdersList.ItemsSource = orders;
            }
            catch
            {
            }
        }

        private void AddOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            App.currentOrder = null;
            EditOrder editOrder = new EditOrder();

            if (editOrder.ShowDialog() == true)
            {
                LoadOrders();
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

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var order = button?.DataContext as Orders;

            if (order == null)
            {
                MessageBox.Show("Заказ не найден", "Ошибка",MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            App.currentOrder = order;
            EditOrder editOrder = new EditOrder();

            if (editOrder.ShowDialog() == true)
            {
                LoadOrders();
            }

        }

        private void AcceptBtn_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var order = button?.DataContext as Orders;

            if (order == null) return;

            var result = MessageBox.Show(
                $"Завершить работу над заказом №{order.Number}?\n\n" +
                $"Клиент: {order.Clients?.FullName}\n" +
                $"Услуг: {order.OrdersServices?.Count ?? 0}",
                "Подтверждение завершения",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    order.Status = 2;

                    App.context.SaveChanges();

                    MessageBox.Show($"Заказ №{order.Number} в ожидании!",
                                  "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                    LoadOrders();
                }
                catch
                {
                }
            }
        }
    }
}