using System;
using System.Collections.Generic;
using System.IO;
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

namespace WorldResort.UserDialog
{
    /// <summary>
    /// Логика взаимодействия для HotelUserDialog.xaml
    /// </summary>
    public partial class HotelsUserDialog : Window
    {
        private Guid _resortId;
        public HotelsUserDialog(ResortForListView resort)
        {
            InitializeComponent();
            _resortId = resort.Id;
            getResort(resort.Id);
            textBlock.Text = "Отели в города " + resort.Name;
        }
        private async Task getResort(Guid resortId)
        {
            List<HotelForListView> hotelList;
            Manager.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Manager.token);
            var response = await Manager.client.GetFromJsonAsync<List<HotelsResponse>>($"api/resortcity/{resortId}/hotels");
            hotelList = new List<HotelForListView>(response.Count);
            foreach (var item in response)
            {
                hotelList.Add(new HotelForListView { Id = item.Id, Name = $"Название: {item.Name}", image = Manager.GetImageFromByteArray(item.img), Star = $"Количество звезд: {item.Star}", Description = $"Описание:\n {item.Description}", CityId = item.CityId });
            }
            listView.ItemsSource = hotelList;
        }

        private void listView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var hotel = (sender as ListView).SelectedItem as HotelForListView;
            HotelUserDialog hotelUserDialog = new HotelUserDialog(hotel.Id, _resortId);
            hotelUserDialog.ShowDialog();
        }
    }
}
