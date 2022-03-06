using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVLTree
{
    public interface ITree<TKey,TValue>
    {
        int Count();
        int Height(Node<TKey, TValue> node);
        void Clear();
        int Height();
        void Insert(TKey key, TValue value);
        void Remove(TKey key);
        bool TryGetValue(TKey key, out TValue value);
        IEnumerable<Node<TKey, TValue>> InorderTraversal();
        TValue this[TKey key] { get; set; }
    }
}
