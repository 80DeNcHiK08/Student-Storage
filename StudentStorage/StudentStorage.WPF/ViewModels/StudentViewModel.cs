using StudentStorage.Collection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentStorage.WPF.ViewModels
{
    public class StudentViewModel
    {
        public string ConcatedName { get; set; }
        public int Age { get; set; }
        public string BirthDate { get; set; }
        public float AM { get; set; }
        public StudentViewModel(Student student)
        {
            this.ConcatedName = student.LastName + " " + student.FirstName + " " + student.MiddleName;
            this.AM = student.MiddleGrade;
            this.BirthDate = student.BirthDate.Date.ToString();
            this.Age = DateTime.Now.Year - student.BirthDate.Year - 1 + 
                ((DateTime.Now.Month > student.BirthDate.Month || DateTime.Now.Month == student.BirthDate.Month && DateTime.Now.Day >= student.BirthDate.Day) ? 1 : 0);
        }
    }
}
