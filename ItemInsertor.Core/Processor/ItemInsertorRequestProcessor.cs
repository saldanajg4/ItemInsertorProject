using System;
using ItemInsertor.Core.Domain;

namespace ItemInsertor.Core.Processor
{
    public class ItemInsertorRequestProcessor
    {
        public ItemInsertorRequestProcessor()
        {
        }

        public InsertItemResult InsertItem(ItemInsertRequest request)
        {
            if(request == null)
                throw new ArgumentNullException(nameof(request));
            return new InsertItemResult{
                Sku = request.Sku,
                Name = request.Name,
                Price = request.Price,
                Quantity = request.Quantity
            };
        }
    }
}