using ItemInsertor.Core.Domain;

namespace ItemInsertor.Core.DataInterface
{
    public interface IItemInsertRepository
    {
         void Save(ItemInsert item);
    }
}