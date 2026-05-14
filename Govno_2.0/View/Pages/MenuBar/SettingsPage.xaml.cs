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
    /// Логика взаимодействия для SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private void Support_Click(object sender, MouseButtonEventArgs e)
        {
            App.MainFrame.Navigate(new SupportPage());
        }

        private void Profile_Click(object sender, MouseButtonEventArgs e)
        {
            App.MainFrame.Navigate(new ProfilPage());
        }

        private void PassChangeBtn_Click(object sender, RoutedEventArgs e)
        {
            ChangePassword changePassword = new ChangePassword();
            if(changePassword.ShowDialog() == true)
            {
                MessageBox.Show("Пароль успешно изменен");
            }
            
        }

        private void PhoneChangeBtn_Click(object sender, RoutedEventArgs e)
        {
            ChangePhone changePhone = new ChangePhone();
            if (changePhone.ShowDialog() == true)
            {
                MessageBox.Show("Пароль успешно изменен");
            }
        }

        private void MailChangeBtn_Click(object sender, RoutedEventArgs e)
        {
            ChangeMail changeMail = new ChangeMail();
            if (changeMail.ShowDialog() == true)
            {
                MessageBox.Show("Пароль успешно изменен");
            }
        }
    }
}
