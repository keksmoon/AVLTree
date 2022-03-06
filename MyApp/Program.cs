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
            //InitCollections();

            //AVL<int, int> avl = new AVL<int, int>();
            //SortedDictionary<int, int> sdict = new SortedDictionary<int, int>();

            //var randomIntegers = GenerateRandomIntegers(10000);

            //Stopwatch stopwatch = Stopwatch.StartNew();
            //foreach (var item in randomIntegers)
            //    avl.Insert(item, 0);
            //stopwatch.Stop();

            //Console.WriteLine("AVL.Insert: " + stopwatch.ElapsedTicks);

            //stopwatch.Restart();
            //foreach (var item in randomIntegers)
            //    sdict.Add(item, 0);
            //stopwatch.Stop();

            //Console.WriteLine("SDICT.Add: " + stopwatch.ElapsedTicks);

            //        Console.WriteLine();

            //stopwatch.Restart();
            //for (int i = 5000; i <= 7000; i++)
            //{
            //    avl.Remove(randomIntegers[i]);
            //}
            //stopwatch.Stop();

            //Console.WriteLine("AVL.Remove: " + stopwatch.ElapsedTicks);
            
            //stopwatch.Restart();
            //for (int i = 5000; i <= 7000; i++)
            //{
            //    sdict.Remove(randomIntegers[i]);
            //}
            //stopwatch.Stop();

            //Console.WriteLine("SDICT.Remove: " + stopwatch.ElapsedTicks);

            //        Console.WriteLine();

            //stopwatch.Restart();
            //foreach (var item in randomIntegers)
            //{
            //    avl.TryGetValue(item, out int _);
            //}
            //stopwatch.Stop();

            //Console.WriteLine("AVL.TryGetValue: " + stopwatch.ElapsedTicks);

            //stopwatch.Restart();
            //foreach (var item in randomIntegers)
            //{
            //    sdict.TryGetValue(item, out int _);
            //}
            //stopwatch.Stop();

            //Console.WriteLine("SDICT.TryGetValue: " + stopwatch.ElapsedTicks);

            //Console.ReadKey();
        }

        private static void InitCollections()
        {
            var avl = new AVL<int, int>();

            for (int i = 0; i < 100; i++)
                avl.Insert(i, i);

            var sdict = new SortedDictionary<int, int>();

            for (int i = 0; i < 100; i++)
                sdict.Add(i, i);
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