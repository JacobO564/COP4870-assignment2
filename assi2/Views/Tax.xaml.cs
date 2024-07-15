using assi2.ViewModels;

namespace assi2.Views;

public partial class Tax : ContentPage
{
	public double taxInput;
	public Tax()
	{
		InitializeComponent();
		BindingContext = new TaxViewModel();
	}

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Inventory");
    }
}