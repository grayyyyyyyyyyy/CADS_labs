using Library;
using System;
using System.Linq;
using System.Xml.Linq;
namespace Library
{
    public class MyArrayList<T> : MyList<T> where T : IComparable
    {
        public MyIteratorList<T> IteratorList() => new MyItr(this);
        public MyIteratorList<T> IteratorSet() => new MyItr(this);
        public class MyItr : MyIteratorList<T>
        {
            MyArrayList<T> list;
            int index = 0;
            T cursor;
            public T Cursor
            {
                get => cursor;
            }
            public MyItr(MyArrayList<T> array, int curIndex = -1)
            {
                list = array;
                curIndex = index;
            }
            public bool HasNext()
            {
                if (index == list.Size())
                    return false;
                return true;
            }
            public T Next()
            {
                cursor = list.elementData[index++];
                return cursor;
            }
            public bool HasPrevious()
            {
                if (index - 1 > 0)
                    return false;
                return true;
            }
            public T Previous()
            {
                if (index == 0)
                {
                    index--;
                    return default(T);
                }
                cursor = list.elementData[--index];
                return cursor;
            }
            public int PreviousIndex() => index - 1;
            public void Remove()
            {
                cursor = list.elementData[index];
                list.Remove(cursor);
                index--;
            }
            public void Set(T obj)
            {
                cursor = obj;
                list.elementData[index] = cursor;
            }
            public void Add(T obj)
            {
                if (list.size + 1 == list.elementData.Length)
                {
                    list.Resize();
                    list.elementData[index + 1] = cursor;
                    list.size++;
                    return;
                }
                T item = list.elementData[index + 1];
                list.elementData[index + 1] = cursor;
                for (int i = index + 2; i <= list.size; i++)
                {
                    T tmp = list.elementData[i];
                    list.elementData[i] = item;
                    item = tmp;
                }
                list.size++;
            }
            public int NextIndex() => index + 1;
        }

        int size;
        T[] elementData;

        public int Capacity() => elementData.Length;

        // 1, 9, 13
        public MyArrayList() => size = 0;
        public bool IsEmpty() => size == 0;
        public int Size() => size;
        public MyArrayList(T[] array)
        {
            elementData = new T[(int)(array.Length * 1.5)];
            for (int i = 0; i < array.Length; i++)
                elementData[i] = array[i];
            size = array.Length;
        }

        // 2
        public MyArrayList(MyCollection<T> array)
        {
            elementData = new T[array.Size() * 2];
            var iter = array.IteratorList();
            int index = 0;
            while (iter.HasNext())
            {
                elementData[++index] = iter.Next();
                size++;
            }
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
            if (elementData == null)
                return;
            size += 1;
            if (size > elementData.Length)
                Resize();
            elementData[--size] = element;
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
        public void AddAll(int index, MyCollection<T> array)
        {
            var iter = array.IteratorList();
            while (iter.HasNext())
            {
                T element = iter.Next();
                Add(index, element);
            }
        }

        // 6
        public void Clear()
        {
            elementData = null;
            size = 0;
        }

        // 7
        public bool Contains(T element)
        {
            for (int i = 0; i < size; i++)
                if (elementData[i].Equals(element))
                    return true;
            return false;
        }

        // 8
        public bool ContainsAll(MyCollection<T> array)
        {
            var iter = array.IteratorList();
            while (iter.HasNext())
            {
                T element = iter.Next();
                foreach (T item in elementData)
                    if (item.Equals(element))
                        return true;
                return false;
            }
            return true;
        }

        // 10
        public void Remove(T element)
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
        public void RemoveAll(MyCollection<T> array)
        {
            var iter = array.IteratorList();
            while (iter.HasNext())
            {
                T item = iter.Next();
                for (int i = 0; i < elementData.Length; i++)
                    if (elementData[i].Equals(item))
                    {
                        if (i == elementData.Length - 1)
                        {
                            size -= 1;
                            return;
                        }
                        for (int j = 0; j < elementData.Length - 1; j++)
                            if (j >= i)
                                elementData[j] = elementData[j + 1];
                        size -= 1;
                    }
            }
        }

        // 12
        public void RetainAll(MyCollection<T> array)
        {
            for (int i = 0; i < size;)
            {
                if (!Contains(elementData[i]))
                {
                    if (i == size - 1)
                        size -= 1;
                    else
                    {
                        for (int index = i; index < size; index++)
                            elementData[index] = elementData[index + 1];
                        size -= 1;
                    }
                }
                else i++;
            }
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
            return elementData[index];
        }

        // 19
        public int IndexOf(T element)
        {
            for (int i = 0; i < size; i++)
                if (element.Equals(elementData[i]))
                    return i;
            return -1;
        }

        // 20
        public int LastIndexOf(T element)
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
        public T[] SubList(int fromIndex, int toIndex)
        {
            if (fromIndex < 0 || fromIndex >= size)
                throw new ArgumentOutOfRangeException("fromIndex");
            if (toIndex < 0 || toIndex >= size)
                throw new ArgumentOutOfRangeException("toIndex");
            T[] list = new T[toIndex - fromIndex + 1];
            for (int i = toIndex; i <= fromIndex; i++)
                list[i] = elementData[i];
            return list;
        }

        public void Print()
        {
            for (int i = 0; i < size; i++)
                Console.WriteLine($"{elementData[i]} ");
            Console.WriteLine();
        }

        public void AddAll(MyCollection<T> array)
        {
            var iter = array.IteratorList();
            while (iter.HasNext())
            {
                T element = iter.Next();
                Add(element);
            }
        }
    }
}