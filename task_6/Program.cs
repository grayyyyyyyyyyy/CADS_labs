using System.Drawing;

class Program
{
    public class MyVector<T>
    {
        T[] elementData;
        int elementCount;
        int capacityIncrement;

        public MyVector(int initialCapacity, int capacityIncr)
        {
            elementData = new T[initialCapacity];
            elementCount = 0;
            capacityIncrement = capacityIncr;
        }
        public MyVector(int initialCapacity)
        {
            elementData = new T[initialCapacity];
            elementCount = 0;
            capacityIncrement = 0;
        }
        public MyVector()
        {
            elementData = new T[10];
            elementCount = 0;
            capacityIncrement = 0;
        }
        public MyVector(T[] a)
        {
            elementData =  new T[a.Length];
            elementCount = a.Length;
            for (int i =0; i< a.Length; i++) elementData[i]= a[i];
        }

        public void Add(T e)
        {
            if (elementCount == elementData.Length)
            {
                T[] newArray = new T[(int)(elementData.Length * 1.5 + 1)];
                int i = 0;
                foreach (T a in elementData) newArray[i++] = a;
                elementData = newArray;
            }
            elementData[elementCount++] = e;
        }
        public void AddAll(T[] a)
        {
            foreach (T e in a) Add(e);
        }
        public void Clear()
        {
            elementCount = 0;
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
            return elementCount == 0;
        }

        public void Remove(object o)
        {
            if (Contains(o))
            {
                int i = 0;
                while (!elementData[i].Equals(o)) i++;
                for (int j = i; j < elementData.Length - 1; j++) elementData[j] = elementData[j + 1];
                elementCount--;
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

        public int Size() => this.elementCount;

        public T[] ToArray()
        {
            T[] a = new T[this.elementCount];
            for (int i = 0; i < elementCount; i++) a[i] = elementData[i];
            return a;
        }
        public T[] ToArray(T[] a)
        {
            if (a.Length < elementCount)
            {
                Console.WriteLine("Not enough capacity for the dynamic array");
                return null;
            }
            if (a == null) a = new T[this.elementCount];
            for (int i = 0; i < a.Length; i++) a[i] = elementData[i];
            return a;
        }

        public void Add(int index, T e)
        {
            if (index < 0 || index > elementCount) throw new ArgumentOutOfRangeException("index");
            if (elementCount == elementData.Length)
            {
                T[] newArray = new T[elementCount + 1];
                int i = 0;
                foreach (T a in elementData) newArray[i++] = a;
                elementData = newArray;
            }
            for (int i = elementCount - 1; i >= index; i--) elementData[i + 1] = elementData[i];
            elementCount++;
            elementData[index] = e;
        }
        public void AddAll(int index, T[] a)
        {
            foreach (T e in a) Add(index++, e);
        }

        public T Get(int index)
        {
            if (index < 0 || index > elementCount) throw new ArgumentOutOfRangeException("index");
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
            if (index < 0 || index > elementCount) throw new ArgumentOutOfRangeException("index");
            T elem = elementData[index];
            Remove(elem);
            return elem;
        }

        public void Set(int index, T e)
        {
            if (index < 0 || index > elementCount) throw new ArgumentOutOfRangeException("index");
            elementData[index] = e;
        }
        public T[] SubList(int fromIndex, int toIndex)
        {
            if (fromIndex < 0 || toIndex < 0 || fromIndex > elementCount || toIndex > elementCount) throw new ArgumentOutOfRangeException("index");
            T[] a = new T[toIndex - fromIndex];
            for (int i = fromIndex; i < toIndex; i++) a[i - fromIndex] = elementData[i];
            return a;
        }
        public T FirstElement()
        {
            if (elementCount == 0) throw new ArgumentOutOfRangeException();
            else return elementData[0];
        }
        public T LastElement()
        {
            if (elementCount == 0) throw new ArgumentOutOfRangeException();
            return elementData[elementCount-1];
        }
        public void RemoveElement(int pos)
        {
            if (pos < 0 || pos > elementCount-1) throw new ArgumentOutOfRangeException();
            for (int i = pos; i < elementCount - 1; i++) elementData[i] = elementData[i + 1];
            elementCount--;   
        }
        public void RemoveRange(int begin, int end)
        {
            for (int i = begin; i < end; i++) RemoveElement(begin);
        }

        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < size; i++) s += elementData[i] + " ";
            return s;
        }
        public void Print()
        {
            Console.WriteLine(this.ToString());
        }
    }
    public static void Main(string[] args)
    {
        int[] a = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        MyVector<int> myVector = new MyVector<int>();
        myVector.AddAll(a);
        myVector.Add(24);
        myVector.Print();
        Console.WriteLine(myVector.Size());
        Console.WriteLine(myVector.Get(4));
        Console.WriteLine(myVector.LastElement());
        Console.WriteLine(myVector.IndexOf(100));
        myVector.Set(4, 100);
        myVector.Print();
        myVector.RemoveRange(2, 5); myVector.Print();
        return;
    }
}
