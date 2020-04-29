using System;
using System.Runtime.Serialization;

namespace StudentStorage.Collection
{
    [Serializable]
    public class Faculty<TKey, TValue> : BinaryTree<TKey, TValue>, ICloneable, IComparable<Faculty<TKey, TValue>> where TKey : IComparable<TKey> where TValue : Group<TKey, Student>, IComparable<TValue>, ICloneable, new()
    {
        public string FacultyName { get; set; }
        private SerializationInfo siInfo;
        public Faculty()
        { }
        public Faculty(string name)
        {
            FacultyName = name;
        }
        public Faculty(SerializationInfo info, StreamingContext context) : base(info, context) 
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

        public int CompareTo(Faculty<TKey, TValue> faculty2)
        {
            return string.CompareOrdinal(this.FacultyName, faculty2.FacultyName);
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("FacultyName", FacultyName);
            base.GetObjectData(info, context);
        }

        public override void OnDeserialization(object sender)
        {
            base.OnDeserialization(sender);
            FacultyName = siInfo.GetString("FacultyName");
        }
    }
}
