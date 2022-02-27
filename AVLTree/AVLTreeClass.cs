using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVLTree
{
    /// <summary>
    /// Дерево Адельсона-Вельского и Ландиса.
    /// </summary>
    public class AVL<TKey, TValue> where TKey : IComparable<TKey>
    {
        public Node<TKey, TValue> root { get; private set; }

        public AVL()
        {
            root = null;
        }

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

            return node.height;
        }

        /// <summary>
        /// Метод, возвращающий высоту всего дерева от корня.
        /// </summary>
        public int Height() => Height(root);

        /// <summary>
        /// Высчитывает баланс узла как разность высот его левого и правого поддеревьев.
        /// </summary>
        public int GetBalance(Node<TKey, TValue> node)
        {
            if (node == null)
            {
                return 0;
            }

            return Height(node.left) - Height(node.right);
        }

        /// <summary>
        /// Добавление в дерево нового элемента.
        /// </summary>
        public bool Insert(TKey key, TValue value)
        {
            Node<TKey, TValue> newItem = new Node<TKey, TValue>(key, value);

            if (root == null)
            {
                //Если дерево пусто, заменяем его на дерево с одним узлом newItem.
                root = newItem;
                root.height = 1;
                Count++;
            }
            else
            {
                var currentNode = root;

                //Иначе необходимо:
                //  1. Найти место для вставки нового элемента.
                //     Если элемент с таким же ключом существует, то throw DuplicationItemsInTreeException.
                //  2. Если подходящее место найдено, то вставляем элемент.
                //     Иначе продолжаем поиск.

                while (currentNode != null)
                {
                    var resultComparison = newItem.key.CompareTo(currentNode.key);
                    // -1 если элемент для вставки меньше рассматриваемого
                    // 1 если элемент для вставки больше рассматриваемого

                    if (resultComparison < 0)
                    {
                        if (currentNode.left == null)
                        {
                            newItem.parent = currentNode;
                            currentNode.left = newItem;
                            currentNode.left.RecalculateHeight();
                            BalanceTree(currentNode);
                            Count++;

                            //TreeNodePrinter.Print(root);

                            return true;
                        }

                        currentNode = currentNode.left;
                    } else if (resultComparison > 0)
                    {
                        if (currentNode.right == null)
                        {
                            newItem.parent = currentNode;
                            currentNode.right = newItem;
                            currentNode.right.RecalculateHeight();
                            BalanceTree(currentNode);
                            Count++;

                            //TreeNodePrinter.Print(root);

                            return true;
                        } 
                        
                        currentNode = currentNode.right;
                    } else
                    {
                        throw new DuplicationItemsInTreeException();
                    }
                }
            }

            //TreeNodePrinter.Print(root);

            return true;
        }

        /// <summary>
        /// Удаление элемента из дерева по ключу.
        /// </summary>
        public bool Remove(TKey key)
        {

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
            object result = null;

            value = (TValue)result;

            return false;
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
                    int leftbalance = GetBalance(node.left);

                    if (leftbalance == 1)
                    {
                        RotateRight(node);
                    }
                    else
                    {
                        RotateLeft(node.left);
                        RotateRight(node);
                    }

                }
                else if (balance == -2)
                {
                    int rightbalance = GetBalance(node.right);

                    if (rightbalance == -1)
                    {
                        RotateLeft(node);
                    }
                    else
                    {
                        RotateRight(node.right);
                        RotateLeft(node);
                    }
                }

                node = node.parent;
            }
            
            
        }

        /// <summary>
        /// Осуществляет поворот АВЛ-поддерева налево.
        /// </summary>
        public Node<TKey, TValue> RotateLeft(Node<TKey, TValue> node)
        {
            node.height -= 2; //узел будет понижаться -> упадет его высота

            var newRoot = node.right;
            var newRootLeft = newRoot.left;
            var parent = node.parent;

            newRoot.parent = parent;
            newRoot.left = node;
            node.right = newRootLeft;
            node.parent = newRoot;

            if (newRootLeft != null)
            {
                newRootLeft.parent = node;
            }

            if (node == root)
            {
                root = newRoot;
            }
            else if (parent.right == node)
            {
                parent.right = newRoot;
                if (newRoot.right.right != null)
                    newRoot.right.right.RecalculateHeight();
            }
            else
            {
                parent.left = newRoot;
                if (newRoot.left.left != null)
                    newRoot.left.left.RecalculateHeight();
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
        public Node<TKey, TValue> RotateRight(Node<TKey, TValue> node)
        {
            node.height -= 2;

            var newRoot = node.left;
            var newRootRight = newRoot.right;
            var parent = node.parent;

            newRoot.parent = parent;
            newRoot.right = node;
            node.left = newRootRight;
            node.parent = newRoot;

            if (newRootRight != null)
            {
                newRootRight.parent = node;
            }

            if (node == root)
            {
                root = newRoot;
            }
            else if (parent.left == node)
            {
                parent.left = newRoot;
                if (newRoot.left.left != null)
                    newRoot.left.left.RecalculateHeight();
            }
            else
            {
                parent.right = newRoot;
                if (newRoot.right.right != null)
                    newRoot.right.right.RecalculateHeight();
            }

            newRoot.RecalculateHeight();

            return newRoot;
        }
    }
}
