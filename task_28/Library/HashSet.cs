using HashMap;
using System.ComponentModel;
using System.Xml.Linq;
using Library;
using System.Linq;
using System;
namespace Library
{
    public class MyHashSet<T> where T : IComparable<T>
    {
        public MyIteratorSet<T> ListIterator() => new MyItr(this);
        public class MyItr : MyIteratorSet<T>
        {
            MyHashSet<T> hashSet;
            T cursor = default(T);
            int lenght = -1;
            T[] mas;
            public T Cursor
            {
                get => cursor;
            }
            public MyItr(MyHashSet<T> array)
            {
                hashSet = array;
                mas = hashSet.map.KeySet();
            }
            public T Next()
            {
                cursor = mas[++lenght];
                return cursor;
            }
            public bool HasNext()
            {
                if (lenght + 1 == mas.Length)
                    return false;
                return true;
            }
            public void Remove()
            {
                hashSet.Remove(mas[lenght]);
            }
        }
        private MyHashMap<T, object> map;

        // 1, 3, 4, 5, 7, 8, 10, 11, 14
        public MyHashSet() => map = new MyHashMap<T, object>();
        public MyHashSet(int initialCapacity, float loadFactor) => map = new MyHashMap<T, object>(initialCapacity, loadFactor);
        public MyHashSet(int initialCapacity) => map = new MyHashMap<T, object>(initialCapacity);
        public void Add(T element) => map.Put(element, false);
        public void Clear() => map.Clear();
        public bool Contains(T element) => map.ContainsKey(element);
        public bool IsEmpty() => map.IsEmpty();
        public void Remove(T element) => map.Remove(element);
        public int Size() => map.Size();

        // 2
        public MyHashSet(T[] array)
        {
            foreach (T item in array)
                map.Put(item, false);
        }

        // 6
        public void AddAll(T[] array)
        {
            foreach (T item in array)
                map.Put(item, false);
        }

        // 9
        public bool ContainsAll(T[] array)
        {
            foreach (T item in array)
                if (!map.ContainsKey(item))
                    return false;
            return true;
        }

        // 12
        public void RemoveAll(T[] array)
        {
            foreach (T item in array)
                map.Remove(item);
        }

        // 13
        public void RetainAll(T[] array)
        {
            T[] key = map.KeySet();
            foreach (T item in key)
                if (array.Contains(item))
                    map.Remove(item);
        }

        // 15
        public T[] ToArray()
        {
            T[] array = map.KeySet();
            return array;
        }

        // 16
        public T[] ToArray(T[] array)
        {
            if (array == null)
                return ToArray();
            T[] newArray = new T[array.Length + map.Size()];
            int ind = 0;
            for (int i = 0; i < array.Length; i++)
                newArray[ind++] = array[i];
            T[] retArray = map.KeySet();
            foreach (T item in retArray)
                newArray[ind++] = item;
            return newArray;
        }

        // 17
        public T First()
        {
            T[] array = map.KeySet();
            T minimum = array[0];
            foreach (T item in array)
                if (minimum.CompareTo(item) > 0)
                    minimum = item;
            return minimum;
        }

        // 18
        public T Last()
        {
            T[] array = map.KeySet();
            T maximum = array[0];
            foreach (T item in array)
                if (maximum.CompareTo(item) < 0)
                    maximum = item;
            return maximum;
        }

        // 19
        public MyHashSet<T> SubSet(T fromElement, T toElement)
        {
            MyHashSet<T> subSet = new MyHashSet<T>();
            T[] array = map.KeySet();
            foreach (T item in array)
                if (item.CompareTo(fromElement) >= 0 && item.CompareTo(toElement) <= 0)
                    subSet.Add(item);
            return subSet;
        }

        // 20
        public MyHashSet<T> HeadSet(T toElement)
        {
            MyHashSet<T> headSet = new MyHashSet<T>();
            T[] array = map.KeySet();
            foreach (T item in array)
                if (item.CompareTo(toElement) < 0)
                    headSet.Add(item);
            return headSet;
        }

        // 21
        public MyHashSet<T> TailSet(T fromElement)
        {
            MyHashSet<T> tailSet = new MyHashSet<T>();
            T[] array = map.KeySet();
            foreach (T item in array)
                if (item.CompareTo(fromElement) > 0)
                    tailSet.Add(item);
            return tailSet;
        }
    }
}