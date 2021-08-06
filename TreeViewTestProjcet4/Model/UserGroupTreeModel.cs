using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Controls;
using TreeViewTestProjcet4.ViewModel;

namespace TreeViewTestProjcet4.Model
{
    public class UserGroupTreeModel : NotifierCollection
    {
        public UserGroupTreeModel()
        {

        }

        public UserGroupTreeModel(int deepCount, string primaryKey, string header)
        {
            this.commandSelectedItem = new DelegateCommand(SelectedItem);
            this.deepCount = deepCount;
            this.primaryKey = primaryKey;
            this.header = header;
        }

        private int deepCount;
        public int DeepCount
        {
            get { return this.deepCount; }
            set { this.deepCount = value; NotifyCollection("DeepCount"); }
        }

        private string primaryKey;
        public string PrimaryKey
        {
            get { return this.primaryKey; }
            set { this.primaryKey = value; NotifyCollection("PrimaryKey"); }
        }

        private string header;
        public string Header
        {
            get { return this.header; }
            set { this.header = value; NotifyCollection("Header"); }
        }

        private ObservableCollection<UserGroupTreeModel> childGroupList;
        public ObservableCollection<UserGroupTreeModel> ChildGroupList
        {
            get
            {
                if (this.childGroupList == null)
                {
                    this.childGroupList = new ObservableCollection<UserGroupTreeModel>();
                }
                return this.childGroupList;
            }
            set { this.childGroupList = value; NotifyCollection("ChildGroupList"); }
        }

        private DelegateCommand commandSelectedItem = null;
        public DelegateCommand CommandSelectedItem
        {
            get => this.commandSelectedItem;
            set => this.commandSelectedItem = value;
        }

        private void SelectedItem(object obj)
        {
            Debug.WriteLine("Test");
            UserGroupTreeModel selectedItem = (UserGroupTreeModel)obj;

            Debug.WriteLine(obj);
            if (selectedItem != null)
            {
                Debug.WriteLine(selectedItem.Header);
            }
            else
            {
                Debug.WriteLine("Null");
            }
        }
    }
}
