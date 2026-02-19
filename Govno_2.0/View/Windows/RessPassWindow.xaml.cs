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
    /// Логика взаимодействия для RessPassWindow.xaml
    /// </summary>
    public partial class RessPassWindow : Window
    {
        public RessPassWindow()
        {
            InitializeComponent();
        }

        private void RessBtn_Click(object sender, RoutedEventArgs e)
        {
            WaitAdmin waitAdmin = new WaitAdmin();
            waitAdmin.ShowDialog();
            Close();
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
        private void PassVisible2Btn_Click(object sender, RoutedEventArgs e)
        {
            PassP2.Visibility = Visibility.Visible;
            PassT2.Visibility = Visibility.Collapsed;
            PassVisible2Btn.Visibility = Visibility.Visible;
            PassInvisible2Btn.Visibility = Visibility.Collapsed;
        }
        private void PassInvisible2Btn_Click(object sender, RoutedEventArgs e)
        {
            PassP2.Visibility = Visibility.Collapsed;
            PassT2.Visibility = Visibility.Visible;
            PassVisible2Btn.Visibility = Visibility.Collapsed;
            PassInvisible2Btn.Visibility = Visibility.Visible;
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
            PassTb2.Text = PassPb2.Password;
        }
        private void Pass2Tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            PassPb2.Password = PassTb2.Text;
        }
    }
}
