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

namespace WorldResort.UserPages
{
    /// <summary>
    /// Логика взаимодействия для ProfileUserPage.xaml
    /// </summary>
    public partial class ProfileUserPage : Page
    {
        public ProfileUserPage()
        {
            InitializeComponent();
            getUserInfo();
            getTickets();
        }

        private async void getUserInfo()
        {
            Manager.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Manager.token);
            var response = await Manager.client.GetFromJsonAsync<User>($"api/profile/{Manager.userId}");
            if (response != null)
            {
                textBlockNameProfile.Text = "Имя: " + response.Name;
                textBlockSurnameProfile.Text = "Фамилия: " + response.Surname;
                textBlockEmailProfile.Text = "Электронная почта: " + response.Email;
                textBlockPhoneNumberProfile.Text = "Номер телефона: " + response.PhoneNumber;
            }
        }
        private async void getTickets()
        {
            Manager.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Manager.token);
            var response = await Manager.client.GetFromJsonAsync<List<Ticket>>($"api/ticket/user/{Manager.userId}");
            var ticketList = new List<TicketForListView>();
            foreach (var ticket in response)
            {
                ticketList.Add
                (
                    new TicketForListView
                    {
                        Id = ticket.Id,
                        CustomerName = ticket.CustomerName,
                        CustomerSurname = ticket.CustomerSurname,
                        CustomerPatronymic = ticket.CustomerPatronymic,
                        Age = ticket.Age,
                        PassportNumber = ticket.PassportNumber,
                        PassportSeries = ticket.PassportSeries,
                        CityId = ticket.CityId,
                        HotelId = ticket.HotelId,
                        TypeRoom = ticket.TypeRoom,
                        DataEndTraveling = DateTimeOffset.FromUnixTimeSeconds(ticket.DataEndTraveling).DateTime.ToString("dd/MM/yyyy"),
                        DataStartTraveling = DateTimeOffset.FromUnixTimeSeconds(ticket.DataStartTraveling).DateTime.ToString("dd/MM/yyyy"),
                        Price = ticket.Price,
                        UserId = ticket.UserId
                    }
                );
            }
            listViewBuyTicket.ItemsSource = ticketList;
        }
    }
}
