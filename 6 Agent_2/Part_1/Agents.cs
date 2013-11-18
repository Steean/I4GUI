using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MvvmFoundation.Wpf;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using Microsoft.Win32;

namespace Part_1
{
    public class Agents : ObservableCollection<Agent>, INotifyPropertyChanged
    {
        public Agents()
        {
            Add(new Agent("007", "James Bond", "Assassination", "Red"));
            Add(new Agent("1", "Karate KID!!!", "Sniper", "Blue"));
        }

        #region Commands

        private ICommand _newAgentCommand;
        public ICommand NewAgentCommand
        {
            get { return _newAgentCommand ?? (_newAgentCommand = new RelayCommand(NewAgent)); }
            
        }

        private void NewAgent()
        {
            ClearItems();
            Add(new Agent());
            CurrentIndex = Count - 1;
        }

        private ICommand _openCommand;
        public ICommand OpenCommand
        {
            get { return _openCommand ?? (_openCommand = new RelayCommand(OpenAgent)); }
        }

        private void OpenAgent()
        {
            var dlg = new OpenFileDialog();
            dlg.Filter = "XML documents | *.xml";

            if (dlg.ShowDialog().Value)
            {
                saveFile = dlg.FileName;

                XmlSerializer serializer = new XmlSerializer(typeof (List<Agent>));
                TextReader reader = new StreamReader(dlg.FileName);

                var openElement = (List<Agent>) serializer.Deserialize(reader);
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
            get { return _saveCommand ?? (_openCommand = new RelayCommand(SaveAgent, () => File.Exists(saveFile))); }
        }

        private ICommand _saveAsCommand;
        public ICommand SaveAsCommand
        {
            get { return _saveAsCommand ?? (_saveAsCommand = new RelayCommand(SaveAsAgent)); }
        }

        private void SaveAsAgent()
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
                saveFile = dlg.FileName;
                SaveAgent();
            }
        }

        private void SaveAgent()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Agent>));
            TextWriter writer = new StreamWriter(saveFile);
            var data = Items.ToList();
            serializer.Serialize(writer, data);
            writer.Close();
        }

        private ICommand _exitCommand;
        public ICommand ExitCommand
        {
            get { return _exitCommand ?? (_exitCommand = new RelayCommand(Exit)); }
        }

        private void Exit()
        {
            Application.Current.Shutdown();
        }

        private ICommand _removeCommand;
        public ICommand RemoveCommand
        {
            get { return _removeCommand ?? (_removeCommand = new RelayCommand(RemoveAgent)); }
        }

        private void RemoveAgent()
        {           
            RemoveAt(CurrentIndex);
            CurrentIndex = 0;
        }

        ICommand _addCommand;
        public ICommand AddCommand
        {
            get { return _addCommand ?? (_addCommand = new RelayCommand(AddAgent)); }
        }

        private void AddAgent()
        {
            Add(new Agent());
            CurrentIndex = Count - 1;
        }

        ICommand _nextCommand;
        public ICommand NextCommand
        {
            get
            {
                return _nextCommand ?? (_nextCommand = new RelayCommand(
                    () => ++CurrentIndex,
                    () => CurrentIndex < (Count - 1)));
            }
        }

        ICommand _PreviousCommand;
        public ICommand PreviousCommand
        {
            get { return _PreviousCommand ?? (_PreviousCommand = new RelayCommand(PreviousCommandExecute, PreviousCommandCanExecute)); }
        }

        private void PreviousCommandExecute()
        {
            if (CurrentIndex > 0)
                --CurrentIndex;
        }

        private bool PreviousCommandCanExecute()
        {
            if (CurrentIndex > 0)
                return true;
            else
                return false;
        }

        #endregion // Commands

        #region Properties

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

        #region Fields

        private string saveFile = "";

        int currentIndex = -1;

        #endregion // Fields
    }
}