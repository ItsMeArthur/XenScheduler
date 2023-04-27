using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCalendar.Core.Models;

namespace XenScheduler.Models
{
    public class ConnectableDay : CalendarDay
    {
        public bool ConnectsToTop { get; set; }
        public bool ConnectsToBottom { get; set; }
        public bool ConnectsToLeft { get; set; }
        public bool ConnectsToRight { get; set; }
    }
}
