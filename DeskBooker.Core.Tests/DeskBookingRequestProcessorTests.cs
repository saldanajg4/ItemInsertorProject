using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly List<Desk> _availableDesks;
        private readonly Mock<IDeskBookingRepository> _deskBookingRepositoryMock;
        private readonly Mock<IDeskRepository> _DesksRepositoryMock;
        private DeskBookingRequestProcessor processor;
       
        public DeskBookingRequestProcessorTests()
        {
            request = new BookDeskRequest{
                FistName = "Jose",
                LastName = "Saldana",
                Email = "saldanajg4@hotmail.com",
                BookingDate = new DateTime(2020,10,29),
            };
            //pretend there are some available desks in the database, so this is the one that 
            //needs the DeskId
            _availableDesks = new List<Desk>{
                new Desk{ Id = 7}
            };
            _deskBookingRepositoryMock= new Mock<IDeskBookingRepository>();
            _DesksRepositoryMock = new Mock<IDeskRepository>();
            _DesksRepositoryMock.Setup(m => m.GetAvailableDesks(request.BookingDate))
                .Returns(_availableDesks);
            processor = new DeskBookingRequestProcessor(_deskBookingRepositoryMock.Object,
                _DesksRepositoryMock.Object);
        }
        [Fact]
        public void ShouldReturnBookDesKResult(){
            //arrange
            
            
            //actks
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
            //DeskBooking does not contain DeskId yet, that will be for Desk entity
            //Verigying the Save() takes a DeskBooking parameter to be saved and called once
            _deskBookingRepositoryMock.Verify(m => m.Save(It.IsAny<DeskBooking>()),Times.Once);

            //assert
            Assert.NotNull(savedDeskBooking);
            Assert.Equal(request.FistName, savedDeskBooking.FistName);
            Assert.Equal(request.LastName, savedDeskBooking.LastName);
            Assert.Equal(request.Email, savedDeskBooking.Email);
            Assert.Equal(request.BookingDate, savedDeskBooking.BookingDate);
            //now add the deskId to DeskBooking object to be saved
            //fails because not set in the savedDeskBooking object
            Assert.Equal(_availableDesks.First().Id, savedDeskBooking.DeskId);
        }

        //call BookDesk(request) from the processor, then verify that Save() was never called
        [Fact]
        public void ShouldNotCallSaveMethodWhenNotAvailableDesks(){
            _availableDesks.Clear();//so when verifying Save() is not executed
            processor.BookDesk(request);
            _deskBookingRepositoryMock.Verify(m => m.Save(It.IsAny<DeskBooking>()),Times.Never);

        }

        [Theory]
        [InlineData(DeskBookingResultCode.Success,true)]
        [InlineData(DeskBookingResultCode.NoDeskAvailable,false)]
        public void ShouldReturnExpectedResultCode(DeskBookingResultCode expectedResultCode,
            bool isDeskAvailable){
                if(!isDeskAvailable){
                    _availableDesks.Clear();
                }
        }

    }

    
}