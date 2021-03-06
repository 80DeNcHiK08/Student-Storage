﻿using System;

namespace StudentStorage.Collection
{
    [Serializable]
    public class Node<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }
        public Node<TKey, TValue> Left { get; set; }
        public Node<TKey, TValue> Right { get; set; }
        public Node<TKey, TValue> Parent { get; set; }
        public Node<TKey, TValue> Prev { get; set; }
        public Node<TKey, TValue> Next { get; set; }
        public Node(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
}
