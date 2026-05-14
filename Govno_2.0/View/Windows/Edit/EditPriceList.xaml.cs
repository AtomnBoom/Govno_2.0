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
using System.Data.Entity;

namespace Govno_2._0.View.Windows.Edit
{
    /// <summary>
    /// Логика взаимодействия для EditPriceList.xaml
    /// </summary>
    public partial class EditPriceList : Window
    {
        public EditPriceList()
        {
            InitializeComponent();
            LoadGroups();
        }

        private void LoadGroups()
        {
            try
            {
                var groups = App.context.SGroup
                    .Include(g => g.Services)
                    .OrderBy(g => g.Name)
                    .ToList();
                GroupsListBox.ItemsSource = groups;
                GroupsListBox.SelectedItem = null;
                ServicesListBox.ItemsSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки категорий: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadServices(int groupId)
        {
            try
            {
                var group = App.context.SGroup
                    .Include(g => g.Services)
                    .FirstOrDefault(g => g.ID == groupId);
                if (group != null)
                    ServicesListBox.ItemsSource = group.Services.OrderBy(s => s.Name).ToList();
                else
                    ServicesListBox.ItemsSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки услуг: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GroupsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedGroup = GroupsListBox.SelectedItem as SGroup;
            if (selectedGroup != null)
                LoadServices(selectedGroup.ID);
            else
                ServicesListBox.ItemsSource = null;
        }

        private void AddGroup_Click(object sender, RoutedEventArgs e)
        {
            string newName = ShowInputDialog("Введите название новой категории:", "Добавление категории");
            if (!string.IsNullOrWhiteSpace(newName))
            {
                if (App.context.SGroup.Any(g => g.Name == newName))
                {
                    MessageBox.Show("Категория с таким названием уже существует.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                var newGroup = new SGroup { Name = newName };
                App.context.SGroup.Add(newGroup);
                App.context.SaveChanges();
                LoadGroups();
                GroupsListBox.SelectedItem = newGroup;
            }
        }

        private void EditGroup_Click(object sender, RoutedEventArgs e)
        {
            var group = GroupsListBox.SelectedItem as SGroup;
            if (group == null)
            {
                MessageBox.Show("Выберите категорию для редактирования.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            string newName = ShowInputDialog("Редактирование названия категории:", "Редактирование", group.Name);
            if (!string.IsNullOrWhiteSpace(newName) && newName != group.Name)
            {
                if (App.context.SGroup.Any(g => g.Name == newName && g.ID != group.ID))
                {
                    MessageBox.Show("Категория с таким названием уже существует.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                group.Name = newName;
                App.context.SaveChanges();
                LoadGroups();
                GroupsListBox.SelectedItem = group;
            }
        }

        private void DeleteGroup_Click(object sender, RoutedEventArgs e)
        {
            var group = GroupsListBox.SelectedItem as SGroup;
            if (group == null)
            {
                MessageBox.Show("Выберите категорию для удаления.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (group.Services != null && group.Services.Any())
            {
                MessageBox.Show("Нельзя удалить категорию, содержащую услуги. Сначала удалите все услуги внутри неё.", "Запрещено", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (MessageBox.Show($"Удалить категорию \"{group.Name}\"?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                App.context.SGroup.Remove(group);
                App.context.SaveChanges();
                LoadGroups();
            }
        }

        private void AddService_Click(object sender, RoutedEventArgs e)
        {
            var group = GroupsListBox.SelectedItem as SGroup;
            if (group == null)
            {
                MessageBox.Show("Сначала выберите категорию, в которую хотите добавить услугу.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var dialog = new ServiceEditDialog();
            if (dialog.ShowDialog() == true)
            {
                var newService = new Services
                {
                    Name = dialog.ServiceName,
                    Price = dialog.ServicePrice,
                    GoupName = group.ID
                };
                App.context.Services.Add(newService);
                App.context.SaveChanges();
                LoadServices(group.ID);
                ServicesListBox.SelectedItem = newService;
            }
        }

        private void EditService_Click(object sender, RoutedEventArgs e)
        {
            var service = ServicesListBox.SelectedItem as Services;
            if (service == null)
            {
                MessageBox.Show("Выберите услугу для редактирования.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            var dialog = new ServiceEditDialog(service.Name, service.Price);
            if (dialog.ShowDialog() == true)
            {
                service.Name = dialog.ServiceName;
                service.Price = dialog.ServicePrice;
                App.context.SaveChanges();
                var group = GroupsListBox.SelectedItem as SGroup;
                if (group != null) LoadServices(group.ID);
                ServicesListBox.SelectedItem = service;
            }
        }

        private void DeleteService_Click(object sender, RoutedEventArgs e)
        {
            var service = ServicesListBox.SelectedItem as Services;
            if (service == null)
            {
                MessageBox.Show("Выберите услугу для удаления.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (MessageBox.Show($"Удалить услугу \"{service.Name}\"?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                App.context.Services.Remove(service);
                App.context.SaveChanges();
                var group = GroupsListBox.SelectedItem as SGroup;
                if (group != null) LoadServices(group.ID);
            }
        }

        private void BackToMainBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private string ShowInputDialog(string message, string title, string defaultValue = "")
        {
            var dialog = new InputDialog(message, title, defaultValue);
            if (dialog.ShowDialog() == true)
                return dialog.Result;
            return null;
        }
    }

    public class InputDialog : Window
    {
        public string Result { get; private set; }
        private TextBox inputBox;

        public InputDialog(string message, string title, string defaultValue)
        {
            this.Title = title;
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            this.Width = 400;
            this.Height = 180;
            this.Background = System.Windows.Media.Brushes.White;
            this.ResizeMode = ResizeMode.NoResize;

            var grid = new Grid();
            grid.Margin = new Thickness(10);
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            var messageBlock = new TextBlock { Text = message, FontSize = 16, Margin = new Thickness(0, 0, 0, 10) };
            Grid.SetRow(messageBlock, 0);
            grid.Children.Add(messageBlock);

            inputBox = new TextBox { Text = defaultValue, FontSize = 16, Margin = new Thickness(0, 0, 0, 10) };
            Grid.SetRow(inputBox, 1);
            grid.Children.Add(inputBox);

            var buttonPanel = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Right };
            var okBtn = new Button { Content = "OK", Width = 70, Margin = new Thickness(0, 0, 10, 0), Style = (Style)Application.Current.Resources["NemuBtn"] };
            okBtn.Click += (s, e) => { Result = inputBox.Text; DialogResult = true; };
            var cancelBtn = new Button { Content = "Отмена", Width = 70, Style = (Style)Application.Current.Resources["NemuBtn"] };
            cancelBtn.Click += (s, e) => { DialogResult = false; };
            buttonPanel.Children.Add(okBtn);
            buttonPanel.Children.Add(cancelBtn);
            Grid.SetRow(buttonPanel, 2);
            grid.Children.Add(buttonPanel);

            this.Content = grid;
        }
    }
}