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
    /// Логика взаимодействия для ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Window
    {
        public ChangePassword()
        {
            InitializeComponent();
        }

        private void MailChangeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (NewMailTb.Text == NewMailAccesTb.Text)
            {
                WaitAdmin waitAdmin = new WaitAdmin();
                waitAdmin.ShowDialog();
                if (DialogResult == true)
                {
                    App.currentUser.Mail = NewMailAccesTb.Text;
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
