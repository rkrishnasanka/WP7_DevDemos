using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Database_Demo
{
    public partial class MainPage : PhoneApplicationPage ,INotifyPropertyChanged
    {
        private ToDoDataContext toDoDB;

        private ObservableCollection<ToDoItem> _toDoItems;

        public ObservableCollection<ToDoItem> ToDoItems
        {
            get { return _toDoItems; }
            set 
            { 
                if(_toDoItems != value)
                {
                    _toDoItems = value;
                    NotifyPropertyChanged("ToDoItems");    
                }
            }
        }
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            toDoDB = new ToDoDataContext(ToDoDataContext.DBConnectionString);

            this.DataContext = this;
        }


        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            var toDoItemsInDB = from  ToDoItem todo in toDoDB.ToDoItems select todo;

            ToDoItems = new ObservableCollection<ToDoItem>(toDoItemsInDB);
            base.OnNavigatedTo(e);
        }

        #region INotifyPropertyChanged Members
        
        public event PropertyChangedEventHandler PropertyChanged;
        
        private void NotifyPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this,new PropertyChangedEventArgs(propertyName));
            }
        }
 
        #endregion

        private void newToDoTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            newToDoTextBox.Text = string.Empty;

        }

        private void newToDoAddButton_Click(object sender, RoutedEventArgs e)
        {
            ToDoItem newToDo = new ToDoItem { ItemName = newToDoTextBox.Text };

            ToDoItems.Add(newToDo);

            toDoDB.ToDoItems.InsertOnSubmit(newToDo);
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            toDoDB.SubmitChanges();
        }

        private void deleteTaskButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            if (button != null)
            {
                ToDoItem todofordelete = button.DataContext as ToDoItem;

                ToDoItems.Remove(todofordelete);

                toDoDB.ToDoItems.DeleteOnSubmit(todofordelete);

                toDoDB.SubmitChanges();

                this.Focus();
            }
        }

    }

    [Table]
    public class ToDoItem : INotifyPropertyChanging, INotifyPropertyChanged
    {
        private int _toDoItemId;

        [Column(IsPrimaryKey = true , IsDbGenerated = true , DbType = "INT NOT NULL Identity", CanBeNull = false , AutoSync = AutoSync.OnInsert)]
        public int ToDoItemId
        {
            get { return _toDoItemId; }
            set
            {
                if (_toDoItemId != value)
                {
                    NotifyPropertyChanging("ToDoItemId");
                    _toDoItemId = value;
                    NotifyPropertyChanged("ToDoItemId");
                }
            }
        }


        private string _itemName;
        
        [Column]
        public string ItemName
        {
            get { return _itemName; }
            set
            {
                if (_itemName != value)
                {
                    NotifyPropertyChanging("ItemName");
                    _itemName = value;
                    NotifyPropertyChanged("ItemName");
                }
            }
        }

        private bool _isComplete;

        [Column]
        public bool IsComplete
        {
            get { return _isComplete; }
            set
            {
                if (_isComplete != value)
                {
                    NotifyPropertyChanging("IsComplete");
                    _isComplete = value;
                    NotifyPropertyChanged("IsComplete");
                }
            }
        }
        
        private Binary _version;

        [Column(IsVersion= true)]
        public Binary Version
        {
            get { return _version; }
            set 
            {
                NotifyPropertyChanging("Version");
                _version = value;
                NotifyPropertyChanged("Version");
            }
        }

        #region INotifyPropertyChanged Members
        
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangingEventHandler PropertyChanging;

        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion
    }

    public class ToDoDataContext : DataContext
    {
        public static string DBConnectionString = "Data Source=isostore:/ToDo.sdf";

        public ToDoDataContext(string connectionString):base(connectionString)
        {}

        public Table<ToDoItem> ToDoItems;
    }
}