using System;
using DeskBooker.Core.DataInterface;
using DeskBooker.Core.Domain;

namespace DeskBooker.Core.Processor
{
    public class DeskBookingRequestProcessor
    {
        private readonly IDeskBookingRepository deskBookingRepository;

        public DeskBookingRequestProcessor(IDeskBookingRepository deskBookingRepository)
        {
            this.deskBookingRepository = deskBookingRepository;
        }

        public BookDeskResult BookDesk(BookDeskRequest bookRequest)
        {
            if(bookRequest == null)
                throw new ArgumentNullException(nameof(bookRequest));

            deskBookingRepository.Save(new DeskBooking{
                FistName = bookRequest.FistName,
                LastName = bookRequest.LastName,
                Email = bookRequest.Email,
                BookingDate = bookRequest.BookingDate
            });
            return new BookDeskResult{
                FistName = bookRequest.FistName,
                LastName = bookRequest.LastName,
                Email = bookRequest.Email,
                BookingDate = bookRequest.BookingDate
            };
        }
    }
}