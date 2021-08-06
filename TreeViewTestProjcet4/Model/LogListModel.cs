using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeViewTestProjcet4.ViewModel;

namespace TreeViewTestProjcet4.Model
{
    public class LogListModel : NotifierCollection
    {
        public LogListModel(string level, string message)
        {
            this.level = level;
            this.message = message;
        }

        private string timeStamp = DateTime.Now.ToString();
        public string TimeStamp
        {
            get { return this.timeStamp; }
            set { this.timeStamp = value; NotifyCollection("TimeStamp"); }
        }

        private string level = string.Empty;
        public string Level
        {
            get { return this.level; }
            set { this.level = value; NotifyCollection("Level"); }
        }

        private string message = string.Empty;
        public string Message
        {
            get { return this.message; }
            set { this.message = value; NotifyCollection("Message"); }
        }
    }
}
