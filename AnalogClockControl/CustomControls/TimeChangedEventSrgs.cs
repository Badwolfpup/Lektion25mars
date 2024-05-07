using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AnalogClockControl.CustomControls
{
    public class TimeChangedEventSrgs : RoutedEventArgs
    {
        public DateTime NewTime { get; set; }

        public TimeChangedEventSrgs()
        {
        }

        public TimeChangedEventSrgs(RoutedEvent routedEvent) : base(routedEvent)
        {
        }

        public TimeChangedEventSrgs(RoutedEvent routedEvent, object source) : base(routedEvent, source)
        {
        }
    }
}
