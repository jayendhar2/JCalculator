namespace Calculator;

public partial class SettingPage : ContentPage
{
    public SettingPage()
    {
        InitializeComponent();
        collectionView.SelectionChanged += OnSelectionChanged;
    }

    void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var item = e.CurrentSelection.FirstOrDefault() as FlyoutPageItem;
        if (item != null)
        {
            this.Resources["BgColor"] = item.itemColor;
            this.Resources["lblColor"] = item.lblColor;
            this.Resources["btnColor"] = item.lblColor;
            this.Resources["navColor"] = item.navColor;
            App.pageBgColor = item.itemColor;
            App.lblColor = item.lblColor;
            App.navColor = item.navColor;
        }
    }
}