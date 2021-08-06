using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeViewTestProjcet4.Model;

namespace TreeViewTestProjcet4.ViewModel
{
    public class LogListViewModel : Notifier
    {
        public LogListViewModel()
        {
            LogList.Add(new LogListModel("Info", "Test"));
        }

        private ObservableCollection<LogListModel> logList = new ObservableCollection<LogListModel>();
        public ObservableCollection<LogListModel> LogList
        {
            get { return this.logList; }
            set { this.logList = value; Notify("LogList"); }
        }
    }
}
