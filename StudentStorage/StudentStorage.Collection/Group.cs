using System;

namespace StudentStorage.Collection
{
    [Serializable]
    public class Group<TKey, TValue> : BinaryTree<TKey, TValue>, ICloneable, IComparable<Group<TKey, TValue>> where TKey : IComparable<TKey> where TValue : Student, IComparable<TValue>, ICloneable, new()
    {
        public string GroupName { get; set; }
        public Group()
        { }
        public Group(string name)
        {
            GroupName = name;
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

        public int CompareTo(Group<TKey, TValue> group2)
        {
            return string.CompareOrdinal(this.GroupName, group2.GroupName);
        }
    }
}
