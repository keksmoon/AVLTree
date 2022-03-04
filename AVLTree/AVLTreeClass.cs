using System;
using System.Collections.Generic;

namespace AVLTree
{
    /// <summary>
    /// Дерево Адельсона-Вельского и Ландиса.
    /// </summary>
    public class AVL<TKey, TValue> where TKey : IComparable<TKey>
    {
        public Node<TKey, TValue> Root { get; private set; }

        public AVL() => Root = null;

        /// <summary>
        /// Количество элементов в АВЛ дереве.
        /// </summary>
        public int Count { get; private set; } = 0;

        /// <summary>
        /// Возвращает высоту выбранного поддерева. 
        /// </summary>
        public int Height(Node<TKey, TValue> node)
        {
            if (node == null)
            {
                return 0;
            }

            return node.Height;
        }

        public void Clear()
        {
            Root = null;
            Count = 0;
        }

        /// <summary>
        /// Метод, возвращающий высоту всего дерева от корня.
        /// </summary>
        public int Height() => Height(Root);

        /// <summary>
        /// Высчитывает баланс узла как разность высот его левого и правого поддеревьев.
        /// </summary>
        public int GetBalance(Node<TKey, TValue> node)
        {
            if (node == null)
            {
                return 0;
            }

            return Height(node.Left) - Height(node.Right);
        }

        /// <summary>
        /// Добавление в дерево нового элемента.
        /// </summary>
        public void Insert(TKey key, TValue value)
        {
            if (Root == null)
            {
                Root = new Node<TKey, TValue>(key, value);
            }
            else
            {
                var parentNode = Find(key);
                var resultComparison = parentNode.Key.CompareTo(key);

                if (resultComparison == 0)
                {
                    throw new DuplicationItemsInTreeException();
                }
                else if (resultComparison < 0)
                {
                    parentNode.Right = new Node<TKey, TValue>(key, value, parentNode);
                }
                else
                {
                    parentNode.Left = new Node<TKey, TValue>(key, value, parentNode);
                }

                parentNode.RecalculateHeight();
                BalanceTree(parentNode);
            }

            Count++;
        }
        /// <summary>
        /// Удаление элемента из дерева по ключу.
        /// </summary>
        public void Remove(TKey key)
        {
            var node = Find(key);

            if (node.Key.CompareTo(key) != 0)
                throw new KeyNotFoundException();

            if (node.Left == null || node.Right == null)
            {
                var v = node.Left ?? node.Right;
                var tmp = node.Parent;

                if (tmp == null)
                    Root = v;
                else if (node == tmp.Left)
                    tmp.Left = v;
                else
                    tmp.Right = v;

                if (v != null)
                    v.Parent = tmp;

                tmp.RecalculateHeight();

                BalanceTree(tmp);
            }
            else
            {
                var newNode = node.Left;

                bool inLeft = true;
                while (newNode.Right != null)
                {
                    inLeft = false;
                    newNode = newNode.Right;
                }

                var tmp = newNode;

                if (tmp.Left != null)
                {
                    tmp.Parent.Left = tmp.Left;
                    tmp.Left = tmp.Parent;
                }
                else
                {
                    if (!inLeft)
                        tmp.Parent.Right = null;
                    else
                        tmp.Parent.Left = null;

                    tmp.Parent.RecalculateHeight();
                }

                node.Key = tmp.Key;
                node.Value = tmp.Value;

                // а зачем нам спускаться непонятно куда??
                while (node.Right != null || node.Left != null)
                {
                    node = node.Right ?? node.Left;
                }

                BalanceTree(node);
            }

            Count--;
        }

        /// <summary>
        /// Поиск значения элемента в дереве по его ключу.
        /// </summary>
        /// <param name="key">Ключ для поиска.</param>
        /// <param name="value">Значение value будет задано, если такой элемент найдется.</param>
        /// <returns>True, если элемент найден и False в ином случае.</returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            Node<TKey, TValue> current = Find(key);
            if (current.Key.CompareTo(key) != 0)
            {
                value = default(TValue);
                return false;
            }

            value = current.Value;
            return true;
        }

        /// <summary>
        /// Выполняет поиск узла по ключу.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Если узел найден, то возращает узел. В противном случае родительский узел.</returns>
        private Node<TKey, TValue> Find(TKey key)
        {
            var current = Root;
            while (current != null)
            {
                int result = current.Key.CompareTo(key);

                if (result > 0)
                {
                    if (current.Left == null)
                        break;
                    current = current.Left ?? current;
                }
                else if (result < 0)
                {
                    if (current.Right == null)
                        break;
                    current = current.Right ?? current;
                }
                else
                {
                    break;
                }
            }

            return current;
        }

        /// <summary>
        /// Выполняет балансировку дереву.
        /// </summary>
        public void BalanceTree(Node<TKey, TValue> node)
        {
            //Балансировка необходима <=> когда высота левого и правого поддеревьев = 2
            //идея алгоритма балансировки: 
            //от узла вставки делаем переход к его родителям пока не вернемся в корень
            //на каждом узле в процессе обхода считаем баланс. 
            // Если баланс = 2 -> левое поддерево длинее правого -> RotateRight
            // Если баланс = -2 -> RotateLeft

            while (node != null)
            {
                int balance = GetBalance(node);

                if (balance == 2)
                {
                    int leftbalance = GetBalance(node.Left);

                    if (leftbalance == 1)
                    {
                        RotateRight(node);
                    }
                    else
                    {
                        RotateLeft(node.Left);
                        RotateRight(node);
                    }

                }
                else if (balance == -2)
                {
                    int rightbalance = GetBalance(node.Right);

                    if (rightbalance == -1)
                    {
                        RotateLeft(node);
                    }
                    else
                    {
                        RotateRight(node.Right);
                        RotateLeft(node);
                    }
                }

                node = node.Parent;
            }
        }

        /// <summary>
        /// Осуществляет поворот АВЛ-поддерева налево.
        /// </summary>
        internal void RotateLeft(Node<TKey, TValue> node)
        {
            Node<TKey, TValue> newNode = node.Right;
            node.Right = newNode.Left;
            if (newNode.Left != null)
            {
                newNode.Left.Parent = node;
            }

            newNode.Parent = node.Parent;

            if (node.Parent == null)
            {
                Root = newNode;
            }
            else if (node == node.Parent.Left)
            {
                node.Parent.Left = newNode;
            }
            else
            {
                node.Parent.Right = newNode;
            }

            newNode.Left = node;
            node.Parent = newNode;

            node.RecalculateHeight();
            newNode.RecalculateHeight();
        }

        /// <summary>
        /// Осуществляет поворот АВЛ-поддерева направо.
        /// </summary>
        internal void RotateRight(Node<TKey, TValue> node)
        {
            Node<TKey, TValue> newNode = node.Left;
            node.Left = newNode.Right;
            if (newNode.Right != null)
            {
                newNode.Right.Parent = node;
            }

            newNode.Parent = node.Parent;

            if (node.Parent == null)
            { 
                Root = newNode;
            }
            else if (node == node.Parent.Right)
            { 
                node.Parent.Right = newNode;
            }
            else
            {
                node.Parent.Left = newNode;
            }

            newNode.Right = node;
            node.Parent = newNode;

            node.RecalculateHeight();
            newNode.RecalculateHeight();
        }

        /// <summary>
        /// Выполняет прямой обход дерева.
        /// </summary>
        /// <returns>Возвращает список узлов.</returns>
        public IEnumerable<Node<TKey, TValue>> InorderTraversal()
        {
            var nodes = new List<Node<TKey, TValue>>();

            InorderTraversal(Root, nodes);

            return nodes;
        }

        /// <summary>
        /// Обход дерева
        /// </summary>
        /// <param name="node">Вершина</param>
        /// <param name="children">Коллекция обхода</param>
        internal void InorderTraversal(Node<TKey, TValue> node, List<Node<TKey, TValue>> children)
        {
            if (node != null)
            {
                InorderTraversal(node.Left, children);
                children.Add(node);
                InorderTraversal(node.Right, children);
            }
        }

        /// <summary>
        /// Реализует поиск значения по индексу (ключу) и может изменять значения найденного ключа.
        /// </summary>
        /// <param name="key">Ключ для поиска</param>
        public TValue this[TKey key]
        {
            get
            {
                TValue value;
                bool keyIsFound = TryGetValue(key, out value);

                if (!keyIsFound)
                {
                    throw new KeyNotFoundException();
                }

                return value;
            }

            set
            {
                var node = Find(key);
                if (this.Root.Key.Equals(key))
                {
                    this.Root.Value = value;
                }
                else if (node.Key.Equals(key))
                {
                    node.Value = value;
                }
                else
                {
                    throw new KeyNotFoundException();
                }
            }
        }
    }
}
