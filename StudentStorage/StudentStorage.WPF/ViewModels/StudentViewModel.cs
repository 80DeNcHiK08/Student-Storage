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
        public string ShortName { get; set; }
        public int Age { get; set; }
        public string BirthDate { get; set; }
        public string AM { get; set; }
        public int Key { get; set; }
        public GroupViewModel Parent { get; set; }
        public string SponsorType { get; set; }
        public StudentViewModel(Student student, GroupViewModel parent)
        {
            this.Parent = parent;
            this.ConcatedName = student.LastName + " " + student.FirstName + " " + student.MiddleName;
            this.ShortName = student.LastName + " " + student.FirstName.Substring(1) + ". " + student.MiddleName.Substring(1) + ".";
            this.AM = String.Format("{0:0.#}", student.MiddleGrade.ToString());
            this.BirthDate = student.BirthDate.Date.ToString();
            this.Age = DateTime.Now.Year - student.BirthDate.Year - 1 + 
                ((DateTime.Now.Month > student.BirthDate.Month || DateTime.Now.Month == student.BirthDate.Month && DateTime.Now.Day >= student.BirthDate.Day) ? 1 : 0);
        }

        public StudentViewModel(Student student, GroupViewModel parent, List<ScoolarshipRule> rules)
        {
            this.Parent = parent;
            this.ConcatedName = student.LastName + " " + student.FirstName + " " + student.MiddleName;
            this.AM = String.Format("{0:0.#}", student.MiddleGrade.ToString());
            this.BirthDate = student.BirthDate.Date.ToString();
            this.Age = DateTime.Now.Year - student.BirthDate.Year - 1 +
                ((DateTime.Now.Month > student.BirthDate.Month || DateTime.Now.Month == student.BirthDate.Month && DateTime.Now.Day >= student.BirthDate.Day) ? 1 : 0);
            this.SponsorType = GetSponsorType(rules);
        }
        public StudentViewModel() {  }

        private string GetSponsorType(List<ScoolarshipRule> rules)
        {
            foreach(var rule in rules)
            {
                if(float.Parse(this.AM) >= rule.MinMark && ((this.Parent.SponsoredCount * 100) / (this.Parent.Students.Count + 1)) <= rule.PercToReach)
                {
                    this.Parent.SponsoredCount++;
                    return rule.Name;
                }
            }
            return "none";
        }
    }
}
