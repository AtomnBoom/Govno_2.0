using Govno_2._0.Models;
using Govno_2._0.View.Windows.Edit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
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

namespace Govno_2._0.View.Pages.Edit
{
    /// <summary>
    /// Логика взаимодействия для EditPriceList.xaml
    /// </summary>
    public partial class EditPriceList : Page
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
            var group = GroupsListBox.SelectedItem as SGroup;
            
            var dialog = new ServiceGroupEditDialog();
            if (dialog.ShowDialog() == true)
            {
                var newGroup = new SGroup
                {
                    Name = dialog.GroupName
                };
                App.context.SGroup.Add(newGroup);
                App.context.SaveChanges();
                LoadGroups();
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
            var dialog = new ServiceGroupEditDialog(group.Name);
            if (dialog.ShowDialog() == true)
            {
                group.Name = dialog.GroupName;
                App.context.SaveChanges();
                LoadGroups();
            }
        }

        private void DeleteGroup_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var group = GroupsListBox.SelectedItem as SGroup;
                if (group == null)
                {
                    MessageBox.Show("Выберите категорию для удаления.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if (MessageBox.Show($"Удалить категорию \"{group.Name}\"?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    App.context.SGroup.Remove(group);
                    App.context.SaveChanges();
                }
                LoadGroups();
            }
            catch
            {
                MessageBox.Show("Неудается удалить категорю в которой существуют услуги, удалите услуги в категории а затем попробуйте снова");
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
            App.MainFrame.Navigate(new PriceListPage());
        }
    }
}