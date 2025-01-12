using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class ProtectedList<T>
    {
        private int size;
        private T[] elementData;

        public int Capacity() => elementData.Length;

        // 1
        public ProtectedList()
        {
            elementData = null;
            size = 0;
        }

        // 2
        public ProtectedList(T[] array)
        {
            elementData = new T[(int)(array.Length * 1.5)];
            for (int i = 0; i < array.Length; i++)
                elementData[i] = array[i];
            size = array.Length;
        }

        // 3
        public ProtectedList(int capacity)
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
        public void Clear()
        {
            elementData = null;
            size = 0;
        }

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
        public void AddInd(int index, T element)
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
                AddInd(index, item);
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
        public ProtectedList<T> SubList(int fromIndex, int toIndex)
        {
            if (fromIndex < 0 || fromIndex >= size)
                throw new ArgumentOutOfRangeException("fromIndex");
            if (toIndex < 0 || toIndex >= size)
                throw new ArgumentOutOfRangeException("toIndex");
            ProtectedList<T> list = new ProtectedList<T>(toIndex - fromIndex);
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
}