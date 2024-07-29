using assi2.ViewModels;

namespace assi2.Views;

public partial class Import : ContentPage
{
	public Import()
	{

		InitializeComponent();
        BindingContext = new ImportViewModel();
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Inventory");
    }
    
    private async void FileSelectClicked(object sender, EventArgs e)
    {
        // allow only csv
        var customFileType = new FilePickerFileType(
            new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                { DevicePlatform.WinUI, new[] { ".csv", ".txt" } },
            });

        var FileSelected = await FilePicker.Default.PickAsync(new PickOptions
        {
            PickerTitle="Pick file to import",
            FileTypes =customFileType
        });

        if (FileSelected == null)
        {
            return;
        }

        var stream = await FileSelected.OpenReadAsync();

        (BindingContext as ImportViewModel).ImportFile(stream);
    }
}