using assi2.ViewModels;

namespace assi2.Views;
[QueryProperty(nameof(ItemId), "itemId")]
public partial class ItemView : ContentPage
{
    public int ItemId {  get; set; }
	public ItemView()
	{
		InitializeComponent();
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Inventory");
        
    }

    private void AddClicked(object sender, EventArgs e)
    {
        (BindingContext as ItemViewModel).Add();
        Shell.Current.GoToAsync("//Inventory");
    }

    private void DeleteClicked(System.Object sender, System.EventArgs e)
    {
        (BindingContext as ItemViewModel).Delete();
        Shell.Current.GoToAsync("//Inventory");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new ItemViewModel(ItemId);
    }

}