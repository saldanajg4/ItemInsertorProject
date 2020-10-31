using System;
using ItemInsertor.Core.DataInterface;
using ItemInsertor.Core.Domain;

namespace ItemInsertor.Core.Processor
{
    public class ItemInsertorRequestProcessor
    {
        private readonly IItemRepository itemRepository;

        private readonly IItemInsertRepository InsertRepository;
        public ItemInsertorRequestProcessor(IItemInsertRepository insertRepository, 
            IItemRepository itemRepository)
        {
            this.InsertRepository = insertRepository;
            this.itemRepository = itemRepository;
        }

        public InsertItemResult InsertItem(ItemInsertRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            var item = this.itemRepository.GetItem(request.Name.ToLower());
            if (item == null){
                InsertRepository.Save(new ItemInsert{
                Sku = request.Sku,
                Name = request.Name,
                Price = request.Price,
                Quantity = request.Quantity
            });
            }
            
            return new InsertItemResult
            {
                Sku = request.Sku,
                Name = request.Name,
                Price = request.Price,
                Quantity = request.Quantity
            };
        }
    }
}