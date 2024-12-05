using HashMap;
using System.Text.RegularExpressions;

public class Program
{
    static void Main(string[] args)
    {
        MyHashMap<string, int> tags = new MyHashMap<string, int>();
        string path = "input.txt";
        StreamReader sr = new StreamReader(path);
        string? line = sr.ReadLine();

        string pattern = @"</?[a-z | A-Z]+\w*>";

        while (line != null)
        {
            MatchCollection matches = Regex.Matches(line, pattern);
            foreach (Match match in matches)
            {
                if (tags.ContainsKey(match.Value.ToLower())) tags.Put(match.Value.ToLower(), tags.Get(match.Value.ToLower()) + 1);
                else tags.Put(match.Value.ToLower(), 1);
            }
            line = sr.ReadLine();
        }
        sr.Close();
        IEnumerable<KeyValuePair<string, int>> ans = tags.EntrySet();
        foreach(var pair in ans) Console.WriteLine($"{pair.Key} : {pair.Value}");

    }
}