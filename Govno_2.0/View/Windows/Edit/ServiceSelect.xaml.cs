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
    /// Логика взаимодействия для ServiceSelect.xaml
    /// </summary>
    public partial class ServiceSelect : Window
    {
        public Services SelectedService { get; private set; }

        private List<Services> allServices;
        public ServiceSelect()
        {
            InitializeComponent();

            LoadServices();
            LoadGroups();
        }
        private void LoadGroups()
        {
            var groups = App.context.SGroup.ToList();

            GroupSelectCmb.ItemsSource = groups;
            GroupSelectCmb.DisplayMemberPath = "Name";
            GroupSelectCmb.SelectedValuePath = "ID";

            if (groups.Count > 0)
                GroupSelectCmb.SelectedIndex = 0;
        }

        private void LoadServices()
        {
            allServices = App.context.Services
                .Include("SGroup")
                .ToList();

            FilterServicesByGroup();
        }

        private void FilterServicesByGroup()
        {
            if (GroupSelectCmb.SelectedValue == null) return;

            int selectedGroupId = (int)GroupSelectCmb.SelectedValue;

            var filteredServices = allServices
                .Where(s => s.GoupName == selectedGroupId)
                .ToList();

            NameServiceCmb.ItemsSource = filteredServices;
            NameServiceCmb.DisplayMemberPath = "Name";
            NameServiceCmb.SelectedValuePath = "ID";

            if (filteredServices.Count > 0)
                NameServiceCmb.SelectedIndex = 0;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            SelectedService = null;
            DialogResult = false;
            Close();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (NameServiceCmb.SelectedItem == null)
            {
                MessageBox.Show("Выберите услугу!", "Ошибка",MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            SelectedService = NameServiceCmb.SelectedItem as Services;
            DialogResult = true;
            Close();
        }

        private void GroupSelectCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterServicesByGroup();
        }

        private void NameServiceCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NameServiceCmb.SelectedItem is Services service)
            {
                PriceTbl.Text = $"{service.Price:F2} руб.";
            }
            else
            {
                PriceTbl.Text = "";
            }
        }

    }
}
