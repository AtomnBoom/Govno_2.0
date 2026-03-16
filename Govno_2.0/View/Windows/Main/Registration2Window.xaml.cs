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
    /// Логика взаимодействия для Registration2Window.xaml
    /// </summary>
    public partial class Registration2Window : Window
    {
        User _currentUser;
        public Registration2Window()
        {
            InitializeComponent();
            RoleCmb.DisplayMemberPath = "Name";
            RoleCmb.ItemsSource = App.context.UserType.ToList();
            RoleCmb.SelectedIndex = 0;
        }

        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(SnameTb.Text) && !string.IsNullOrEmpty(NameTb.Text) && !string.IsNullOrEmpty(LnameTb.Text))
            {
                var selectedRole = RoleCmb.SelectedItem as UserType;
                string generatedLogin = GenerateLogin(selectedRole, LnameTb.Text, NameTb.Text);
                while (App.context.User.Any(u => u.Login == generatedLogin));

                User newUser = new User()
                {
                    Login = generatedLogin,
                    SecondName = SnameTb.Text,
                    Name = NameTb.Text,
                    LastName = LnameTb.Text,
                    Mail = MailTb.Text,
                    Phone = PhoneTb.Text,
                    Type = selectedRole.ID
                };

                App.context.User.Add(newUser);
                App.context.SaveChanges();

                _currentUser = newUser;

                WaitAdmin waitAdmin = new WaitAdmin();

                if (waitAdmin.ShowDialog() == true)
                {
                    RegistrationWindows reg = new RegistrationWindows(_currentUser);
                    reg.Show();
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля");
            }
        }

        private string GenerateLogin(UserType role, string lastName, string name)
        {
            string initials = (lastName.Substring(0, 1) + name.Substring(0, 1)).ToLower();

            string prefix;
            string roleName = role.Name.ToLower();

            switch (roleName)
            {
                case "администратор":
                    prefix = "adm";
                    break;
                case "менеджер":
                    prefix = "mgr";
                    break;
                case "мастер":
                    prefix = "mst";
                    break;
                default:
                    prefix = "usr";
                    break;
            }

            int count = App.context.User.Where(u => u.Login.StartsWith(prefix)).Count() + 1;

            return string.Format("{0}_{1}_{2:D3}", prefix, initials, count);
        }
    }
}
