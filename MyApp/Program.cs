using System;
using System.Collections.Generic;
using System.Diagnostics;
using AVLTree;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AVL<int, string> avl = new AVL<int, string>();

            //for (int i = 0; i < 1000; i++)
            //    avl.Insert(i, String.Empty);
            avl = new AVL<int, string>();

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

            //for (int i = 10; i < 20; i++)
            //{
            //    avl.Insert(i, "i");
            //}

            //SortedDictionary<int, string> sdc = new SortedDictionary<int, string>();

            //Stopwatch stopWatch = new Stopwatch();
            //stopWatch.Start();

            //for (int i = 0; i < 1000000; i++)
            //{
            //    avl.Insert(i, "i");
            //}

            //stopWatch.Stop();
            //Console.WriteLine("AVL: " + stopWatch.ElapsedMilliseconds);

            //stopWatch.Restart();

            //for (int i = 0; i < 1000000; i++)
            //{
            //    sdc.Add(i, "i");
            //}

            //stopWatch.Stop();

            //Console.WriteLine("SDC: " + stopWatch.ElapsedMilliseconds);
            TreeNodePrinter.Print(avl.root);

            Console.ReadKey();
        }
    }
}
