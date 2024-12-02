using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayList
{
    public class MyArrayList<T>
    {
        private int size;
        private T[] elementData;

        public int Capacity() => elementData.Length;

        // 1
        public MyArrayList()
        {
            elementData = new T[10];
            size = 0;
        }

        // 2
        public MyArrayList(T[] array)
        {
            elementData = new T[(int)(array.Length * 1.5)];
            for (int i = 0; i < array.Length; i++)
                elementData[i] = array[i];
            size = array.Length;
        }

        // 3
        public MyArrayList(int capacity)
        {
            elementData = new T[capacity];
            size = 0;
        }

        // 4
        public void Add(T element)
        {
            if (size == elementData.Length)
                Resize();
            elementData[size++] = element;
        }
        private void Resize()
        {
            int newCapacity = (int)(elementData.Length * 1.5) + 1;
            T[] newArray = new T[newCapacity];
            for (int i = 0; i < size; i++)
                newArray[i] = elementData[i];
            elementData = newArray;
        }

        //5
        public void AddAll(T[] array)
        {
            foreach (T item in array)
                Add(item);
        }

        // 6
        public void Clear() => size = 0;

        // 7
        public bool Contains(object element)
        {
            for (int i = 0; i < size; i++)
                if (elementData[i].Equals(element))
                    return true;
            return false;
        }

        // 8
        public bool ContainsAll(T[] array)
        {
            foreach (T item in array)
                for (int i = 0; i < size; i++)
                    if (elementData[i].Equals(item))
                        return false;
            return true;
        }

        // 9
        public bool IsEmpty()
        {
            if (elementData == null)
                return false;
            return true;
        }

        // 10
        public void Remove(object element)
        {
            if (Contains(element))
            {
                int flag = 0;
                for (int i = 0; i < size - 1; i++)
                    if (flag == 0)
                    {
                        if (elementData[i].Equals(element))
                        {
                            elementData[i] = elementData[i + 1];
                            flag = 1;
                        }
                    }
                    else if (flag == 1)
                        elementData[i] = elementData[i + 1];
                size--;
            }
        }

        // 11
        public void RemoveAll(T[] array)
        {
            foreach (T item in array)
                Remove(item);
        }

        // 12
        public void RetainAll(T[] array)
        {
            T[] newArray = new T[size];
            int newSize = 0;
            foreach (T item in array)
                for (int i = 0; i < size; i++)
                    if (item.Equals(elementData[i]))
                    {
                        newArray[newSize] = elementData[i];
                        newSize++;
                    }
            elementData = newArray;
            size = newSize;
        }

        // 13
        public int Size()
        {
            return size;
        }

        // 14
        public T[] ToArray()
        {
            T[] array = new T[size];
            for (int i = 0; i < size; i++)
                array[i] = elementData[i];
            return array;
        }

        // 15
        public T[] ToArray(T[] array)
        {
            if (array == null) ToArray();
            for (int i = 0; i < size && i < array.Length; i++)
                array[i] = elementData[i];
            return array;
        }

        // 16
        public void Add(int index, T element)
        {
            if (index >= size)
            {
                Add(element);
                return;
            }
            T[] array = new T[size + 1];
            for (int i = 0, j = 0; i <= size; i++, j++)
            {
                if (i == index)
                {
                    array[j] = element;
                    i++;
                }
                array[j] = elementData[j];
            }
            elementData = array;
            size++;
        }

        // 17
        public void AddAllInd(int index, T[] array)
        {
            foreach (T item in array)
            {
                Add(index, item);
                index++;
            }
        }

        // 18
        public T Get(int index)
        {
            if (index >= size || index < 0)
                throw new ArgumentOutOfRangeException();
            T element = elementData[index];
            return element;
        }

        // 19
        public int IndexOf(object element)
        {
            for (int i = 0; i < size; i++)
                if (element.Equals(elementData[i]))
                    return i;
            return -1;
        }

        // 20
        public int LastIndexOf(object element)
        {
            int index = -1;
            for (int i = 0; index < size; i++)
                if (element.Equals(elementData[i]))
                    index = i;
            return -1;
        }

        // 21
        public T Remove(int index)
        {
            if (index < 0 || index >= size)
                throw new ArgumentOutOfRangeException("index");
            T element = elementData[index];
            Remove(element);
            return element;
        }

        // 22
        public void Set(int index, T element)
        {
            if (index < 0 || index >= size)
                throw new ArgumentOutOfRangeException("index");
            if (element == null)
                throw new ArgumentNullException();
            elementData[index] = element;
        }

        // 23
        public MyArrayList<T> SubList(int fromIndex, int toIndex)
        {
            if (fromIndex < 0 || fromIndex >= size)
                throw new ArgumentOutOfRangeException("fromIndex");
            if (toIndex < 0 || toIndex >= size)
                throw new ArgumentOutOfRangeException("toIndex");
            MyArrayList<T> list = new MyArrayList<T>(toIndex - fromIndex);
            for (int i = 0; i < list.size; i++)
                list.Set(i, elementData[i + fromIndex]);
            return list;
        }

        public void Print()
        {
            for (int i = 0; i < size; i++)
                Console.WriteLine($"{elementData[i]} ");
            Console.WriteLine();
        }
    }
    //public class MyArrayList<T>
    //{
    //    int size;
    //    T[] elementData;

    //    public MyArrayList()
    //    {
    //        size = 0;
    //        elementData = new T[1];
    //    }
    //    public MyArrayList(T[] a)
    //    {
    //        size = a.Length;
    //        elementData = new T[size];
    //        for (int i = 0; i < size; i++) elementData[i] = a[i];

    //    }
    //    public MyArrayList(int capacity)
    //    {
    //        size = 0;
    //        elementData = new T[capacity];
    //    }

    //    //public void Add(T e)
    //    //{
    //    //    if (size == elementData.Length)
    //    //    {
    //    //        T[] newArray = new T[(int)(elementData.Length * 1.5 + 1)];
    //    //        int i = 0;
    //    //        foreach (T a in elementData) newArray[i++] = a;
    //    //        elementData = newArray;
    //    //    }
    //    //    elementData[size++] = e;
    //    //}
    //    public void Add(T element)
    //    {
    //        if (size == elementData.Length)
    //            Resize();
    //        elementData[size++] = element;
    //    }
    //    private void Resize()
    //    {
    //        int newCapacity = (int)(elementData.Length * 1.5) + 1;
    //        T[] newArray = new T[newCapacity];
    //        for (int i = 0; i < size; i++)
    //            newArray[i] = elementData[i];
    //        elementData = newArray;
    //    }
    //    public void AddAll(T[] a)
    //    {
    //        foreach (T e in a) Add(e);
    //    }
    //    public void Clear()
    //    {
    //        size = 0;
    //        elementData = null;
    //    }

    //    public bool Contains(object o)
    //    {
    //        foreach (T a in elementData) if (a.Equals(o)) return true;
    //        return false;
    //    }
    //    public bool ContainsAll(T[] a)
    //    {
    //        foreach (T e in a) if (!Contains(e)) return false;
    //        return true;
    //    }
    //    public bool IsEmpty()
    //    {
    //        return size == 0;
    //    }

    //    public void Remove(object o)
    //    {
    //        if (Contains(o))
    //        {
    //            int i = 0;
    //            while (!elementData[i].Equals(o)) i++;
    //            for (int j = i; j < elementData.Length - 1; j++) elementData[j] = elementData[j + 1];
    //            size--;
    //        }
    //        else Console.WriteLine($"Element {o} does not exist in this array");
    //    }
    //    public void RemoveAll(T[] a)
    //    {
    //        foreach (T e in a) Remove(e);
    //    }
    //    public void RetainAll(T[] a)
    //    {
    //        bool flag;
    //        foreach (T e1 in elementData)
    //        {
    //            flag = false;
    //            foreach (T e2 in a)
    //            {
    //                if (e2.Equals(e1)) flag = true;
    //            }
    //            if (!flag) Remove(e1);
    //        }
    //    }

    //    public int Size() => this.size;

    //    public T[] ToArray()
    //    {
    //        T[] a = new T[this.size];
    //        for (int i = 0; i < size; i++) a[i] = elementData[i];
    //        return a;
    //    }
    //    public T[] ToArray(T[] a)
    //    {
    //        if (a.Length < size)
    //        {
    //            Console.WriteLine("Not enough capacity for the dynamic array");
    //            return null;
    //        }
    //        if (a == null) a = new T[this.size];
    //        for (int i = 0; i < a.Length; i++) a[i] = elementData[i];
    //        return a;
    //    }

    //    public void Add(int index, T e)
    //    {
    //        if (index < 0 || index > size) throw new ArgumentOutOfRangeException("index");
    //        if (size == elementData.Length)
    //        {
    //            T[] newArray = new T[size + 1];
    //            int i = 0;
    //            foreach (T a in elementData) newArray[i++] = a;
    //            elementData = newArray;
    //        }
    //        for (int i = size - 1; i >= index; i--) elementData[i + 1] = elementData[i];
    //        size++;
    //        elementData[index] = e;
    //    }
    //    public void AddAll(int index, T[] a)
    //    {
    //        foreach (T e in a) Add(index++, e);
    //    }

    //    public T Get(int index)
    //    {
    //        if (index < 0 || index > size) throw new ArgumentOutOfRangeException("index");
    //        return elementData[index];
    //    }

    //    public int IndexOf(object o)
    //    {
    //        if (!Contains(o)) return -1;
    //        int i = 0;
    //        while (true) if (elementData[i++].Equals(o)) return i;
    //    }
    //    public int LastIndexOf(object o)
    //    {
    //        if (!Contains(o)) return -1;
    //        int i = 0;
    //        int index = 0;
    //        while (i < elementData.Length) if (elementData[i++].Equals(o)) index = i;
    //        return index;
    //    }

    //    public T Remove(int index)
    //    {
    //        if (index < 0 || index > size) throw new ArgumentOutOfRangeException("index");
    //        T elem = elementData[index];
    //        Remove(elem);
    //        return elem;
    //    }

    //    public void Set(int index, T e)
    //    {
    //        if (index < 0 || index > size) throw new ArgumentOutOfRangeException("index");
    //        elementData[index] = e;
    //    }
    //    public T[] SubList(int fromIndex, int toIndex)
    //    {
    //        if (fromIndex < 0 || toIndex < 0 || fromIndex > size || toIndex > size) throw new ArgumentOutOfRangeException("index");
    //        T[] a = new T[toIndex - fromIndex];
    //        for (int i = fromIndex; i < toIndex; i++) a[i - fromIndex] = elementData[i];
    //        return a;
    //    }


    //    //public override string ToString()
    //    //{
    //    //    string s = "";
    //    //    for (int i = 0; i < size; i++) s += elementData[i] + " ";
    //    //    return s;
    //    //}
    //    public void Print()
    //    {
    //        Console.WriteLine(this.ToString());
    //    }
    //}
}
