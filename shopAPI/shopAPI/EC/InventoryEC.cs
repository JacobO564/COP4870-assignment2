using Assignment1;
using shopAPI.Databases;

namespace shopAPI.EC
{
    public class InventoryEC
    {
        public InventoryEC() { }
        public async Task<IEnumerable<Item>> Get()
        {
            // probably breaks if trying to get empty inventory
            return FakeDatabase.data.Item;
        }

        public async Task<bool> Delete(int id)
        {
            return FakeDatabase.data.Delete(id);
        }

        public async Task<Item> AddOrUpdate(Item p)
        {
            return FakeDatabase.data.AddOrUpdate(p);
        }
    }
}
