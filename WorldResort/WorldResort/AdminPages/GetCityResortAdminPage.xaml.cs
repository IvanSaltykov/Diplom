using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
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
using WorldResort.AdminDialog;
using WorldResort.Model;

namespace WorldResort.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для GetCityResortAdminPage.xaml
    /// </summary>
    public partial class GetCityResortAdminPage : Page
    {
        public GetCityResortAdminPage()
        {
            InitializeComponent();
            getCities();
        }
        private async Task getCities()
        {
            //Manager.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Manager.token);
            var response = await Manager.client.GetFromJsonAsync<List<City>>($"api/resortcity");
            listViewGetCityResort.ItemsSource = response;
        }

        private void listViewGetCityResort_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var city = (sender as ListView).SelectedItem as City;
            if (city != null)
            {
                CityResortAdminDialog cityDialog = new CityResortAdminDialog(city);
                cityDialog.ShowDialog();
            }
        }
    }
}
