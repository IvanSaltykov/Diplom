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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WorldResort.AdminDialog;
using WorldResort.Model;

namespace WorldResort.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для GetCountryAdminPage.xaml
    /// </summary>
    public partial class GetCountryAdminPage : Page
    {
        public GetCountryAdminPage()
        {
            InitializeComponent();
            getCountries();
        }
        private async void getCountries()
        {
            Manager.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Manager.token);
            var response = await Manager.client.GetFromJsonAsync<List<Country>>($"api/country");
            listViewGetCountry.ItemsSource = response;
        }

        private async void listViewGetCountryt_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var country = (sender as ListView).SelectedItem as Country;
            CountryAdminDialog countryDialog = new CountryAdminDialog(country);
            countryDialog.ShowDialog();
        }
    }
}
