using System;
using DeskBooker.Core.Domain;
using DeskBooker.Core.Processor;
using Xunit;
namespace DeskBooker.Core.Tests
{
    public class DeskBookingRequestProcessorTests
    {
        private DeskBookingRequestProcessor processor;
        public DeskBookingRequestProcessorTests()
        {
            processor = new DeskBookingRequestProcessor();
        }
        [Fact]
        public void ShouldReturnBookDesKResult(){
            //arrange
            var request = new BookDeskRequest{
                FistName = "Jose",
                LastName = "Saldana",
                Email = "saldanajg4@hotmail.com",
                BookingDate = new DateTime(2020,10,29)
            };
            
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
    }

    
}