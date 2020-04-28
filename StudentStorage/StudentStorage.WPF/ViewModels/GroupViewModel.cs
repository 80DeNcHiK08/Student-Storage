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
        public GroupViewModel(Group<int, Student> group)
        {
            this.Name = group.GroupName;
            this.Students = new ObservableCollection<StudentViewModel>();
            foreach(var student in group)
            {
                StudentViewModel s = new StudentViewModel(student.Value);
                s.Key = student.Key;
                this.Students.Add(s);
            }
        }
        public string Name { get; set; }
        public int Key { get; set; }
        public ObservableCollection<StudentViewModel> Students { get; set; }
        public string AM
        {
            get
            {
                double res = 0;
                foreach(var s in this.Students)
                    res += Double.Parse(s.AM);
                res /= this.Students.Count;
                return res.ToString().Substring(0, 5);
            }
        }
    }
}
