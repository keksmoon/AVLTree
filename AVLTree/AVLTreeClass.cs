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
        public bool Insert(TKey key, TValue value)
        {
            Node<TKey, TValue> newItem = new Node<TKey, TValue>(key, value);

            if (Root == null)
            {
                //Если дерево пусто, заменяем его на дерево с одним узлом newItem.
                Root = newItem;
                Root.Height = 1;
                Count++;
            }
            else
            {
                var currentNode = Root;

                //Иначе необходимо:
                //  1. Найти место для вставки нового элемента.
                //     Если элемент с таким же ключом существует, то throw DuplicationItemsInTreeException.
                //  2. Если подходящее место найдено, то вставляем элемент.
                //     Иначе продолжаем поиск.

                while (currentNode != null)
                {
                    var resultComparison = newItem.Key.CompareTo(currentNode.Key);
                    // -1 если элемент для вставки меньше рассматриваемого
                    // 1 если элемент для вставки больше рассматриваемого

                    if (resultComparison < 0)
                    {
                        if (currentNode.Left == null)
                        {
                            newItem.Parent = currentNode;
                            currentNode.Left = newItem;
                            currentNode.Left.RecalculateHeight();
                            BalanceTree(currentNode);
                            Count++;

                            return true;
                        }

                        currentNode = currentNode.Left;
                    }
                    else if (resultComparison > 0)
                    {
                        if (currentNode.Right == null)
                        {
                            newItem.Parent = currentNode;
                            currentNode.Right = newItem;
                            currentNode.Right.RecalculateHeight();
                            BalanceTree(currentNode);
                            Count++;

                            return true;
                        }

                        currentNode = currentNode.Right;
                    }
                    else
                    {
                        throw new DuplicationItemsInTreeException();
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Удаление элемента из дерева по ключу.
        /// </summary>
        public bool Remove(TKey key)
        {
            var node = Find(key);

            if (node == null)
            {
                throw new KeyNotFoundException();
            }

            if (node.Left == null || node.Right == null)
            {
                node.Height -= 1;
                node.RecalculateHeight();

                var v = node.Left ?? node.Right;
                var tmp = node.Parent;

                if (tmp == null)
                {
                    Root = v;
                }
                else if (node == tmp.Left)
                {
                    tmp.Left = v;
                }
                else
                {
                    tmp.Right = v;
                }

                if (v != null)
                {
                    v.Parent = tmp;
                }

                BalanceTree(tmp);

                return true;
            }

            return false;
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
            if (current == null)
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
        /// <returns>Если узел найден, то возращает узел. В противном случае null.</returns>
        private Node<TKey, TValue> Find(TKey key)
        {
            var current = Root;
            while (current != null)
            {
                int result = current.Key.CompareTo(key);

                if (result > 0)
                {
                    current = current.Left;
                }
                else if (result < 0)
                {
                    current = current.Right;
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
        internal Node<TKey, TValue> RotateLeft(Node<TKey, TValue> node)
        {
            node.Height -= 2; //узел будет понижаться -> упадет его высота

            var newRoot = node.Right;
            var newRootLeft = newRoot.Left;
            var parent = node.Parent;

            newRoot.Parent = parent;
            newRoot.Left = node;
            node.Right = newRootLeft;
            node.Parent = newRoot;

            if (newRootLeft != null)
            {
                newRootLeft.Parent = node;
            }

            if (node == Root)
            {
                Root = newRoot;
            }
            else if (parent.Right == node)
            {
                parent.Right = newRoot;
                if (newRoot.Right.Right != null)
                    newRoot.Right.Right.RecalculateHeight();
            }
            else
            {
                parent.Left = newRoot;
                if (newRoot.Left.Left != null)
                    newRoot.Left.Left.RecalculateHeight();
            }

            //здесь newRoot "в цепочке". Надо пробежать по его родителям и пересчитать высоты
            //за основу брать высоту newRoot и его брата если есть
            newRoot.RecalculateHeight();

            //Как сделать, чтобы высоты были нормально!!!

            return newRoot;
        }

        /// <summary>
        /// Осуществляет поворот АВЛ-поддерева направо.
        /// </summary>
        internal Node<TKey, TValue> RotateRight(Node<TKey, TValue> node)
        {
            node.Height -= 2;

            var newRoot = node.Left;
            var newRootRight = newRoot.Right;
            var parent = node.Parent;

            newRoot.Parent = parent;
            newRoot.Right = node;
            node.Left = newRootRight;
            node.Parent = newRoot;

            if (newRootRight != null)
            {
                newRootRight.Parent = node;
            }

            if (node == Root)
            {
                Root = newRoot;
            }
            else if (parent.Left == node)
            {
                parent.Left = newRoot;
                if (newRoot.Left.Left != null)
                    newRoot.Left.Left.RecalculateHeight();
            }
            else
            {
                parent.Right = newRoot;
                if (newRoot.Right.Right != null)
                    newRoot.Right.Right.RecalculateHeight();
            }

            newRoot.RecalculateHeight();

            return newRoot;
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
                if (node==null)
                {
                    throw new KeyNotFoundException();
                }

                node.Value = value;
            }
        }
    }
}
