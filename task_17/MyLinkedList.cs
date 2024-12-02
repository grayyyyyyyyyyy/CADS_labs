using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedList
{
    public class MyLinkedList<T>
    {
        private class LinkedElement<T>
        {
            public T value;
            public LinkedElement<T> next;
            public LinkedElement<T> prev;

            public LinkedElement(T value)
            {
                next = null;
                prev = next;
                this.value = value;
            }
        }

        LinkedElement<T> first;
        LinkedElement<T> last;
        int size;

        //1
        public MyLinkedList()
        {
            size = 0;
            first = null;
            last = null;
        }
        //2
        public MyLinkedList(T[] a)
        {
            foreach (T t in a) Add(t);
        }

        //3
        public void Add(T e)
        {
            LinkedElement<T> element = new LinkedElement<T>(e);
            if (size == 0)
            {
                first = element;
                last = element;
            }
            else
            {
                last.next = element;
                element.prev = last;
                last = element;
            }
            size++;
        }
        //4
        public void AddAll(T[] a)
        {
            foreach (T t in a) Add(t);
        }
        //5
        public void Clear()
        {
            first = null;
            last = null;
            size = 0;
        }
        //6
        public bool Contains(object o)
        {
            LinkedElement<T> element = first;
            while (element != null)
            {
                if (element.value.Equals(o)) return true;
                element = element.next;
            }
            return false;
        }
        //7
        public bool ContainsAll(T[] a)
        {
            int contains = 0;
            foreach (T t in a) if (Contains(t)) contains++;
            return contains == a.Length;
        }
        //8
        public bool IsEmpty() => size == 0;
        //9
        public void Remove(object o)
        {
            if (Contains(o))
            {
                if (first.value.Equals(o))
                {
                    first = first.next;
                    first.prev = null;
                    size--;
                    return;
                }
                LinkedElement<T> element = first;
                while (element != null)
                {
                    element = element.next;
                    if (element.value.Equals(o))
                    {
                        element.prev.next = element.next;
                        element = element.next;
                        element.prev = element.prev.prev;
                        size--;
                        return;
                    }
                }
            }
        }
        //10
        public void RemoveAll(T[] a)
        {
            foreach (T t in a) Remove(t);
        }
        //11
        public void RetainAll(T[] array)
        {
            T[] newArray = new T[array.Length];
            int index = 0;
            for (int i = 0; i < size; i++)
            {
                int fl = 0;
                for (int j = 0; j < array.Length; j++)
                {
                    if (Get(i).Equals(array[j]))
                    {
                        fl = 0;
                        break;
                    }
                    else
                        fl = 1;
                }
                if (fl == 1)
                    Remove(Get(i));
            }
        }
        //12
        public int Size() => size;
        //13
        public T[] ToArray()
        {
            int i = 0;
            T[] array = new T[size];
            LinkedElement<T> element = first;
            while (element != null)
            {
                array[i++] = element.value;
                element = element.next;
            }
            return array;
        }
        //14
        public T[] ToArray(T[] a)
        {
            if (a == null || a.Length < size) a = new T[size];
            a = ToArray();
            return a;
        }


        //15
        public void Add(int index, T e)
        {
            if (index < 0 || index >= size) throw new IndexOutOfRangeException("index");
            LinkedElement<T> newElement = new LinkedElement<T>(e);
            LinkedElement<T> element = first;
            int current = 0;
            while (current != index)
            {
                element = element.next;
                current++;
            }
            if (index == 0)
            {
                first.prev = newElement;
                newElement.next = first;
                first = newElement;
                return;
            }
            if (index == size - 1)
            {
                last.next = newElement;
                newElement.prev = last;
                last = newElement;
                return;
            }

            newElement.next = element;
            newElement.prev = element.prev;
            element.prev.next = newElement;
            element.prev = newElement;
        }
        //16
        public void AddAll(int index, T[] a)
        {
            for (int i = a.Length - 1; i >= 0; i--) Add(index, a[i]);
        }
        //17
        public T Get(int index)
        {
            if (index < 0 || index >= size) throw new IndexOutOfRangeException("index");
            LinkedElement<T> element = first;
            int current = 0;
            while (current != index)
            {
                element = element.next;
                current++;
            }
            return element.value;
        }
        //18
        public int IndexOf(object o)
        {
            if (!Contains(o)) return -1;
            int index = 0;
            LinkedElement<T> element = first;
            while (!element.value.Equals(o))
            {
                element = element.next;
                index++;
            }
            return index;
        }
        //19
        public int LastIndexOf(object o)
        {
            if (!Contains(o)) return -1;
            int index = -1;
            int current = 0;
            LinkedElement<T> element = first;
            while (element != null)
            {
                if (element.value.Equals(o)) index = current;
                current++;
                element = element.next;
            }
            return index;
        }
        //20
        public T RemoveI(int index)
        {
            T item = Get(index);
            Remove(item);
            return item;
        }
        //21
        public void Set(int index, T e)
        {
            int current = 0;
            LinkedElement<T> element = first;
            while (current != index)
            {
                current++;
                element = element.next;
            }
            element.value = e;
        }
        //22
        public T[] sublist(int fromIndex, int toIndex)
        {
            if (fromIndex < 0 || toIndex < 0 || fromIndex >= toIndex || fromIndex > size - 1 || toIndex > size - 1) throw new ArgumentOutOfRangeException("ti durak?");
            T[] result = new T[toIndex - fromIndex];
            int current = 0;
            LinkedElement<T> element = first;
            while (current != fromIndex)
            {
                current++;
                element = element.next;
            }
            while (current != toIndex - 1)
            {
                result[current++ - fromIndex] = element.value;
            }
            return result;
        }

        //23
        public T Element() => first.value;
        //24
        public bool Offer(T obj)
        {
            Add(obj);
            return Contains(obj);
        }
        //25-30
        public T Peek() => IsEmpty() ? default(T) : first.value;
        //public T Poll() => IsEmpty() ? default(T) : Remove(0);
        public void AddFirst(T obj) => Add(0, obj);
        public void AddLast(T obj) => Add(size - 1, obj);
        public T GetFirst() => first.value;
        public T GetLast() => Get(size - 1);
        //31
        public bool OfferFirst(T obj)
        {
            AddFirst(obj);
            return Get(0).Equals(obj);
        }
        //32
        public bool OfferLast(T obj)
        {
            AddLast(obj);
            return Get(size - 1).Equals(obj);
        }
        //33-40
        //public T Pop() => Remove(0);
        public void Push(T obj) => AddFirst(obj);
        public T PeekFirst() => IsEmpty() ? default(T) : GetFirst();
        public T PeekLast() => IsEmpty() ? default(T) : GetLast();
        //public T PollFirst() => Poll();
        //public T PollLast() => IsEmpty() ? default(T) : Remove(size - 1);
        //public T RemoveLast() => Remove(size - 1);
        //public T RemoveFirst() => Pop();

        //41
        //public bool RemoveLastOccurance(object obj)
        //{
        //    if (!Contains(obj)) return false;
        //    T elem = Remove(LastIndexOf(obj));
        //    return true;
        //}
        ////42
        //public bool RemoveFirstOccurance(object obj)
        //{
        //    if (!Contains(obj)) return false;
        //    T elem = Remove(IndexOf(obj));
        //    return true;
        //}

        public void Print()
        {
            if (IsEmpty()) return;
            LinkedElement<T> element = first;
            while (element != null)
            {
                Console.Write(element.value + " ");
                element = element.next;
            }
            Console.WriteLine();
        }

    }
}
