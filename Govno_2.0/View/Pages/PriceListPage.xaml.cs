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
    /// Логика взаимодействия для PriceListPage.xaml
    /// </summary>
    public partial class PriceListPage : Page
    {
        public PriceListPage()
        {
            InitializeComponent();
            LoadPriceList();
        }
        private void LoadPriceList()
        {
            try
            {
                var groups = App.context.SGroup.Include("Services").OrderBy(g => g.Name).ToList();

                GroupsList.ItemsSource = groups;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки прайс-листа: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void EditServiceBtn_Click(object sender, RoutedEventArgs e)
        {
            EditPriceList editPriceList = new EditPriceList();

            if (editPriceList.ShowDialog() == true)
            {
                LoadPriceList();
            }
        }
    }
}
