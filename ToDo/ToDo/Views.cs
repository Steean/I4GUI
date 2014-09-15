using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo
{
    public class Views : ObservableCollection<string>
    {
        public Views()
        {
            Add("Todo");
            Add("Due today");
            Add("Completed");
        }
    }
}
