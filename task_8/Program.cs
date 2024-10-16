using MyVectorLibrary;
 class Program 
{
    public class MyStack<T> : MyVector<T>
    {
        MyVector<T> stack;
        int top;
        public MyStack()
        {
            top = -1;
            stack = new MyVector<T>();
        }

        public void Push(T item)
        {
            this.Add(item);
            top++;
        }
        public T Pop()
        {
            T i = this.LastElement();
            this.Remove(top);
            top--;
            return i;
        }
        public T Peek()
        {
            if (top == -1) throw new Exception("Stack is empty");   
            return this.LastElement();
        }
        public bool Empty()
        {
            return top==-1;
        }
        public int Search(T item)
        {
            if (this.Contains(item)) return top - this.LastIndexOf(item)+1;
            return -1;
        }
        public override void Print()
        {
            base.Print();
        }
    }   
    static void Main(string[] args)
    {
        int[] arr = { 1, 2, 3, 4, 5 };
        MyStack<int> stack = new MyStack<int>();
        for (int i = 0;  i < arr.Length; i++)
        {
            stack.Push(arr[i]);
        }
        stack.Print();
        int a = stack.Pop();
        stack.Print();
        Console.WriteLine(a);
        Console.WriteLine(stack.Empty());
        Console.WriteLine(stack.Peek());
        Console.WriteLine(stack.Search(2));
        Console.WriteLine(stack.Search(100));
    }
}
