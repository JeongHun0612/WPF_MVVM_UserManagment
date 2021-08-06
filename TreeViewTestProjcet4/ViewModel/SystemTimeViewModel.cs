using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace TreeViewTestProjcet4.ViewModel
{
    public class SystemTimeViewModel : Notifier
    {
        public SystemTimeViewModel()
        {
            KSTTimer();
            UTCTimer();
        }

        private string KSTdateTime = DateTime.Now.ToString();
        public string KSTDateTime
        {
            get { return this.KSTdateTime; }
            set { this.KSTdateTime = value; Notify("KSTDateTime"); }
        }

        private string UTCdateTime = DateTime.UtcNow.ToString();
        public string UTCDateTime
        {
            get { return this.UTCdateTime; }
            set { this.UTCdateTime = value; Notify("UTCDateTime"); }
        }

        private string GPSweek = "1";
        public string GPSWeek
        {
            get { return this.GPSweek; }
            set { this.GPSweek = value; Notify("GPSWeek"); }
        }

        private void KSTTimer()
        {
            DispatcherTimer KSTTimer = new DispatcherTimer();
            KSTTimer.Start();
            KSTTimer.Interval = TimeSpan.FromMilliseconds(1000);
            KSTTimer.Tick += (o, e) =>
            {
                KSTDateTime = DateTime.Now.ToString();
            };
        }
            
        private void UTCTimer()
        {
            DispatcherTimer UTCTimer = new DispatcherTimer();
            UTCTimer.Start();
            UTCTimer.Interval = TimeSpan.FromMilliseconds(1000);
            UTCTimer.Tick += (o, e) =>
            {
                UTCDateTime = DateTime.UtcNow.ToString();
            };
        }
    }
}
