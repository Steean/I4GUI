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
using System.Windows.Data;

namespace Part_2
{
    public class Agents : ObservableCollection<Agent>, INotifyPropertyChanged
    {
        public Agents()
        {
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
            NotifyPropertyChanged("Count");
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
            get { return _removeCommand ?? (_removeCommand = new RelayCommand(RemoveAgent, RemoveAgent_CanExecute)); }
        }

        private void RemoveAgent()
        {           
            RemoveAt(CurrentIndex);            
            NotifyPropertyChanged("Count");
        }

        private bool RemoveAgent_CanExecute()
        {
            if (Count > 0 && CurrentIndex >= 0)
                return true;
            else
                return false;
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
            NotifyPropertyChanged("Count");
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

        public IReadOnlyCollection<string> FilterSpecialities
        {
            get
            {
                ObservableCollection<string> result = new ObservableCollection<string>();
                result.Add("All");
                foreach (var s in new AgentSpeciality())
                    result.Add(s);
                return result;
            }
        }

        int currentSpecialityIndex = 0;

        public int CurrentSpecialityIndex
        {
            get { return currentSpecialityIndex; }
            set
            {
                if (currentSpecialityIndex != value)
                {
                    ICollectionView cv = CollectionViewSource.GetDefaultView(this);
                    currentSpecialityIndex = value;
                    if (currentSpecialityIndex == 0)
                        cv.Filter = null; // Index 0 is no filter (show all)
                    else
                    {
                        filter = FilterSpecialities.ElementAt(currentSpecialityIndex);
                        cv.Filter = CollectionViewSource_Filter;
                    }
                    NotifyPropertyChanged();
                }
            }
        }

        private bool CollectionViewSource_Filter(object ag)
        {
            Agent agent = ag as Agent;
            return (agent.Speciality == filter);
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

        private string filter = "";

        #endregion // Fields
    }
}