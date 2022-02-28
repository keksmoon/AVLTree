using System;
using System.Collections.Generic;
using AVLTree;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ////Генерация массива из 10к случайных элементов
            //List<int> randomIntegers = new List<int>();
            //Random random = new Random(DateTime.Now.Millisecond);

            //while (randomIntegers.Count < 10000){
            //    int newRandomInteger = random.Next(-50000, 50000);

            //    if (!randomIntegers.Contains(newRandomInteger))
            //        randomIntegers.Add(newRandomInteger);
            //}

            ////Создание дерева и его заполнение сгенерированными числами
            AVL<int, int> avl = new AVL<int, int>();

            //int ord = 0;
            //foreach (int i in randomIntegers)
            //{
            //    avl.Insert(i, ord++);
            //}

            //for (int i = 1; i <= 54 / 2; i++)
            //{
            //    int sign = (int)Math.Pow(-1, i);
            //    int item = (int)((long)Math.Pow(5, i) % 162);
            //    avl.Insert(sign * item, "a");
            //}

            avl.Insert(2, 1);
            avl.Insert(3, 1);
            avl.Insert(4, 1);
            avl.Insert(5, 1);

            TreeNodePrinter.Print(avl.Root);

            avl.Remove(4);

            TreeNodePrinter.Print(avl.Root);

            Console.ReadKey();
        }
    }
}
