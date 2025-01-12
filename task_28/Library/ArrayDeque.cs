using Library;
using System;
using System.Drawing;

public class MyArrayDeque<T>
{
    public MyIteratorSet<T> ListIterator() => new MyItr(this);
    public class MyItr : MyIteratorSet<T>
    {
        MyArrayDeque<T> Deque;
        private T cursor;
        int lenght;
        int tail = 0;
        bool flag = false;
        public T Cursor
        {
            get => cursor;
        }
        public MyItr(MyArrayDeque<T> array)
        {
            Deque = array;
            lenght = array.Size();
        }
        public T Next()
        {
            cursor = Deque.ToArray()[tail];
            tail++;
            return cursor;
        }
        public bool HasNext()
        {
            if (tail >= Deque.Size() || tail == -1)
                return false;
            return true;
        }
        public void Remove()
        {
            tail = Deque.IndexOf(cursor);
            Deque.Remove(cursor);
            flag = true;
        }
    }

    private T[] elements;
    private int head;
    private int tail;

    // support

    public int IndexOf(T element)
    {
        T item = element;
        if (tail == -1)
            return -1;
        for (int i = 0; i < tail; i++)
            if (elements[i].Equals(element))
                return i;
        return -1;
    }

    public int Get(T item)
    {
        for (int i = 0; i < tail; i++)
            if (item.Equals(elements[i]))
                return i;
        return -1;
    }
    public void Print()
    {
        for (int i = 0; i < tail; i++)
            Console.WriteLine(elements[i]);
    }

    // 1
    public MyArrayDeque()
    {
        elements = new T[16];
        tail = 0;
        head = 0;
    }

    // 2
    public MyArrayDeque(T[] array)
    {
        elements = new T[array.Length];
        head = 0;
        tail = array.Length;
        for (int i = 0; i < array.Length; i++)
            elements[i] = array[i];
    }

    // 3
    public MyArrayDeque(int numElements)
    {
        elements = new T[numElements];
        head = 0;
        tail = numElements;
    }

    // 4
    public void Add(T item)
    {
        if (tail == elements.Length)
        {
            T[] array = new T[(tail * 2) + 1];
            for (int i = 0; i < tail; i++)
                array[i] = elements[i];
            elements = array;
            elements[tail] = item;
            tail++;
        }
        else
            elements[tail++] = item;
    }

    // 5
    public void AddAll(T[] array)
    {
        foreach (T item in array)
            Add(item);
    }

    // 6, 9, 13, 14, 16, 21, 22, 23
    public void Clear() => tail = 0;
    public bool IsEmpty() => tail == 0;
    public int Size() => tail;
    public T[] ToArray() => elements;
    public T Element() => elements[head];
    public void AddLast(T item) => Add(item);
    public T GetFirst() => elements[head];
    public T GetLast() => elements[tail - 1];

    // 7
    public bool Contains(T item)
    {
        foreach (T obj in elements)
            if (obj.Equals(item))
                return true;
        return false;
    }

    // 8
    public bool ContainsAll(T[] array)
    {
        bool[] newArray = new bool[array.Length];
        for (int i = 0; i < array.Length; i++)
            if (Contains(array[i]))
                newArray[i] = true;
        for (int i = 0; i < newArray.Length; i++)
            if (!newArray[i])
                return false;
        return true;
    }

    // 10
    public void Remove(T item)
    {
        if (Contains(item))
        {
            int index = Get(item);
            T[] array = new T[tail--];
            for (int i = 0; i < index; i++)
                array[i] = elements[i];
            for (int i = index; i < tail; i++)
                array[i] = elements[i + 1];
            elements = array;
        }
    }

    // 11
    public void RemoveAll(T[] array)
    {
        foreach (T item in elements)
            Remove(item);
    }

    // 12
    public void RetainAll(T[] array)
    {
        for (int i = 0; i < array.Length; i++)
            elements[i] = array[i];
        tail = array.Length;
    }

    // 15
    public T[] ToArray(T[] array)
    {
        for (int i = 0; i < array.Length; i++)
            Add(array[i]);
        tail += array.Length;
        T[] newArray = new T[tail];
        for (int i = 0; i < newArray.Length; i++)
            newArray[i] = elements[i];
        return newArray;
    }

    // 17
    public bool Offer(T item)
    {
        Add(item);
        if (Contains(item))
            return true;
        return false;
    }

    // 18
    public T Peek()
    {
        if (tail == 0)
            throw new IndexOutOfRangeException("Empty deque");
        else
            return elements[0];
    }

    // 19
    public T Poll()
    {
        if (tail == 0)
            throw new IndexOutOfRangeException("Empty deque");
        T element = elements[head];
        Remove(elements[head]);
        return element;
    }

    // 20
    public void AddFirst(T item)
    {
        T[] array = new T[tail + 1];
        array[0] = item;
        for (int i = 1, j = 0; i < tail + 1; i++, j++)
            array[i] = elements[j];
        elements = array;
        tail++;
    }

    // 24
    public bool OfferFirst(T item)
    {
        AddFirst(item);
        if (Contains(item)) return true;
        else return false;
    }

    // 25
    public bool OfferLast(T item)
    {
        AddLast(item);
        if (Contains(item)) return true;
        else return false;
    }

    // 26
    public T Pop()
    {
        T item = elements[head];
        Remove(item);
        return item;
    }

    // 27
    public void Push(T item)
    {
        T[] array = new T[tail + 1];
        array[0] = item;
        for (int i = 1, j = 0; i < tail + 1; i++, j++)
            array[i] = elements[j];
        elements = array;
        tail++;
    }

    // 28
    public T PeekFirst()
    {
        if (tail == 0)
            throw new IndexOutOfRangeException("Empty deque");
        return GetFirst();
    }

    // 29
    public T PeekLast()
    {
        if (tail == 0)
            throw new IndexOutOfRangeException("Empty deque");
        return GetLast();
    }

    // 30
    public T PollFirst()
    {
        if (tail == 0)
            throw new IndexOutOfRangeException();
        else
        {
            T element = elements[head];
            Remove(elements[head]);
            return element;
        }
    }

    // 31
    public T PollLast()
    {
        if (tail == 0)
            throw new IndexOutOfRangeException();
        else
        {
            T element = elements[tail];
            Remove(elements[tail]);
            return element;
        }
    }

    // 32
    public T RemoveLast()
    {
        T element = elements[tail - 1];
        Remove(elements[tail - 1]);
        return element;
    }

    // 33
    public T RemoveFirst()
    {
        T element = elements[head];
        Remove(elements[head]);
        return element;
    }

    // 34
    public bool RemoveLastOccurrence(T element)
    {
        int index = -1;
        for (int i = 0; i < tail; i++)
            if (elements[i].Equals(element))
            {
                index = i;
                break;
            }
        if (index > -1)
        {
            T[] array = new T[tail--];
            for (int i = 0; i < index; i++)
                array[i] = elements[i];
            for (int i = index + 1; i < tail--; i++)
                array[i] = elements[i];
            elements = array;
            tail--;
            return true;
        }
        return false;
    }

    // 35
    public bool RemoveFirstOccurrence(T element)
    {
        int index = -1;
        for (int i = 0; i < tail; i++)
            if (elements[i].Equals(element))
                index = i;
        if (index > -1)
        {
            T[] array = new T[tail--];
            for (int i = 0; i < index; i++)
                array[i] = elements[i];
            for (int i = index + 1; i < tail--; i++)
                array[i] = elements[i];
            elements = array;
            tail--;
            return true;
        }
        return false;
    }
}