using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Part_3
{
    public class Agents : ObservableCollection<Agent>
    {
        public Agents()
        {
            Add(new Agent("007", "James Bond", "Assassination", "Red"));
            Add(new Agent("1", "Karate KID!!!", "Sniper", "Blue"));
        }
    }
}
