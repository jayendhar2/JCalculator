namespace Calculator;


public partial class FlyoutMenuPage : ContentPage
{
	public FlyoutMenuPage()
	{
		InitializeComponent();
		if (App.pageBgColor!= null)
		{
            this.BackgroundColor = App.pageBgColor;
        }
    }
}