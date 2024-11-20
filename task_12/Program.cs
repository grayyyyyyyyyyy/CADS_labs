using MyPriorityQueue;
using System.Diagnostics;

class Program {
    public class Request : IComparable<Request>
    {
        public int number { get; set; }
        public int priority { get; set; }
        public int step { get; set; }
        public Request(int priority, int number,  int step)
        {
            this.number = number;
            this.priority = priority;
            this.step = step;
        }
        public int CompareTo(Request other)
        {
            if (priority != other.priority) return priority.CompareTo(other.priority);
            else return number.CompareTo(other.number);
        }
    }
    static void Main(string[] args)
    {
        string path = "log.txt";
        MyPriorityQueue<Request> queue = new MyPriorityQueue<Request>();

        Console.WriteLine("Amount of steps: ");
        int n = Convert.ToInt32(Console.ReadLine());
        int k = 0;


        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        StreamWriter sw = new StreamWriter(path);

        for (int i = 0; i<n; i++)
        {
            Random rnd = new Random();
            int amountOfRequests = rnd.Next(1, 11);

            for (int j =0; j< amountOfRequests; j++)
            {
                int priority = rnd.Next(1, 6);
                Request request = new Request(priority, j, i);
                queue.Add(request);
                sw.WriteLine($"ADD: {request.number} {request.priority} {request.step}");
                k++;
            }
        }
        for (int i =0; i<k; i++)
        {
            Request tmp = queue.Peek();
            sw.WriteLine($"REMOVE: {tmp.number} {tmp.priority} {tmp.step}");
            queue.Remove(tmp);
        }
        stopwatch.Stop();
        sw.WriteLine($"Время выполнения: {stopwatch.Elapsed}");
        sw.Close();
    }
}