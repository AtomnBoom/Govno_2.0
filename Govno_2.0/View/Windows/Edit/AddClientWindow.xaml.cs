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
    /// Логика взаимодействия для AddClientWindow.xaml
    /// </summary>
    public partial class AddClientWindow : Window
    {
        public Clients NewClient { get; private set; }
        private readonly bool _isEditMode;
        private readonly Clients _editingClient;
        public AddClientWindow()
        {
            InitializeComponent();
            _isEditMode = false;
            TitleLb.Content = "Новый клиент";
            AddBtn.Content = "Добавить клиента";
        }
        public AddClientWindow(Clients client)
        {
            InitializeComponent();
            _isEditMode = true;
            _editingClient = client;
            TitleLb.Content = "Редактирование клиента";
            AddBtn.Content = "Сохранить изменения";

            FullNameTxt.Text = client.FullName;
            PhoneTxt.Text = client.Phone;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            string fullName = FullNameTxt.Text;
            string phone = PhoneTxt.Text;

            if (string.IsNullOrWhiteSpace(FullNameTxt.Text))
            {
                MessageBox.Show("Введите ФИО!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(PhoneTxt.Text))
            {
                MessageBox.Show("Введите телефон!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_isEditMode && phone == _editingClient.Phone)
            {
            }
            else if (App.context.Clients.Any(c => c.Phone == phone && c.ID != _editingClient.ID))
            {
                MessageBox.Show("Клиент с таким телефоном уже существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                if (_isEditMode)
                {
                    _editingClient.FullName = fullName;
                    _editingClient.Phone = phone;
                    App.context.SaveChanges();
                    NewClient = _editingClient;
                    DialogResult = true;
                    Close();
                }
                else
                {
                    var newClient = new Clients
                    {
                        FullName = fullName,
                        Phone = phone
                    };
                    App.context.Clients.Add(newClient);
                    App.context.SaveChanges();
                    NewClient = newClient;

                    DialogResult = true;
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка",MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            NewClient = null;
            DialogResult = false;
            Close();
        }
    }
}
