using System;
using ItemInsertor.Core.DataInterface;
using ItemInsertor.Core.Domain;

namespace ItemInsertor.Core.Processor
{
    public class ItemInsertorRequestProcessor
    {
        public IItemInsertRepository InsertRepository { get; }
        public ItemInsertorRequestProcessor(IItemInsertRepository insertRepository)
        {
            this.InsertRepository = insertRepository;
        }

        public InsertItemResult InsertItem(ItemInsertRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            InsertRepository.Save(new ItemInsert{
                Sku = request.Sku,
                Name = request.Name,
                Price = request.Price,
                Quantity = request.Quantity
            });
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