using System.Windows.Input;
using XCalendar.Core.Enums;
using XCalendar.Core.Extensions;
using XCalendar.Core.Models;
using XenScheduler.Helpers;

namespace XenScheduler.ViewModels
{
    public class CalendarViewModel: BaseViewModel
    {
        
        #region Properties
        public Calendar<CalendarDay> Calendar { get; set; } = new Calendar<CalendarDay>()
        {
            NavigatedDate = DateTime.Today,
            NavigationLowerBound = DateTime.Today.AddYears(-2),
            NavigationUpperBound = DateTime.Today.AddYears(2),
            StartOfWeek = DayOfWeek.Monday,
            SelectionAction = SelectionAction.Replace,
            NavigationLoopMode = NavigationLoopMode.LoopMinimumAndMaximum,
            SelectionType = SelectionType.Single,
            PageStartMode = PageStartMode.FirstDayOfMonth,
            Rows = 2,
            AutoRows = true,
            AutoRowsIsConsistent = true,
            TodayDate = DateTime.Today
        };

        public List<string> NavigationTimeUnits { get; } = new List<string>()
        {
            "Day",
            "Week",
            "Month",
            "Year"
        };

        public string NavigationTimeUnit { get; set; } = "Month";
        public bool CalendarIsVisible { get; set; } = true;
        public double DaysViewHeightRequest { get; set; } = 300;
        public double DayNamesHeightRequest { get; set; } = 25;
        public double NavigationHeightRequest { get; set; } = 50;
        public double DayHeightRequest { get; set; } = 45;
        public double DayWidthRequest { get; set; } = 45;
        public int ForwardsNavigationAmount { get; set; } = 1;
        public int BackwardsNavigationAmount { get; set; } = -1;
        public Color CalendarBackgroundColor { get; set; } = (Color)Application.Current.Resources["CalendarBackgroundColor"];
        public Color NavigationBackgroundColor { get; set; } = (Color)Application.Current.Resources["CalendarPrimaryColor"];
        public Color NavigationTextColor { get; set; } = (Color)Application.Current.Resources["CalendarPrimaryTextColor"];
        public Color NavigationArrowColor { get; set; } = (Color)Application.Current.Resources["CalendarPrimaryTextColor"];
        public Color NavigationArrowBackgroundColor { get; set; } = (Color)Application.Current.Resources["CalendarPrimaryColor"];
        public Color DayCurrentMonthBackgroundColor { get; set; } = (Color)Application.Current.Resources["CalendarBackgroundColor"];
        public Color DayCurrentMonthTextColor { get; set; } = (Color)Application.Current.Resources["CalendarBackgroundTextColor"];
        public Color DayOtherMonthBackgroundColor { get; set; } = (Color)Application.Current.Resources["CalendarBackgroundColor"];
        public Color DayOtherMonthTextColor { get; set; } = Color.FromArgb("#FFA0A0A0");
        public Color DayTodayBackgroundColor { get; set; } = (Color)Application.Current.Resources["CalendarBackgroundColor"];
        public Color DayTodayTextColor { get; set; } = (Color)Application.Current.Resources["CalendarPrimaryColor"];
        public Color DaySelectedBackgroundColor { get; set; } = (Color)Application.Current.Resources["CalendarPrimaryColor"];
        public Color DaySelectedTextColor { get; set; } = (Color)Application.Current.Resources["CalendarPrimaryTextColor"];
        public Color DayInvalidBackgroundColor { get; set; } = (Color)Application.Current.Resources["CalendarBackgroundColor"];
        public Color DayInvalidTextColor { get; set; } = (Color)Application.Current.Resources["CalendarTertiaryColor"];
        #endregion

        #region Commands
        public ICommand ShowCustomDayNamesOrderDialogCommand { get; set; }
        public ICommand NavigateCalendarCommand { get; set; }
        public ICommand ChangeDateSelectionCommand { get; set; }
        public ICommand ChangeCalendarVisibilityCommand { get; set; }
        #endregion

        #region Constructors
        public CalendarViewModel()
        {
            ShowCustomDayNamesOrderDialogCommand = new Command(ShowCustomDayNamesOrderDialog);
            NavigateCalendarCommand = new Command<int>(NavigateCalendar);
            ChangeDateSelectionCommand = new Command<DateTime>(ChangeDateSelection);
            ChangeCalendarVisibilityCommand = new Command<bool>(ChangeCalendarVisibility);
        }
        #endregion

        #region Methods
        public void NavigateCalendar(int amount)
        {
            TimeSpan timeSpanToNavigateBy;

            switch (NavigationTimeUnit)
            {
                case "Day":
                    if (Calendar.NavigatedDate.TryAddDays(amount, out DateTime addDaysDateTime))
                    {
                        timeSpanToNavigateBy = addDaysDateTime - Calendar.NavigatedDate;
                    }
                    else
                    {
                        timeSpanToNavigateBy = amount > 0 ? TimeSpan.MaxValue : TimeSpan.MinValue;
                    }
                    break;

                case "Week":
                    if (Calendar.NavigatedDate.TryAddWeeks(amount, out DateTime addWeeksDateTime))
                    {
                        timeSpanToNavigateBy = addWeeksDateTime - Calendar.NavigatedDate;
                    }
                    else
                    {
                        timeSpanToNavigateBy = amount > 0 ? TimeSpan.MaxValue : TimeSpan.MinValue;
                    }
                    break;

                case "Month":
                    if (Calendar.NavigatedDate.TryAddMonths(amount, out DateTime addMonthsDateTime))
                    {
                        timeSpanToNavigateBy = addMonthsDateTime - Calendar.NavigatedDate;
                    }
                    else
                    {
                        timeSpanToNavigateBy = amount > 0 ? TimeSpan.MaxValue : TimeSpan.MinValue;
                    }
                    break;

                case "Year":
                    if (Calendar.NavigatedDate.TryAddYears(amount, out DateTime addYearsDateTime))
                    {
                        timeSpanToNavigateBy = addYearsDateTime - Calendar.NavigatedDate;
                    }
                    else
                    {
                        timeSpanToNavigateBy = amount > 0 ? TimeSpan.MaxValue : TimeSpan.MinValue;
                    }
                    break;

                default:
                    throw new NotImplementedException($"{nameof(NavigationTimeUnit)} '{NavigationTimeUnit}' is not implemented.");
            }

            Calendar.Navigate(timeSpanToNavigateBy);
        }
        public void ChangeDateSelection(DateTime dateTime)
        {
            Calendar?.ChangeDateSelection(dateTime);
        }
        public void ChangeCalendarVisibility(bool isVisible)
        {
            CalendarIsVisible = isVisible;
        }
        public async void ShowCustomDayNamesOrderDialog()
        {
            IEnumerable<DayOfWeek> newCustomDayNamesOrder = await PopupHelper.ShowConstructListDialogPopupAsync(Calendar.CustomDayNamesOrder ?? new ObservableRangeCollection<DayOfWeek>(), Calendar.StartOfWeek.GetWeekAsFirst());

            if (newCustomDayNamesOrder.Any())
            {
                if (Calendar.CustomDayNamesOrder == null)
                {
                    Calendar.CustomDayNamesOrder = new ObservableRangeCollection<DayOfWeek>(newCustomDayNamesOrder);
                }
                else
                {
                    Calendar.CustomDayNamesOrder.ReplaceRange(newCustomDayNamesOrder);
                }
            }
            else
            {
                Calendar.CustomDayNamesOrder = null;
            }
        }
        #endregion
    }
}
