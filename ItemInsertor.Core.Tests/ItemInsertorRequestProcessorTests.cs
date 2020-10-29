using System;
using ItemInsertor.Core.DataInterface;
using ItemInsertor.Core.Domain;
using ItemInsertor.Core.Processor;
using Moq;
using Xunit;

namespace ItemInsertor.Core.Tests
{

    public class ItemInsertorRequestProcessorTests
    {
        private readonly ItemInsertRequest request;
        private readonly Mock<IItemInsertRepository> itemInsertRepositoryMock;
        private ItemInsertorRequestProcessor _processor;
        public ItemInsertorRequestProcessorTests()
        {
            request = new ItemInsertRequest
            {
                Sku = "Hua001",
                Name = "Huarache",
                Quantity = 2,
                Price = 10.00
            };
            itemInsertRepositoryMock = new Mock<IItemInsertRepository>();
            _processor = new ItemInsertorRequestProcessor(itemInsertRepositoryMock.Object);
        }
        //first test is the ItemInsertorRequestProcessor()
        [Fact]
        public void ShouldReturnItemInsertingResultWithRequestValues()
        {
            //arrange
           

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
        //making sure Save(ItemInsert) is called once and with the correct 
        //parameter
        [Fact]
        public void ShouldSaveItem(){
            //arrange
            ItemInsert itemInserted = null;
            itemInsertRepositoryMock.Setup(m => m.Save(It.IsAny<ItemInsert>()))
                .Callback<ItemInsert>(itemInsertedBack => {
                    itemInserted = itemInsertedBack;
                });
            //act
            _processor.InsertItem(request);

            //assert
            itemInsertRepositoryMock.Verify(m => m.Save(It.IsAny<ItemInsert>()), Times.Once);
            Assert.Equal(request.Sku, itemInserted.Sku);
            Assert.Equal(request.Name, itemInserted.Name);
            Assert.Equal(request.Price, itemInserted.Price);
            Assert.Equal(request.Quantity, itemInserted.Quantity);
        }
    }


}