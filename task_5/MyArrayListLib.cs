using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyArrayListLib
{
    public class MyArrayList<T>
    {
        public int size;
        public T[] elementData;

        public MyArrayList()
        {
            size = 0;
            elementData = new T[1];
        }
        public MyArrayList(T[] a)
        {
            size = a.Length;
            elementData = new T[size];
            for (int i = 0; i < size; i++) elementData[i] = a[i];

        }
        public MyArrayList(int capacity)
        {
            size = 0;
            elementData = new T[capacity];
        }

        public void Add(T e)
        {
            if (size == elementData.Length)
            {
                T[] newArray = new T[(int)(elementData.Length * 1.5 + 1)];
                int i = 0;
                foreach (T a in elementData) newArray[i++] = a;
                elementData = newArray;
            }
            elementData[size++] = e;
        }
        public void AddAll(T[] a)
        {
            foreach (T e in a) Add(e);
        }
        public void Clear()
        {
            size = 0;
            Array.Clear(elementData);
        }

        public bool Contains(object o)
        {
            foreach (T a in elementData) if (a.Equals(o)) return true;
            return false;
        }
        public bool ContainsAll(T[] a)
        {
            foreach (T e in a) if (!Contains(e)) return false;
            return true;
        }
        public bool IsEmpty()
        {
            return size == 0;
        }

        public void Remove(object o)
        {
            if (Contains(o))
            {
                int i = 0;
                while (!elementData[i].Equals(o)) i++;
                for (int j = i; j < elementData.Length - 1; j++) elementData[j] = elementData[j + 1];
                size--;
            }
            else Console.WriteLine($"Element {o} does not exist in this array");
        }
        public void RemoveAll(T[] a)
        {
            foreach (T e in a) Remove(e);
        }
        public void RetainAll(T[] a)
        {
            bool flag;
            foreach (T e1 in elementData)
            {
                flag = false;
                foreach (T e2 in a)
                {
                    if (e2.Equals(e1)) flag = true;
                }
                if (!flag) Remove(e1);
            }
        }

        public int Size() => this.size;

        public T[] ToArray()
        {
            T[] a = new T[this.size];
            for (int i = 0; i < size; i++) a[i] = elementData[i];
            return a;
        }
        public T[] ToArray(T[] a)
        {
            if (a.Length < size)
            {
                Console.WriteLine("Not enough capacity for the dynamic array");
                return null;
            }
            if (a == null) a = new T[this.size];
            for (int i = 0; i < a.Length; i++) a[i] = elementData[i];
            return a;
        }

        public void Add(int index, T e)
        {
            if (index < 0 || index > size) throw new ArgumentOutOfRangeException("index");
            if (size == elementData.Length)
            {
                T[] newArray = new T[size + 1];
                int i = 0;
                foreach (T a in elementData) newArray[i++] = a;
                elementData = newArray;
            }
            for (int i = size - 1; i >= index; i--) elementData[i + 1] = elementData[i];
            size++;
            elementData[index] = e;
        }
        public void AddAll(int index, T[] a)
        {
            foreach (T e in a) Add(index++, e);
        }

        public T Get(int index)
        {
            if (index < 0 || index > size) throw new ArgumentOutOfRangeException("index");
            return elementData[index];
        }

        public int IndexOf(object o)
        {
            if (!Contains(o)) return -1;
            int i = 0;
            while (true) if (elementData[i++].Equals(o)) return i;
        }
        public int LastIndexOf(object o)
        {
            if (!Contains(o)) return -1;
            int i = 0;
            int index = 0;
            while (i < elementData.Length) if (elementData[i++].Equals(o)) index = i;
            return index;
        }

        public T Remove(int index)
        {
            if (index < 0 || index > size) throw new ArgumentOutOfRangeException("index");
            T elem = elementData[index];
            Remove(elem);
            return elem;
        }

        public void Set(int index, T e)
        {
            if (index < 0 || index > size) throw new ArgumentOutOfRangeException("index");
            elementData[index] = e;
        }
        public T[] SubList(int fromIndex, int toIndex)
        {
            if (fromIndex < 0 || toIndex < 0 || fromIndex > size || toIndex > size) throw new ArgumentOutOfRangeException("index");
            T[] a = new T[toIndex - fromIndex];
            for (int i = fromIndex; i < toIndex; i++) a[i - fromIndex] = elementData[i];
            return a;
        }

        public void Print()
        {
            if (size != 0)
            {
                for (int i = 0; i < size; i++) Console.Write(elementData[i] + " ");
                Console.WriteLine();
            }
        }
    }
}
