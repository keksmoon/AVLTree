using System;
using System.Collections.Generic;

namespace AVLTree
{
    internal class NodeInfo<TKey, TValue>
    {
        public Node<TKey, TValue> Node;
        public string Text;
        public int StartPos;
        public int Size { get { return Text.Length; } }
        public int EndPos { get { return StartPos + Size; } set { StartPos = value - Size; } }
        public NodeInfo<TKey, TValue> Parent, Left, Right;
    }
    public static class TreeNodePrinter
    {
        public static void Print<TKey, TValue>(this Node<TKey, TValue> root, int topMargin = 2, int leftMargin = 2, int itemKey = 0)
        {
            if (root == null) return;
            int rootTop = Console.CursorTop + topMargin;
            var last = new List<NodeInfo<TKey, TValue>>();
            var next = root;
            for (int level = 0; next != null; level++)
            {
                var item = new NodeInfo<TKey, TValue> { Node = next, Text = string.Format("({0}:{1})", next.Key, next.Value) };
                if (level < last.Count)
                {
                    item.StartPos = last[level].EndPos + 1;
                    last[level] = item;
                }
                else
                {
                    item.StartPos = leftMargin;
                    last.Add(item);
                }
                if (level > 0)
                {
                    item.Parent = last[level - 1];
                    if (next == item.Parent.Node.Left)
                    {
                        item.Parent.Left = item;
                        item.EndPos = Math.Max(item.EndPos, item.Parent.StartPos);
                    }
                    else
                    {
                        item.Parent.Right = item;
                        item.StartPos = Math.Max(item.StartPos, item.Parent.EndPos);
                    }
                }
                next = next.Left ?? next.Right;
                for (; next == null; item = item.Parent)
                {
                    Print(item, rootTop + 2 * level, itemKey: itemKey);
                    if (--level < 0) break;
                    if (item == item.Parent.Left)
                    {
                        item.Parent.StartPos = item.EndPos;
                        next = item.Parent.Node.Right;
                    }
                    else
                    {
                        if (item.Parent.Left == null)
                            item.Parent.EndPos = item.StartPos;
                        else
                            item.Parent.StartPos += (item.StartPos - item.Parent.EndPos) / 2;
                    }
                }
            }
            Console.SetCursorPosition(0, rootTop + 2 * last.Count - 1);
        }

        private static void Print<TKey, TValue>(NodeInfo<TKey, TValue> item, int top, int itemKey = 0, bool iKey = false)
        {
            if (item.Node.Key.Equals(itemKey))
                iKey = true;
            SwapColors(iKey);
            Print(item.Text, top, item.StartPos, itemKey: itemKey, iKey: iKey);
            SwapColors(iKey);
            if (item.Left != null)
                PrintLink(top + 1, "┌", "┘", item.Left.StartPos + item.Left.Size / 2, item.StartPos, itemKey: itemKey, iKey: iKey);
            if (item.Right != null)
                PrintLink(top + 1, "└", "┐", item.EndPos - 1, item.Right.StartPos + item.Right.Size / 2, itemKey: itemKey, iKey: iKey);
        }

        private static void PrintLink(int top, string start, string end, int startPos, int endPos, int itemKey = 0, bool iKey = false)
        {
            Print(start, top, startPos, itemKey: itemKey, iKey: iKey);
            Print("─", top, startPos + 1, endPos, itemKey: itemKey, iKey: iKey);
            Print(end, top, endPos, itemKey: itemKey, iKey: iKey);
        }

        private static void Print(string s, int top, int left, int right = -1, int itemKey = 0, bool iKey = false)
        {
            top -= 1;
            Console.SetCursorPosition(left, top);
            if (right < 0) right = left + s.Length;
            while (Console.CursorLeft < right) Console.Write(s);
        }

        private static void SwapColors(bool iKey)
        {
            if (iKey)
            {
                Console.BackgroundColor = Console.BackgroundColor == ConsoleColor.Black ? ConsoleColor.DarkYellow : ConsoleColor.Black;
                Console.ForegroundColor = Console.ForegroundColor == ConsoleColor.Black ? ConsoleColor.White : ConsoleColor.Black;
            }
            else
            {
                Console.BackgroundColor = Console.BackgroundColor == ConsoleColor.Black ? ConsoleColor.Yellow : ConsoleColor.Black;
                Console.ForegroundColor = Console.ForegroundColor == ConsoleColor.Black ? ConsoleColor.White : ConsoleColor.Black;
            }
        }
    }
}
