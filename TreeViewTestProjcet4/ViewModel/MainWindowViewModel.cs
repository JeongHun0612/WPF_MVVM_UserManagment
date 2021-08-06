using System;
using System.Windows;
using TreeViewTestProjcet4.DBConncet;

namespace TreeViewTestProjcet4.ViewModel
{
    public class MainWindowViewModel : Notifier
    {
        public MainWindowViewModel()
        {
        }

        public static MySQLManager manager = new MySQLManager();
        public static TitleBarViewModel titleBarViewModel { get; set; } = new TitleBarViewModel();
        public static ContentTitleViewModel contentTitleViewModel { get; set; } = new ContentTitleViewModel();
        public static SystemTimeViewModel systemTimeViewModel { get; set; } = new SystemTimeViewModel();
        public static UserGroupTreeViewModel userGroupViewModel { get; set; } = new UserGroupTreeViewModel();
        public static UserListInfoViewModel userListInfoViewModel { get; set; } = new UserListInfoViewModel();
        public static LogListViewModel logListViewModel { get; set; } = new LogListViewModel();

        public static WindowState state = WindowState.Normal;
        public WindowState State
        {
            get { return state; }
            set { state = value; Notify("State"); }
        }
    }
}
