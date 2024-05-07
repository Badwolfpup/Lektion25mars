using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AnalogClockControl.CustomControls
{
    public delegate void TimeChangedEventHandler(object sender, TimeChangedEventSrgs args);

    public class AnalogClock : Clock
    {
        private Line hourhand;
        private Line minutehand;
        private Line secondhand;



  
        static AnalogClock()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AnalogClock), new FrameworkPropertyMetadata(typeof(AnalogClock)));
        }

        public override void OnApplyTemplate()
        {

            hourhand = Template.FindName("PART_hourhand", this) as Line;
            minutehand = Template.FindName("PART_minutehand", this) as Line;
            secondhand = Template.FindName("PART_secondhand", this) as Line;

                   
            UpdateHandAngles();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += (s, e) => UpdateHandAngles();
            timer.Start();
            base.OnApplyTemplate();
        }



        private void UpdateHandAngles()
        {
            hourhand.RenderTransform = new RotateTransform((DateTime.Now.Hour / 12.0) *360, 0.5, 0.5);
            minutehand.RenderTransform = new RotateTransform((DateTime.Now.Minute / 60.0) * 360, 0.5, 0.5);
            secondhand.RenderTransform = new RotateTransform((DateTime.Now.Second / 60.0) * 360, 0.5, 0.5);
        }
    }
}
