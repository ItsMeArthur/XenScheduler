using System.Windows.Input;
using XCalendar.Core.Enums;
using XCalendar.Core.Extensions;
using XCalendar.Core.Models;

namespace XenScheduler.ViewModels
{
    public class CalendarViewModel: BaseViewModel
    {
        
        #region Properties
        public Calendar<CalendarDay> Calendar { get; set; } = new Calendar<CalendarDay>()
        {
            NavigatedDate = DateTime.Today,
            NavigationLowerBound = DateTime.Today.AddMonths(0),
            NavigationUpperBound = DateTime.Today.AddMonths(2),
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

        public string NavigationTimeUnit { get; set; } = "Month";
        public int ForwardsNavigationAmount { get; set; } = 1;
        public int BackwardsNavigationAmount { get; set; } = -1;
        #endregion

        #region Commands
        public ICommand NavigateCalendarCommand { get; set; }
        public ICommand ChangeDateSelectionCommand { get; set; }
        #endregion

        #region Constructors
        public CalendarViewModel()
        {
            NavigateCalendarCommand = new Command<int>(NavigateCalendar);
            ChangeDateSelectionCommand = new Command<DateTime>(ChangeDateSelection);
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
            Shell.Current.DisplayAlert("Date", dateTime.ToString(), "OK");
        }
        #endregion
    }
}
