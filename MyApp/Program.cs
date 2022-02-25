using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AVLTree;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AVL<int, int> avl = new AVL<int, int>();

            for (int i = 0; i < 5; i++)
            {
                avl.Insert(i, i);
            }

            BTreePrinter.Print(avl.root);

            Console.ReadKey();
        }
    }
}
