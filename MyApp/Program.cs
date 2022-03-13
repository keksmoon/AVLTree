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
            InitCollections();

            AVL<int, object> avl = new AVL<int, object>();
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

            Console.WriteLine("Hello! I can execute the following commands: \n" +
                "Adding:\t\t + [int: newItemKey] [any: newItemValue] \n" +
                "Removing:\t - [int: itemKey] \n" +
                "Find:\t\t ? [int: itemKey] \n" +
                "Quit:\t\t q");


            while (true)
            {
                Console.Write("-> ");
                string[] command = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (command.Length == 0) {
                    Console.WriteLine("Error command!");
                    continue;
                }

                switch (command[0])
                {
                    case "+":
                        {
                            int itemKey;
                            if (command.Length < 3)
                            {
                                Console.WriteLine("Error command!");
                                continue;
                            }

                            if (!int.TryParse(command[1], out itemKey))
                            {
                                Console.WriteLine("Error command!");
                                continue;
                            }

                            object itemValue = command[2];
                            try
                            {
                                avl.Insert(itemKey, itemValue);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                continue;
                            }

                            TreeNodePrinter.Print(avl.Root);
                        }
                        break;
                    case "-":
                        {
                            int itemKey;
                            if (command.Length < 2)
                            {
                                Console.WriteLine("Error command!");
                                continue;
                            }

                            if (!int.TryParse(command[1], out itemKey))
                            {
                                Console.WriteLine("Error command!");
                                continue;
                            }

                            try
                            {
                                avl.Remove(itemKey);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                continue;
                            }

                            if (avl.Count == 0)
                            {
                                Console.WriteLine("Tree is empty!");
                            }
                            else
                            {
                                TreeNodePrinter.Print(avl.Root);
                            }
                        }
                        break;
                    case "?":
                        {
                            int itemKey;
                            if (command.Length < 2)
                            {
                                Console.WriteLine("Error command!");
                                continue;
                            }

                            if (!int.TryParse(command[1], out itemKey))
                            {
                                Console.WriteLine("Error command!");
                                continue;
                            }

                            object outobj;
                            if (!avl.TryGetValue(itemKey, out outobj))
                            {
                                Console.WriteLine("Item not found!");
                                continue;
                            }
                            else
                            {
                                TreeNodePrinter.Print(avl.Root, itemKey: itemKey);
                            }
                        }
                        break;
                    default:
                        Console.WriteLine("Error command!");
                        continue;
                }
            }
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