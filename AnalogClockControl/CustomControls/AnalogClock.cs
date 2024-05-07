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

    public class AnalogClock : Control
    {
        private Line hourhand;
        private Line minutehand;
        private Line secondhand;

        public static DependencyProperty ShowSecondsProperty = DependencyProperty.Register("ShowSeconds", typeof(bool), typeof(AnalogClock), new PropertyMetadata(true));

        //public static RoutedEvent TimeChangedEvent = EventManager.RegisterRoutedEvent("TimeChanged", RoutingStrategy.Bubble, typeof(TimeChangedEventHandler), typeof(AnalogClock));
        public static RoutedEvent TimeChangedEvent = EventManager.RegisterRoutedEvent("TimeChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<DateTime>), typeof(AnalogClock));



        public bool ShowSeconds
        {
            get {  return (bool)GetValue(ShowSecondsProperty);}
            set { SetValue(ShowSecondsProperty, value); }
        }

        //public event TimeChangedEventHandler TimeChanged
        //{
        //    add
        //    {
        //        AddHandler(TimeChangedEvent, value);
        //    }
        //    remove
        //    {
        //        RemoveHandler(TimeChangedEvent, value);
        //    }
        //}


        public event RoutedPropertyChangedEventHandler<DateTime> TimeChanged
        {
            add
            {
                AddHandler(TimeChangedEvent, value);
            }
            remove
            {
                RemoveHandler(TimeChangedEvent, value);
            }
        }

        static AnalogClock()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AnalogClock), new FrameworkPropertyMetadata(typeof(AnalogClock)));
        }

        public override void OnApplyTemplate()
        {

            hourhand = Template.FindName("PART_hourhand", this) as Line;
            minutehand = Template.FindName("PART_minutehand", this) as Line;
            secondhand = Template.FindName("PART_secondhand", this) as Line;

            //Binding showSecondHandBinding = new Binding
            //{
            //    Path = new PropertyPath(nameof(ShowSeconds)),
            //    Source = this,
            //    Converter = new BooleanToVisibilityConverter()
            //};

            //secondhand.SetBinding(VisibilityProperty, showSecondHandBinding);
                
            UpdateHandAngles(DateTime.Now);
            onTimeChanged(DateTime.Now);
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += (s, e) => onTimeChanged(DateTime.Now);
            timer.Start();
            base.OnApplyTemplate();
        }

        protected virtual void onTimeChanged(DateTime time)
        {
            UpdateHandAngles(time);
            UpdateTimeState(time);
            //RaiseEvent(new TimeChangedEventSrgs(TimeChangedEvent, this) { NewTime = time });
            RaiseEvent(new RoutedPropertyChangedEventArgs<DateTime>(DateTime.Now.AddSeconds(-1), DateTime.Now, TimeChangedEvent));
        }


        private void UpdateTimeState(DateTime time)
        {
            if(time.Hour < 6 && time.Hour > 18)
            {
                VisualStateManager.GoToState(this, "Day", false);
            }
            else
            {
                VisualStateManager.GoToState(this, "Night", false);

            }

        }

        private void UpdateHandAngles(DateTime time)
        {
            hourhand.RenderTransform = new RotateTransform((time.Hour / 12.0) *360, 0.5, 0.5);
            minutehand.RenderTransform = new RotateTransform((time.Minute / 60.0) * 360, 0.5, 0.5);
            secondhand.RenderTransform = new RotateTransform((time.Second / 60.0) * 360, 0.5, 0.5);
        }
    }
}
