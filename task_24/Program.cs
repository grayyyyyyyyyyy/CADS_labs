using RBTreeLib;
class Program
{
    public static void Main(string[] args)
    {
        RBTree<int> tree = new RBTree<int>();
        tree.Print();
        tree.Add(13);
        tree.Add(8);
        tree.Add(17);
        tree.Add(1);
        tree.Add(15);
        tree.Add(25);
        tree.Add(6);
        tree.Add(8);
        tree.Add(22);
        tree.Add(27);
        tree.Print();
        
    }
}