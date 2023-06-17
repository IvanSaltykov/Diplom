using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using WorldResort.Model;

namespace WorldResort
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void buttonRegistration_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxCheckPasswordRegistration.Password == textBoxPasswordRegistration.Password)
            {
                if (textBoxLoginRegistration.Text.ToLower().Contains("/admin"))
                    userRegistration("Admin", textBoxLoginRegistration.Text.Replace("/admin", ""));
                else
                    userRegistration("User", textBoxLoginRegistration.Text);
            }
            else MessageBox.Show("Пароли не совпадают!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        private async void userRegistration(string role, string login)
        {
            if (checkPassword(textBoxPasswordRegistration.Password, textBoxCheckPasswordRegistration.Password))
            {
                string name = textBoxNameRegistration.Text;
                string surname = textBoxSurnameRegistration.Text;
                string phoneNumber = textBoxPhoneNumberRegistration.Text;
                if (name != "" &&
                    surname != "" &&
                    checkEmail(login) &&
                    checkPhoneNumber(phoneNumber))
                {
                    var userNew = new UserResistration
                    {
                        FirstName = name,
                        LastName = surname,
                        UserName = login,
                        PhoneNumber = phoneNumber,
                        Email = login,
                        Password = textBoxPasswordRegistration.Password,
                        Roles = role
                    };
                    var response = await Manager.client.PostAsJsonAsync($"api/authentication/registr", userNew);
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Аккаунт создан", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                        Close();
                    }
                    else
                    {
                        MessageBox.Show(response.StatusCode.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                    MessageBox.Show("Введите корректные данные", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
                MessageBox.Show("Подтвердите пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        private bool checkPhoneNumber(string phoneNumber)
        {
            if (phoneNumber.Length == 11)
                return true;
            else
                return false;
        }
        private bool checkPassword(string password, string doublePassword)
        {
            if (password.Length >= 8 && password == doublePassword)
                return true;
            else
                return false;
        }
        private bool checkEmail(string email)
        {
            if (email.Length >= 8 && email.Contains('@'))
                return true;
            else
                return false;
        }
        private void buttonBackRegistration_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void textBoxPhoneNumberRegistration_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                string text = textBox.Text;
                StringBuilder builder = new StringBuilder();

                foreach (char c in text)
                {
                    if (char.IsDigit(c))
                    {
                        builder.Append(c);
                    }
                }

                textBox.Text = builder.ToString();
                textBox.CaretIndex = builder.Length;
            }
        }
    }
}
