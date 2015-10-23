using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryHeap
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> test1 = new List<int>();

            Random ran = new Random();

            for (int i = 0; i < 50; i++) 
            {
                test1.Add(ran.Next(1,50));
            }

            BinaryHeap<int> bh = new BinaryHeap<int>(test1, CompareInts);

            for (int i = 0; i < 50; i++)
            {
                Console.WriteLine(bh.ExtractTop());
            }

            Console.ReadLine();
        }

        //t1 < t2 = 1, you get a minHeap
        //t1 < t2 = -1, you get a maxHeap
        private static int CompareInts(int t1, int t2)
        {
            if (t1 < t2)
            {
                return -1;
            }
            else if (t1 > t2)
            {
                return 1;
            }
            else return 0;
        }
    }
}
