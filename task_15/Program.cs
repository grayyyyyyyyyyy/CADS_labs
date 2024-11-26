using MyArrayDeque;
using System;
using System.IO;
public class Sorts
{
    private int DigitCount(string s)
    {
        int count = 0;
        foreach (char c in s)
        {
            if (c >='0' && c<='9') count++;
        }
        return count;
    }
    private int SpaceCount(string s)
    {
        int count = 0;
        foreach(char c in s)
        {
            if (c == ' ') count++;
        }
        return count;
    }

    public void DoingTask(int n)
    {
        MyArrayDeque<string> deque = new MyArrayDeque<string>();
        string input = "input.txt";
        string output = "sorted.txt";

        string[] lines = File.ReadAllLines(input);

        if (lines.Length > 0)
        {
            deque.Add(lines[0]);
            int d = DigitCount(lines[0]);
            File.WriteAllText(output, lines[0]);
            for (int i = 1;  i < lines.Length; i++)
            {
                int d1 = DigitCount(lines[i]);
                int s1 = SpaceCount(lines[i]);
                string fileText = File.ReadAllText(output);

                if (d1 > d) File.WriteAllText(output, fileText + "\n" + lines[i]);
                else File.WriteAllText(output, lines[i] + "\n" + fileText);

                if (d1 > d && s1 < n) deque.AddLast(lines[i]);
                else if (d1 <= d && s1 < n)
                {
                    deque.AddFirst(lines[i]);
                    d = d1;
                }

            }
            deque.Print();
        }
    }
}
public class Program
{
    static void Main(string[] args)
    {
        Sorts sorted = new Sorts();
        int n = Convert.ToInt32(Console.ReadLine());
        sorted.DoingTask(n);
    }
}