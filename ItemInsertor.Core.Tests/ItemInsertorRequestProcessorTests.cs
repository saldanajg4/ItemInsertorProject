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
        private Item _item;
        private readonly Mock<IItemRepository> _itemRepository;
        private readonly Mock<IItemInsertRepository> itemInsertRepositoryMock;
        private ItemInsertorRequestProcessor _processor;
        public ItemInsertorRequestProcessorTests()
        {
            request = new ItemInsertRequest
            {
                Sku = "hua001",
                Name = "huarache",
                Quantity = 2,
                Price = 10.00
            };
           _item = null;
            _itemRepository = new Mock<IItemRepository>();
            itemInsertRepositoryMock = new Mock<IItemInsertRepository>();
            //how to test if item does not exist
            // _itemRepository.Setup(m => m.GetItem(_item.Name))
            //     .Returns(_item);
            _processor = new ItemInsertorRequestProcessor(itemInsertRepositoryMock.Object,
                _itemRepository.Object);
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
        //Mocking the IItemRepository when item exists then do not save
        [Fact]
        public void ShouldNotInsertIfItemFoundByName(){
            //arrange
             _item = new Item{
                Sku = "hua0asdfsdfsdfg01",
                Name = "huarache",
                Quantity = 2,
                Price = 10.00
            };
            //when the GetItem(name) return item, then save method is not called and our test 
            //passes
             _itemRepository.Setup(m => m.GetItem(_item.Name))
                .Returns(_item);//this returns Name = "huarache" and the GetItem()
                    //in processor returns Item with request Name = "huarache" so finds it
            //act
            _processor.InsertItem(request);
            //assert
            itemInsertRepositoryMock.Verify(m => m.Save(It.IsAny<ItemInsert>()), Times.Never);
        }
    }


}