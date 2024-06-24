
using Assignment1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace assi2.ViewModels
{
    public class ItemViewModel
    {
        public override string ToString()
        {
            if(Model == null)
            {
                return string.Empty;
            }
            return $"{Model.id} - {Model.name} - {Model.description} - {Model.price:C}";
        }
        public Item? Model { get; set; }

        public string DisplayPrice
        {
            get
            {
                if (Model == null) { return string.Empty; }
                return $"{Model.price:C}";
            }
        }

        public int AmountInCart
        {
            get
            {
                if (Model == null) { return 0; }
                return Shop.Current.GetItemFromCart(Model.id)?.stock ?? 0;
            }
            set
            {
                if (Model != null) 
                { 
                    Shop.Current.AddToCart(Model.id, value);
                }
            }
        }

        public string CartPrice
        {
            get
            {
                return $"{Shop.Current.CartCost():C}";
            }
        }

        public string PriceAsString
        {
            set
            {
                if (Model == null)
                {
                    return;
                }
                if (decimal.TryParse(value, out var price))
                {
                    Model.price = ((double)price);
                }
                else
                {

                }
            }
        }

        public ItemViewModel()
        {
            Model = new Item();
        }

        public ItemViewModel(Item? model)
        {
            if (model != null)
            {
                Model = model;
            }
            else
            {
                Model = new Item();
            }
        }

        public ItemViewModel(int id)
        {
            if (id == 0)
            {
                Model = new Item();
                return;
            }
            Model = Shop.Current?.GetInventory()?.FirstOrDefault(i => i.id == id);
            if (Model == null)
            {
                Model = new Item();
            }

        }

        public void Add()
        {
            if (Model != null)
            {
                Shop.Current.AddOrUpdateInventory(Model);
            }
        }

        public void Delete()
        {
            if (Model != null)
            {
                Shop.Current.RemoveItemInventroy(Model.id);
            }
        }

        public void RemoveFromCart()
        {
            if (Model != null)
            {
                Shop.Current.RemoveFromCart(Model.id);
            }
        }
         

    }
}
