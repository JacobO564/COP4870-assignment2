using assi2.ViewModels;

namespace assi2.Views;

[QueryProperty(nameof(ItemId), "itemId")]
public partial class BuyItemView : ContentPage
{
    public int ItemId { get; set; }
    public BuyItemView()
	{
		InitializeComponent();
	}
    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Shop");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new ItemViewModel(ItemId);
    }
}