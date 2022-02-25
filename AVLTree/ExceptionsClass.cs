using System;

namespace AVLTree
{
    /// <summary>
    /// Duplication of elements in the AVL-tree is not allowed!
    /// </summary>
    public class DuplicationItemsInTreeException : Exception
    {
        //Дублирование элементов в AVL-дереве не допускается
        public DuplicationItemsInTreeException()
            : base("Duplication of elements in the AVL-tree is not allowed!") { }
        public DuplicationItemsInTreeException(string message)
            : base(message) { }
    }

    /// <summary>
    /// AVL tree not contains items!
    /// </summary>
    public class AVLTreeIsEmptyException : Exception
    {
        //АВЛ дерево не сожержит элементов
        public AVLTreeIsEmptyException()
            : base("AVL tree not contains items!") { }
        public AVLTreeIsEmptyException(string message)
            : base(message) { }
    }
}
