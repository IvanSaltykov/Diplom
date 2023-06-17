using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    public partial class HotelUserDialog : Window
    {
        private Guid _hotelId;
        private Guid _cityId;
        private int _priceHotel = 0;
        private int _priceTypeRoom = 0;
        private string _typeRoom = "";
        private int _price = 0;
        public HotelUserDialog(Guid hotelId, Guid cityId)
        {
            InitializeComponent();
            _hotelId = hotelId;
            _cityId = cityId;
            getHotel();
            checkFavouriteHotelAsync();
        }

        private async Task checkFavouriteHotelAsync()
        {
            Manager.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Manager.token);
            var response = await Manager.client.GetFromJsonAsync<List<HotelsResponse>>($"api/favouritehotels/{Manager.userId}");
            foreach(var item in response)
            {
                if (item.Id == _hotelId)
                {
                    buttonAddFavouriteHotelDialog.IsEnabled = false;
                    buttonAddFavouriteHotelDialog.Content = "Добавлен";
                }
            }
        }

        private ObservableCollection<CheckBoxItem> _checkBoxItems { get; set; }
        private HotelResponse _response { get; set; }
        private async void getHotel()
        {
            Manager.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Manager.token);
            var response = await Manager.client.GetFromJsonAsync<HotelResponse>($"api/resortcity/{_cityId}/hotels/{_hotelId}");
            if (response != null)
            {
                _response = response;
                imageHotelDialog.Source = Manager.GetImageFromByteArray(_response.hotel.img);
                textBlockNameHotelDialog.Text = "Название: " + _response.hotel.Name;
                textBlockStarHotelDialog.Text = "Количество: " + _response.hotel.Star;
                textBlockDescriptionHotelDialog.Text = "Описание:\n " + _response.hotel.Description;
                _checkBoxItems = new ObservableCollection<CheckBoxItem>();
                foreach (var item in _response.typeRoom)
                {
                    _checkBoxItems.Add(new CheckBoxItem { Name = $"{item.Name}\n\tЦена:{item.Price}", Price = item.Price });

                }
                listView.ItemsSource = _checkBoxItems;
                textBlockPriceHotelDialog.Text = $"Цена: {_response.hotel.Price}";
                _priceHotel = _response.hotel.Price;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var clickedCheckBox = (CheckBox)sender;
            foreach (var checkBoxItem in _checkBoxItems)
            {
                if (checkBoxItem.Name != clickedCheckBox.Content.ToString())
                {
                    checkBoxItem.IsSelected = false;
                }
                else
                {
                    textBlockPriceHotelDialog.Text = $"Цена: {_response.hotel.Price + checkBoxItem.Price}";
                    _priceTypeRoom = checkBoxItem.Price;
                    _typeRoom = checkBoxItem.Name;
                }
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            textBlockPriceHotelDialog.Text = $"Цена: {_response.hotel.Price}";
            _priceTypeRoom = 0;
            _typeRoom = "";
        }

        private async void buttonAddFavouriteHotelDialog_Click(object sender, RoutedEventArgs e)
        {
            Manager.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Manager.token);
            var response = await Manager.client.PostAsJsonAsync($"api/favouritehotels/{Manager.userId}", new FavouriteHotelCreate { hotelId = _hotelId });
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                buttonAddFavouriteHotelDialog.Content = "Добавлен";
                buttonAddFavouriteHotelDialog.IsEnabled = false;
            }
            MessageBox.Show(DecryptNumber(int.Parse(textBoxPassportNumderDialogHotel.Text), 191423).ToString());
        }
        private int EncryptNumber(int number, int key)
        {
            return number ^ key;
        }

        private int DecryptNumber(int encryptedNumber, int key)
        {
            return encryptedNumber ^ key;
        }

        private async void buttonBookHotelDialog_Click(object sender, RoutedEventArgs e)
        {
            if (datePickerStartTraveling.SelectedDate != null && datePickerEndTraveling.SelectedDate != null)
                CreateTicket();
            else
                MessageBox.Show("Введите корректные данные и выберите тип номера", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        private async void CreateTicket()
        {
            string name = textBoxCustomerNameDialogHotel.Text;
            string surname = textBoxCustomerSurnameDialogHotel.Text;
            string patronymic = textBoxCustomerPatronymicDialogHotel.Text;
            var startTraveling = datePickerStartTraveling.SelectedDate.Value;
            var endTraveling = datePickerEndTraveling.SelectedDate.Value;
            int age = int.Parse(textBoxAgeDialogHotel.Text);
            int passportSeries = int.Parse(textBoxPassportSeriesDialogHotel.Text);
            int passportNumber = int.Parse(textBoxPassportNumderDialogHotel.Text);
            if (name != "" &&
                surname != "" &&
                patronymic != "" &&
                checkAge(age) &&
                checkPassportNumber(passportNumber) &&
                checkPassportSeries(passportSeries) &&
                _typeRoom != "")
            {
                var ticket = new TicketCreate
                {
                    CustomerName = textBoxCustomerNameDialogHotel.Text,
                    CustomerSurname = textBoxCustomerSurnameDialogHotel.Text,
                    CustomerPatronymic = textBoxCustomerPatronymicDialogHotel.Text,
                    Age = int.Parse(textBoxAgeDialogHotel.Text),
                    PassportSeries = EncryptNumber(int.Parse(textBoxPassportSeriesDialogHotel.Text), 19146423),
                    PassportNumber = EncryptNumber(int.Parse(textBoxPassportNumderDialogHotel.Text), 19146423),
                    CityId = _cityId,
                    HotelId = _hotelId,
                    TypeRoom = _typeRoom,
                    DataEndTraveling = (long)(endTraveling - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds,
                    DataStartTraveling = (long)(startTraveling - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds,
                    Price = _price,
                    UserId = new Guid(Manager.userId)
                };
                Manager.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Manager.token);
                var response = await Manager.client.PostAsJsonAsync($"api/ticket/user/{Manager.userId}", ticket);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                    MessageBox.Show("Путевка забронирована");
                else
                    MessageBox.Show(response.StatusCode.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
                MessageBox.Show("Введите корректные данные и выберите тип номера", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        private bool checkAge(int age)
        {
            if (age >= 18 && age <= 99) 
                return true; 
            else 
                return false;
        }
        private bool checkPassportSeries(int series)
        {
            if (series >= 1000 && series <= 9999)
                return true;
            else
                return false;
        }
        private bool checkPassportNumber(int number)
        {
            if (number >= 100000 && number <= 999999)
                return true;
            else
                return false;
        }
        private void textBoxIntDialogHotel_TextChanged(object sender, TextChangedEventArgs e)
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

        private void datePickerEndTraveling_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datePickerStartTraveling.SelectedDate != null && _priceTypeRoom != 0)
            {
                var startTraveling = datePickerStartTraveling.SelectedDate.Value;
                var endTraveling = datePickerEndTraveling.SelectedDate.Value;
                double dayCount = (endTraveling - startTraveling).TotalDays;
                _price = (int)(_priceHotel + (dayCount * _priceTypeRoom));
                textBlockPriceHotelDialog.Text = $"Цена: {_price}";
            }
            else
                MessageBox.Show("Выберите тип номера, начальную и конечную дату", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        
    }
}
