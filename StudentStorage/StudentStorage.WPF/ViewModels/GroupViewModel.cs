using StudentStorage.Collection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentStorage.WPF.ViewModels
{
    public class GroupViewModel
    {
        public GroupViewModel()
        {
            this.Students = new ObservableCollection<StudentViewModel>();
        }
        public string Name { get; set; }
        public ObservableCollection<StudentViewModel> Students { get; set; }
    }
}
