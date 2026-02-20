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

            try
            {
                if (Properties.Settings.Default.LoginValue != string.Empty && Properties.Settings.Default.PasswordValue != string.Empty)
                {
                    RemCb.IsChecked = true;
                }
            }
            catch { }
        }

        private void LogBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (RemCb.IsChecked == true)
                {
                    Properties.Settings.Default.LoginValue = LoginTb.Text;
                    Properties.Settings.Default.PasswordValue = PassPb.Password;
                }
                else
                {
                    Properties.Settings.Default.LoginValue = string.Empty;
                    Properties.Settings.Default.PasswordValue = string.Empty;
                }
                Properties.Settings.Default.Save();
            }
            catch { }

            Login login = new Login();
            login.ShowDialog();

            MainWindows main = new MainWindows();
            main.Show();

            Close();
        }

        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindows regw = new RegistrationWindows();
            regw.Show();
            Close();
        }
        private void ResPassBtn_Click(object sender, RoutedEventArgs e)
        {
            RessPassWindow ress = new RessPassWindow();
            ress.ShowDialog();
        }
        private void PassPb_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PassTb.Text = PassPb.Password;
        }
        private void PassTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            PassPb.Password = PassTb.Text;
        }
        private void PassVisibleBtn_Click(object sender, RoutedEventArgs e)
        {
            PassP.Visibility = Visibility.Visible;
            PassT.Visibility = Visibility.Collapsed;
            PassVisibleBtn.Visibility = Visibility.Visible;
            PassInvisibleBtn.Visibility = Visibility.Collapsed;
        }
        private void PassInvisibleBtn_Click(object sender, RoutedEventArgs e)
        {
            PassP.Visibility = Visibility.Collapsed;
            PassT.Visibility = Visibility.Visible;
            PassVisibleBtn.Visibility = Visibility.Collapsed;
            PassInvisibleBtn.Visibility = Visibility.Visible;
        }
    }
}
