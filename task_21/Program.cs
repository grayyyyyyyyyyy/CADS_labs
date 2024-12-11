public class MyTreeMap<K, V> where K : IComparable<K>
{
    private class Node
    {
        public K key;
        public V value;
        public Node left;
        public Node right;
        public Node(K key, V value)
        {
            this.key = key;
            this.value = value;
        }
    }
    Comparer<K> cmp;
    Node root;
    int size;

    public void Print()
    {
        if (root == null)
        {
            Console.WriteLine("Empty");
            return;
        }
        Stack<Node> stack = new Stack<Node>();
        stack.Push(root);
        while (stack.Count > 0)
        {
            Node node = stack.Pop();
            Console.WriteLine($"{node.key}: {node.value}");
            if (node.right != null) stack.Push(node.right);
            if (node.left != null) stack.Push(node.left);
        }
    }
    private Node RemoveNode(Node node, K key)
    {
        if (node == null) return null;
        if (node.key.CompareTo(key) < 0) node.left = RemoveNode(node.left, key);
        else if (node.key.CompareTo(key) > 0) node.right = RemoveNode(node.right, key);
        else
        {
            if (node.left == null) return node.right;
            else if (node.right == null) return node.left;
            else
            {
                Node tmp = GetMin(node.right);
                node.key = tmp.key;
                node.value = tmp.value;
                node.right = RemoveNode(node.right, tmp.key);
            }
        }
        return node;
    }
    private Node GetMin(Node node)
    {
        while (node != null) node = node.left;
        return node;
    }
    private void IterKeySet(List<K> key)
    {
        Stack<Node> stack = new Stack<Node>();
        Node curr = null;
        while (curr != null || stack.Count > 0)
        {
            while (curr != null)
            {
                stack.Push(curr);
                curr = curr.left;
            }
            curr = stack.Pop();
            key.Add(curr.key);
            curr = curr.right;
        }
    }
    private void IterEntrySet(List<KeyValuePair<K, V>> entry)
    {
        Stack<Node> stack = new Stack<Node>();
        Node curr = null;
        while (curr != null || stack.Count > 0)
        {
            while (curr != null)
            {
                stack.Push(curr);
                curr = curr.left;
            }
            curr = stack.Pop();
            entry.Add(new KeyValuePair<K, V>(curr.key, curr.value));
            curr = curr.right;
        }
    }
    //1-4
    public MyTreeMap() => root = null;
    public MyTreeMap(Comparer<K> cmp) => this.cmp = cmp;
    public void Clear() => size = 0;
    public bool ContainsKey(K key) => Get(key) != null;
    //5
    public bool ContainsValue(V value)
    {
        if (root == null) return false;
        Stack<Node> stack = new Stack<Node>();
        stack.Push(root);
        while (stack.Count > 0)
        {
            Node curr = stack.Pop();
            if (curr.value.Equals(value)) return true;
            if (curr.left != null) stack.Push(curr.left);
            if (curr.right != null) stack.Push(curr.right);
        }
        return false;
    }
    //6
    public List<KeyValuePair<K, V>> EntrySet()
    {
        List<KeyValuePair<K, V>> keyValuePairs = new List<KeyValuePair<K, V>>();
        IterEntrySet(keyValuePairs);
        return keyValuePairs;
    }
    //7
    public V Get(K key)
    {
        Node curr = root;
        while (curr != null)
        {
            if (key.CompareTo(curr.key) < 0) curr = curr.left;
            else if (key.CompareTo(curr.key) > 0) curr = curr.right;
            else return curr.value;
        }
        return default(V);
    }
    //8
    public bool IsEmpty() => size == 0;
    //9
    public List<K> KeySet()
    {
        List<K> keys = new List<K>();
        IterKeySet(keys);
        return keys;
    }
    //10
    public void Put(K key, V value)
    {
        if (root == null)
        {
            root = new Node(key, value);
            size++;
            return;
        }
        Node curr = root;
        Node? parent = null;
        while (curr != null)
        {
            parent = curr;
            if (key.CompareTo(curr.key) < 0) curr = curr.left;
            else if (key.CompareTo(curr.key) > 0) curr = curr.right;
            else { curr.value = value; return; }

        }
        Node newNode = new Node(key, value);
        if (parent != null)
        {
            if (key.CompareTo(parent.key) < 0) parent.left = newNode;
            else parent.right = newNode;
        }
        size++;
    }
    //11
    public V Remove(K key)
    {
        Node curr = root;
        Node? parent = null, nodeToRemove = null;
        bool isLeft = false;
        while (curr != null)
        {
            if (key.CompareTo(curr.key) < 0) { parent = curr; curr = curr.left; }
            else if (key.CompareTo(curr.key) > 0) { parent = curr; curr = curr.right; }
            else { nodeToRemove = curr; break; }
        }
        if (nodeToRemove == null) return default(V);
        V valueToReturn = nodeToRemove.value;
        if (nodeToRemove.left == null & nodeToRemove.right == null)
        {
            if (nodeToRemove == root) root = null;
            else if (isLeft) parent.left = null;
            else parent.right = null;
        }
        else if (nodeToRemove.left == null)
        {
            if (nodeToRemove == root) root = nodeToRemove.right;
            else if (isLeft) parent.left = nodeToRemove.right;
            else parent.right = nodeToRemove.right;
        }
        else if (nodeToRemove.right == null)
        {
            if (nodeToRemove == root) root = nodeToRemove.left;
            else if (isLeft) parent.left = nodeToRemove.left;
            else parent.right = nodeToRemove.left;
        }
        else
        {
            Node child = GetMin(nodeToRemove.left);
            nodeToRemove.key= child.key;
            nodeToRemove.value= child.value;
            Remove(child.key);
        }
        return valueToReturn;
    }
    //12-14
    public int Size() => size;
    public K FirstKey() => root.key;
    public K LastKey() => root == null ? throw new InvalidOperationException() : GetMin(root).key;
    //15
    public MyTreeMap<K, V> HeadMap(K end)
    {
        MyTreeMap<K, V> head = new MyTreeMap<K, V>();
        if (root == null) return head;
        Stack<Node> stack = new Stack<Node>();
        stack.Push(root);
        while(stack.Count > 0)
        {
            Node curr = stack.Pop();
            if (curr.key.CompareTo(end)<0)
            {
                head.Put(curr.key, curr.value);
                if (curr.left!=null) stack.Push(curr.left);
                if (curr.right!=null) stack.Push(curr.right);
            }
            else if (curr.left!=null) stack.Push(curr.left);
        }
        return head;
    }
    //16
    public MyTreeMap<K, V> SubMap(K start, K end)
    {
        MyTreeMap<K, V> head = new MyTreeMap<K, V>();
        if (root == null) return head;
        Stack<Node> stack = new Stack<Node>();
        stack.Push(root);
        while (stack.Count > 0)
        {
            Node curr = stack.Pop();
            if (curr.key.CompareTo(start) >= 0 && curr.key.CompareTo(end) < 0)
            {
                head.Put(curr.key, curr.value);
                if (curr.left != null) stack.Push(curr.left);
                if (curr.right != null) stack.Push(curr.right);
            }
            else if (curr.key.CompareTo(end) >= 0) if (curr.left != null) stack.Push(curr.left);
            else if (curr.right != null) stack.Push(curr.right);
        }
        return head;
    }
    //17
    public MyTreeMap<K, V> TailMap(K start)
    {
        MyTreeMap<K, V> head = new MyTreeMap<K, V>();
        if (root == null) return head;
        Stack<Node> stack = new Stack<Node>();
        stack.Push(root);
        while (stack.Count > 0)
        {
            Node curr = stack.Pop();
            if (curr.key.CompareTo(start) >= 0)
            {
                head.Put(curr.key, curr.value);
                if (curr.left != null) stack.Push(curr.left);
                if (curr.right != null) stack.Push(curr.right);
            }
            else if (curr.right != null) stack.Push(curr.right);
        }
        return head;
    }
    //18
    public KeyValuePair<K, V> LowerEntry(K key)
    {
        KeyValuePair<K, V>? entry = null;
        if (root == null) return entry?? default;
        Stack<Node> stack = new Stack<Node> ();
        stack.Push(root);
        while (stack.Count > 0)
        {
            Node curr = stack.Pop();
            if (curr.key.CompareTo(key) < 0)
            {
                entry = new KeyValuePair<K, V>(curr.key, curr.value);
                stack.Push(curr.left);
                stack.Push(curr.right);
            }
            else stack.Push(curr.left);
        }
        return entry ?? default;
    }
    //19
    public KeyValuePair<K, V> FloorEntry(K key)
    {
        KeyValuePair<K, V>? entry = null;
        if (root == null) return entry ?? default;
        Stack<Node> stack = new Stack<Node>();
        stack.Push(root);
        while (stack.Count > 0)
        {
            Node curr = stack.Pop();
            if (curr.key.CompareTo(key) <= 0)
            {
                entry = new KeyValuePair<K, V>(curr.key, curr.value);
                stack.Push(curr.left);
                stack.Push(curr.right);
            }
            else stack.Push(curr.left);
        }
        return entry ?? default;
    }
    //20
    public KeyValuePair<K, V> HigherEntry(K key)
    {
        Node curr = root;
        KeyValuePair<K, V>? entry = null;
        while (curr!=null)
        {
            if (curr.key.CompareTo(key) > 0)
            {
                entry = new KeyValuePair<K, V>(curr.key, curr.value);
                curr = curr.left;
            }
            else curr = curr.right;
        }
        return entry ?? default;
    }
    //21
    public KeyValuePair<K, V> CeilingEntry(K key)
    {
        Node curr = root;
        KeyValuePair<K, V>? entry = null;
        while (curr != null)
        {
            if (curr.key.CompareTo(key) >= 0)
            {
                entry = new KeyValuePair<K, V>(curr.key, curr.value);
                curr = curr.left;
            }
            else curr = curr.right;
        }
        return entry ?? default;
    }
    //22
    public K? LowerKey(K key)
    {
        Node curr = root;
        K? res = default(K);
        while (curr!=null)
        {
            if (key.CompareTo(curr.key) > 0) { res = curr.key; curr = curr.left; }
            else curr=curr.right;
        }
        return res;
    }
    //23
    public K? FloorKey(K key)
    {
        Node curr = root;
        K? res = default(K);
        while (curr != null)
        {
            if (key.CompareTo(curr.key) >= 0) { res = curr.key; curr = curr.left; }
            else curr = curr.right;
        }
        return res;
    }
    //24
    public K? HigherKey(K key)
    {
        Node curr = root;
        K? res = default(K);
        while (curr != null)
        {
            if (key.CompareTo(curr.key) < 0) { res = curr.key; curr = curr.left; }
            else curr = curr.right;
        }
        return res;
    }
    //25
    public K? CeilingKey(K key)
    {
        Node curr = root;
        K? res = default(K);
        while (curr != null)
        {
            if (key.CompareTo(curr.key) <= 0) { res = curr.key; curr = curr.left; }
            else curr = curr.right;
        }
        return res;
    }
    //26
    public KeyValuePair<K, V> PollFirstEntry()
    {
        if (root == null) return default;
        KeyValuePair<K, V> entry = FirstEntry();
        root = RemoveNode(root, entry.Key);
        size--;
        return entry;
    }
    //27
    public KeyValuePair<K, V> PollLastEntry()
    {
        if (root == null) return default;
        KeyValuePair<K, V> entry = LastEntry();
        root = RemoveNode(root, entry.Key);
        size--;
        return entry;
    }
    //28
    public KeyValuePair<K, V> FirstEntry()
    {
        if (root == null) return default;
        Node minNode = GetMin(root);
        return new KeyValuePair<K, V>(minNode.key, minNode.value);
    }
    //29
    public KeyValuePair<K, V> LastEntry()
    {
        if (root == null) return default;
        Node curr = root;
        Node? maxNode = null;
        while (curr != null)
        {
            maxNode = curr;
            curr = curr.right;
        }
        return new KeyValuePair<K, V>(maxNode.key, maxNode.value);
    }
}
public class Program
{
    static void Main(string[] args)
    {
        MyTreeMap<int, string> treeMap = new MyTreeMap<int, string>();
        treeMap.Put(5, "five");
        treeMap.Put(2, "two");
        treeMap.Put(8, "eight");
        treeMap.Put(1, "one");
        treeMap.Put(9, "nine");
        treeMap.Put(6, "six");
        treeMap.Put(4, "four");
        treeMap.Print();
    }
}
