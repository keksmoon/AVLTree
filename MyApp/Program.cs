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

            avl.Insert(3, 1);
            avl.Insert(4, 1);
            avl.Insert(5, 1);
            avl.Insert(6, 1);

            TreeNodePrinter.Print(avl.Root);

            Console.ReadKey();
        }

        private static List<int> GenerateRandomIntegers(int count)
        {
            List<int> randomIntegers = new List<int>();
            Random random = new Random(DateTime.Now.Millisecond);

            while (randomIntegers.Count < count)
            {
                int newRandomInteger = random.Next(-2 * count, 2 * count);

                if (!randomIntegers.Contains(newRandomInteger))
                    randomIntegers.Add(newRandomInteger);
            }

            Console.WriteLine("Integers generated");

            return randomIntegers;
        }
    }
}