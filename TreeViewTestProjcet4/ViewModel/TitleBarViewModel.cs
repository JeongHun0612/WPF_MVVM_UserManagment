using System;
using System.Windows;

namespace TreeViewTestProjcet4.ViewModel
{
    public class TitleBarViewModel
    {
        public TitleBarViewModel()
        {
            MainWindowViewModel.manager.Initialize();

            this.commandDragMove = new DelegateCommand(DragMove);
            this.commandMinimizeClick = new DelegateCommand(MinimizeClick);
            this.commandMaximizeClick = new DelegateCommand(MaximizeClick);
            this.commandExitClick = new DelegateCommand(ExitClick);
        }

        private DelegateCommand commandDragMove = null;
        private DelegateCommand commandMinimizeClick = null;
        private DelegateCommand commandMaximizeClick = null;
        private DelegateCommand commandExitClick = null;

        public DelegateCommand CommandDragMove
        {
            get => this.commandDragMove;
            set => this.commandDragMove = value;
        }

        public DelegateCommand CommandMinimizeClick
        {
            get => this.commandMinimizeClick;
            set => this.commandMinimizeClick = value;
        }

        public DelegateCommand CommandMaximizeClick
        {
            get => this.commandMaximizeClick;
            set => this.commandMaximizeClick = value;
        }

        public DelegateCommand CommandExitClick
        {
            get => this.commandExitClick;
            set => this.commandExitClick = value;
        }

        private void DragMove(object obj)
        {
            ((MainWindow)Application.Current.MainWindow).DragMove();
        }

        private void MinimizeClick(object obj)
        {
            ((MainWindow)Application.Current.MainWindow).WindowState = WindowState.Minimized;
        }

        private void MaximizeClick(object obj)
        {
            if (MainWindowViewModel.state == WindowState.Normal)
            {
                ((MainWindow)Application.Current.MainWindow).WindowState = WindowState.Maximized;
            }
            else
            {
                ((MainWindow)Application.Current.MainWindow).WindowState = WindowState.Normal;
            }
        }

        private void ExitClick(object obj)
        {
            Environment.Exit(0);
            //System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
    }
}
