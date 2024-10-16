using MyVectorLibrary;

class Program
{
    public static void CheckBlock(string block, ref string IP, ref bool isDone, ref int blockCount)
    {
        int part = Convert.ToInt32(block);
        if (part > 0 && part < 256 && block.Length == 3)
        {
            IP += part + ".";
            blockCount++;
        }
        else
        {
            IP = "";
            blockCount = 0;
        }
        if (blockCount == 4)
        {
            IP = IP.Remove(IP.Length - 1);
            blockCount = 0;
            isDone = true;
        }
    }

    public static void Main(string[] args)
    {
        MyVector<string> myVector = new MyVector<string>();
        string pathInput = "Input.txt";
        StreamReader r = new StreamReader(pathInput);
        string line = r.ReadLine();

        string currentIP = "";
        bool isDone = false;
        int blockCount = 0;

        while (line != null)
        {
            string[] partedString = line.Split(' ');
            foreach (string s in partedString)
            {
                string[] blocks = s.Split(".");
                foreach (string b in blocks)
                {
                    CheckBlock(b, ref currentIP, ref isDone, ref blockCount);
                    if (isDone)
                    {
                        //if (myVector.Size()==0) myVector.Add(currentIP);
                        if (!myVector.Contains(currentIP))
                        {
                            myVector.Add(currentIP);
                            currentIP = "";
                            isDone = false;
                        }
                        else
                        {
                            currentIP = "";
                            isDone = false;
                        }
                    }
                    
                }
            }
            line = r.ReadLine();
        }
        r.Close();
        string pathOutput = "Output.txt";
        StreamWriter wr = new StreamWriter(pathOutput);
        for (int i = 0; i < myVector.Size(); i++) wr.WriteLine(myVector.Get(i));
        wr.Close();
        myVector.Print();
        
    }
}
    