using assi2.ViewModels;

namespace assi2.Views;

public partial class CartView : ContentPage
{
	public CartView()
	{
		InitializeComponent();
		BindingContext = new CartViewModel();
	}

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as CartViewModel)?.Refresh();
    }
    private void BuyCartClicked(object sender, EventArgs e)
    {
        (BindingContext as CartViewModel)?.BuyCart();
        Shell.Current.GoToAsync("//Shop");
    }
    private void DeleteClicked(object sender, EventArgs e)
    {
        (BindingContext as CartViewModel)?.DeleteSelected();
    }
    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Shop");
    }
}