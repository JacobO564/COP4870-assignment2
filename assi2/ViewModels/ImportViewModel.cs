using Assignment1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assi2.ViewModels
{
    public class ImportViewModel
    {
        public string FilePath { get; set; }

        public async void ImportFile(Stream? stream = null)
        {
            StreamReader? sr = null;
            try
            {
                if (stream == null) { sr = new StreamReader(FilePath); }
                else
                {
                    sr = new StreamReader(stream);
                }

            }
            catch (Exception ex)
            {

            }
            string line = string.Empty;
            var products = new List<Item>();
            while ((line = sr.ReadLine()) != null)
            {
                var tokens = line.Split(['|']);

                products.Add(new Item
                {
                    name = tokens[0],
                    price = double.Parse(tokens[1]),
                    stock = int.Parse(tokens[2])
                });
            }

            foreach (var product in products)
            {
                await Shop.Current.AddOrUpdateInventory(product);
            }
        }
    }
}
