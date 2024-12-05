using HashMap;
using System.Text.RegularExpressions;
public class Program
{
    static void Main(string[] args)
    {
        MyHashMap<string, string> defs = new MyHashMap<string, string>();
        string path = "input.txt";
        StreamReader sr = new StreamReader(path);
        string? line = sr.ReadLine();

        string pattern = @"(double|int|float) \D+\S* = ?(\S)+?(?=;)";

        while (line != null)
        {
            MatchCollection matches = Regex.Matches(line, pattern);
            foreach(Match match in matches)
            {
                string[] part = match.Value.Split(' ');
                string type = part[0].Trim();
                string value = part[3].Trim();
                string name = part[1].Trim();
                string typeValue = type + " " + value;
                if (defs.ContainsKey(name)) Console.WriteLine($"Redefenition attempt: {type} {name} = {value} ");
                else defs.Put(name, typeValue);
            }
            line = sr.ReadLine();
        }
        sr.Close();
        IEnumerable<KeyValuePair<string, string>> ans = defs.EntrySet();
        foreach(var pair in ans) Console.WriteLine($"{pair.Value.Split(' ')[0]} = {pair.Key} {pair.Value.Split(' ')[1]}");
    }
}
