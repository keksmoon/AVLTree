using System;

namespace AVLTree
{
    /// <summary>
    /// Узел (вершина) дерева.
    /// </summary>
    /// <typeparam name="TKey">Ключ вершины.</typeparam>
    /// <typeparam name="TValue">Значение вершины.</typeparam>
    public class Node<TKey, TValue>
    {
        public TKey Key { get; internal set; } 
        public TValue Value { get; internal set; }

        public Node<TKey, TValue> Parent { get; internal set; }
        public Node<TKey, TValue> Left { get; internal set; }
        public Node<TKey, TValue> Right { get; internal set; }

        public int Height { get; internal set; } = 1;

        public Node(TKey key, TValue value)
        {
            this.Value = value;
            this.Key = key;
            Parent = null;
        }

        public Node(TKey key, TValue value, Node<TKey, TValue> parent)
        {
            this.Value = value;
            this.Key = key;
            this.Parent = parent;
            this.Height = 0;
        }

        /// <summary>
        /// Метод пересчета высоты на которой расположен этот узел в дереве.
        /// </summary>
        internal void RecalculateHeight()
        {
            var currentNode = this;

            while (currentNode.Parent != null)
            {
                currentNode = currentNode.Parent;
                
                if (currentNode.Left == null)
                {
                    currentNode.Height = currentNode.Right.Height + 1;
                } else if (currentNode.Right == null)
                {
                    currentNode.Height = currentNode.Left.Height + 1;
                } else 
                {
                    int maxHeight = Math.Max(currentNode.Right.Height, currentNode.Left.Height) + 1;
                    currentNode.Height = maxHeight;
                }
            }
        }

        public override bool Equals(object obj)
        {
            Node<TKey, TValue> otherNode = obj as Node<TKey, TValue>;

            if (otherNode == null)
            {
                return false;
            }
            else
            {
                if (this.Key.Equals(otherNode.Key) && this.Value.Equals(otherNode.Value))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", Key, Value);
        }
    }
}
