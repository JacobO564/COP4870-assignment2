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
    public class InventoryViewModel : INotifyPropertyChanged
    {

        public List<ItemViewModel> Items
        {   
            get
            {
                return Shop.Current.GetInventory()?.Where(Item => Item != null)
                    .Select(p => new ItemViewModel(p)).ToList() 
                    ?? new List<ItemViewModel>();
            }
        }

        public void Refresh()
        {
            NotifyPropertyChanged(nameof(Items));
        }

        public ItemViewModel? SelectedItem {  get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
