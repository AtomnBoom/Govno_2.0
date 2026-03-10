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
    /// Логика взаимодействия для OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        public OrdersPage()
        {
            InitializeComponent();
        }

        private void AddOrderBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void HideAdd_MouseEnter(object sender, MouseEventArgs e)
        {
            VisibleAdd.Visibility= Visibility.Visible;
            HideAdd.Visibility= Visibility.Collapsed;
        }

        private void VisibleAdd_MouseLeave(object sender, MouseEventArgs e)
        {
            VisibleAdd.Visibility = Visibility.Collapsed;
            HideAdd.Visibility = Visibility.Visible;
        }
    }
}
