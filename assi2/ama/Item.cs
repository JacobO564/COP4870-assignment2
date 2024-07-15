using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    public class Item
    {
        public string? name
        {
            get;
            set; 
        }

        public string description { get;  set;}
        public double price { get;  set; }
        public int id { get;  set;  }
        public int stock
        {
            get;
            set;
        }

        public bool bogo { get; set; }

        public double markdown { get; set; }

        public string DisplayPrice
        {
            get
            {
                return $"{price:C}";
            }
        }


        public Item()
        {
            price = 0.00;
            id = 0;
            stock = 0;
            name = "no name";
            description = "no description";
            bogo = false;
            markdown = 0.0;
        }

        public Item(string name ,string description, double price)
        {
            this.name = name;
            this.price = price;
            this.description = description;
            id = 0;
            stock = 0;
            bogo = false;
            markdown = 0.0;
        }

        public Item(Item item)
        {
            name = item.name;
            price = item.price;
            description = item.description;
            id = item.id;
            stock = item.stock;
            bogo = item.bogo;
            markdown = item.markdown;
        }

        public void IncreaseStock(int increaseAmount)
        {
            stock += increaseAmount;
        }

        public double GetCost()
        {
            int countedItems = stock;
            if (bogo)
            {
                countedItems = (int)stock / 2;
                if (stock % 2 == 1)
                {
                    countedItems += 1;
                }
            }

            return price * countedItems * (1 - markdown);
        }

        public override string ToString()
        {
            return $"[{id}] {name} ${price:F2} Amount in stock: {stock}\n\t- {description}";
        }

        public Item Copy()
        {
            return new Item(this);
        }
    }

}
