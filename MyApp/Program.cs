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
            SortedDictionary<int, string> kvp = new SortedDictionary<int, string>();

            for (int i = 0; i < 10000; i++)
            {
                avl.Insert(i, "a");
                kvp.Add(i, "b");
            }

            //for (int i = 1; i <= 54 / 2; i++)
            //{
            //    int sign = (int)Math.Pow(-1, i);
            //    int item = (int)((long)Math.Pow(5, i) % 162);
            //    avl.Insert(sign * item, "a");
            //}

            AVL<int, string> avlh = new AVL<int, string>();
            Random rnd = new Random(DateTime.Now.Millisecond);

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            for (int i = 0; i < 10000; i++)
            {
                int sign = rnd.Next(-10, 10) > 0 ? 1 : -1;

                avlh.Insert(sign * i, "a");
            }

            stopWatch.Stop();
            long MyQueueEnqueueTime = stopWatch.ElapsedMilliseconds;

            SortedDictionary<int, string> kvph = new SortedDictionary<int, string>();

            stopWatch.Reset();

            for (int i = 0; i < 10000; i++)
            {
                int sign = rnd.Next(-10, 10) > 0 ? 1 : -1;

                kvph.Add(sign * i, "a");
            }

            stopWatch.Stop();
            long kvpEnqueueTime = stopWatch.ElapsedMilliseconds;

            Console.WriteLine(kvpEnqueueTime);
            Console.WriteLine(MyQueueEnqueueTime);

            //avl.Insert(-5, "a");
            //avl.Insert(-4, "a");
            //avl.Insert(-8, "a");
            //avl.Insert(-6, "a");
            //avl.Insert(-9, "a");
            //avl.Insert(-7, "a");

            //for (int i = 0; i < 10; i++)
            //{
            //    avl.Insert(i, "i");
            //}

            //TreeNodePrinter.Print(avl.root);

            Console.ReadKey();
        }
    }
}
