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

namespace Govno_2._0.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();

            if (Properties.Settings.Default.LoginValue != string.Empty && Properties.Settings.Default.PasswordValue != string.Empty)
            {
                LoginTb.Text = Properties.Settings.Default.LoginValue;
                PassPb.Password = Properties.Settings.Default.PasswordValue;
                RemCb.IsChecked = true;
            }
        }

        private void LogBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(LoginTb.Text) && string.IsNullOrEmpty(PassPb.Password))
            {
                MessageBox.Show("Заполните все поля");
            }
            else
            {
                var user = App.context.User.FirstOrDefault(u => (u.Login == LoginTb.Text && u.Password == PassPb.Password) || (u.Mail == LoginTb.Text && u.Password == PassPb.Password));
                if (user != null)
                {
                    if (RemCb.IsChecked == true)
                    {
                        Properties.Settings.Default.LoginValue = LoginTb.Text;
                        Properties.Settings.Default.PasswordValue = PassPb.Password;
                        Properties.Settings.Default.Save();
                    }
                    else
                    {
                        Properties.Settings.Default.LoginValue = string.Empty;
                        Properties.Settings.Default.PasswordValue = string.Empty;
                        Properties.Settings.Default.Save();
                    }

                    App.currentUser = user;
                    Login login = new Login();
                    login.ShowDialog();

                    MainWindows main = new MainWindows();
                    main.Show();

                    Close();
                }
                else
                {
                    MessageBox.Show("Введены некоректные данные!");
                }
            }
        }

        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            Registration2Window regw = new Registration2Window();
            regw.Show();
            Close();
        }
        private void ResPassBtn_Click(object sender, RoutedEventArgs e)
        {
            RessPassWindow ress = new RessPassWindow();
            ress.Show();
            Close();
        }
        private void PassVisibleBtn_Click(object sender, RoutedEventArgs e)
        {
            PassPb.Password = PassTb.Text;
            
            PassP.Visibility = Visibility.Visible;
            PassT.Visibility = Visibility.Collapsed;
            PassVisibleBtn.Visibility = Visibility.Visible;
            PassInvisibleBtn.Visibility = Visibility.Collapsed;
        }
        private void PassInvisibleBtn_Click(object sender, RoutedEventArgs e)
        {
            PassTb.Text = PassPb.Password;
            PassP.Visibility = Visibility.Collapsed;
            PassT.Visibility = Visibility.Visible;
            PassVisibleBtn.Visibility = Visibility.Collapsed;
            PassInvisibleBtn.Visibility = Visibility.Visible;
        }
    }
}
