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

            //Нагружаем самое левое поддерево
            //avl.Insert(5, 0);
            //avl.Insert(3, 0);
            //avl.Insert(7, 0);
            //avl.Insert(1, 0);
            //avl.Insert(4, 0);
            //avl.Insert(6, 0);
            //avl.Insert(8, 0);
            //avl.Insert(0, 0);
            //avl.Insert(2, 0);


            ////Нагружаем левое-правое поддерево
            //avl.Insert(5, 0);
            //avl.Insert(1, 0);
            //avl.Insert(7, 0);
            //avl.Insert(0, 0);
            //avl.Insert(3, 0);
            //avl.Insert(6, 0);
            //avl.Insert(8, 0);
            //avl.Insert(2, 0);
            //avl.Insert(4, 0);


            ////Нагружаем правое-левое поддерево
            //avl.Insert(3, 0);
            //avl.Insert(1, 0);
            //avl.Insert(7, 0);
            //avl.Insert(0, 0);
            //avl.Insert(2, 0);
            //avl.Insert(5, 0);
            //avl.Insert(8, 0);
            //avl.Insert(4, 0);
            //avl.Insert(6, 0);

            ////нагружаем правое-правое поддерево
            //avl.Insert(3, 0);
            //avl.Insert(1, 0);
            //avl.Insert(5, 0);
            //avl.Insert(0, 0);
            //avl.Insert(2, 0);
            //avl.Insert(4, 0);
            //avl.Insert(7, 0);
            //avl.Insert(8, 0);
            //avl.Insert(6, 0);

            SortedDictionary<int, int> sd = new SortedDictionary<int, int>();

            for (int i = 0; i < 100; i++)
            {
                avl.Insert(i, i);
                sd.Add(i, i);
            }

            avl.Clear();
            sd.Clear();

            //Stopwatch sw = new Stopwatch();

            List<int> randomIntegers = new List<int>();
            Random random = new Random(DateTime.Now.Millisecond);

            while (randomIntegers.Count < 10000)
            {
                int newRandomInteger = random.Next(-50000, 50000);

                if (!randomIntegers.Contains(newRandomInteger))
                    randomIntegers.Add(newRandomInteger);
            }

            Console.WriteLine("Integers generated");

            foreach (int i in randomIntegers)
            {
                avl.Insert(i, 0);
            }

            randomIntegers.Sort();
            var q = avl.InorderTraversal();
            int k = 0;
            foreach (var j in q)
            {
                if (j.Key!= randomIntegers[k])
                {
                    Console.WriteLine(false);
                }
                k++;
            }




            for (int i = 5000; i <= 7000; i++)
            {
                avl.Remove(randomIntegers[i]);
            }

            //    Console.WriteLine("STOP");
            Console.ReadKey();
        }
    }
}