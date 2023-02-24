namespace Calculator;

public partial class App : Application
{
	public static Color pageBgColor = null;
	public static Color lblColor = null;
	public static Color navColor = null;

    public App()
	{
		InitializeComponent();
		//MainPage = new MainPage();
		MainPage = new AppFlyout();
	}
}
