using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPriorityQueue
{
    public class MyPriorityQueue<T> where T : IComparable<T>
    {
        T[] queue;
        int size;
        Comparer<T> comparer;

        public MyPriorityQueue()
        {
            queue = new T[11];
            size = 0;
        }
        public MyPriorityQueue(T[] a)
        {
            queue = new T[a.Length];
            size = 0;
            for (int i = 0; i < a.Length; i++) Add(a[i]);
        }
        public MyPriorityQueue(int initialCapacity)
        {
            queue = new T[initialCapacity];
            size = initialCapacity;
        }
        public MyPriorityQueue(int initialCapacity, Comparer<T> comparer)
        {
            queue = new T[initialCapacity];
            size = initialCapacity;
            this.comparer = comparer;
        }
        public MyPriorityQueue(MyPriorityQueue<T> c)
        {
            queue = new T[c.size];
            size = c.size;
            for (int i = 0; i < size; i++) queue[i] = c.queue[i];
        }

        private void Swap(int index_1, int index_2)
        {
            T tmp = queue[index_1];
            queue[index_1] = queue[index_2];
            queue[index_2] = tmp;
        }
        private void Heapify(int index, Comparer<T> comparer)
        {
            while (index > 0)
            {
                int parentIndex = (index - 1) / 2;
                if (queue[index].CompareTo(queue[parentIndex]) <= 0) break;
                Swap(index, parentIndex);
                index = parentIndex;
            }
        }
        private void Resize()
        {
            int newCapacity = queue.Length < 64 ? queue.Length + 2 : queue.Length * 3 / 2;
            Array.Resize(ref queue, newCapacity);
        }
        private int FindIndex(object o)
        {
            if (Contains(o))
            {
                for (int i = 0; i < queue.Length; i++)
                {
                    if (o.Equals(queue[i])) return i;
                }
            }
            return -1;
        }

        public void Add(T e)
        {
            if (size == queue.Length) Resize();
            queue[size++] = e;
            Heapify(size - 1, comparer);
        }
        public void AddAll(T[] a)
        {
            for (int i = 0; i < a.Length; i++) Add(a[i]);
        }
        public void Clear()
        {
            Array.Clear(queue, 0, size);
            size = 0;
        }
        public bool Contains(object o)
        {
            for (int i = 0; i < size; i++)
            {
                if (Equals(o, queue[i])) return true;
            }
            return false;
        }
        public bool ContainsAll(T[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                if (!Contains(a[i])) return false;
            }
            return true;
        }
        public bool IsEmpty() => size == 0;
        public void Remove(object o)
        {
            if (Contains(o))
            {
                int k = FindIndex(o);
                T[] array = new T[size - 1];
                for (int i = 0; i < k; i++) array[i] = queue[i];
                for (int i = k + 1; i < size; i++) array[i - 1] = queue[i];
                queue = array;
                size--;
                Heapify(size - 1, comparer);
            }
        }
        public void RemoveAll(T[] a)
        {
            for (int i = 0; i < a.Length; i++) Remove(a[i]);
        }
        public void RetainAll(T[] a)
        {
            if (a.Length > queue.Length) Array.Resize(ref queue, a.Length);
            for (int i = 0; i < a.Length; i++) queue[i] = a[i];
            size = a.Length;
        }
        public int Size() => size;
        public T[] ToArray()
        {
            T[] array = new T[size];
            for (int i = 0; i < size; i++) array[i] = queue[i];
            return array;
        }
        public void ToArray(T[] a)
        {
            if (a == null) a = new T[size];
            else if (a.Length < size) Array.Resize<T>(ref a, size);
            for (int i = 0; i < a.Length; i++) a[i] = queue[i];
        }
        public T Element() => queue[0];
        public bool Offer(T obj)
        {
            Add(obj);
            if (Contains(obj)) return true;
            return false;
        }
        public T Peek() => size == 0 ? default(T) : queue[0];
        public T Poll()
        {
            if (size == 0) return default(T);
            T obj = queue[0];
            Remove(obj);
            return obj;
        }
        public void Print()
        {
            Console.Write("Current queue is: ");
            if (size == 0)
            {
                Console.WriteLine("empty");
                return;
            }
            for (int i = 0; i < size; i++)
            {
                Console.Write(queue[i] + " ");
            }
            Console.WriteLine();
        }
    }
}
