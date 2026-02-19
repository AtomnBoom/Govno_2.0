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
        public RegistrationWindows()
        {
            InitializeComponent();
        }

        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PassVisibleBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PassVisibl2eBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PassPb_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PassTb.Text = PassPb.Password;
        }

        private void PassTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            PassPb.Password = PassTb.Text;
        }
        private void Pass2Pb_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PassTb.Text = PassPb.Password;
        }

        private void Pass2Tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            PassPb.Password = PassTb.Text;
        }
    }
}
