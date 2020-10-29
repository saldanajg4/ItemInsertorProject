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
            if (bookRequest == null)
                throw new ArgumentNullException(nameof(bookRequest));

            deskBookingRepository.Save(CreateDeskBookDomain<DeskBooking>(bookRequest));
            return CreateDeskBookDomain<BookDeskResult>(bookRequest);
        }

        //generic method to accept bookDeskRequest types
        private static T CreateDeskBookDomain<T>(BookDeskRequest bookRequest) where T : DeskBookingBase, new()
        {
            return new T
            {
                FistName = bookRequest.FistName,
                LastName = bookRequest.LastName,
                Email = bookRequest.Email,
                BookingDate = bookRequest.BookingDate
            };
        }
    }
}