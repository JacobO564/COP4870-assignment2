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

        public List<ItemViewModel> CartItems
        {
            get
            {
                return Shop.Current.GetCart()?.Where(Item => Item != null)
                                    .Select(p => new ItemViewModel(p)).ToList()
                                    ?? new List<ItemViewModel>();
            }
        }

        public ItemViewModel? SelectedItem { get; set; }

        public void Refresh()
        {
            NotifyPropertyChanged(nameof(Items));
            NotifyPropertyChanged(nameof(CartItems));
        }

        public void CycleCartRight()
        {
            int selected = (Shop.Current.currentCart + 1) % Shop.Current.cartsList.Count;
            Shop.Current.SwitchActiveCart(selected);
            NotifyPropertyChanged(nameof(CartItems));
        }

        public void CycleCartLeft()
        {
            int selected = (Shop.Current.currentCart - 1) % Shop.Current.cartsList.Count;
            Shop.Current.SwitchActiveCart(selected);
            NotifyPropertyChanged(nameof(CartItems));
        }

        public void AddCart()
        {
            Shop.Current.AddCart();
            NotifyPropertyChanged(nameof(CartItems));
        }

        public void RemoveCurrentCart()
        {
            Shop.Current.RemoveActiveCart();
            NotifyPropertyChanged(nameof(CartItems));
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
