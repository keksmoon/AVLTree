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
            AVL<int, int> avl = new AVL<int, int>();

            SortedDictionary<int, int> sd = new SortedDictionary<int, int>();

            for (int i = 0; i < 100; i++)
            {
                avl.Insert(i, i);
                sd.Add(i, i);
            }

            avl.Clear();
            sd.Clear();

            Stopwatch sw = new Stopwatch();

            List<int> randomIntegers = new List<int>();
            Random random = new Random(DateTime.Now.Millisecond);

            while (randomIntegers.Count < 30)
            {
                int newRandomInteger = random.Next(-50, 50);

                if (!randomIntegers.Contains(newRandomInteger))
                    randomIntegers.Add(newRandomInteger);
            }

            Console.WriteLine("Integers generated");

            foreach (int i in randomIntegers)
            {
                avl.Insert(i, 0);
                TreeNodePrinter.Print(avl.Root);
            }

            Console.WriteLine("STOP");
            Console.ReadKey();
        }
    }
}
