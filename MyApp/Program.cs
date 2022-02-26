using System;
using AVLTree;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AVL<int, string> avl = new AVL<int, string>();

            //for (int i = 1; i <= 54 / 2; i++)
            //{
            //    int sign = (int)Math.Pow(-1, i);
            //    int item = (int)((long)Math.Pow(5, i) % 162);
            //    avl.Insert(sign * item, "a");
            //}

            //avl.Insert(-5, "a");
            //avl.Insert(-4, "a");
            //avl.Insert(-8, "a");
            //avl.Insert(-6, "a");
            //avl.Insert(-9, "a");
            //avl.Insert(-7, "a");

            for (int i = 0; i < 9; i++)
            {
                avl.Insert(i, "i");
            }

            TreeNodePrinter.Print(avl.root);

            Console.ReadKey();
        }
    }
}
