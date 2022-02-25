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
        /// Высота дерева от его корня.
        /// </summary>
        public int Height { get => root == null ? 0 : root.height; private set { } }

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
                            BalanceTree();
                            Count++;

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
                            BalanceTree();
                            Count++;

                            return true;
                        } 
                        
                        currentNode = currentNode.right;
                    } else
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
        public void BalanceTree()
        {
            //Балансировка необходима <=> когда высота левого и правого поддеревьев = 2

        }

        /// <summary>
        /// Осуществляет поворот АВЛ-дерева налево.
        /// </summary>
        public void RotateLeft()
        {
            
        }

        /// <summary>
        /// Осуществляет поворот АВЛ-дерева направо.
        /// </summary>
        public void RotateRight()
        {

        }
    }
}
