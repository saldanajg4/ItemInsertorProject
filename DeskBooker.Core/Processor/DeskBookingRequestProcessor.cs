using System;
using DeskBooker.Core.Domain;

namespace DeskBooker.Core.Processor
{
    public class DeskBookingRequestProcessor
    {
        public DeskBookingRequestProcessor()
        {
             
        }

        public BookDeskResult BookDesk(BookDeskRequest bookRequest)
        {
            if(bookRequest == null)
                throw new ArgumentNullException(nameof(bookRequest));

            return new BookDeskResult{
                FistName = bookRequest.FistName,
                LastName = bookRequest.LastName,
                Email = bookRequest.Email,
                BookingDate = bookRequest.BookingDate
            };
        }
    }
}