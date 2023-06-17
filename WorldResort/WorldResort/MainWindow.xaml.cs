using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WorldResort.Model;
using System.Net.Http.Json;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace WorldResort
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Manager.client.BaseAddress = new Uri("http://localhost:5128/");
            Manager.client.DefaultRequestHeaders.Accept.Clear();
            Manager.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private void buttonRegistMain_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrWindow = new RegistrationWindow();
            registrWindow.ShowDialog();
        }
        private async Task RunAsync(string userName, string password)
        {
            try
            {
                var userLogin = new UserLogin { userName = userName, password = password };
                await LoginUserAsync(userLogin);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                MessageBox.Show(e.Message);
                throw;
            }
        }
        private async Task LoginUserAsync(UserLogin userLogin)
        {
            var response = await Manager.client.PostAsJsonAsync("api/authentication", userLogin);
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                Manager.token = response.Content.ReadAsAsync<TokenResponse>().Result.token;
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(Manager.token);
                Manager.userId = securityToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                Manager.role = securityToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
                if (Manager.role == "Admin")
                {
                    AdminWindow admin = new AdminWindow();
                    admin.Show();
                    Close();
                }
                else
                {
                    UserWindow user = new UserWindow();
                    user.Show();
                    Close();
                }
            }
        }

        private void buttonAuthMain_Click(object sender, RoutedEventArgs e)
        {
            var login = textBoxLoginMain.Text;
            var password = textBoxPasswordMain.Password;
            if (login != "" && password != "")
                RunAsync(login, password);
            else
                MessageBox.Show("Поля не должны быть пустыми", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
