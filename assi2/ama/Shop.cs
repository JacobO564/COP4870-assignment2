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

            //todo - remove
            Item item_add = new Item("dummy", "dummy desc", 2.00);
            item_add.IncreaseStock(10);
            AddToInventory(item_add);

            Item item_add2 = new Item("dummy2", "dummy desc", 4.00);
            item_add2.IncreaseStock(10);
            AddToInventory(item_add2);

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

        public bool BuyCart()
        {
            if (inventory.CanSubtractInventory(cart)){
                inventory.SubtractInventories(cart);
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

        public bool RemoveItemInventroy(int id)
        {
           return inventory.Delete(id);
        }

        public Item? GetItemInventory(int id)
        {
            return inventory.GetItem(id);
        }

        public Item? UpdateItemInventory(Item item)
        {
            return inventory.Update(item);
        }

        public Item? AddToInventory(Item item)
        {
            return inventory.Add(item);
        }

        public Item? AddOrUpdateInventory(Item item)
        {
            return inventory.AddOrUpdate(item);
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
