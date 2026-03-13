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
    /// Логика взаимодействия для AddClientWindow.xaml
    /// </summary>
    public partial class AddClientWindow : Window
    {
        public Clients NewClient { get; private set; }
        public AddClientWindow()
        {
            InitializeComponent();
        }
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FullNameTxt.Text))
            {
                MessageBox.Show("Введите ФИО!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(PhoneTxt.Text))
            {
                MessageBox.Show("Введите телефон!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (App.context.Clients.Any(c => c.Phone == PhoneTxt.Text))
            {
                MessageBox.Show("Клиент с таким телефоном уже существует!", "Ошибка",MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                NewClient = new Clients
                {
                    FullName = FullNameTxt.Text.Trim(),
                    Phone = PhoneTxt.Text.Trim()
                };

                App.context.Clients.Add(NewClient);
                App.context.SaveChanges();

                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка",MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            NewClient = null;
            DialogResult = false;
            Close();
        }
    }
}
