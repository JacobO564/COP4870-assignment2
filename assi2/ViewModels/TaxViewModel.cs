using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment1;

namespace assi2.ViewModels
{
    public class TaxViewModel
    {
        public double tax
        {
            get
            {
                return Shop.Current.taxRate;
            }
            set
            {
                Shop.Current.taxRate = value;
            }
        }

    }
}
