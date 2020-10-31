using System;
using System.Collections.Generic;
using System.Linq;
using DeskBooker.Core.DataInterface;
using DeskBooker.Core.Domain;

namespace DeskBooker.Core.Processor
{
    public class DeskBookingRequestProcessor
    {
        private readonly IDeskBookingRepository deskBookingRepository;
        private readonly IDeskRepository deskRepository;

        public DeskBookingRequestProcessor(IDeskBookingRepository deskBookingRepository,
            IDeskRepository deskRepository)
        {
            this.deskRepository = deskRepository;
            this.deskBookingRepository = deskBookingRepository;
        }

        //From the IEnumerable variable use Count() linq methodks
        public BookDeskResult BookDesk(BookDeskRequest bookRequest)
        {
            if (bookRequest == null)
                throw new ArgumentNullException(nameof(bookRequest));
            var availableDesks = deskRepository.GetAvailableDesks(bookRequest.BookingDate);
            if(availableDesks.Count() > 0)
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