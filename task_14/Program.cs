namespace Program
{
    public class MyArrayDeque<T>
    {
        E[] elements;
        int head;
        int tail;

        public MyArrayDeque()
        {
            elements = new T[16];
            tail = 0;
            head = 0;
        }
        public MyArrayDeque(T[] elements)
        {
            this.elements = elements;
            head = 0;
            tail = elements.Length;
        }
        public MyArrayDeque(int numElements)
        {
            this.elements = new T[numElements];
            head = 0;
            tail = 0;
        }

        private void Resize()
        {
            E[] elements = new E[this.elements.Length * 2 + 1];
            for (int i = head; i < tail; i++) elements[i] = this.elements[i];
            this.elements = elements;
        }
        private int FindIndex(object o)
        {
            for (int i = head; i < tail; i++)
            {
                if (elements[i].Equals(o)) return i;
            }
            return -1;
        }

        public void Add(T e)
        {
            if (tail == elements.Length) Resize();
            elements[tail++] = e;
        }
        public void AddAll(T[] e)
        {
            foreach (T a in e) Add(a);
        }
        public void Clear()
        {
            tail = 0;
        }
        public bool Contains(object o)
        {
            for (int i = head; i < tail; i++)
            {
                if (elements[i].Equals(o)) return true;
            }
            return false;
        }
        public bool ContainsAll(T[] a)
        {
            foreach (object o in a)
            {
                if (!Contains(o)) return false;
            }
            return true;
        }
        public bool IsEmpty() => head == tail;
        public void Remove(object o)
        {
            if (Contains(o))
            {
                int k = FindIndex(o);
                T[] array = new T[tail - 1];
                for (int i = head; i < k; i++) array[i] = elements[i];
                for (int i = k; i < tail - 1; i++) array[i] = elements[i + 1];
                tail--;
                elements = array;
            }
        }
        public void RemoveAll(T[] a)
        {
            foreach (T t in a) Remove(t);
        }
        public void RetainAll(T[] a)
        {
            bool flag;
            for (int i = head; i < tail; i++)
            {
                flag = false;
                foreach (T t in a)
                {
                    if (elements[i].Equals(t)) flag = true;
                }
                if (!flag) Remove(elements[i]);
            }
        }
        public int Size() => tail - head + 1;
        public T[] ToArray()
        {
            T[] array = new T[Size()];
            for (int i = head; i < tail; i++) array[i] = elements[i];
            return array;
        }
        public void ToArray(T[] a)
        {
            a = new T[Size()];
            for(int i = head; i < tail; i++) a[i]= elements[i];
        }
        public T Element() => IsEmpty() ? throw new InvalidOperationException() : elements[head];
        public bool Offer(T e)
        {
            if (tail == elements.Length) return false;
            elements[tail++] = e;
            return true;
        }
        public T Peek() => IsEmpty() ? default(T) : elements[head];
        public T Poll()
        {
            if (IsEmpty()) return default(T);
            T t = elements[head];
            Remove(elements[head]);
            return t;
        }

        public void AddFirst(T obj)
        {   
            if (head==0)
            {
                T[] array = new T[Size() + 1];
                array[0] = obj;
                for (int i = 1; i<tail; i++) array[i] = elements[i];
                elements = array;
                return;
            }
            elements[--head] = obj;
        }
        public void AddLast(T obj)
        {
            if (tail == elements.Length)
            {
                T[] array = new T[Size() + 1];
                for (int i = head; i < tail; i++) array[i] = elements[i];
                array[tail++] = obj;
                elements = array;
                return;
            }
            elements[tail++] = obj;
        }
        public T GetFirst() => IsEmpty() ? throw new InvalidOperationException() : elements[0];
        public T GetLast() => IsEmpty() ? throw new InvalidOperationException() : elements.Last();
        public bool OfferFirst(T obj)
        {
            if (head == 0) return false;
            AddFirst(obj);
            return true;
        }
        public bool OfferLast(T obj)
        {
            if (tail==elements.Length) return false;
            AddLast(obj);
            return true;
        }
        public T Pop() => IsEmpty() ? throw new InvalidOperationException() : Poll();
        public void Push(T obj) => AddFirst(obj);
        public T PeekFirst() => Peek();
        public T PeekLast() => IsEmpty() ? throw new InvalidOperationException() : elements[tail - 1];
        public T PollFirst() => Poll();
        public T PollLast()
        {
            if (IsEmpty()) return default(T);
            T t = elements[tail - 1];
            Remove(elements[tail - 1]);
            return t;
        }
        public T RemoveLast() => IsEmpty() ? throw new InvalidOperationException() : PollLast();
        public T RemoveFirst() => IsEmpty() ? throw new InvalidOperationException() : PollFirst();
        bool RemoveLastOccurrence(object obj)
        {
            if (!Contains(obj)) return false;
            int ind;
            for (int i = head; i < tail; i++) if (elements[i].Equals(obj)) ind = i;
            T[] array = new T[tail-1];
            for (int i = head; i< ind; i++) array[i] = elements[i];
            for (int i = ind + 1; i < tail; i++) array[i - 1] = elements[i];
            elements = array;
            tail--;
            return true;
        }
        bool RemoveFirstOccurrence(object obj)
        {
            if (!Contains(obj)) return false;
            Remove(obj);
            return true;
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            int[] array = { 1, 123, 222, -6, -4, 4, 2 };
            MyArrayDeque<int> deque = new MyArrayDeque<int>(array);
            deque.Print();
            deque.Add(26);
            deque.Print();
            deque.AddAll(array);
            deque.Print();
            deque.Clear();
            deque.Print();
            Console.WriteLine(deque.IsEmpty());
            Console.WriteLine(deque.Contains(123));
            deque.AddAll(array);
            Console.WriteLine(deque.Contains(123));
            Console.WriteLine(deque.ContainsAll(array));
            deque.Remove(4);
            deque.Print();
            int[] array2 = { 123, 4, -6 };
            deque.RemoveAll(array2);
            deque.Print();
            int[] array3 = {1, 222, 2, 456, 29 };
            deque.RetainAll(array3);
            deque.Print();
            Console.WriteLine(deque.Size());
            array3 = deque.ToArray();
            foreach (int i in array3) Console.Write(i + " "); Console.WriteLine();
            int[] array4 = { 1, 2, 3 } ;
            deque.ToArray(array4);
            foreach (int i in array4) Console.Write(i + " "); Console.WriteLine();
            Console.WriteLine(deque.Element());
            Console.WriteLine(deque.Offer(43));
            Console.WriteLine(deque.Peek());
            Console.WriteLine(deque.Poll());
            deque.Print();
        }
    }
}
