using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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
using System.Windows.Shapes;
using WorldResort.Model;

namespace WorldResort.AdminDialog
{
    /// <summary>
    /// Логика взаимодействия для CountryAdminDialog.xaml
    /// </summary>
    public partial class CountryAdminDialog : Window
    {
        public CountryAdminDialog(Country country)
        {
            InitializeComponent();
            initialComponent(country);
        }
        private async void initialComponent(Country country)
        {
            textBlockIdCountryDialog.Text = "id: " + country.id.ToString();
            textBlockNameCountryDialog.Text = "Название: " + country.Name;
            getCitites(country.id);
        }
        private async void getCitites(Guid countryId)
        {
            Manager.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Manager.token);
            var response = await Manager.client.GetFromJsonAsync<List<City>>($"api/country/citybycountry/{countryId}");
            listViewGetCountry.ItemsSource = response;
        }
    }
}
