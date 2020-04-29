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
        public GroupViewModel(Group<int, Student> group, FacultyViewModel parent)
        {
            this.Name = group.GroupName;
            this.Parent = parent;
            this.Students = new ObservableCollection<StudentViewModel>();
            foreach(var student in group)
            {
                StudentViewModel s = new StudentViewModel(student.Value, this);
                s.Key = student.Key;
                this.Students.Add(s);
            }
        }

        public GroupViewModel(Group<int, Student> group, FacultyViewModel parent, List<ScoolarshipRule> rules)
        {
            this.Name = group.GroupName;
            this.Parent = parent;
            this.Students = new ObservableCollection<StudentViewModel>();
            foreach (var student in group)
            {
                StudentViewModel s = new StudentViewModel(student.Value, this, rules);
                s.Key = student.Key;
                this.Students.Add(s);
            }
        }
        public string Name { get; set; }
        public int Key { get; set; }
        public ObservableCollection<StudentViewModel> Students { get; set; }
        public FacultyViewModel Parent { get; set; }
        public int SponsoredCount { get; set; }
        public string AM
        {
            get
            {
                double res = 0;
                foreach(var s in this.Students)
                    res += Double.Parse(s.AM);
                res /= this.Students.Count;
                return String.Format("{0:0.#}", res.ToString());
            }
        }
    }
}
