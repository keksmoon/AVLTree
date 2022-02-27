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

            TreeNodePrinter.Print(avl.Root);

            Console.ReadKey();
        }
    }
}
