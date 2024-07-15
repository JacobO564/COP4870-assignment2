using assi2.ViewModels;

namespace assi2.Views;

public partial class InventoryView : ContentPage
{
	public InventoryView()
	{
		InitializeComponent();
		BindingContext = new InventoryViewModel();
	}

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }


    private void AddClicked(object sender, EventArgs e)
    {
        var x = (BindingContext as InventoryViewModel)?.SelectedItem;
        Shell.Current.GoToAsync($"//Product?itemId={(BindingContext as InventoryViewModel)?.SelectedItem?.Model?.id ?? 0}");
    }

    private void UnselectClicked(object sender, EventArgs e)
    {
        (BindingContext as InventoryViewModel).SelectedItem = null;
    }

    private void TaxClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Tax");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as InventoryViewModel)?.Refresh();
    }


}
