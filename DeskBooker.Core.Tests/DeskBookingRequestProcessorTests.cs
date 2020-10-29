using System;
using DeskBooker.Core.DataInterface;
using DeskBooker.Core.Domain;
using DeskBooker.Core.Processor;
using Moq;
using Xunit;
namespace DeskBooker.Core.Tests
{
    public class DeskBookingRequestProcessorTests
    {
        private readonly BookDeskRequest request;
        private readonly Mock<IDeskBookingRepository> _deskBookingRepositoryMock;
        private DeskBookingRequestProcessor processor;
       
        public DeskBookingRequestProcessorTests()
        {
            request = new BookDeskRequest{
                FistName = "Jose",
                LastName = "Saldana",
                Email = "saldanajg4@hotmail.com",
                BookingDate = new DateTime(2020,10,29)
            };
            _deskBookingRepositoryMock= new Mock<IDeskBookingRepository>();
            processor = new DeskBookingRequestProcessor(_deskBookingRepositoryMock.Object);
        }
        [Fact]
        public void ShouldReturnBookDesKResult(){
            //arrange
            
            
            //act
            BookDeskResult result = processor.BookDesk(request);
            
            //assert
            Assert.NotNull(result);
            Assert.Equal(result.FistName, request.FistName);
            Assert.Equal(result.LastName, request.LastName);
            Assert.Equal(result.Email, request.Email);
            Assert.Equal(result.BookingDate, request.BookingDate);
        }
        [Fact]
        public void ShouldThrowExceptionWhenRequestIsNull(){
            //arrange

            //act
            var results = Assert.Throws<ArgumentNullException>(() => processor.BookDesk(null));

            //assert
            Assert.Equal("bookRequest",results.ParamName);
        }
        [Fact]
        public void ShouldSaveDeskBooking(){
            //arrange setting up the Save() in the mock object repository
            DeskBooking savedDeskBooking = null;
            //mock and setup the Save() in the mock repository object
            //I cannot call Save() but Set it up as mock
            _deskBookingRepositoryMock.Setup(m => m.Save(It.IsAny<DeskBooking>()))
                .Callback<DeskBooking>(deskBooking => {
                    savedDeskBooking = deskBooking;
                });
            //act - verify that Save() is called once in processor.BookDesk
            processor.BookDesk(request);
            _deskBookingRepositoryMock.Verify(m => m.Save(It.IsAny<DeskBooking>()),Times.Once);

            //assert
            Assert.NotNull(savedDeskBooking);
            Assert.Equal(request.FistName, savedDeskBooking.FistName);
            Assert.Equal(request.LastName, savedDeskBooking.LastName);
            Assert.Equal(request.Email, savedDeskBooking.Email);
            Assert.Equal(request.BookingDate, savedDeskBooking.BookingDate);
        }

    }

    
}