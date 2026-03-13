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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Govno_2._0.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для OrderListPage.xaml
    /// </summary>
    public partial class OrderListPage : Page
    {
        public OrderListPage()
        {
            InitializeComponent();
            LoadReadyOrders();
        }
        private void LoadReadyOrders()
        {
            var readyOrders = App.context.Orders
                .Include("Clients")
                .Include("OrdersServices")
                .Include("OrdersServices.Services")
                .Where(o => o.Status == 2)
                .OrderBy(o => o.Number)
                .ToList();

            foreach (var order in readyOrders)
            {
                order.TotalPrice = order.OrdersServices
                    .Sum(os => os.Services?.Price ?? 0);
            }

            OrdersList.ItemsSource = readyOrders;
        }
        private void AcceptBtn_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var order = button?.DataContext as Orders;

            if (order == null) return;

            var result = MessageBox.Show(
                $"Подтвердить выполнение заказа №{order.Number}?",
                "Подтверждение",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    order.Status = 3;
                    App.context.SaveChanges();

                    LoadReadyOrders();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var order = button?.DataContext as Orders;

            if (order == null) return;

            var result = MessageBox.Show(
                $"Удалить заказ №{order.Number}?",
                "Подтверждение удаления",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    var orderServices = App.context.OrdersServices
                        .Where(os => os.OrderId == order.ID)
                        .ToList();

                    foreach (var os in orderServices)
                    {
                        App.context.OrdersServices.Remove(os);
                    }

                    App.context.Orders.Remove(order);
                    App.context.SaveChanges();

                    LoadReadyOrders();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления: {ex.Message}", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}