using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentStorage.WPF.ViewModels
{
    public class ScoolarshipRule
    {
        public string Name { get; set; }
        public float MinMark { get; set; }
        public float PercToReach { get; set; }

        public ScoolarshipRule(string name, float mark, float percent)
        {
            this.Name = name;
            this.PercToReach = percent;
            this.MinMark = mark;
        }
    }
}
