using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XenScheduler.Models
{
    public class Event : BaseObservableModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Today;
        public Color Color { get; set; }
    }
}
