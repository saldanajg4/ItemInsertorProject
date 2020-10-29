using System;
using ItemInsertor.Core.Domain;
using ItemInsertor.Core.Processor;
using Xunit;

namespace ItemInsertor.Core.Tests
{

    public class ItemInsertorRequestProcessorTests
    {
        private ItemInsertorRequestProcessor _processor;
        public ItemInsertorRequestProcessorTests()
        {
            _processor = new ItemInsertorRequestProcessor();
        }
        //first test is the ItemInsertorRequestProcessor()
        [Fact]
        public void ShouldReturnItemInsertingResultWithRequestValues()
        {
            //arrange
            var request = new ItemInsertRequest
            {
                Sku = "Hua001",
                Name = "Huarache",
                Quantity = 2,
                Price = 10.00
            };

            //act
            InsertItemResult result = _processor.InsertItem(request);

            //assert
            Assert.NotNull(result);
            Assert.Equal(request.Sku, result.Sku);
            Assert.Equal(request.Name, result.Name);
            Assert.Equal(request.Quantity, result.Quantity);
            Assert.Equal(request.Price, result.Price);

        }
        [Fact]
        public void ShouldReturnExceptionIfRequestIsNull()
        {
            //arrange

            //act
            var exception = Assert.Throws<ArgumentNullException>(() => _processor.InsertItem(null));

            //assert should returned the name of the param in the request
            Assert.Equal("request", exception.ParamName);
        }
    }


}