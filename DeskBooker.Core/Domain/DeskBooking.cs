using System;

namespace DeskBooker.Core.Domain
{
    public class DeskBooking
    {
          public string FistName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BookingDate { get; set; }
    }
}