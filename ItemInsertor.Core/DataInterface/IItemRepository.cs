

using ItemInsertor.Core.Domain;

namespace ItemInsertor.Core.DataInterface
{
    public interface IItemRepository
    {
         Item GetItem(string name);
    }
}