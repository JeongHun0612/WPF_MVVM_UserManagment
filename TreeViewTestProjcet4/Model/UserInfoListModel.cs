using System.Diagnostics;
using System.Windows.Controls;
using TreeViewTestProjcet4.ViewModel;

namespace TreeViewTestProjcet4.Model
{
    public class UserInfoListModel : NotifierCollection
    {
        public UserInfoListModel(string userId, string userPw, string userName, string userGroup)
        {
            this.userId = userId;
            this.userPw = userPw;
            this.userName = userName;
            this.userGroup = userGroup;
        }

        public UserInfoListModel(string primaryKey, string userId, string userPw, string userName, string userGroup, bool isReadOnly, bool isSaveBtnVisibility, bool isCancleBtnVisibility)
        {
            this.commandUserInfoEditClick = new DelegateCommand(UserInfoEditClick);
            this.commandUserInfoDeleteClick = new DelegateCommand(UserInfoDeleteClick);
            this.commandUserInfoSaveClick = new DelegateCommand(UserInfoSaveClick);
            this.commandUserInfoCancleClick = new DelegateCommand(UserInfoCancleClick);

            this.primaryKey = primaryKey;
            this.userId = userId;
            this.userPw = userPw;
            this.userName = userName;
            this.userGroup = userGroup;
            this.isReadOnly = isReadOnly;
            this.isSaveBtnVisibility = isSaveBtnVisibility;
            this.isCancleBtnVisibility = isCancleBtnVisibility;
        }

        private string primaryKey = string.Empty;
        public string PrimaryKey
        {
            get { return this.primaryKey; }
            set { this.primaryKey = value; NotifyCollection("PrimaryKey"); }
        }

        private string userId = string.Empty;
        public string UserId
        {
            get { return this.userId; }
            set { this.userId = value; NotifyCollection("UserId"); }
        }

        private string userPw = string.Empty;
        public string UserPw
        {
            get { return this.userPw; }
            set { this.userPw = value; NotifyCollection("UserPw"); }
        }

        private string userName = string.Empty;
        public string UserName
        {
            get { return this.userName; }
            set { this.userName = value; NotifyCollection("UserName"); }
        }

        private string userGroup = string.Empty;
        public string UserGroup
        {
            get { return this.userGroup; }
            set { this.userGroup = value; NotifyCollection("UserGroup"); }
        }

        private bool isReadOnly = true;
        public bool IsReadOnly
        {
            get { return this.isReadOnly; }
            set { this.isReadOnly = value; NotifyCollection("IsReadOnly"); }
        }

        private bool isSaveBtnVisibility = false;
        public bool IsSaveBtnVisibility
        {
            get { return this.isSaveBtnVisibility; }
            set { this.isSaveBtnVisibility = value; NotifyCollection("IsSaveBtnVisibility"); }
        }

        private bool isCancleBtnVisibility = false;
        public bool IsCancleBtnVisibility
        {
            get { return this.isCancleBtnVisibility; }
            set { this.isCancleBtnVisibility = value; NotifyCollection("IsCancleBtnVisibility"); }
        }

        private DelegateCommand commandUserInfoEditClick = null;
        private DelegateCommand commandUserInfoDeleteClick = null;
        private DelegateCommand commandUserInfoSaveClick = null;
        private DelegateCommand commandUserInfoCancleClick = null;

        public DelegateCommand CommandUserInfoEditClick
        {
            get => this.commandUserInfoEditClick;
            set => this.commandUserInfoEditClick = value;
        }
        public DelegateCommand CommandUserInfoDeleteClick
        {
            get => this.commandUserInfoDeleteClick;
            set => this.commandUserInfoDeleteClick = value;
        }
        public DelegateCommand CommandUserInfoSaveClick
        {
            get => this.commandUserInfoSaveClick;
            set => this.commandUserInfoSaveClick = value;
        }
        public DelegateCommand CommandUserInfoCancleClick
        {
            get => this.commandUserInfoCancleClick;
            set => this.commandUserInfoCancleClick = value;
        }

        private void UserInfoEditClick(object obj)
        {
            Debug.WriteLine(obj);

            //ListBox listBox = obj as ListBox;
            //UserInfoListModel selectedItem = (UserInfoListModel)listBox.SelectedItem;
        }

        private void UserInfoDeleteClick(object obj)
        {
            Debug.WriteLine("Delete Click");
        }

        private void UserInfoSaveClick(object obj)
        {
            //MainWindowViewModel.userListInfoViewModel.UserInfoCollection.Add(new UserInfoListModel(userId, userPw, userName, userGroup, isReadOnly, isSaveBtnVisibility, isCancleBtnVisibility));
            MainWindowViewModel.userListInfoViewModel.UserInfoCollection.RemoveAt(0);
        }
        private void UserInfoCancleClick(object obj)
        {
            MainWindowViewModel.userListInfoViewModel.UserInfoCollection.RemoveAt(0);
        }
    }
}
