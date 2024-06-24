using Assignment1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace assi2.ViewModels
{
    public class ShopViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;
        public List<ItemViewModel> Items
        {
            get
            {
                return Shop.Current.GetInventory()?.Where(Item => Item != null)
                    .Select(p => new ItemViewModel(p)).ToList()
                    ?? new List<ItemViewModel>();
            }
        }

        public ItemViewModel? SelectedItem { get; set; }

        public void Refresh()
        {
            NotifyPropertyChanged(nameof(Items));
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
