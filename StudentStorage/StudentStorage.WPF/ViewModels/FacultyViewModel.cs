using StudentStorage.Collection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentStorage.WPF.ViewModels
{
    public class FacultyViewModel
    {
        public FacultyViewModel()
        {
            this.Groups = new ObservableCollection<GroupViewModel>();
        }
        public string Name { get; set; }
        public ObservableCollection<GroupViewModel> Groups { get; set; }
    }
}
