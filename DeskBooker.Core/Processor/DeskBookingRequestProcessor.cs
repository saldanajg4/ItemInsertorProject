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

        //From the IEnumerable variable use Count() linq method
        //bookRequest is like my Dto, the DeskBooking object is like my entity
        //to be saved into the database
        //the test fails when availableDesks is not setting the DeskId
        public BookDeskResult BookDesk(BookDeskRequest bookRequest)
        {
            if (bookRequest == null)
                throw new ArgumentNullException(nameof(bookRequest));
            var availableDesks = deskRepository.GetAvailableDesks(bookRequest.BookingDate);
            //creates a relationship between first available desk(Desk) using Id
            // and the DeskBooking using DeskId
            if(availableDesks.FirstOrDefault() is Desk availableDesk){
                // var availableDesk = availableDesks.First();
                var deskBooking = CreateDeskBookDomain<DeskBooking>(bookRequest);
                deskBooking.DeskId = availableDesk.Id;
                deskBookingRepository.Save(deskBooking);
            }
                
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