using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace StudentStorage.Collection
{
    [Serializable]
    public class BinaryTree<TKey, TValue> : ICollection<Node<TKey, TValue>>, IComparable<BinaryTree<TKey, TValue>>, ICloneable, ISerializable, IDeserializationCallback where TKey : IComparable<TKey> where TValue : IComparable<TValue>, ICloneable, new()
    {
        protected Node<TKey, TValue> Root;
        protected IComparer<Node<TKey, TValue>> comparer;
        private SerializationInfo siInfo;
        public BinaryTree() : this(Comparer<Node<TKey, TValue>>.Default) { }
        public BinaryTree(IComparer<Node<TKey, TValue>> defaultComparer)
        {
            if (defaultComparer == null)
                throw new ArgumentNullException("Default comparer is null");
            comparer = defaultComparer;
        }
        protected BinaryTree(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new System.ArgumentNullException("info");
            siInfo = info;
        }
        public BinaryTree(IEnumerable<Node<TKey, TValue>> collection) : this(collection, Comparer<Node<TKey, TValue>>.Default) { }
        public BinaryTree(IEnumerable<Node<TKey, TValue>> collection, IComparer<Node<TKey, TValue>> defaultComparer) : this(defaultComparer)
        {
            AddRange(collection);
        }
        public Node<TKey, TValue> MinValue
        {
            get
            {
                if (Root == null)
                    throw new InvalidOperationException("Tree is empty");
                var current = Root;
                while (current.Left != null)
                    current = current.Left;
                return current;
            }
        }
        public Node<TKey, TValue> MaxValue
        {
            get
            {
                if (Root == null)
                    throw new InvalidOperationException("Tree is empty");
                var current = Root;
                while (current.Right != null)
                    current = current.Right;
                return current;
            }
        }
        public void AddRange(IEnumerable<Node<TKey, TValue>> colletion)
        {
            foreach (var value in colletion)
                Add(value);
        }
        public IEnumerable<Node<TKey, TValue>> Inorder()
        {
            if (Root == null)
                yield break;

            var stack = new Stack<Node<TKey, TValue>>();
            var node = Root;

            while (stack.Count > 0 || node != null)
            {
                if (node == null)
                {
                    node = stack.Pop();
                    yield return node;
                    node = node.Right;
                }
                else
                {
                    stack.Push(node);
                    node = node.Left;
                }
            }
        }
        public IEnumerable<Node<TKey, TValue>> Preorder()
        {
            if (Root == null)
                yield break;

            var stack = new Stack<Node<TKey, TValue>>();
            stack.Push(Root);

            while (stack.Count > 0)
            {
                var node = stack.Pop();
                yield return node;
                if (node.Right != null)
                    stack.Push(node.Right);
                if (node.Left != null)
                    stack.Push(node.Left);
            }
        }
        public IEnumerable<Node<TKey, TValue>> Postorder()
        {
            if (Root == null)
                yield break;

            var stack = new Stack<Node<TKey, TValue>>();
            var node = Root;

            while (stack.Count > 0 || node != null)
            {
                if (node == null)
                {
                    node = stack.Pop();
                    if (stack.Count > 0 && node.Right == stack.Peek())
                    {
                        stack.Pop();
                        stack.Push(node);
                        node = node.Right;
                    }
                    else
                    {
                        yield return node;
                        node = null;
                    }
                }
                else
                {
                    if (node.Right != null)
                        stack.Push(node.Right);
                    stack.Push(node);
                    node = node.Left;
                }
            }
        }
        public IEnumerable<Node<TKey, TValue>> Levelorder()
        {
            if (Root == null)
                yield break;

            var queue = new Queue<Node<TKey, TValue>>();
            queue.Enqueue(Root);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                yield return node;
                if (node.Left != null)
                    queue.Enqueue(node.Left);
                if (node.Right != null)
                    queue.Enqueue(node.Right);
            }
        }
        public int Count { get; protected set; } = 0;
        public int Height { get; protected set; } = 0;
        private Node<TKey, TValue> RotateR(Node<TKey, TValue> parent)
        {
            Node<TKey, TValue> pivot = parent.Right;
            parent.Right = pivot.Left;
            if (pivot.Left != null)
                pivot.Left.Parent = parent;
            pivot.Parent = parent.Parent;
            parent.Parent = pivot;
            pivot.Left = parent;
            return pivot;
        }
        private Node<TKey, TValue> RotateL(Node<TKey, TValue> parent)
        {
            Node<TKey, TValue> pivot = parent.Left;
            parent.Left = pivot.Right;
            if (pivot.Right != null)
                pivot.Right.Parent = parent;
            pivot.Parent = parent.Parent;
            parent.Parent = pivot;
            pivot.Right = parent;
            return pivot;
        }
        private Node<TKey, TValue> RotateRL(Node<TKey, TValue> parent)
        {
            Node<TKey, TValue> pivot = parent.Left;
            parent.Left = RotateR(pivot);
            var result = RotateL(parent);
            result.Left.Parent = result;
            return result;
        }
        private Node<TKey, TValue> RotateLR(Node<TKey, TValue> parent)
        {
            Node<TKey, TValue> pivot = parent.Right;
            parent.Right = RotateL(pivot);
            var result = RotateR(parent);
            result.Right.Parent = result;
            return result;
        }
        private int GetHeight(Node<TKey, TValue> parent)
        {
            int height = 0;
            if (parent != null)
            {
                int l = GetHeight(parent.Left);
                int r = GetHeight(parent.Right);
                int m = (l > r) ? l : r;
                height = m + 1;
            }
            return height;
        }
        private Node<TKey, TValue> BalanceTree(Node<TKey, TValue> parent)
        {
            int bfactor = BalanceFactor(parent);
            if (bfactor > 1)
            {
                parent = (BalanceFactor(parent.Left) > 0) ? RotateL(parent) : RotateLR(parent);
            }
            else if (bfactor < -1)
            {
                parent = (BalanceFactor(parent.Right) > 0) ? RotateRL(parent) : RotateR(parent);
            }
            return parent;
        }
        private int BalanceFactor(Node<TKey, TValue> parent)
        {
            int l = GetHeight(parent.Left);
            int r = GetHeight(parent.Right);
            int b_factor = l - r;
            return b_factor;
        }
        private Node<TKey, TValue> SearchDeep(TKey key = default(TKey), TValue value = default(TValue))
        {
            if (Root == null)
                return null;
            var current = Root;
            while (current != null)
            {
                if (key.CompareTo(default(TKey)) != 0)
                {
                    if (current.Key.Equals(key))
                        return current;
                }
                else if (value.CompareTo(default(TValue)) != 0)
                {
                    if (current.Value.Equals(value))
                        return current;
                }
                current = (key.CompareTo(current.Key) < 0) ? current.Left : current.Right;
            }
            return current;
        }
        private Node<TKey, TValue> SearchWide(TKey key = default(TKey), TValue value = default(TValue))
        {
            if (key.CompareTo(default(TKey)) != 0)
            {
                foreach (var node in Levelorder())
                {
                    if (node.Key.CompareTo(key) == 0)
                        return node;
                }
            }
            else if (value.CompareTo(default(TValue)) != 0)
            {
                foreach (var node in Levelorder())
                {
                    if (node.Value.CompareTo(value) == 0)
                        return node;
                }
            }

            return null;
        }
        public virtual void Add(Node<TKey, TValue> node)
        {
            if (Root == null)
                Root = node;
            else
            {
                Node<TKey, TValue> current = Root, parent = null;
                while (current != null)
                {
                    parent = current;
                    current = (current.Key.CompareTo(node.Key) > 0) ?
                        current = current.Left :
                        current = current.Right;
                }
                current = node;
                current.Parent = parent;
                if (parent.Key.CompareTo(node.Key) > 0)
                {
                    parent.Left = current;
                }
                else
                {
                    parent.Right = current;
                }

                if (parent.Parent != null)
                {
                    Node<TKey, TValue> balancedNode = parent.Parent;
                    while (balancedNode != null)
                    {
                        balancedNode = BalanceTree(balancedNode);
                        if (balancedNode.Parent == null)
                        {
                            Root = balancedNode;
                            break;
                        }
                        else
                        {
                            if (balancedNode.Key.CompareTo(balancedNode.Parent.Key) < 0)
                            {
                                balancedNode.Parent.Left = balancedNode;
                            }
                            else if (balancedNode.Key.CompareTo(balancedNode.Parent.Key) > 0)
                            {
                                balancedNode.Parent.Right = balancedNode;
                            }
                            balancedNode = balancedNode.Parent;
                        }
                    }
                }
            }
            ++Count;
        }
        public virtual bool Remove(Node<TKey, TValue> node)
        {
            if (Root == null)
                return false;
            Node<TKey, TValue> current = Root, parent = null;
            int result;
            do
            {
                result = current.Key.CompareTo(node.Key);
                if (result > 0)
                {
                    parent = current;
                    current = current.Left;
                }
                else if (result < 0)
                {
                    parent = current;
                    current = current.Right;
                }
                if (current == null)
                    return false;
            }
            while (result != 0);
            if (current.Right == null)
            {
                if (current == Root)
                    Root = current.Left;
                else
                {
                    result = current.Key.CompareTo(parent.Key);
                    if (result < 0)
                        parent.Left = current.Left;
                    else
                        parent.Right = current.Left;
                }
            }
            else if (current.Right.Left == null)
            {
                current.Right.Left = current.Left;
                if (current == Root)
                    Root = current.Right;
                else
                {
                    result = current.Key.CompareTo(parent.Key);
                    if (result < 0)
                        parent.Left = current.Right;
                    else
                        parent.Right = current.Right;
                }
            }
            else
            {
                Node<TKey, TValue> min = current.Right.Left, prev = current.Right;
                while (min.Left != null)
                {
                    prev = min;
                    min = min.Left;
                }
                prev.Left = min.Right;
                min.Left = current.Left;
                min.Right = current.Right;

                if (current == Root)
                    Root = min;
                else
                {
                    result = current.Key.CompareTo(parent.Key);
                    if (result < 0)
                        parent.Left = min;
                    else
                        parent.Right = min;
                }
            }
            --Count;
            return true;
        }
        public void Add(TKey key, TValue value)
        {
            Node<TKey, TValue> node = new Node<TKey, TValue>(key, value);
            Add(node);
        }
        public void Add(TValue value)
        {
            TKey key = (Root != null) ? this.MaxValue.Key : default(TKey);
            Node<TKey, TValue> node = new Node<TKey, TValue>(key, value);
            Add(node);
        }
        public virtual bool Remove(TKey key, bool searchtype)
        {
            var node = (searchtype) ?
                SearchDeep(key) :
                SearchWide(key);
            return Remove(node);
        }
        public virtual bool Remove(TValue value, bool searchtype)
        {
            var node = (searchtype) ?
                SearchDeep(default(TKey), value) :
                SearchWide(default(TKey), value);
            return Remove(node);
        }
        public bool TryGetValue(TKey key, out TValue value, bool searchtype)
        {
            Node<TKey, TValue> result;
            result = (searchtype) ?
                SearchDeep(key) :
                SearchWide(key);
            if (result == null)
                value = default(TValue);
            else
                value = result.Value;
            return (result != null) ? true : false;
        }
        public bool ContainsValue(TValue value, bool searchtype)
        {
            Node<TKey, TValue> result;
            result = (searchtype) ?
                SearchDeep(default(TKey), value) :
                SearchWide(default(TKey), value);
            return (result != null) ? true : false;
        }
        public void Clear()
        {
            Root = null;
            Count = 0;
        }
        public void CopyTo(Node<TKey, TValue>[] array, int arrayIndex)
        {
            foreach (var value in Inorder())
                array[arrayIndex++] = value;
            foreach (var value in array)
                value.Left = value.Right = value.Parent = null;
        }
        public virtual bool IsReadOnly
        {
            get
            {
                return false;
            }
        }
        public bool Contains(Node<TKey, TValue> item)
        {
            var current = Root;
            while (current != null)
            {
                var result = comparer.Compare(item, current);
                if (result == 0)
                    return true;
                if (result < 0)
                    current = current.Left;
                else
                    current = current.Right;
            }
            return false;
        }
        public IEnumerator<Node<TKey, TValue>> GetEnumerator()
        {
            return Inorder().GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public object Clone()
        {
            Type T = this.GetType();
            BinaryTree<TKey, TValue> tempTree = (BinaryTree<TKey, TValue>)Activator.CreateInstance(T);
            //foreach(var value in Inorder())

            return tempTree;
        }

        public int CompareTo(BinaryTree<TKey, TValue> other)
        {
            return this.Count.CompareTo(other.Count);
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");
            info.AddValue("Count", Count); //This is the length of the bucket array.
            info.AddValue("Comparer", comparer, typeof(IComparer<Node<TKey, TValue>>));
            info.AddValue("MaxValue", MaxValue, typeof(Node<TKey, TValue>));
            info.AddValue("MinValue", MinValue, typeof(Node<TKey, TValue>));

            if (Root != null)
            {
                Node<TKey, TValue>[] items = new Node<TKey, TValue>[Count];
                CopyTo(items, 0);
                info.AddValue("Items", items, typeof(Node<TKey, TValue>[]));
            }
        }

        public virtual void OnDeserialization(object sender)
        {
            if (comparer != null)
            {
                return;
            }

            if (siInfo == null)
            {
                throw new SerializationException("InvalidOnDeser");//Serialization_InvalidOnDeser
            }

            comparer = (IComparer<Node<TKey, TValue>>)siInfo.GetValue("Comparer", typeof(IComparer<Node<TKey, TValue>>));
            int savedCount = siInfo.GetInt32("Count");

            if (savedCount != 0)
            {
                Node<TKey, TValue>[] items = (Node<TKey, TValue>[])siInfo.GetValue("Items", typeof(Node<TKey, TValue>[]));

                if (items == null)
                    throw new SerializationException("MissingValues");

                for (int i = 0; i < items.Length; i++)
                {
                    Add(items[i]);
                }

                if (Count != savedCount)
                {
                    throw new SerializationException("MismatchedCount");
                }
                siInfo = null;
            }
        }
    }
}
