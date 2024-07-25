using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Assignment1
{
    public class Shop
    {
        public int currentCart { get; private set; }
        public List<Inventory> cartsList {  get; private set; }
        private Inventory inventory;
        private Inventory cart
        {
            get
            {
                return cartsList[currentCart];
            }
            set
            {
                cartsList[currentCart] = value;
            }
        }
        public double taxRate = 0.07;

        private static Shop? instance;
        private static object instanceLock = new object();
        
        public Shop()
        {
            inventory = new Inventory();
            currentCart = 0;
            cartsList = new List<Inventory>();
            cartsList.Add(new Inventory());


            SyncInventory();

        }
        public int AddCart()
        {
            cartsList.Add(new Inventory());
            return cartsList.Count;
        }

        public void SwitchActiveCart(int cartNum)
        {
            if (cartNum >= 0 && cartNum < cartsList.Count) 
            {
                currentCart = cartNum;
            }
        }

        public void RemoveActiveCart()
        {
            if (currentCart > 0)
            {
                cartsList.RemoveAt(currentCart);
                currentCart -= 1;
            }
            else if (cartsList.Count > 1)
            {
                cartsList.RemoveAt(currentCart);
            }

        }

        public bool AddToCart(int id, int amount)
        {
            //add or update
            // Note removing items from inventory doesnt happen untill after sale


            Item? item = inventory.GetItem(id);

            if (item == null)
            {
                return false;
            }

            Item? incart = cart.GetItem(id);

            if (incart != null) 
            {
                // item is already in cart
                if (0 > amount || amount  > item.stock)
                {
                    // either amount is less then 0 or getting then inventory stock
                    return false;
                }
                incart.stock = amount;
                return true;
            }

            if (amount > item.stock)
            {
                return false;
            }

            Item cartItem = new Item(item);
            cartItem.stock = amount;

            
            cart.Add(cartItem);
            return true;
        }

        public double CartCost()
        {
            return cart.TotalCost() * (1 + taxRate);
        }

        public async Task<bool> BuyCart()
        {
            SyncInventory();
            if (inventory.CanSubtractInventory(cart)){
                // inventory.SubtractInventories(cart);
                
                foreach (Item item in cart.Item)
                {
                    var NewItem = inventory.GetItem(item.id);
                    NewItem.IncreaseStock(-item.stock);
                    await AddOrUpdate(NewItem);
                    inventory.AddOrUpdate(NewItem);
                }
                SyncInventory();
                ClearCart();
                return true;
            }
            return false;
            
        }

        public Item? GetItemFromCart(int id)
        {
            return cart.GetItem(id);
        }

        public bool RemoveFromCart(int id)
        {
            return cart.Delete(id);
        }

        public void ClearCart()
        {
            cart = new Inventory();
        }

        public void ClearInventory() {
            inventory = new Inventory(); 
        }

        public ReadOnlyCollection<Item>? GetInventory()
        {
            return inventory.Item;
        }

        public ReadOnlyCollection<Item>? GetCart()
        {
            return cart.Item;
        }


        // todo add api interface
        public async Task<bool> RemoveItemInventroy(int id)
        {
            var x = await Delete(id);
            inventory.Delete(id);
            SyncInventory();
            return x ?? false;
        }


        public Item? GetItemInventory(int id)
        {
            return inventory.GetItem(id);
        }



  
        public async Task<Item?> AddOrUpdateInventory(Item item)
        {
            var x = await AddOrUpdate(item);
            inventory.AddOrUpdate(x);
            SyncInventory();
            return x;
            //return inventory.AddOrUpdate(item);
        }

        public async Task<IEnumerable<Item>> Get()
        {
            var result = await new assi2.ama.WebRequestHandler().Get("/Inventory");
            if (result is null){
                return new List<Item>();
            }
            var deserializedResult = JsonConvert.DeserializeObject<List<Item>>(result);
            return deserializedResult?.ToList() ?? new List<Item>();
        }

        public async Task<Item?> AddOrUpdate(Item item)
        {
            var result = await new assi2.ama.WebRequestHandler().Post("/Inventory", item);
            return JsonConvert.DeserializeObject<Item>(result);
        }

        public async Task<bool?> Delete(int id)
        {
            var response = await new assi2.ama.WebRequestHandler().Delete($"/{id}");
            var itemToDelete = JsonConvert.DeserializeObject<bool>(response);
            return itemToDelete;
        }

        public async void SyncInventory()
        {
            var ItemsFromWeb = await Get();
            ClearInventory();
            foreach (var item in ItemsFromWeb)
            {
                inventory.AddOrUpdate(item);
            }
        }

        public string InventoryString()
        {
            return inventory.ToString();
        }
        public string CartString()
        {
            return $"{cart.ToString()}\nTotal Cost: ${(cart?.TotalCost() * (1 + taxRate)):F2}";
        }

        public static Shop Current
        {
            get
            {
                lock(instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new Shop();
                    }
                }
                return instance;
            }
        }
    }
}
