using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using TreeViewTestProjcet4.Model;

namespace TreeViewTestProjcet4.ViewModel
{
    public class UserGroupTreeViewModel : Notifier
    {
        public UserGroupTreeViewModel()
        {
            PopulateParentGroupTree();
            this.commandTreeAddClick = new DelegateCommand(TreeAddClick);
            this.commandTreeDeleteClick = new DelegateCommand(TreeDeleteClick);
            this.commandSelectedItem = new DelegateCommand(SelectedItem);
            this.commandLeftDown = new DelegateCommand(LeftDown);
        }

        private string[] tableName = new string[3] { "userparentgroup", "usergroup", "users" };

        private ObservableCollection<UserGroupTreeModel> parentGroupList = new ObservableCollection<UserGroupTreeModel>();
        public ObservableCollection<UserGroupTreeModel> ParentGroupList
        {
            get { return this.parentGroupList; }
            set { this.parentGroupList = value; Notify("ParentGroupList"); }
        }

        private string inputHeader = string.Empty;
        public string InputHeader
        {
            get { return this.inputHeader; }
            set { this.inputHeader = value; Notify("InputHeader"); }
        }

        private DelegateCommand commandTreeAddClick = null;
        private DelegateCommand commandTreeDeleteClick = null;
        private DelegateCommand commandSelectedItem = null;
        private DelegateCommand commandLeftDown = null;

        public DelegateCommand CommandTreeAddClick
        {
            get => this.commandTreeAddClick;
            set => this.commandTreeAddClick = value;
        }
        public DelegateCommand CommandTreeDeleteClick
        {
            get => this.commandTreeDeleteClick;
            set => this.commandTreeDeleteClick = value;
        }
        public DelegateCommand CommandSelectedItem
        {
            get => this.commandSelectedItem;
            set => this.commandSelectedItem = value;
        }
        public DelegateCommand CommandLeftDown
        {
            get => this.commandLeftDown;
            set => this.commandLeftDown = value;
        }

        private void PopulateParentGroupTree()
        {
            string parentGroupSelectQuery = string.Format("SELECT * FROM {0}", tableName[0]);
            DataSet parentGroupDataSet = MainWindowViewModel.manager.Select(parentGroupSelectQuery, tableName[0]);

            for (int idx = 0; idx < parentGroupDataSet.Tables[0].Rows.Count; idx++)
            {
                string parentGroupNodeHeader = parentGroupDataSet.Tables[0].Rows[idx]["parent_group_name"].ToString();
                string parentGroupKey = parentGroupDataSet.Tables[0].Rows[idx]["parent_group_id"].ToString();
                UserGroupTreeModel parentGroupNode = new UserGroupTreeModel(0, parentGroupKey, parentGroupNodeHeader);

                ParentGroupList.Add(parentGroupNode);
                PopulateUserGroupTree(parentGroupNode, parentGroupKey);
            }
        }

        private void PopulateUserGroupTree(UserGroupTreeModel parentGroupNode, string parentGroupKey)
        {
            string userGroupSelectQuery = string.Format("SELECT * FROM {0} WHERE parent_group_id = '{1}'", tableName[1], parentGroupKey);
            DataSet userGroupDataSet = MainWindowViewModel.manager.Select(userGroupSelectQuery, tableName[1]);

            for (int idx = 0; idx < userGroupDataSet.Tables[0].Rows.Count; idx++)
            {
                string userGroupNodeHeader = userGroupDataSet.Tables[0].Rows[idx]["group_name"].ToString();
                string userGroupKey = userGroupDataSet.Tables[0].Rows[idx]["group_id"].ToString();

                UserGroupTreeModel childGroupNode = new UserGroupTreeModel(1, userGroupKey, userGroupNodeHeader);

                parentGroupNode.ChildGroupList.Add(childGroupNode);
                PopulateUserTree(childGroupNode, userGroupKey);
            }
        }

        private void PopulateUserTree(UserGroupTreeModel childGroupNode, string childGroupId)
        {
            string userSelectQuery = string.Format("SELECT * FROM {0} WHERE group_id = '{1}'", tableName[2], childGroupId);
            DataSet userDataSet = MainWindowViewModel.manager.Select(userSelectQuery, tableName[2]);

            for (int idx = 0; idx < userDataSet.Tables[0].Rows.Count; idx++)
            {
                string userNodeHeader = userDataSet.Tables[0].Rows[idx]["user_name"].ToString();
                string userKey = userDataSet.Tables[0].Rows[idx]["id"].ToString();

                UserGroupTreeModel userNode = new UserGroupTreeModel(2, userKey, userNodeHeader);

                childGroupNode.ChildGroupList.Add(userNode);
            }
        }

        private void TreeAddClick(object obj)
        {
            TreeView treeView = obj as TreeView;
            UserGroupTreeModel selectedItem = (UserGroupTreeModel)treeView.SelectedItem;

            if (InputHeader != string.Empty)
            {
                if (selectedItem == null)
                {
                    string parentGroupNameSelectQuery = string.Format("SELECT * FROM {0} WHERE parent_group_name = '{1}'", tableName[0], InputHeader);
                    DataSet parentGroupNameDataSet = MainWindowViewModel.manager.Select(parentGroupNameSelectQuery, tableName[0]);
                    if (parentGroupNameDataSet.Tables[0].Rows.Count > 0)
                    {
                        MessageBox.Show("동일한 이름이 있습니다.");
                    }
                    else
                    {
                        string parentGroupInsertQuery = string.Format("INSERT INTO {0}(parent_group_name) VALUES ('{1}')", tableName[0], InputHeader);
                        if (MainWindowViewModel.manager.MySqlQueryExecuter(parentGroupInsertQuery))
                        {
                            string parentGroupSelectQuery = string.Format("SELECT * FROM {0} WHERE parent_group_name = '{1}'", tableName[0], InputHeader);
                            DataSet parentGroupDataSet = MainWindowViewModel.manager.Select(parentGroupSelectQuery, tableName[0]);

                            UserGroupTreeModel parentGroupNode = new UserGroupTreeModel(0, parentGroupDataSet.Tables[0].Rows[0]["parent_group_id"].ToString(), InputHeader);
                            ParentGroupList.Add(parentGroupNode);
                        }
                    }
                }
                else
                {
                    if (selectedItem.DeepCount == 0)
                    {
                        string userGroupNameSelectQuery = string.Format("SELECT * FROM {0} WHERE parent_group_id = '{1}' AND group_name = '{2}'", tableName[1], selectedItem.PrimaryKey, InputHeader);
                        DataSet userGroupNameDataSet = MainWindowViewModel.manager.Select(userGroupNameSelectQuery, tableName[1]);
                        if (userGroupNameDataSet.Tables[0].Rows.Count > 0)
                        {
                            MessageBox.Show("동일한 이름이 있습니다.");
                        }
                        else
                        {
                            string userGroupInsertQuery = string.Format("INSERT INTO {0}(group_name, parent_group_id) VALUES ('{1}', '{2}')", tableName[1], InputHeader, selectedItem.PrimaryKey);
                            if (MainWindowViewModel.manager.MySqlQueryExecuter(userGroupInsertQuery))
                            {
                                string userGroupSelectQuery = string.Format("SELECT * FROM {0} WHERE parent_group_id = '{1}' AND group_name = '{2}'", tableName[1], selectedItem.PrimaryKey, InputHeader);
                                DataSet userGroupDataSet = MainWindowViewModel.manager.Select(userGroupSelectQuery, tableName[1]);

                                UserGroupTreeModel childGroupNode = new UserGroupTreeModel(1, userGroupDataSet.Tables[0].Rows[0]["group_id"].ToString(), InputHeader);
                                selectedItem.ChildGroupList.Add(childGroupNode);
                            }
                        }
                    }

                }
            }
            else
            {
                if (selectedItem.DeepCount == 1)
                {
                    MainWindowViewModel.userListInfoViewModel.UserInfoCollection.Insert(0, new UserInfoListModel(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, false, true, true));
                }
            }
            InputHeader = string.Empty;
        }

        private void TreeDeleteClick(object obj)
        {
            TreeView treeView = obj as TreeView;
            UserGroupTreeModel selectedItem = (UserGroupTreeModel)treeView.SelectedItem;

            if (selectedItem != null)
            {
                if (selectedItem.DeepCount == 0)
                {
                    if (ParentGroupList.Remove(selectedItem))
                    {
                        string tableName = "userparentgroup";
                        string parentGroupDeleteQuery = string.Format("DELETE FROM {0} WHERE parent_group_id = '{1}'", tableName, selectedItem.PrimaryKey);
                        MainWindowViewModel.manager.MySqlQueryExecuter(parentGroupDeleteQuery);
                    }
                }
                else
                {
                    for (int idx1 = 0; idx1 < ParentGroupList.Count; idx1++)
                    {
                        if (ParentGroupList[idx1].ChildGroupList.Remove(selectedItem))
                        {
                            string tableName = "usergroup";
                            string userGroupDeleteQuery = string.Format("DELETE FROM {0} WHERE group_id = '{1}'", tableName, selectedItem.PrimaryKey);
                            MainWindowViewModel.manager.MySqlQueryExecuter(userGroupDeleteQuery);
                        }

                        for (int idx2 = 0; idx2 < ParentGroupList[idx1].ChildGroupList.Count; idx2++)
                        {
                            if (ParentGroupList[idx1].ChildGroupList[idx2].ChildGroupList.Remove(selectedItem))
                            {
                                string tableName = "users";
                                string userDeleteQuery = string.Format("DELETE FROM {0} WHERE id = '{1}'", tableName, selectedItem.PrimaryKey);
                                MainWindowViewModel.manager.MySqlQueryExecuter(userDeleteQuery);
                            }
                        }
                    }
                }
            }
        }

        private void SelectedItem(object obj)
        {
            UserGroupTreeModel selectedItem = obj as UserGroupTreeModel;

            if (selectedItem != null)
            {
                Debug.WriteLine(selectedItem.Header);
            }
        }

        private void LeftDown(object obj)
        {

        }
    }
}
