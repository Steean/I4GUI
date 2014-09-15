using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MvvmFoundation.Wpf;
using System.Windows;
using System.Windows.Data;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Win32;

namespace ToDo
{
    public class Todos : ObservableCollection<TodoItem>, INotifyPropertyChanged
    {
        private string _fileName;
        private bool dirty = false;

        public Todos()
        {
        }

        #region Commands

        ICommand _NewFileCommand;
        public ICommand NewFileCommand
        {
            get { return _NewFileCommand ?? (_NewFileCommand = new RelayCommand(NewFileCommand_Execute)); }
        }

        private void NewFileCommand_Execute()
        {
            if (dirty)
            {
                MessageBoxResult res = MessageBox.Show("You have unsaved data. Are you sure you want to initiate a new file without saving data first?",
                    "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (res == MessageBoxResult.No)
                {
                    return;
                }
            }

            Clear();
            _fileName = "";
            dirty = false;
        }

        private ICommand _openCommand;
        public ICommand OpenCommand
        {
            get { return _openCommand ?? (_openCommand = new RelayCommand(OpenTodo)); }
        }

        private void OpenTodo()
        {
            var dlg = new OpenFileDialog();
            dlg.Filter = "XML documents | *.xml";

            if (dlg.ShowDialog().Value)
            {
                _fileName = dlg.FileName;

                XmlSerializer serializer = new XmlSerializer(typeof(List<TodoItem>));
                TextReader reader = new StreamReader(dlg.FileName);

                var openElement = (List<TodoItem>)serializer.Deserialize(reader);
                ClearItems();
                foreach (var item in openElement)
                {
                    Add(item);
                }
                reader.Close();
            }
        }

        private ICommand _saveCommand;
        public ICommand SaveCommand
        {
            get { return _saveCommand ?? (_saveCommand = new RelayCommand(SaveTodo, () => File.Exists(_fileName))); }
        }

        private ICommand _saveAsCommand;
        public ICommand SaveAsCommand
        {
            get { return _saveAsCommand ?? (_saveAsCommand = new RelayCommand(SaveAsTodo)); }
        }

        private void SaveAsTodo()
        {
            var dlg = new SaveFileDialog();
            dlg.FileName = "agentlist";
            dlg.DefaultExt = ".xml";
            dlg.Filter = "XML documents |*.xml";

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                _fileName = dlg.FileName;
                SaveTodo();                
            }
        }

        private void SaveTodo()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<TodoItem>));
            TextWriter writer = new StreamWriter(_fileName);
            var data = Items.ToList();
            serializer.Serialize(writer, data);
            writer.Close();
            dirty = false;
        }

        ICommand _CloseAppCommand;
        public ICommand CloseAppCommand
        {
            get { return _CloseAppCommand ?? (_CloseAppCommand = new RelayCommand(CloseCommand_Execute)); }
        }

        private void CloseCommand_Execute()
        {
            bool regret = false;

            if (dirty)
            {
                MessageBoxResult res = MessageBox.Show("You have unsaved data. Are you sure you want to close the application without saving data first?",
                    "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (res == MessageBoxResult.No)
                {
                    regret = true;
                }
            }
            if (!regret)
                Application.Current.MainWindow.Close();
        }

        #region Edit Commands

        ICommand _addCommand;
        public ICommand AddCommand
        {
            get { return _addCommand ?? (_addCommand = new RelayCommand(AddTask)); }
        }

        private void AddTask()
        {
            // Show Modal Dialog
            var dlg = new TodoDetailedWindow();
            dlg.Title = "Add New Task";
            TodoItem newItem = new TodoItem();
            dlg.DataContext = newItem;
            if (dlg.ShowDialog() == true)
            {
                Add(newItem);
                CurrentViewIndex = 0;
                CurrentTask = newItem;
                dirty = true;
                NotifyPropertyChanged("Count");
            }
        }

        ICommand _editCommand;
        public ICommand EditCommand
        {
            get { return _editCommand ?? (_editCommand = new RelayCommand(EditTask, DeleteTask_CanExecute)); }
        }

        private void EditTask()
        {
            // Show Modal Dialog
            var dlg = new TodoDetailedWindow();
            dlg.Title = "Edit Task";
            // Need a temp to-do in case of cancel
            TodoItem tempItem = new TodoItem();
            tempItem.Title = currentTask.Title;
            tempItem.Description = currentTask.Description;
            tempItem.Priority = currentTask.Priority;
            tempItem.CreationDate = currentTask.CreationDate;
            tempItem.DueDate = currentTask.DueDate;
            tempItem.Done = currentTask.Done;
            dlg.DataContext = tempItem;
            if (dlg.ShowDialog() == true)
            {
                currentTask.Title = tempItem.Title;
                currentTask.Description = tempItem.Description;
                currentTask.Priority = tempItem.Priority;
                currentTask.CreationDate = tempItem.CreationDate;
                currentTask.DueDate = tempItem.DueDate;
                currentTask.Done = tempItem.Done;
                dirty = true;
            }
        }    

        ICommand _deleteCommand;
        public ICommand DeleteCommand
        {
            get { return _deleteCommand ?? (_deleteCommand = new RelayCommand(DeleteTask, DeleteTask_CanExecute)); }
        }

        private void DeleteTask()
        {
            MessageBoxResult res = MessageBox.Show("Are you sure you want to delete the current task?", 
                "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (res == MessageBoxResult.Yes)
            {
                Remove(CurrentTask);
                NotifyPropertyChanged("Count");
                dirty = true;
            }
        }

        private bool DeleteTask_CanExecute()
        {
            if (Count > 0 && CurrentIndex >= 0)
                return true;
            else
                return false;
        }

        #endregion

        #endregion

        #region Properties

        int currentIndex = -1;

        public int CurrentIndex
        {
            get { return currentIndex; }
            set
            {
                if (currentIndex != value)
                {
                    currentIndex = value;
                    NotifyPropertyChanged();
                }
            }
        }

        TodoItem currentTask = null;

        public TodoItem CurrentTask
        {
            get { return currentTask; }
            set
            {
                if (currentTask != value)
                {
                    currentTask = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public IReadOnlyCollection<string> FilterViews
        {
            get
            {
                ObservableCollection<string> result = new ObservableCollection<string>();
                result.Add("All");
                foreach (var s in new Views())
                    result.Add(s);
                return result;
            }
        }

        int currentViewIndex = 0;

        public int CurrentViewIndex
        {
            get { return currentViewIndex; }
            set
            {
                if (currentViewIndex != value)
                {
                    ICollectionView cv = CollectionViewSource.GetDefaultView(this);
                    currentViewIndex = value;

                    switch (currentViewIndex)
                    {
                        case 0:
                            cv.Filter = null; // Index 0 is no filter (show all)    
                            break;
                            
                        case 1:                           
                            cv.Filter = CollectionViewSource_FilterTodo;
                            break;
                        case 2:
                            cv.Filter = CollectionViewSource_FilterTodoToday;
                            break;
                        case 3:
                            cv.Filter = CollectionViewSource_FilterCompleted;
                            break;
                        default:
                            cv.Filter = null; // Index 0 is no filter (show all)    
                            break;
                    }
                    NotifyPropertyChanged();
                }
            }
        }

        private bool CollectionViewSource_FilterTodo(object t)
        {
            TodoItem todo = t as TodoItem;
            return (todo.Done == false);
        }

        private bool CollectionViewSource_FilterTodoToday(object t)
        {
            TodoItem todo = t as TodoItem;
            return (todo.DueDate.Date == DateTime.Today.Date && todo.Done == false);
        }

        private bool CollectionViewSource_FilterCompleted(object t)
        {
            TodoItem todo = t as TodoItem;
            return (todo.Done);
        }

        #endregion

        #region INotifyPropertyChanged implementation

        public new event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }

    
}
