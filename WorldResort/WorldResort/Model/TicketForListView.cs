using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldResort.Model
{
    public class TicketForListView
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerSurname { get; set; }
        public string CustomerPatronymic { get; set; }
        public int Age { get; set; }
        public int PassportNumber { get; set; }
        public int PassportSeries { get; set; }
        public string CityId { get; set; }
        public string HotelId { get; set; }
        public string TypeRoom { get; set; }
        public string DataStartTraveling { get; set; }
        public string DataEndTraveling { get; set; }
        public int Price { get; set; }
        public Guid UserId { get; set; }
    }
}
