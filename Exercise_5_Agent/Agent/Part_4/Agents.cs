using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MvvmFoundation.Wpf;

namespace Part_4
{
    public class Agents : ObservableCollection<Agent>
    {
        public Agents()
        {
            Add(new Agent("007", "James Bond", "Assassination", "Red"));
            Add(new Agent("1", "Karate KID!!!", "Sniper", "Blue"));
        }

        public ICommand _PreviousCommand;

        public ICommand PreviousCommand
        {
            get
            {
                return _PreviousCommand ??
                       (_PreviousCommand = new RelayCommand(
                                               PreviousCommandExecute, PreviousCommandCanExecute));
            }
        }

        private void PreviousCommandExecute()
        {
            if (LbxAgents.SelectedIndex > 0)
                LbxAgents.SelectedIndex--;
        }

        private bool PreviousCommandCanExecute()
        {
            if (LbxAgents.SelectedIndex > 0)
                return true;
            else
                return false;
        }

    }
}
