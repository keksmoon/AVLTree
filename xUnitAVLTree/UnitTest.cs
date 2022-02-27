using System;
using Xunit;
using AVLTree;

namespace xUnitAVLTree
{
    public class UnitTest
    {
        // При создании дерева, количество элементов в нем должно быть 0.
        [Fact]
        public void Test1()
        {
            AVL<int, string> avl = new AVL<int, string>();

            Assert.Equal(0, avl.Count);
        }

        //При добавлении элемента в дерево количество элементов +1.
        [Fact]
        public void Test2()
        {
            AVL<int, string> avl = new AVL<int, string>();
            int oldItemsCountInAVL = avl.Count;
            avl.Insert(0, "A");
            int newItemsCountInAVL = avl.Count;

            Assert.Equal(oldItemsCountInAVL + 1, newItemsCountInAVL);
        }

        //При добавлении элемента нового элемента в дерево ожидается true.
        [Fact]
        public void Test3()
        {
            AVL<int, string> avl = new AVL<int, string>();
            bool resultInsertation = avl.Insert(0, "A");

            Assert.True(resultInsertation);
        }

        //При повторном добавлении элемента с одинаковым ключом ожидается DuplicationItemsInTreeException
        [Fact]
        public void Test4()
        {
            AVL<int, string> avl = new AVL<int, string>();
            avl.Insert(0, "A");

            Assert.Throws<DuplicationItemsInTreeException>(() =>
            {
                avl.Insert(0, "B");
            }
            );
        }

        //При добавлении элемента он должен стать корнем дерева.
        [Fact]
        public void Test5()
        {
            AVL<int, string> avl = new AVL<int, string>();
            avl.Insert(0, "A");

            Assert.Equal(0, avl.root.key);
        }

        //При добавлении элемента меньше чем корень дерева, он должен быть установлен левее корня.
        [Fact]
        public void Test6()
        {
            AVL<int, string> avl = new AVL<int, string>();
            avl.Insert(0, "A");
            avl.Insert(-1, "A");

            Assert.Equal(-1, avl.root.left.key);
        }

        //При добавлении элемента больше чем корень дерева, он должен быть установлен левее корня.
        [Fact]
        public void Test7()
        {
            AVL<int, string> avl = new AVL<int, string>();
            avl.Insert(0, "A");
            avl.Insert(1, "A");

            Assert.Equal(1, avl.root.right.key);
        }

        //При сцеплении двух узлов, один должен быть parent, другой left & right
        [Fact]
        public void Test8()
        {
            AVL<int, string> avl = new AVL<int, string>();
            avl.Insert(0, "A");
            avl.Insert(1, "A");
            avl.Insert(-1, "A");

            Assert.Equal(avl.root, avl.root.left.parent);
            Assert.Equal(avl.root, avl.root.right.parent);
        }

        //В пустом дереве выста дерева должна быть 0
        [Fact]
        public void Test9()
        {
            AVL<int, string> avl = new AVL<int, string>();

            Assert.Equal(0, avl.Height());
        }

        //При добавлении двух упорядоченных элементов в пустое дерево, высота должна увеличиваться
        [Fact]
        public void Test10()
        {
            //   1
            //    \
            //     2        
            AVL<int, string> avl = new AVL<int, string>();

            for (int i = 1; i < 3; ++i)
            {
                avl.Insert(i, String.Empty);
                Assert.Equal(i, avl.Height());
            }
        }

        //После добавления трех упорядоченных элементов в пустое дерево, высота должна быть 2
        [Fact]
        public void Test11() 
        {
            //   2
            //  / \
            // 1   3        
            AVL<int, string> avl = new AVL<int, string>();

            for (int i = 1; i < 4; ++i)
            {
                avl.Insert(i, String.Empty);
            }

            Assert.Equal(2, avl.Height());
        }

        //При добавлении трех упорядоченных элементов в пустое дерево, баланс дерева должен быть 0
        [Fact]
        public void Test12()
        {
            AVL<int, string> avl = new AVL<int, string>();

            for (int i = 1; i < 4; ++i)
            {
                avl.Insert(i, String.Empty);
            }

            Assert.Equal(0, avl.GetBalance(avl.root));
        }
    }
}
