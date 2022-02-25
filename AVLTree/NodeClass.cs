using System;
using System.Text;

namespace AVLTree
{
    /// <summary>
    /// Узел (вершина) дерева.
    /// </summary>
    /// <typeparam name="TKey">Ключ вершины.</typeparam>
    /// <typeparam name="TValue">Значение вершины.</typeparam>
    public class Node<TKey, TValue>
    {
        public TKey key { get; internal set; } 
        public TValue value { get; internal set; }

        public Node<TKey, TValue> parent { get; internal set; }
        public Node<TKey, TValue> left { get; internal set; }
        public Node<TKey, TValue> right { get; internal set; }

        public int height { get; internal set; } = 1;

        public Node(TKey key, TValue value)
        {
            this.value = value;
            this.key = key;
            parent = null;
        }

        public Node(TKey key, TValue value, Node<TKey, TValue> parent)
        {
            this.value = value;
            this.key = key;
            this.parent = parent;
            this.height = 0;
        }

        /// <summary>
        /// Метод пересчета высоты на которой расположен этот узел в дереве.
        /// </summary>
        public void RecalculateHeight()
        {
            var currentNode = this;

            while (currentNode.parent != null)
            {
                currentNode = currentNode.parent;
                
                if (currentNode.left == null)
                {
                    currentNode.height = currentNode.right.height + 1;
                } else if (currentNode.right == null)
                {
                    currentNode.height = currentNode.left.height + 1;
                } else 
                {
                    int maxHeight = Math.Max(currentNode.right.height, currentNode.left.height) + 1;
                    currentNode.height = maxHeight;
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
                if (this.key.Equals(otherNode.key) && this.value.Equals(otherNode.value))
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
            return string.Format("{0} {1}", key, value);
        }
    }
}
