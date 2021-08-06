using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using TreeViewTestProjcet4.DBConncet;
using TreeViewTestProjcet4.Model;

namespace TreeViewTestProjcet4.ViewModel
{
    public class UserListInfoViewModel : Notifier
    {
        public UserListInfoViewModel()
        {
            PopulateUserInfo();
            this.commandUserInfoAddClick = new DelegateCommand(UserInfoAddClick);
        }

        private ObservableCollection<UserInfoListModel> userInfoCollection = new ObservableCollection<UserInfoListModel>();
        public ObservableCollection<UserInfoListModel> UserInfoCollection
        {
            get { return this.userInfoCollection; }
            set { this.userInfoCollection = value; Notify("UserInfoCollection"); }
        }

        private DelegateCommand commandUserInfoAddClick = null;

        public DelegateCommand CommandUserInfoAddClick
        {
            get => this.commandUserInfoAddClick;
            set => this.commandUserInfoAddClick = value;
        }

        private void PopulateUserInfo()
        {
            string tableName = "users";
            string selectQuery = string.Format("SELECT * FROM {0}", tableName);
            DataSet userDataSet = MainWindowViewModel.manager.Select(selectQuery, tableName);

            for (int i = 0; i < userDataSet.Tables[0].Rows.Count; i++)
            {
                string primaryKey = userDataSet.Tables[0].Rows[i]["id"].ToString();
                string userId = userDataSet.Tables[0].Rows[i]["user_id"].ToString();
                string userPw = userDataSet.Tables[0].Rows[i]["user_pw"].ToString();
                string userName = userDataSet.Tables[0].Rows[i]["user_name"].ToString();
                string userGroupId = userDataSet.Tables[0].Rows[i]["group_id"].ToString();

                PopulateGroupName(primaryKey, userId, userPw, userName, userGroupId);
            }
        }

        private void PopulateGroupName(string primaryKey, string userId, string userPw, string userName, string userGroupId)
        {
            string tableName = "usergroup";
            string selectQuery = string.Format("SELECT * FROM {0} WHERE group_id = '{1}'", tableName, userGroupId);
            DataSet userDataSet = MainWindowViewModel.manager.Select(selectQuery, tableName);

            string userGroup = userDataSet.Tables[0].Rows[0]["group_name"].ToString();
            UserInfoCollection.Add(new UserInfoListModel(primaryKey, userId, userPw, userName, userGroup, true, false, false));
        }

        private void UserInfoAddClick(object obj)
        {
            UserInfoCollection.Insert(0, new UserInfoListModel(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, false, true, true));
        }
    }
}
