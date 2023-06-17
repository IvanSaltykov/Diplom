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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WorldResort.AdminDialog;
using WorldResort.Model;
using f = System.Windows.Forms;
using Image = System.Drawing.Image;

namespace WorldResort.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для PostHotelResortAdminPage.xaml
    /// </summary>
    public partial class PostHotelResortAdminPage : Page
    {
        private Guid _partWorldId;
        private Guid _countryId;
        private Guid _cityId;
        private byte[] _image;
        private List<TypeRoom> _typeRooms;
        public PostHotelResortAdminPage()
        {
            InitializeComponent();
            getPartWorld();
        }
        private void getPartWorld()
        {
            Manager.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Manager.token);
            var response = Manager.client.GetFromJsonAsync<List<PartWorld>>("api/partworld");
            comboBoxPartWorldPostHotelResort.DisplayMemberPath = "Name";
            comboBoxPartWorldPostHotelResort.ItemsSource = response.Result;
        }

        private void comboBoxPartWorldPostHotelResort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var partWorld = comboBoxPartWorldPostHotelResort.SelectedValue as PartWorld;
            _partWorldId = partWorld.Id;
            var response = Manager.client.GetFromJsonAsync<List<Country>>($"api/partworld/{_partWorldId}/country");
            comboBoxCountryPostHotelResort.IsEnabled = true;
            comboBoxCountryPostHotelResort.DisplayMemberPath = "Name";
            comboBoxCountryPostHotelResort.ItemsSource = response.Result;
        }

        private void comboBoxCountryPostHotelResort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var counrty = comboBoxCountryPostHotelResort.SelectedValue as Country;
            _countryId = counrty.id;
            var response = Manager.client.GetFromJsonAsync<List<City>>($"api/country/{_countryId}/cities");
            comboBoxCityPostHotelResort.IsEnabled = true;
            comboBoxCityPostHotelResort.DisplayMemberPath = "Name";
            comboBoxCityPostHotelResort.ItemsSource = response.Result;
        }
        private void comboBoxCityPostHotelResort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var city = comboBoxCityPostHotelResort.SelectedValue as City;
            _cityId = city.Id;
            textBoxNameHotelResort.IsEnabled = true;
            textBoxStarHotelResort.IsEnabled = true;
            textBoxCountTypeRoomResort.IsEnabled = true;
            textBoxDescriptionHotelResort.IsEnabled = true;
            textBoxPriceHotelResort.IsEnabled = true;

        }
        private void textBoxStarHotelResort_TextChanged(object sender, TextChangedEventArgs e)
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
            if (textBoxNameHotelResort.Text.Length != 0 && textBoxStarHotelResort.Text.Length != 0)
                buttonPostHotelResort.IsEnabled = true;
        }
        private void buttonCreateTypeRoom_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxCountTypeRoomResort.Text != "")
            {
                int countTypeRoom = int.Parse(textBoxCountTypeRoomResort.Text);
                _typeRooms = new List<TypeRoom>(countTypeRoom);
                for (int i = 0; i < countTypeRoom; i++)
                {
                    CreateTypeRoomDialog dialog = new CreateTypeRoomDialog();
                    dialog.ShowDialog();
                    _typeRooms.Add
                        (
                            new TypeRoom { Name = dialog.TextBoxNameTypeRoom, Price = int.Parse(dialog.TextBoxPriceTypeRoom) }
                        );
                }
            }
            else
                MessageBox.Show("Введите количество типов отелей!!!");
        }
        private void buttonPostHotelResort_Click(object sender, RoutedEventArgs e)
        {
            if (_image != null && 
                textBoxCountTypeRoomResort.Text != "" && 
                textBoxDescriptionHotelResort.Text != "" &&
                textBoxNameHotelResort.Text != "" &&
                textBoxPriceHotelResort.Text != "" &&
                textBoxStarHotelResort.Text != "")
            {
                HotelResponse createHotel = new HotelResponse()
                {
                    hotel = new HotelsResponse
                    {
                        Name = textBoxNameHotelResort.Text,
                        CityId = _cityId,
                        Star = int.Parse(textBoxStarHotelResort.Text),
                        img = _image,
                        Description = textBoxDescriptionHotelResort.Text,
                        Price = int.Parse(textBoxPriceHotelResort.Text)
                    },
                    typeRoom = _typeRooms
                };
                var response = Manager.client.PostAsJsonAsync($"api/resortcity/{_cityId}/hotels", createHotel);
                response.Result.EnsureSuccessStatusCode();
                if (!response.Result.IsSuccessStatusCode)
                    MessageBox.Show(response.Result.IsSuccessStatusCode.ToString());
                else
                    MessageBox.Show("Элемент создан");
            }
            else
                MessageBox.Show("Заполните все данные!!!");
        }
        private void buttonCreatePhotoPostHotelResort_Click(object sender, RoutedEventArgs e)
        {
            AddImg();
        }
        private void AddImg()
        {
            f.OpenFileDialog dialog = new f.OpenFileDialog();
            dialog.Filter = "| *.png; *.jpg";
            if (dialog.ShowDialog() == f.DialogResult.OK)
            {
                imagePhotobuttonCreatePhotoPostHotelResort.Source = new BitmapImage(new Uri(dialog.FileName));
                byte[] imageBytes = File.ReadAllBytes(dialog.FileName);
                using (MemoryStream ms = new MemoryStream(imageBytes))
                {
                    Image image = Image.FromStream(ms);
                    MemoryStream stream = new MemoryStream();
                    image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    byte[] bytes = stream.ToArray();
                    _image = bytes;
                }
            }
        }

        private void textBoxCountTypeRoomResort_TextChanged(object sender, TextChangedEventArgs e)
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
            if (textBoxCountTypeRoomResort.Text.Length == 0)
                buttonCreateTypeRoom.IsEnabled = false;
            else
                buttonCreateTypeRoom.IsEnabled = true;
        }

        private void textBoxPriceHotelResort_TextChanged(object sender, TextChangedEventArgs e)
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
