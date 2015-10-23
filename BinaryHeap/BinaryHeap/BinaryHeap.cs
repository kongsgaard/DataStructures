using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryHeap
{
    public class BinaryHeap<T>
    {
        private void HeapifyUp(int index) 
        {


            int compareResulst = comparer.Invoke(array[index], array[((index-1) / 2)]);

            //size is bigger than size/2, swap and call recursively
            if (compareResulst == 1) 
            {
                T tmp = array[index];
                array[index] = array[((index-1) / 2)];
                array[((index-1) / 2)] = tmp;
                HeapifyUp((index-1) / 2);
            }
            else if (compareResulst == -1 || compareResulst == 0) 
            {
                //Nothing happens
            }
        }

        private void HeapifyDown(int index) 
        {
            if (size-1 < ((index + 1) * 2) - 1) 
            {//At bottom of heap structure, return
                return;
            }
            if (size - 1 < (index + 1) * 2) 
            {//Swap left is only option
                int compareResultLeftOnly = comparer.Invoke(array[index], array[((index + 1) * 2) - 1]);
                if (compareResultLeftOnly == -1)
                {//Swap left
                    T tmp = array[index];
                    array[index] = array[((index + 1) * 2) - 1];
                    array[((index + 1) * 2) - 1] = tmp;
                    HeapifyDown(((index + 1) * 2) - 1);
                }
                return;
            }

            int compareResultLeft = comparer.Invoke(array[index], array[((index + 1) * 2) - 1]);
            int compareResultRight = comparer.Invoke(array[index], array[((index + 1) * 2)]);

            if (compareResultLeft == -1 && compareResultRight == -1) 
            {//Both left and right are are swapable targets
                int compareResultLeftRight = comparer.Invoke(array[((index + 1) * 2) - 1], array[((index + 1) * 2)]);
                if (compareResultLeftRight == -1)
                {//Right is better to swap
                    T tmp = array[index];
                    array[index] = array[((index + 1) * 2)];
                    array[((index + 1) * 2)] = tmp;
                    HeapifyDown(((index + 1) * 2));
                }
                else 
                {//Swap left
                    T tmp = array[index];
                    array[index] = array[((index + 1) * 2) - 1];
                    array[((index + 1) * 2) - 1] = tmp;
                    HeapifyDown(((index + 1) * 2) - 1);
                }
            }
            else if (compareResultRight == -1)
            {//Swap Right
                T tmp = array[index];
                array[index] = array[((index + 1) * 2)];
                array[((index + 1) * 2)] = tmp;
                HeapifyDown(((index + 1) * 2));
            }
            else if (compareResultLeft == -1)
            {//Swap left
                T tmp = array[index];
                array[index] = array[((index + 1) * 2) - 1];
                array[((index + 1) * 2) - 1] = tmp;
                HeapifyDown(((index + 1) * 2) - 1);
            }
            else 
            {//Nothing happens
            
            }

        }

        private void ResizeUp() 
        {
            int newSize = array.Length;
            T[] newArray = new T[newSize*2];
            Array.Copy(array, newArray, newSize);
            array = newArray;
        }

        public void Insert(T item) 
        {
            if (size < array.Length - 1)
            {
                array[size] = item;
                HeapifyUp(size);
                size++;
            }
            else 
            {
                ResizeUp();
                array[size] = item;
                HeapifyUp(size);
                size++;
            }
        }

        public T ExtractTop() 
        {
            size--;
            T top = array[0];
            array[0] = array[size];
            array[size] = default(T);
            HeapifyDown(0);
            
            return top;
        }

        //Current  implementation allows your to return object and alter the value, thus destroying the heap property of structure, make sure you don't do this!
        public T Peek() 
        {
            return array[0];
        }

        public BinaryHeap(List<T> dataItems, Comparison<T> comp) 
        {
            comparer = comp;

            foreach (T item in dataItems) 
            {
                Insert(item);
            }
        }

        private T[] array = new T[1];
        private int size = 0;

        private Comparison<T> comparer = null;
    }
}
