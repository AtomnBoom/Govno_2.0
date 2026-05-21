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
    /// Логика взаимодействия для HistoryPage.xaml
    /// </summary>
    public partial class HistoryPage : Page
    {
        public HistoryPage()
        {
            InitializeComponent();
            LoadHistoryOrders();
        }

        private void LoadHistoryOrders()
        {
            var completedOrders = App.context.Orders
                .Include("Clients")
                .Include("OrdersServices")
                .Include("OrdersServices.Services")
                .Where(o => o.Status == 3) // статус "Завершён"
                .OrderByDescending(o => o.Number) // или o.ID, если нужна сортировка
                .ToList();

            foreach (var order in completedOrders)
            {
                order.TotalPrice = order.OrdersServices
                    .Sum(os => os.Services?.Price ?? 0);
            }

            OrdersList.ItemsSource = completedOrders;
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var order = button?.DataContext as Orders;

            if (order == null) return;

            var result = MessageBox.Show(
                $"Удалить заказ №{order.Number} из истории?",
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
                        App.context.OrdersServices.Remove(os);

                    App.context.Orders.Remove(order);
                    App.context.SaveChanges();

                    LoadHistoryOrders();
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