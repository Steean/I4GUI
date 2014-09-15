using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    public class Priorities : ObservableCollection<string>
    {
        public Priorities()
        {
            Add("High");
            Add("Normal");
            Add("Low");
        }
    }
}
