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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Govno_2._0.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProfilPage.xaml
    /// </summary>
    public partial class ProfilPage : Page
    {
        public User CurrentUser { get; set; }
        public ProfilPage()
        {
            InitializeComponent();
            LoadUserData();
        }

        private void LoadUserData()
        {
            try
            {
                var userId = App.currentUser?.ID;

                if (userId == null)
                {
                    MessageBox.Show("Пользователь не авторизован", "Ошибка");
                    return;
                }

                using (var context = new FixitEntities())
                {
                    CurrentUser = context.User
                        .Include("UserType")
                        .FirstOrDefault(u => u.ID == userId);
                }

                if (CurrentUser == null)
                {
                    MessageBox.Show("Данные пользователя не найдены", "Ошибка");
                    return;
                }

                DataContext = this;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки профиля: {ex.Message}", "Ошибка");
            }
        }

        private void Support_Click(object sender, MouseButtonEventArgs e)
        {
            App.MainFrame.Navigate(new SupportPage());
        }
        private void Settings_Click(object sender, MouseButtonEventArgs e)
        {
            App.MainFrame.Navigate(new SettingsPage());
        }
    }
}