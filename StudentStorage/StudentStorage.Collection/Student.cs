using System;
using System.Runtime.Serialization;

namespace StudentStorage.Collection
{
    [Serializable]
    public class Student : ICloneable, IComparable<Student>
    {
        private string fName, lName, mName;
        private DateTime birthDate;
        private float midGrade;

        public string FirstName
        {
            get { return fName; }
            set { fName = value ?? null; }
        }

        public string LastName
        {
            get { return lName; }
            set { lName = value ?? null; }
        }

        public string MiddleName
        {
            get { return mName; }
            set { mName = value ?? null; }
        }

        public DateTime BirthDate
        {
            get { return birthDate; }
            set { birthDate = value; }
        }

        public float MiddleGrade
        {
            get { return midGrade; }
            set { midGrade = (value >= 0 && value <= 100) ? value : 0; }
        }

        public Student()
        {
            FirstName = LastName = MiddleName = default(string);
            BirthDate = default(DateTime);
            MiddleGrade = default(float);
        }

        public Student(string fName, string lName, string mName, DateTime bDate, float mGrade = 0)
        {
            FirstName = fName;
            LastName = lName;
            MiddleName = mName;
            BirthDate = bDate;
            MiddleGrade = mGrade;
        }

        public Student(Student student)
        {
            FirstName = student.FirstName;
            LastName = student.LastName;
            MiddleName = student.MiddleName;
            BirthDate = student.BirthDate;
            MiddleGrade = student.MiddleGrade;
        }

        public Student(SerializationInfo info, StreamingContext context)
        {

        }

        public override bool Equals(object obj)
        {
            Student studObj;
            if ((studObj = obj as Student) != null)
            {
                return FirstName == studObj.FirstName &&
                       LastName == studObj.LastName &&
                       MiddleName == studObj.MiddleName &&
                       BirthDate == studObj.BirthDate &&
                       MiddleGrade == studObj.MiddleGrade;
            }

            return false;
        }

        public override string ToString()
        {
            return LastName + ' ' + FirstName + ' ' + MiddleName + " Birth Date: " + BirthDate.Date + " Middle Grade: " + MiddleGrade;
        }

        public override int GetHashCode()
        {
            var multiplier = 59;
            int res = 1;
            res *= multiplier + (FirstName == null ? 43 : FirstName.GetHashCode());
            res *= multiplier + (LastName == null ? 43 : LastName.GetHashCode());
            res *= multiplier + (MiddleName == null ? 43 : MiddleName.GetHashCode());
            res *= multiplier + BirthDate.GetHashCode();
            res *= multiplier + MiddleGrade.GetHashCode();
            return res;
        }

        public bool Equals(Student student)
        {
            //
            return FirstName == student.FirstName &&
                   LastName == student.LastName &&
                   MiddleName == student.MiddleName &&
                   BirthDate == student.BirthDate &&
                   MiddleGrade == student.MiddleGrade;
        }

        public object Clone()
        {
            return new Student(FirstName, LastName, MiddleName, BirthDate, MiddleGrade);
        }

        public static int CompareTo(Student student1, Student student2)
        {
            return ReferenceEquals(student1, null) ? -1 : student1.CompareTo(student2);
        }

        public int CompareTo(Student student2)
        {
            if (ReferenceEquals(student2, null))
            {
                return 1;
            }

            int result = string.CompareOrdinal(LastName, student2.LastName);
            if (result == 0)
            {
                result = string.CompareOrdinal(FirstName, student2.FirstName);
                if (result == 0)
                {
                    result = string.CompareOrdinal(MiddleName, student2.MiddleName);
                    if (result == 0)
                    {
                        result = BirthDate.CompareTo(student2.BirthDate);
                        if (result == 0)
                        {
                            result = MiddleGrade.CompareTo(student2.MiddleGrade);
                        }
                    }
                }
            }

            return result;
        }

        public static bool operator ==(Student student1, Student student2)
        {
            return CompareTo(student1, student2) == 0;
        }

        public static bool operator !=(Student student1, Student student2)
        {
            return CompareTo(student1, student2) != 0;
        }

        public static bool operator <(Student student1, Student student2)
        {
            return CompareTo(student1, student2) < 0;
        }
        public static bool operator >(Student student1, Student student2)
        {
            return CompareTo(student1, student2) > 0;
        }

        public static bool operator >=(Student student1, Student student2)
        {
            return CompareTo(student1, student2) >= 0;
        }

        public static bool operator <=(Student student1, Student student2)
        {
            return CompareTo(student1, student2) <= 0;
        }
    }
}
