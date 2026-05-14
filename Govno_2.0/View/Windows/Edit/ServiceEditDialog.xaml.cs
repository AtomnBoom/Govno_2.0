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
    /// Логика взаимодействия для ServiceEditDialog.xaml
    /// </summary>
    public partial class ServiceEditDialog : Window
    {
        public string ServiceName { get; private set; }
        public decimal ServicePrice { get; private set; }

        public ServiceEditDialog(string name = "", decimal price = 0)
        {
            InitializeComponent();
            NameBox.Text = name;
            PriceBox.Text = price.ToString();
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameBox.Text))
            {
                MessageBox.Show("Введите название услуги.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!decimal.TryParse(PriceBox.Text, out decimal price) || price < 0)
            {
                MessageBox.Show("Введите корректную цену (число >= 0).", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            ServiceName = NameBox.Text.Trim();
            ServicePrice = price;
            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}