using Api.ToDoApplication.Persistence;
using Assignment1;


namespace shopAPI.EC
{
    public class InventoryEC
    {
        public InventoryEC() { }
        public async Task<IEnumerable<Item>> Get()
        {
            // probably breaks if trying to get empty inventory
            // return FakeDatabase.data.Item;
            return Filebase.Current.Items;
        }

        public async Task<bool> Delete(int id)
        {
            //return FakeDatabase.data.Delete(id);
            return Filebase.Current.Delete($"{id}");
        }

        public async Task<Item> AddOrUpdate(Item p)
        {
            //return FakeDatabase.data.AddOrUpdate(p);
            return Filebase.Current.AddOrUpdate(p);
        }
    }
}
