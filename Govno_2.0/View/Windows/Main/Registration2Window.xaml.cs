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
    /// Логика взаимодействия для Registration2Window.xaml
    /// </summary>
    public partial class Registration2Window : Window
    {
        public Registration2Window()
        {
            InitializeComponent();
        }

        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {

            WaitAdmin waitAdmin = new WaitAdmin();
            waitAdmin.ShowDialog();
            if (DialogResult == true)
            {
                
                RegistrationWindows reg = new RegistrationWindows();
                reg.Show();
                this.Close();
            }
        }
    }
}
