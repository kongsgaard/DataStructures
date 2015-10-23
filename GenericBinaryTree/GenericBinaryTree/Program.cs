using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericBinaryTree
{
    class Program
    {
        static void Main(string[] args)
        {
            GenericBinaryTree<intwrap> tree = new GenericBinaryTree<intwrap>(new intwrap(13),CompareInts);
            tree.Insert(new intwrap(11));
            tree.Insert(new intwrap(12));
            tree.Insert(new intwrap(1));
            tree.Insert(new intwrap(9));
            tree.Insert(new intwrap(10));
            tree.Insert(new intwrap(2));
            tree.Insert(new intwrap(8));
            tree.Insert(new intwrap(3));
            tree.Insert(new intwrap(4));
            tree.Insert(new intwrap(6));
            tree.Insert(new intwrap(5));

            bool happen = tree.Delte(new intwrap(11));
        }

        //t1 < t2 = 1, you get a minHeap
        //t1 < t2 = -1, you get a maxHeap
        private static int CompareInts(intwrap t1, intwrap t2)
        {
            if (t1.i < t2.i)
            {
                return 1;
            }
            else if (t1.i > t2.i)
            {
                return -1;
            }
            else return 0;
        }
    }

    class intwrap 
    {
        public intwrap(int j) { i = j; }
        public int i;
    }
}
