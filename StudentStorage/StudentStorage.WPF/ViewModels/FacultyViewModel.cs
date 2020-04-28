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
        public FacultyViewModel(Faculty<int, Group<int, Student>> faculty)
        {
            this.Name = faculty.FacultyName;
            this.Groups = new ObservableCollection<GroupViewModel>();
            foreach(var group in faculty)
            {
                GroupViewModel g = new GroupViewModel(group.Value);
                g.Key = group.Key;
                this.Groups.Add(g);
            }
            
        }
        public string Name { get; set; }
        public int Key { get; set; }
        public ObservableCollection<GroupViewModel> Groups { get; set; }
        public string AM
        {
            get
            {
                double res = 0;
                foreach (var g in this.Groups)
                    res += Double.Parse(g.AM);
                res /= this.Groups.Count;
                return res.ToString().Substring(0, 5);
            }
        }
    }
}
