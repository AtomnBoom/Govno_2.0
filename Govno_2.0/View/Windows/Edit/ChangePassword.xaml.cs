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

namespace Govno_2._0.View.Windows.Edit
{
    /// <summary>
    /// Логика взаимодействия для ChangeMail.xaml
    /// </summary>
    public partial class ChangeMail : Window
    {
        public ChangeMail()
        {
            InitializeComponent();
        }

        private void PassChangeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (NewPassTb.Text == NewPassAccesTb.Text)
            {
                WaitAdmin waitAdmin = new WaitAdmin();
                waitAdmin.ShowDialog();
                if (DialogResult == true)
                {
                    App.currentUser.Password = NewPassAccesTb.Text;
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
