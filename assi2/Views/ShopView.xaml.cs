using assi2.ViewModels;

namespace assi2.Views;

public partial class ShopView : ContentPage
{
	public ShopView()
	{
		InitializeComponent();
		BindingContext = new ShopViewModel();
	}

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as ShopViewModel).Refresh();
    }

    private void BuySelectedClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync($"//BuyItem?itemId={(BindingContext as ShopViewModel)?.SelectedItem?.Model?.id ?? 0}");
    }

    private void CheckOutClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Cart");
    }

}