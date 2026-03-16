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

namespace Govno_2._0.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindows.xaml
    /// </summary>
    public partial class RegistrationWindows : Window
    {
        User _newUser;
        public RegistrationWindows(User _currentUser)
        {
            InitializeComponent();

            _newUser = _currentUser;
            LoginTb.Content = _newUser.Login;
        }

        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            if (PassPb.Password != string.Empty && PassPb2.Password != string.Empty)
            {
                if (PassPb.Password == PassPb2.Password)
                {
                    WaitAdmin waitAdmin = new WaitAdmin();

                    if (waitAdmin.ShowDialog() == true)
                    {
                        _newUser.Password = PassPb.Password;

                        App.context.SaveChanges();
                        LoginWindow loginWindow = new LoginWindow();
                        loginWindow.Show();
                        Close();
                    }
                    else
                    {
                        App.context.User.Remove(_newUser);
                        App.context.SaveChanges();
                        LoginWindow loginWindow = new LoginWindow();
                        loginWindow.Show();
                        Close();
                    }
                }
            }
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
        private void PassVisible2Btn_Click(object sender, RoutedEventArgs e)
        {
            PassPb2.Password = PassTb2.Text;
            PassP2.Visibility = Visibility.Visible;
            PassT2.Visibility = Visibility.Collapsed;
            PassVisible2Btn.Visibility = Visibility.Visible;
            PassInvisible2Btn.Visibility = Visibility.Collapsed;
        }
        private void PassInvisible2Btn_Click(object sender, RoutedEventArgs e)
        {
            PassTb2.Text = PassPb2.Password;
            PassP2.Visibility = Visibility.Collapsed;
            PassT2.Visibility = Visibility.Visible;
            PassVisible2Btn.Visibility = Visibility.Collapsed;
            PassInvisible2Btn.Visibility = Visibility.Visible;
        }
        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            App.context.User.Remove(_newUser);
            App.context.SaveChanges();
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            Close();
        }
    }
}
