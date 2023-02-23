namespace Calculator;


public partial class AppFlyout : FlyoutPage
{
	public AppFlyout()
	{
		InitializeComponent();

        flyoutPage.collectionView.SelectionChanged += OnSelectionChanged;
    }

    void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var item = e.CurrentSelection.FirstOrDefault() as FlyoutPageItem;
        if (item != null)
        {
            Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
            IsPresented = false;
            if (App.pageBgColor != null)
            {
                this.Resources["BgColor"] = App.pageBgColor;
                this.Resources["lblColor"] = App.lblColor;
                this.Resources["btnColor"] = App.lblColor;
                this.Resources["navColor"] = App.navColor;
            }
        }
    }
}