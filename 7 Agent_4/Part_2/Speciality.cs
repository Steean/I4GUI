using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Part_2
{
    public class AgentSpeciality : ObservableCollection<string>
    {
        public AgentSpeciality()
        {
            Add("Assasination");
            Add("Martini");
            Add("Dying");
        }
    }
}
