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
using WorldResort.Model;
using WorldResort.UserDialog;

namespace WorldResort.UserPages
{
    /// <summary>
    /// Логика взаимодействия для FavouriteUserPage.xaml
    /// </summary>
    public partial class FavouriteUserPage : Page
    {
        public FavouriteUserPage()
        {
            InitializeComponent();
            getFavouriteHotel();
        }

        private async void getFavouriteHotel()
        {
            Manager.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Manager.token);
            var response = await Manager.client.GetFromJsonAsync<List<HotelsResponse>>($"api/favouritehotels/{Manager.userId}");
            List<HotelForListView> hotelList = new List<HotelForListView>(response.Count);
            foreach (var item in response)
            {
                hotelList.Add(new HotelForListView { Id = item.Id, Name = $"Название: {item.Name}", image = Manager.GetImageFromByteArray(item.img), Star = $"Количество звезд: {item.Star}", Description = $"Описание:\n {item.Description}", CityId = item.CityId });
            }
            listView.ItemsSource = hotelList;
        }

        private async void buttonFavouriteUserWindow_Click(object sender, RoutedEventArgs e)
        {
            var favouriteHotel = (sender as Button).DataContext as HotelForListView;
            if (favouriteHotel != null)
            {
                Manager.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Manager.token);
                var response = await Manager.client.DeleteAsync($"api/favouritehotels/{Manager.userId}/delete/{favouriteHotel.Id}");
                if (response.IsSuccessStatusCode)
                    getFavouriteHotel();
            }
        }

        private void listView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var hotel = (sender as ListView).SelectedItem as HotelForListView;
            HotelUserDialog hotelUserDialog = new HotelUserDialog(hotel.Id, hotel.CityId);
            hotelUserDialog.ShowDialog();
        }
    }
}
