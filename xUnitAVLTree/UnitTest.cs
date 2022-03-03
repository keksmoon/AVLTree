using System;
using Xunit;
using AVLTree;
using System.Collections.Generic;

namespace xUnitAVLTree
{
    public class UnitTest
    {
        // ��� �������� ������, ���������� ��������� � ��� ������ ���� 0.
        [Fact]
        public void Test1()
        {
            AVL<int, string> avl = new AVL<int, string>();

            Assert.Equal(0, avl.Count);
        }

        //��� ���������� �������� � ������ ���������� ��������� +1.
        [Fact]
        public void Test2()
        {
            AVL<int, string> avl = new AVL<int, string>();
            int oldItemsCountInAVL = avl.Count;
            avl.Insert(0, "A");
            int newItemsCountInAVL = avl.Count;

            Assert.Equal(oldItemsCountInAVL + 1, newItemsCountInAVL);
        }

        [Fact]
        public void Test3()
        {
            
        }

        //��� ��������� ���������� �������� � ���������� ������ ��������� DuplicationItemsInTreeException
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

        //��� ���������� �������� �� ������ ����� ������ ������.
        [Fact]
        public void Test5()
        {
            AVL<int, string> avl = new AVL<int, string>();
            avl.Insert(0, "A");

            Assert.Equal(0, avl.Root.Key);
        }

        //��� ���������� �������� ������ ��� ������ ������, �� ������ ���� ���������� ����� �����.
        [Fact]
        public void Test6()
        {
            AVL<int, string> avl = new AVL<int, string>();
            avl.Insert(0, "A");
            avl.Insert(-1, "A");

            Assert.Equal(-1, avl.Root.Left.Key);
        }

        //��� ���������� �������� ������ ��� ������ ������, �� ������ ���� ���������� ����� �����.
        [Fact]
        public void Test7()
        {
            AVL<int, string> avl = new AVL<int, string>();
            avl.Insert(0, "A");
            avl.Insert(1, "A");

            Assert.Equal(1, avl.Root.Right.Key);
        }

        //��� ��������� ���� �����, ���� ������ ���� parent, ������ left & right
        [Fact]
        public void Test8()
        {
            AVL<int, string> avl = new AVL<int, string>();
            avl.Insert(0, "A");
            avl.Insert(1, "A");
            avl.Insert(-1, "A");

            Assert.Equal(avl.Root, avl.Root.Left.Parent);
            Assert.Equal(avl.Root, avl.Root.Right.Parent);
        }

        //� ������ ������ ����� ������ ������ ���� 0
        [Fact]
        public void Test9()
        {
            AVL<int, string> avl = new AVL<int, string>();

            Assert.Equal(0, avl.Height());
        }

        //��� ���������� ���� ������������� ��������� � ������ ������, ������ ������ �������������
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

        //����� ���������� ���� ������������� ��������� � ������ ������, ������ ������ ���� 2
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

        //��� ���������� ���� ������������� ��������� � ������ ������, ������ ������ ������ ���� 0
        [Fact]
        public void Test12()
        {
            AVL<int, string> avl = new AVL<int, string>();

            for (int i = 1; i < 4; ++i)
            {
                avl.Insert(i, String.Empty);
            }

            Assert.Equal(0, avl.GetBalance(avl.Root));
        }

        //�������� ������ ������� ��������� 10 ���������� � ��������, ��� ��� ���� ��� �����
        [Fact]
        public void Test13()
        {
            AVL<int, int> avl = new AVL<int, int>();
            for (int i = 0; i < 10; i++)
            {
                avl.Insert(i, i);
            }
            var nodes = new List<Node<int, int>>(avl.InorderTraversal());
            List<int> keyValues = new List<int>();
            for (int i = 0; i < 10; i++)
            {
                keyValues.Add(i);
            }

            for (int i = 0; i < 10; i++)
            {
                Assert.Equal(keyValues[i], nodes[i].Key);
            }
        }

        //�������� ������ ������� ��������� 10 ���������� � ��������, ��� ��� ���� ��� ��������
        [Fact]
        public void Test14()
        {
            AVL<int, int> avl = new AVL<int, int>();
            for (int i = 0; i < 10; i++)
            {
                avl.Insert(i, i);
            }
            var nodes = new List<Node<int, int>>(avl.InorderTraversal());
            List<int> keyValues = new List<int>();
            for (int i = 0; i < 10; i++)
            {
                keyValues.Add(i);
            }

            for (int i = 0; i < 10; i++)
            {
                Assert.Equal(keyValues[i], nodes[i].Value);
            }
        }

        //�������� ������ ������ ��������� 10 ���������� � ��������, ��� ��� ���� ��� �����
        [Fact]
        public void Test15()
        {
            AVL<int, int> avl = new AVL<int, int>();
            for (int i = 0; i < 10; i++)
            {
                avl.Insert(-i, -i);
            }
            var nodes = new List<Node<int, int>>(avl.InorderTraversal());
            List<int> keyValues = new List<int>();
            for (int i = 0; i < 10; i++)
            {
                keyValues.Add(-i);
            }
            keyValues.Sort();
            for (int i = 0; i < 10; i++)
            {
                Assert.Equal(keyValues[i], nodes[i].Key);
            }
        }

        //�������� ������ ������ ��������� 10 ���������� � ��������, ��� ��� ���� ��� ��������
        [Fact]
        public void Test16()
        {
            AVL<int, int> avl = new AVL<int, int>();
            for (int i = 0; i < 10; i++)
            {
                avl.Insert(-i, -i);
            }
            var nodes = new List<Node<int, int>>(avl.InorderTraversal());
            List<int> keyValues = new List<int>();
            for (int i = 0; i < 10; i++)
            {
                keyValues.Add(-i);
            }
            keyValues.Sort();
            for (int i = 0; i < 10; i++)
            {
                Assert.Equal(keyValues[i], nodes[i].Value);
            }
        }

        //�������� �������-������ ��������� ���������� � ��������, ��� ��� ���� ��� �����
        [Fact]
        public void Test17()
        {
            AVL<int, int> avl = new AVL<int, int>();
            avl.Insert(5, 5);
            avl.Insert(4, 4);
            avl.Insert(8, 8);
            avl.Insert(6, 6);
            avl.Insert(9, 9);
            avl.Insert(7, 7);
            var nodes = new List<Node<int, int>>(avl.InorderTraversal());
            List<int> keyValues = new List<int>() { 5, 4, 8, 6, 9, 7 };
            keyValues.Sort();
            for (int i = 0; i < keyValues.Count; i++)
            {
                Assert.Equal(keyValues[i], nodes[i].Key);
            }

        }

        //�������� �������-������ ��������� ���������� � ��������, ��� ��� ���� ��� ��������
        [Fact]
        public void Test18()
        {
            AVL<int, int> avl = new AVL<int, int>();
            avl.Insert(5, 5);
            avl.Insert(4, 4);
            avl.Insert(8, 8);
            avl.Insert(6, 6);
            avl.Insert(9, 9);
            avl.Insert(7, 7);
            var nodes = new List<Node<int, int>>(avl.InorderTraversal());
            List<int> keyValues = new List<int>() { 5, 4, 8, 6, 9, 7 };
            keyValues.Sort();
            for (int i = 0; i < keyValues.Count; i++)
            {
                Assert.Equal(keyValues[i], nodes[i].Value);
            }
        }

        //�������� ������-������� ��������� ���������� � ��������, ��� ��� ���� ��� �����
        [Fact]
        public void Test19()
        {
            AVL<int, int> avl = new AVL<int, int>();
            avl.Insert(-5, -5);
            avl.Insert(-4, -4);
            avl.Insert(-8, -8);
            avl.Insert(-6, -6);
            avl.Insert(-9, -9);
            avl.Insert(-7, -7);
            var nodes = new List<Node<int, int>>(avl.InorderTraversal());
            List<int> keyValues = new List<int>() { -5, -4, -8, -6, -9, -7 };
            keyValues.Sort();
            for (int i = 0; i < keyValues.Count; i++)
            {
                Assert.Equal(keyValues[i], nodes[i].Key);
            }

        }
        //�������� ������-������� ��������� ���������� � ��������, ��� ��� ���� ��� ��������
        [Fact]
        public void Test20()
        {
            AVL<int, int> avl = new AVL<int, int>();
            avl.Insert(-5, -5);
            avl.Insert(-4, -4);
            avl.Insert(-8, -8);
            avl.Insert(-6, -6);
            avl.Insert(-9, -9);
            avl.Insert(-7, -7);
            var nodes = new List<Node<int, int>>(avl.InorderTraversal());
            List<int> keyValues = new List<int>() { -5, -4, -8, -6, -9, -7 };
            keyValues.Sort();
            for (int i = 0; i < keyValues.Count; i++)
            {
                Assert.Equal(keyValues[i], nodes[i].Value);
            }
        }

        //�������� ������ TryGetValue - true, ���� ���� ���������
        [Fact]
        public void Test21()
        {
            AVL<int, string> avl = new AVL<int, string>();
            avl.Insert(0, "a");
            string a;
            Assert.True(avl.TryGetValue(0, out a));
        }

        //�������� ������ TryGetValue - �������� �������� ����������, ���� ���� ���������
        [Fact]
        public void Test22()
        {
            AVL<int, string> avl = new AVL<int, string>();
            string expected = "a";
            avl.Insert(0, "a");
            string a;
            bool f = avl.TryGetValue(0, out a);
            Assert.Equal(a, expected);
        }

        //�������� ������ TryGetValue - false, ���� ���� �� ���������
        [Fact]
        public void Test23()
        {
            AVL<int, string> avl = new AVL<int, string>();
            avl.Insert(0, "a");
            string a;
            Assert.False(avl.TryGetValue(1, out a));
        }

        //�������� ������ TryGetValue - �������� �������� �� ����������, ���� ���� �� ���������
        [Fact]
        public void Test24()
        {
            AVL<int, string> avl = new AVL<int, string>();
            string expected = null;
            avl.Insert(0, "a");
            string a;
            bool f = avl.TryGetValue(1, out a);
            Assert.Equal(a, expected);
        }

        //�������� ����������� �� get �� �����, ��� ������ ��������
        [Fact]
        public void Test25()
        {
            AVL<int, string> avl = new AVL<int, string>();
            string expected = "a";
            avl.Insert(0, "a");
            Assert.Equal(expected, avl[0]);
        }

        //�������� ����������� �� get �� �����, ��� �� ������ ��������
        [Fact]
        public void Test26()
        {
            AVL<int, string> avl = new AVL<int, string>();
            avl.Insert(0, "A");
            Assert.Throws<KeyNotFoundException>(() =>
            {
                var a = avl[1];
            }
            );
        }

        //�������� ����������� �� set �� �����, ��� �������� ������
        [Fact]
        public void Test27()
        {
            AVL<int, string> avl = new AVL<int, string>();
            string expected = "b";
            avl.Insert(0, "a");
            avl[0] = "b";
            Assert.Equal(expected, avl[0]);
        }

        //�������� ����������� �� set �� �����, ��� �������� �� ������
        [Fact]
        public void Test28()
        {
            AVL<int, string> avl = new AVL<int, string>();
            avl.Insert(0, "A");
            Assert.Throws<KeyNotFoundException>(() =>
            {
                avl[1] = "b";
            }
            );
        }

        //����� ������ ������� ������, ���������� ��������� ��������� ������ ���� 0
        [Fact]
        public void Test29()
        {
            AVL<int, string> avl = new AVL<int, string>();

            var InOrderTraversalResults = avl.InorderTraversal();

            int CntItemsInOrderTraversalResults = 0;

            foreach (var items in InOrderTraversalResults)
            {
                CntItemsInOrderTraversalResults++;
            }

            Assert.Equal(0, CntItemsInOrderTraversalResults);
        }
    }
}
