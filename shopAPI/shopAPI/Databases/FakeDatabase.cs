using Assignment1;

namespace shopAPI.Databases
{
    public static class FakeDatabase
    {
        public static Inventory data = new Inventory();
        
        public static void PopulateDatabase()
        {
            Item item1 = new Item("Item1", "item1 desc", 43.01)
            {
                stock = 10

            };
            Item item2 = new Item("Item2", "item2 desc", 43.01)
            {
                stock = 11

            };
            Item item3 = new Item("Item3", "item3 desc", 43.01)
            {
                stock = 12

            };
            data.Add(item1);
            data.Add(item2);
            data.Add(item3);
        }
    }
}
