using Library;
using System.Collections.Generic;
public class Program
{
    static void Main(string[] args)
    {
        int[] array1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        MyLinkedList<int> linkedList = new MyLinkedList<int>(array1);
        var item1 = linkedList.ListIterator();
        while (item1.HasNext())
        {
            item1.Next();
            Console.Write(item1.Cursor + " ");
        }
        Console.WriteLine();

        int[] array2 = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        MyArrayDeque<int> arrayDeque = new MyArrayDeque<int>(array2);
        var item2 = arrayDeque.ListIterator();
        while (item2.HasNext())
        {
            item2.Next();
            Console.Write(item2.Cursor + " ");
        }
    }
}