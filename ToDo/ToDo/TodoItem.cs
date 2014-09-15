using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace ToDo
{
    [Serializable]
    public class TodoItem : INotifyPropertyChanged
    {
        private string _title;
        private string _decription;
        private string _priority;
        private DateTime _creationDate = DateTime.Now;
        private DateTime _dueDate = DateTime.Now;
        private string _completionDate;
        private bool _done = false;
        private bool _overdue = false;


        public TodoItem()
        {}

        public TodoItem(string title, string description, string priority, DateTime dueDate)
        {
            _title = title;
            _decription = description;
            _priority = priority;            
            _dueDate = dueDate;
            _creationDate = DateTime.Now;
        }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                NotifyPropertyChanged();
            }
        }

        public string Description
        {
            get { return _decription; }
            set
            {
                _decription = value;
                NotifyPropertyChanged();
            }
        }

        public string Priority
        {
            get { return _priority; }
            set
            {
                _priority = value;
                NotifyPropertyChanged();
            }
        }

        public DateTime CreationDate
        {
            get { return _creationDate; }
            set
            {
                _creationDate = value;
                NotifyPropertyChanged();
            }
        }

        public DateTime DueDate
        {
            get { return _dueDate; }
            set
            {
                _dueDate = value;
                NotifyPropertyChanged();
            }
        }

        public string CompletionDate
        {
            get { return _completionDate; }
            set
            {
                _completionDate = value;
                NotifyPropertyChanged();
            }
        }

        public bool Done
        {
            get { return _done; }
            set
            {
                _done = value;                
                if (_done == true)
                    CompletionDate = DateTime.Today.ToShortDateString();
                NotifyPropertyChanged();
            }
        }

        /*
        public bool OverDue
        {
            get { return _overdue; }
            set
            {
                if (DateTime.Compare(DateTime.Today.Date, _dueDate) > 0)
                {
                    _overdue = true;
                }
                _overdue = false;
            }
        }
        */
        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

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
