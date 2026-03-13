using Govno_2._0.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
    /// Логика взаимодействия для EditOrder.xaml
    /// </summary>
    public partial class EditOrder : Window
    {
        private ObservableCollection<OrdersServices> currentServices;
        private byte[] selectedImageBytes;
        private Clients selectedClient;
        public EditOrder()
        {
            InitializeComponent();

            currentServices = new ObservableCollection<OrdersServices>();
            ServicesList.ItemsSource = currentServices;

            if (App.currentOrder != null)
            {
                LoadExistingOrder();
            }
            else
            {
                DeleteBtn.Visibility = Visibility.Collapsed;
                SaveBtn.Content = "Добавить заказ";
                DateP.SelectedDate = DateTime.Now;
            }
        }

        private void LoadExistingOrder()
        {
            var order = App.currentOrder;

            DeleteBtn.Visibility = Visibility.Visible;
            SaveBtn.Content = "Сохранить";

            TitleTB.Text = order.Name;

            selectedClient = order.Clients;
            ClientTB.Text = selectedClient?.FullName;

            DateP.SelectedDate = order.DateCreate; 

            if (order.Photo != null)
            {
                selectedImageBytes = order.Photo;
                DisplayImage(order.Photo);
            }

            var services = App.context.OrdersServices
        .AsNoTracking()
        .Include("Services")
        .Where(os => os.OrderId == order.ID)
        .ToList();

            currentServices.Clear();
            foreach (var service in services)
            {
                currentServices.Add(new OrdersServices
                {
                    ID = service.ID,
                    OrderId = service.OrderId,
                    ServiceId = service.ServiceId,
                    Services = service.Services
                });
            }
        }


        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (App.currentOrder == null) return;

            var result = MessageBox.Show("Удалить заказ?", "Подтверждение",
                                       MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    var services = App.context.OrdersServices
                        .Where(os => os.OrderId == App.currentOrder.ID)
                        .ToList();
                    foreach (var service in services)
                    {
                        App.context.OrdersServices.Remove(service);
                    }

                    App.context.Orders.Remove(App.currentOrder);
                    App.context.SaveChanges();

                    DialogResult = true;
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления: {ex.Message}", "Ошибка",MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TitleTB.Text))
            {
                MessageBox.Show("Введите название заказа!", "Ошибка",MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (selectedClient == null)
            {
                MessageBox.Show("Выберите клиента!", "Ошибка",MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (currentServices.Count == 0)
            {
                MessageBox.Show("Добавьте хотя бы одну услугу!", "Ошибка",MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                if (App.currentOrder == null)
                {
                    CreateNewOrder();
                }
                else
                {
                    UpdateExistingOrder();
                }

                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка",MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CreateNewOrder()
        {
            int newNumber = App.context.Orders.Any()
                ? App.context.Orders.Max(o => o.Number) + 1
                : 1001;

            var newOrder = new Orders
            {
                Number = newNumber,
                DateCreate = DateP.SelectedDate ?? DateTime.Now,
                Name = TitleTB.Text,
                Client = selectedClient.ID,
                Status = 1,
                Photo = selectedImageBytes
            };

            App.context.Orders.Add(newOrder);
            App.context.SaveChanges();

            foreach (var service in currentServices)
            {
                App.context.OrdersServices.Add(new OrdersServices
                {
                    OrderId = newOrder.ID,
                    ServiceId = service.ServiceId
                });
            }

            App.context.SaveChanges();
        }

        private void UpdateExistingOrder()
        {
            var order = App.currentOrder;

            order.Name = TitleTB.Text;
            order.DateCreate = DateP.SelectedDate ?? DateTime.Now;
            order.Client = selectedClient.ID;
            order.Photo = selectedImageBytes;

            var oldServices = App.context.OrdersServices.Where(os => os.OrderId == order.ID).ToList();
            foreach (var service in oldServices)
            {
                App.context.OrdersServices.Remove(service);
            }

            foreach (var service in currentServices)
            {
                App.context.OrdersServices.Add(new OrdersServices
                {
                    OrderId = order.ID,
                    ServiceId = service.ServiceId
                });
            }

            App.context.SaveChanges();
        }

        private void RemoveBtn_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var orderService = button?.DataContext as OrdersServices;

            if (orderService != null)
            {
                currentServices.Remove(orderService);
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void AddServiceBtn_Click(object sender, RoutedEventArgs e)
        {
            var serviceSelect = new ServiceSelect();

            if (serviceSelect.ShowDialog() == true && serviceSelect.SelectedService != null)
            {
                var service = serviceSelect.SelectedService;

                if (currentServices.Any(s => s.ServiceId == service.ID))
                {
                    MessageBox.Show("Эта услуга уже добавлена!", "Внимание",MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                currentServices.Add(new OrdersServices
                {
                    ServiceId = service.ID,
                    Services = service
                });
            }
        }

        private void ImageBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Изображения|*.jpg;*.jpeg;*.png;*.bmp|Все файлы|*.*",
                Title = "Выберите фото устройства"
            };

            if (ofd.ShowDialog() == true)
            {
                try
                {
                    selectedImageBytes = File.ReadAllBytes(ofd.FileName);
                    DisplayImage(selectedImageBytes);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки фото: {ex.Message}");
                }
            }
        }

        private void DisplayImage(byte[] imageBytes)
        {
            try
            {
                using (var stream = new MemoryStream(imageBytes))
                {
                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.StreamSource = stream;
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                    bitmap.Freeze();
                    ImageBlock.Source = bitmap;
                }
            }
            catch
            {
                ImageBlock.Source = new BitmapImage(new Uri("/Resources/imageselect.png", UriKind.Relative));
            }
        }

        private void ClientTB_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
var selectClientWindow = new SelectClientWindow();

            if (selectClientWindow.ShowDialog() == true && selectClientWindow.SelectedClient != null)
            {
                selectedClient = selectClientWindow.SelectedClient;
                ClientTB.Text = selectedClient.FullName;
            }
        }
    }
}
