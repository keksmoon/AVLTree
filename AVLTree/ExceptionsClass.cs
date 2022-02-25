using System;

namespace AVLTree
{
    public class DuplicationItemsInTreeException : Exception
    {
        //Дублирование элементов в AVL-дереве не допускается
        public DuplicationItemsInTreeException()
            : base("Duplication of elements in the AVL-tree is not allowed!") { }
        public DuplicationItemsInTreeException(string message)
            : base(message) { }
    }
}
