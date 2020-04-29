using System;
using System.Runtime.Serialization;

namespace StudentStorage.Collection
{
    [Serializable]
    public class Group<TKey, TValue> : BinaryTree<TKey, TValue>, ICloneable, IComparable<Group<TKey, TValue>> where TKey : IComparable<TKey> where TValue : Student, IComparable<TValue>, ICloneable, new()
    {
        public string GroupName { get; set; }
        private SerializationInfo siInfo;
        public Group()
        { }
        public Group(string name)
        {
            GroupName = name;
        }
        public Group(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            if (info == null)
                throw new System.ArgumentNullException("info");
            siInfo = info;
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
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("GroupName", GroupName);
            base.GetObjectData(info, context);
        }

        public override void OnDeserialization(object sender)
        {
            base.OnDeserialization(sender);
            GroupName = siInfo.GetString("GroupName");
        }
    }
}
