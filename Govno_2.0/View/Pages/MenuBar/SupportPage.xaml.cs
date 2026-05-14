using Govno_2._0.View.Windows;
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
    /// Логика взаимодействия для SupportPage.xaml
    /// </summary>
    public partial class SupportPage : Page
    {
        public SupportPage()
        {
            InitializeComponent();
        }
        private void SendRequest_Click(object sender, RoutedEventArgs e)
        {
            if (ProblemsListBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите проблему из списка!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string selectedProblem = ((ListBoxItem)ProblemsListBox.SelectedItem).Content.ToString();
            string comment = CommentBox.Text.Trim();

            WaitAdmin waitAdmin = new WaitAdmin();
            if (waitAdmin.ShowDialog() == true)
            {
                MessageBox.Show($"Заявка отправлена администратору!\n\nПроблема: {selectedProblem}\nКомментарий: {(string.IsNullOrEmpty(comment) ? "—" : comment)}", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);

                ProblemsListBox.SelectedItem = null;
                CommentBox.Clear();
            }
            else
            {
                MessageBox.Show("Ошибка отправки заявки");
            }
        }
        private void Profile_Click(object sender, MouseButtonEventArgs e)
        {
            App.MainFrame.Navigate(new ProfilPage());
        }
        private void Settings_Click(object sender, MouseButtonEventArgs e)
        {
            App.MainFrame.Navigate(new SettingsPage());
        }
    }
}
