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
    /// Логика взаимодействия для ChangePhone.xaml
    /// </summary>
    public partial class ChangePhone : Window
    {
        public ChangePhone()
        {
            InitializeComponent();
        }

        private void PhoneChangeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (AgePhoneTb.Text == App.currentUser.Phone && !string.IsNullOrWhiteSpace(NewPhoneTb.Text))
            {
                WaitAdmin waitAdmin = new WaitAdmin();
                waitAdmin.ShowDialog();
                if (DialogResult == true)
                {
                    App.currentUser.Phone = NewPhoneTb.Text;
                    App.context.SaveChanges();
                    DialogResult = true;
                }
                Close();
            }
            else
            {
                MessageBox.Show("Пароли не совпадают");
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
