using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyVectorLib
{
    //public class MyVector<T>
    //{
    //    public T[] elementData;
    //    public int elementCount;
    //    public int capacityIncrement;

    //    public MyVector(int initialCapacity, int capacityIncr)
    //    {
    //        elementData = new T[initialCapacity];
    //        elementCount = 0;
    //        capacityIncrement = capacityIncr;
    //    }
    //    public MyVector(int initialCapacity)
    //    {
    //        elementData = new T[initialCapacity];
    //        elementCount = 0;
    //        capacityIncrement = 0;
    //    }
    //    public MyVector()
    //    {
    //        elementData = new T[10];
    //        elementCount = 0;
    //        capacityIncrement = 0;
    //    }
    //    public MyVector(T[] a)
    //    {
    //        elementData = new T[a.Length];
    //        elementCount = a.Length;
    //        for (int i = 0; i < a.Length; i++) elementData[i] = a[i];
    //    }

    //    public void Add(T e)
    //    {
    //        if (elementCount == elementData.Length)
    //        {
    //            T[] newArray = new T[(int)(elementData.Length * 1.5 + 1)];
    //            int i = 0;
    //            foreach (T a in elementData) newArray[i++] = a;
    //            elementData = newArray;
    //        }
    //        elementData[elementCount++] = e;
    //    }
    //    public void AddAll(T[] a)
    //    {
    //        foreach (T e in a) Add(e);
    //    }
    //    public void Clear()
    //    {
    //        elementCount = 0;
    //        Array.Clear(elementData);
    //    }

    //    //public bool Contains(object o)
    //    //{
    //    //    if (!this.IsEmpty())
    //    //    {
    //    //        foreach (T a in elementData) if (a.Equals(o)) return true;
    //    //    }
    //    //    return false;
    //    //}
    //    public bool Contains(object o)
    //    {
    //        for (int i = 0; i < this.Size(); i++) if (o.Equals(this.Get(i))) return true;
    //        return false;
    //    }
    //    public bool ContainsAll(T[] a)
    //    {
    //        foreach (T e in a) if (!Contains(e)) return false;
    //        return true;
    //    }
    //    public bool IsEmpty()
    //    {
    //        return elementCount == 0;
    //    }

    //    public void Remove(object o)
    //    {
    //        if (Contains(o))
    //        {
    //            int i = 0;
    //            while (!elementData[i].Equals(o)) i++;
    //            for (int j = i; j < elementData.Length - 1; j++) elementData[j] = elementData[j + 1];
    //            elementCount--;
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

    //    public int Size() => this.elementCount;

    //    public T[] ToArray()
    //    {
    //        T[] a = new T[this.elementCount];
    //        for (int i = 0; i < elementCount; i++) a[i] = elementData[i];
    //        return a;
    //    }
    //    public T[] ToArray(T[] a)
    //    {
    //        if (a.Length < elementCount)
    //        {
    //            Console.WriteLine("Not enough capacity for the dynamic array");
    //            return null;
    //        }
    //        if (a == null) a = new T[this.elementCount];
    //        for (int i = 0; i < a.Length; i++) a[i] = elementData[i];
    //        return a;
    //    }

    //    public void Add(int index, T e)
    //    {
    //        if (index < 0 || index > elementCount) throw new ArgumentOutOfRangeException("index");
    //        if (elementCount == elementData.Length)
    //        {
    //            T[] newArray = new T[elementCount + 1];
    //            int i = 0;
    //            foreach (T a in elementData) newArray[i++] = a;
    //            elementData = newArray;
    //        }
    //        for (int i = elementCount - 1; i >= index; i--) elementData[i + 1] = elementData[i];
    //        elementCount++;
    //        elementData[index] = e;
    //    }
    //    public void AddAll(int index, T[] a)
    //    {
    //        foreach (T e in a) Add(index++, e);
    //    }

    //    public T Get(int index)
    //    {
    //        if (index < 0 || index > elementCount) throw new ArgumentOutOfRangeException("index");
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
    //        if (index < 0 || index > elementCount) throw new ArgumentOutOfRangeException("index");
    //        T elem = elementData[index];
    //        Remove(elem);
    //        return elem;
    //    }

    //    public void Set(int index, T e)
    //    {
    //        if (index < 0 || index > elementCount) throw new ArgumentOutOfRangeException("index");
    //        elementData[index] = e;
    //    }
    //    public T[] SubList(int fromIndex, int toIndex)
    //    {
    //        if (fromIndex < 0 || toIndex < 0 || fromIndex > elementCount || toIndex > elementCount) throw new ArgumentOutOfRangeException("index");
    //        T[] a = new T[toIndex - fromIndex];
    //        for (int i = fromIndex; i < toIndex; i++) a[i - fromIndex] = elementData[i];
    //        return a;
    //    }
    //    public T FirstElement()
    //    {
    //        if (elementCount == 0) throw new ArgumentOutOfRangeException();
    //        else return elementData[0];
    //    }
    //    public T LastElement()
    //    {
    //        if (elementCount == 0) throw new ArgumentOutOfRangeException();
    //        return elementData[elementCount - 1];
    //    }
    //    public void RemoveElement(int pos)
    //    {
    //        if (pos < 0 || pos > elementCount - 1) throw new ArgumentOutOfRangeException();
    //        for (int i = pos; i < elementCount - 1; i++) elementData[i] = elementData[i + 1];
    //        elementCount--;
    //        Array.Resize(ref elementData, elementCount);
    //    }
    //    public void RemoveRange(int begin, int end)
    //    {
    //        for (int i = begin; i < end; i++) RemoveElement(begin);
    //    }
    //    public T RemoveByIndex(int index)
    //    {
    //        if (index < 0 || index > elementCount) throw new ArgumentOutOfRangeException("index");
    //        T element = elementData[index];
    //        for (int j = index; j < elementData.Length - 1; j++) elementData[j] = elementData[j + 1];
    //        elementCount--;
    //        Array.Resize(ref elementData, elementCount);
    //        return element;
    //    }

    //    public virtual void Print()
    //    {
    //        if (elementCount != 0)
    //        {
    //            for (int i = 0; i < elementCount; i++) Console.Write(elementData[i] + " ");
    //            Console.WriteLine();
    //        }
    //    }
    //}
    public class MyVector<T>
    {
        private int elementCount;
        private T[] elementData;
        private int capacityIncrement;
        public MyVector()
        {
            elementData = new T[10];
            elementCount = 0;
            capacityIncrement = 0;
        }
        public MyVector(int initialCapacity, int capacityInc)
        {
            elementCount = 0;
            elementData = new T[initialCapacity];
            capacityIncrement = capacityInc;
        }
        public MyVector(int capacity)
        {
            elementCount = 0;
            elementData = new T[capacity];
            capacityIncrement = 0;
        }
        public MyVector(T[] array)
        {
            elementCount = array.Length;
            elementData = new T[elementCount];
            for (int i = 0; i < elementCount; i++) elementData[i] = array[i];
            capacityIncrement = 0;
        }
        public void Add(T element)
        {
            if (elementCount == elementData.Length) Resize();
            elementData[elementCount] = element;
            elementCount++;
        }
        public void AddAll(T[] array)
        {
            int j = elementCount;
            while (elementCount + array.Length > elementData.Length) Resize();
            for (int i = 0; i < array.Length; i++) elementData[j++] = array[i];
            elementCount += array.Length;
        }
        public void Resize()
        {
            T[] newArray;
            if (capacityIncrement != 0) newArray = new T[elementData.Length + capacityIncrement];
            else newArray = new T[elementData.Length * 2];
            for (int i = 0; i < elementData.Length; i++) newArray[i] = elementData[i];
            Array.Clear(elementData);
            elementData = newArray;
        }
        public void Clear()
        {
            Array.Clear(elementData);
            elementCount = 0;
        }
        public bool Contains(T obj)
        {
            foreach (T member in elementData) if (member.Equals(obj)) return true;
            return false;
        }
        public bool ContainsAll(T[] array)
        {
            bool flag = true;
            foreach (T checkMember in array)
                if (flag)
                {
                    flag = false;
                    foreach (T member in elementData)
                        if (checkMember.Equals(member))
                        {
                            flag = true;
                            break;
                        }
                }
                else break;
            return flag;
        }
        public bool IsEmpty()
        {
            if (elementCount == 0) return true;
            return false;
        }
        public void Remove(T obj)
        {
            for (int i = 0; i < elementCount; i++)
                if (elementData[i].Equals(obj))
                {
                    for (int j = i; j < elementData.Length - 1; j++) elementData[j] = elementData[j + 1];
                    elementCount--;
                    Array.Resize(ref elementData, elementCount);
                    return;
                }
            Console.WriteLine($"Элемента {obj} нет в динамическом массиве");
        }
        public void RemoveAll(T[] array)
        {
            foreach (T member in array) Remove(member);
        }
        public void RetainAll(T[] array)
        {
            T[] arrayCopy = new T[elementData.Length];
            for (int i = 0; i < elementData.Length; i++) arrayCopy[i] = elementData[i];
            bool flag = true;
            if (!ContainsAll(array))
            {
                Console.WriteLine("Какие-то элементы не присутствуют в массиве");
                return;
            }
            foreach (T member in arrayCopy)
            {
                foreach (T retainMember in array)
                    if (member.Equals(retainMember))
                    {
                        flag = false;
                        break;
                    }
                if (flag == true) Remove(member);
                flag = true;
            }
            Array.Clear(arrayCopy);
        }
        public int Size()
        {
            return elementCount;
        }
        public T[] ToArray()
        {
            T[] newArray = new T[elementCount];
            for (int i = 0; i < elementCount; i++) newArray[i] = elementData[i];
            return newArray;
        }
        public T[] ToArray(T[] array)
        {
            if (array.Length < elementCount)
            {
                Console.WriteLine("В массиве недостаточно элементов, для хранения элементов динамического массива");
                return array;
            }
            if (array == null) array = new T[elementCount];
            for (int i = 0; i < elementCount; i++) array[i] = elementData[i];
            return array;
        }
        public void Add(int index, T element)
        {
            if (index < 0 || index > elementCount) throw new ArgumentOutOfRangeException("index");
            if (elementCount == elementData.Length) Resize();
            for (int i = elementCount - 1; i >= index; i--) elementData[i + 1] = elementData[i];
            elementData[index] = element;
            elementCount++;
        }
        public void AddAll(int index, T[] array)
        {
            if (index < 0 || index > elementCount) throw new ArgumentOutOfRangeException("index");
            int j = 0;
            while (elementCount + array.Length > elementData.Length) Resize();
            for (int i = elementCount - 1; i >= index; i--) elementData[i + array.Length] = elementData[i];
            for (int i = index; i < index + array.Length; i++) elementData[i] = array[j++];
            elementCount += array.Length;
        }
        public T Get(int index)
        {
            if (index < 0 || index > elementCount) throw new ArgumentOutOfRangeException("index");
            return elementData[index];
        }
        public int IndexOf(T obj)
        {
            for (int i = 0; i < elementCount; i++) if (elementData[i].Equals(obj)) return i;
            return -1;
        }
        public int LastIndexOf(T obj)
        {
            for (int i = elementCount - 1; i <= 0; i--) if (elementData[i].Equals(obj)) return i;
            return -1;
        }
        public T RemoveByIndex(int index)
        {
            if (index < 0 || index > elementCount) throw new ArgumentOutOfRangeException("index");
            T element = elementData[index];
            for (int j = index; j < elementData.Length - 1; j++) elementData[j] = elementData[j + 1];
            elementCount--;
            Array.Resize(ref elementData, elementCount);
            return element;
        }
        public void Set(int index, T element)
        {
            if (index < 0 || index > elementCount) throw new ArgumentOutOfRangeException("index");
            elementData[index] = element;
        }
        public T[] SubList(int fromIndex, int toIndex)
        {
            int j = 0;
            if (fromIndex < 0 || fromIndex > elementCount) throw new ArgumentOutOfRangeException("fromIndex");
            if (toIndex < 0 || toIndex > elementCount) throw new ArgumentOutOfRangeException("toIndex");
            T[] newArray = new T[toIndex - fromIndex];
            for (int i = fromIndex; i < toIndex; i++) newArray[j++] = elementData[i];
            return newArray;
        }
        public T FirstElement()
        {
            return elementData[0];
        }
        public T LastElement()
        {
            return elementData[Size() - 1];
        }
        public void RemoveElementAt(int pos)
        {
            if (pos < 0 || pos > elementCount) throw new ArgumentOutOfRangeException("pos");
            Remove(elementData[pos]);
        }
        public void RemoveRange(int begin, int end)
        {
            if (begin < 0 || begin > elementCount) throw new ArgumentOutOfRangeException("begin");
            if (end < 0 || end > elementCount) throw new ArgumentOutOfRangeException("end");
            for (int i = begin; i < end; i++) Remove(elementData[i]);
        }
        public void Print()
        {
            if (elementCount != 0)
                for (int i = 0; i < elementCount; i++) Console.Write(elementData[i] + " ");
            Console.WriteLine();
        }
    }
}
