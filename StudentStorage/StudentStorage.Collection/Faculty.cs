using System;

namespace StudentStorage.Collection
{
    [Serializable]
    public class Faculty<TKey, TValue> : BinaryTree<TKey, TValue>, ICloneable, IComparable<Faculty<TKey, TValue>> where TKey : IComparable<TKey> where TValue : Group<TKey, Student>, IComparable<TValue>, ICloneable, new()
    {
        public string FacultyName { get; set; }
        public Faculty()
        { }
        public Faculty(string name)
        {
            FacultyName = name;
        }
        public override string ToString()
        {
            string result = "";
            foreach (var g in this)
            {
                result += g.Value.ToString() + "\n";
            }
            return result;
        }

        public override int GetHashCode()
        {
            var multiplier = 59;
            int res = 1;
            foreach (var g in this)
            {
                res *= multiplier + g.Value.GetHashCode();
            }
            return res;
        }

        public int CompareTo(Faculty<TKey, TValue> faculty2)
        {
            return string.CompareOrdinal(this.FacultyName, faculty2.FacultyName);
        }
    }
}
