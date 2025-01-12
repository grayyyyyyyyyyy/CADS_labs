using Library;
public class Program
{
    static void Main(string[] args)
    {
        int[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        MyArrayList<int> arrayList = new MyArrayList<int>(array);
        var iter1 = arrayList.IteratorList();
        while (iter1.HasNext())
        {
            iter1.Next();
            Console.Write(iter1.Cursor + " ");
        }
    }
}