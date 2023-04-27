using XenScheduler.ViewModels;

namespace XenScheduler.Views;

public partial class CalendarPage : ContentPage
{
	public CalendarPage()
	{
		InitializeComponent();
		BindingContext = new CalendarViewModel();
	}
}