using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeMap
{
    public class MyTreeMap<Key, Value> where Key : IComparable<Key>
    {
        private class Node
        {
            public Key key { get; set; }
            public Value value { get; set; }
            public Node left { get; set; }
            public Node right { get; set; }
            public Node(Key key, Value value)
            {
                this.key = key;
                this.value = value;
            }
        }
        private Node root;
        private int size;
        private Comparer<Key> comparer;

        // helping methods
        public void Print()
        {
            if (root == null)
            {
                Console.WriteLine("TreeMap is empty");
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

        private Node RemoveNode(Node node, Key key)
        {
            if (node == null)
                return null;
            if (node.key.CompareTo(key) < 0)
                node.left = RemoveNode(node.left, key);
            else if (node.key.CompareTo(key) > 0)
                node.right = RemoveNode(node.right, key);
            else
            {
                if (node.left == null)
                    return node.right;
                else if (node.right == null)
                    return node.left;
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
            while (node.left != null)
                node = node.left;
            return node;
        }

        private void IterEntrySet(List<KeyValuePair<Key, Value>> entry)
        {
            Stack<Node> stack = new Stack<Node>();
            Node curr = null;
            while (curr != null || stack.Count != 0)
            {
                while (curr != null)
                {
                    stack.Push(curr);
                    curr = curr.left;
                }
                curr = stack.Pop();
                entry.Add(new KeyValuePair<Key, Value>(curr.key, curr.value));
                curr = curr.right;
            }
        }

        private void IterKeySet(List<Key> key)
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

        // 1, 2, 3, 4, 8, 12, 13
        public MyTreeMap() => root = null;
        public MyTreeMap(Comparer<Key> comp) => comparer = comp;
        public bool ContainsKey(Key key) => Get(key) != null;
        public void Clear() => size = 0;
        public bool IsEmpty() => size == 0;
        public int Size() => size;
        public Key FirstKey() => root.key;

        // 5
        public bool ContainsValue(Value value)
        {
            if (root == null)
                return false;
            Stack<Node> stack = new Stack<Node>();
            stack.Push(root);
            while (stack.Count > 0)
            {
                Node curr = stack.Pop();
                if (curr.value.Equals(value))
                    return true;
                if (curr.left != null)
                    stack.Push(curr.left);
                if (curr.right != null)
                    stack.Push(curr.right);
            }
            return false;
        }

        // 6
        public List<KeyValuePair<Key, Value>> EntrySet()
        {
            List<KeyValuePair<Key, Value>> entry = new List<KeyValuePair<Key, Value>>();
            IterEntrySet(entry);
            return entry;
        }

        // 7
        public Value Get(Key key)
        {
            Node curr = root;
            while (curr != null)
            {
                if (key.CompareTo(curr.key) < 0)
                    curr = curr.left;
                else if (key.CompareTo(curr.key) > 0)
                    curr = curr.right;
                else
                    return curr.value;
            }
            return default(Value);
        }

        // 9
        public List<Key> KeySet()
        {
            List<Key> keys = new List<Key>();
            IterKeySet(keys);
            return keys;
        }

        // 10
        public void Put(Key key, Value value)
        {
            if (root == null)
            {
                root = new Node(key, value);
                size++;
                return;
            }
            Node curr = root;
            Node parent = null;
            while (curr != null)
            {
                parent = curr;
                if (key.CompareTo(curr.key) < 0)
                    curr = curr.left;
                else if (key.CompareTo(curr.key) > 0)
                    curr = curr.right;
                else
                {
                    curr.value = value;
                    return;
                }
            }
            Node newNode = new Node(key, value);
            if (parent != null)
            {
                if (key.CompareTo(parent.key) < 0)
                    parent.left = newNode;
                else
                    parent.right = newNode;
            }
            size++;
        }

        // 11
        public Value Remove(Key key)
        {
            Node curr = root;
            Node parent = null;
            Node nodeToRemove = null;
            bool isLeft = false;
            while (curr != null)
            {
                if (key.CompareTo(curr.key) < 0)
                {
                    parent = curr;
                    curr = curr.left;
                }
                else if (key.CompareTo(curr.key) > 0)
                {
                    parent = curr;
                    curr = curr.right;
                }
                else
                {
                    nodeToRemove = curr;
                    break;
                }
            }
            if (nodeToRemove == null)
                return default(Value);
            Value valueToReturn = nodeToRemove.value;
            if (nodeToRemove.left == null && nodeToRemove.right == null)
            {
                if (nodeToRemove == root)
                    root = null;
                else if (isLeft)
                    parent.left = null;
                else parent.right = null;
            }
            else if (nodeToRemove.left == null)
            {
                if (nodeToRemove == root)
                    root = nodeToRemove.right;
                else if (isLeft)
                    parent.left = nodeToRemove.right;
                else parent.right = nodeToRemove.right;
            }
            else if (nodeToRemove.right == null)
            {
                if (nodeToRemove == root)
                    root = nodeToRemove.left;
                else if (isLeft)
                    parent.left = nodeToRemove.left;
                else parent.right = nodeToRemove.left;
            }
            else
            {
                Node child = GetMin(nodeToRemove.right);
                nodeToRemove.key = child.key;
                nodeToRemove.value = child.value;
                Remove(child.key);
            }
            return valueToReturn;
        }

        // 14
        public Key LastKey()
        {
            if (root == null)
                throw new InvalidOperationException();
            return GetMin(root).key;
        }

        // 15
        public MyTreeMap<Key, Value> HeadMap(Key end)
        {
            MyTreeMap<Key, Value> head = new MyTreeMap<Key, Value>();
            if (root == null)
                return head;
            Stack<Node> stack = new Stack<Node>();
            stack.Push(root);
            while (stack.Count > 0)
            {
                Node curr = stack.Pop();
                if (curr.key.CompareTo(end) < 0)
                {
                    head.Put(curr.key, curr.value);
                    if (curr.left != null)
                        stack.Push(curr.left);
                    if (curr.right != null)
                        stack.Push(curr.right);
                }
                else
                {
                    if (curr.left != null)
                        stack.Push(curr.left);
                }
            }
            return head;
        }

        // 16
        public MyTreeMap<Key, Value> SubMap(Key start, Key end)
        {
            MyTreeMap<Key, Value> head = new MyTreeMap<Key, Value>();
            if (root == null)
                return head;
            Stack<Node> stack = new Stack<Node>();
            stack.Push(root);
            while (stack.Count > 0)
            {
                Node curr = stack.Pop();
                if (curr.key.CompareTo(start) >= 0 && curr.key.CompareTo(end) < 0)
                {
                    head.Put(curr.key, curr.value);
                    if (curr.left != null)
                        stack.Push(curr.left);
                    if (curr.right != null)
                        stack.Push(curr.right);
                }
                else if (curr.key.CompareTo(end) >= 0)
                {
                    if (curr.left != null)
                        stack.Push(curr.left);
                }
                else
                {
                    if (curr.right != null)
                        stack.Push(curr.right);
                }
            }
            return head;
        }

        // 17
        public MyTreeMap<Key, Value> TailMap(Key end)
        {
            MyTreeMap<Key, Value> head = new MyTreeMap<Key, Value>();
            if (root == null)
                return head;
            Stack<Node> stack = new Stack<Node>();
            stack.Push(root);
            while (stack.Count > 0)
            {
                Node curr = stack.Pop();
                if (curr.key.CompareTo(end) >= 0)
                {
                    head.Put(curr.key, curr.value);
                    if (curr.left != null)
                        stack.Push(curr.left);
                    if (curr.right != null)
                        stack.Push(curr.right);
                }
                else
                {
                    if (curr.right != null)
                        stack.Push(curr.right);
                }
            }
            return head;
        }

        // 18
        public KeyValuePair<Key, Value> LowerEntry(Key key)
        {
            KeyValuePair<Key, Value>? entry = null;
            if (root == null)
                return entry ?? default;
            Stack<Node> stack = new Stack<Node>();
            stack.Push(root);
            while (stack.Count > 0)
            {
                Node curr = stack.Pop();
                if (curr.key.CompareTo(key) < 0)
                {
                    entry = new KeyValuePair<Key, Value>(curr.key, curr.value);
                    stack.Push(curr.right);
                    stack.Push(curr.left);
                }
                else
                    stack.Push(curr.left);
            }
            return entry ?? default;
        }

        // 19
        public KeyValuePair<Key, Value> FloorEntry(Key key)
        {
            KeyValuePair<Key, Value>? entry = null;
            if (root == null)
                return entry ?? default;
            Stack<Node> stack = new Stack<Node>();
            stack.Push(root);
            while (stack.Count > 0)
            {
                Node curr = stack.Pop();
                if (curr.key.CompareTo(key) <= 0)
                {
                    entry = new KeyValuePair<Key, Value>(curr.key, curr.value);
                    stack.Push(curr.right);
                    stack.Push(curr.left);
                }
                else
                    stack.Push(curr.left);
            }
            return entry ?? default;
        }

        // 20
        public KeyValuePair<Key, Value> HigherEntry(Key key)
        {
            Node curr = root;
            KeyValuePair<Key, Value>? entry = null;
            while (curr != null)
            {
                if (key.CompareTo(curr.key) < 0)
                {
                    entry = new KeyValuePair<Key, Value>(curr.key, curr.value);
                    curr = curr.left;
                }
                else
                    curr = curr.right;
            }
            return entry ?? default;
        }

        // 21
        public KeyValuePair<Key, Value> CeilingEntry(Key key)
        {
            Node curr = root;
            KeyValuePair<Key, Value>? entry = null;
            while (curr != null)
            {
                if (key.CompareTo(curr.key) <= 0)
                {
                    entry = new KeyValuePair<Key, Value>(curr.key, curr.value);
                    curr = curr.left;
                }
                else
                    curr = curr.right;
            }
            return entry ?? default;
        }

        // 22
        public Key LowerKey(Key key)
        {
            Node curr = root;
            Key result = default(Key);
            while (curr != null)
            {
                if (key.CompareTo(curr.key) > 0)
                {
                    result = curr.key;
                    curr = curr.left;
                }
                else
                    curr = curr.right;
            }
            return result;
        }

        // 23
        public Key FloorKey(Key key)
        {
            Node curr = root;
            Key result = default(Key);
            while (curr != null)
            {
                if (key.CompareTo(curr.key) >= 0)
                {
                    result = curr.key;
                    curr = curr.left;
                }
                else
                    curr = curr.right;
            }
            return result;
        }

        // 24
        public Key HigherKey(Key key)
        {
            Node curr = root;
            Key result = default(Key);
            while (curr != null)
            {
                if (key.CompareTo(curr.key) < 0)
                {
                    result = curr.key;
                    curr = curr.left;
                }
                else
                    curr = curr.right;
            }
            return result;
        }

        // 25
        public Key CeilingKey(Key key)
        {
            Node curr = root;
            Key result = default(Key);
            while (curr != null)
            {
                if (key.CompareTo(curr.key) <= 0)
                {
                    result = curr.key;
                    curr = curr.left;
                }
                else
                    curr = curr.right;
            }
            return result;
        }

        // 26
        public KeyValuePair<Key, Value>? PollFirstEntry()
        {
            if (root == null)
                return default;
            KeyValuePair<Key, Value>? entry = FirstEntry();
            root = RemoveNode(root, entry.Value.Key);
            size--;
            return entry;
        }

        // 27
        public KeyValuePair<Key, Value>? PollLastEntry()
        {
            if (root == null)
                return default;
            KeyValuePair<Key, Value>? entry = LastEntry();
            root = RemoveNode(root, entry.Value.Key);
            size--;
            return entry;
        }

        // 28
        public KeyValuePair<Key, Value> FirstEntry()
        {
            if (root == null)
                return default;
            Node minNode = GetMin(root);
            return new KeyValuePair<Key, Value>(minNode.key, minNode.value);
        }

        // 29
        public KeyValuePair<Key, Value> LastEntry()
        {
            if (root == null)
                return default;
            Node curr = root;
            Node maxNode = null;
            while (curr != null)
            {
                maxNode = curr;
                curr = curr.right;
            }
            return new KeyValuePair<Key, Value>(maxNode.key, maxNode.value);
        }
    }
    /*public class MyTreeMap<K, V> where K : IComparable<K>
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
                nodeToRemove.key = child.key;
                nodeToRemove.value = child.value;
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
            while (stack.Count > 0)
            {
                Node curr = stack.Pop();
                if (curr.key.CompareTo(end) < 0)
                {
                    head.Put(curr.key, curr.value);
                    if (curr.left != null) stack.Push(curr.left);
                    if (curr.right != null) stack.Push(curr.right);
                }
                else if (curr.left != null) stack.Push(curr.left);
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
            if (root == null) return entry ?? default;
            Stack<Node> stack = new Stack<Node>();
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
            while (curr != null)
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
            while (curr != null)
            {
                if (key.CompareTo(curr.key) > 0) { res = curr.key; curr = curr.left; }
                else curr = curr.right;
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
    }*/

    //public class MyComparator<T> : Comparer<T> where T : IComparable
    //{
    //    public override int Compare(T x, T y)
    //    {
    //        return x.CompareTo(y);
    //        throw new NotImplementedException();
    //    }
    //}
    //public class MyTreeMap<K, V> where K : IComparable
    //{
    //    private class Node
    //    {
    //        public K Key { get; set; }
    //        public V Value { get; set; }
    //        public Node Left { get; set; }
    //        public Node Right { get; set; }
    //        public Node(K Key, V Value)
    //        {
    //            this.Key = Key;
    //            this.Value = Value;
    //            Left = null;
    //            Right = null;
    //        }
    //    }
    //    IComparer<K> comparator;
    //    Node root;

    //    int size;
    //    public MyTreeMap()
    //    {
    //        comparator = new MyComparator<K>();
    //        size = 0;
    //    }
    //    public MyTreeMap(IComparer<K> comp)
    //    {
    //        comparator = comp;
    //    }
    //    public void Clear()
    //    {
    //        root = null;
    //        size = 0;
    //    }
    //    public bool ContainsKey(K key)
    //    {
    //        Node step = root;
    //        while (step != null)
    //        {
    //            if (comparator.Compare(key, step.Key) < 0)
    //                step = step.Left;
    //            else if (comparator.Compare(key, step.Key) > 0)
    //                step = step.Right;
    //            else
    //                return true;
    //        }
    //        return false;
    //    }
    //    public bool ContainsValue(V value)
    //    {
    //        Node step = root;
    //        Stack<Node> stack = new Stack<Node>();
    //        while (step != null || stack.Count > 0)
    //        {
    //            while (step != null)
    //            {
    //                if (step.Value.Equals(value)) return true;
    //                stack.Push(step);
    //                step = step.Left;
    //            }
    //            step = stack.Pop();
    //            step = step.Right;
    //        }
    //        return false;
    //    }
    //    public List<KeyValuePair<K, V>> EntrySet()
    //    {
    //        List<KeyValuePair<K, V>> entries = new List<KeyValuePair<K, V>>();
    //        Node step = root;
    //        Stack<Node> stack = new Stack<Node>();
    //        while (step != null || stack.Count > 0)
    //        {
    //            while (step != null)
    //            {
    //                entries.Add(new KeyValuePair<K, V>(step.Key, step.Value));
    //                stack.Push(step);
    //                step = step.Left;
    //            }
    //            step = stack.Pop();
    //            step = step.Right;
    //        }
    //        return entries;
    //    }
    //    public V Get(K key)
    //    {
    //        Node step = root;
    //        while (step != null)
    //        {
    //            if (comparator.Compare(key, step.Key) < 0)
    //                step = step.Left;
    //            else if (comparator.Compare(key, step.Key) > 0)
    //                step = step.Right;
    //            else
    //                return step.Value;
    //        }
    //        return default(V);
    //    }
    //    public bool IsEmpty() => size == 0;
    //    public K[] KeySet()
    //    {
    //        K[] set = new K[size];
    //        Node step = root;
    //        Stack<Node> stack = new Stack<Node>();
    //        int index = 0;
    //        while (step != null || stack.Count > 0)
    //        {
    //            while (step != null)
    //            {
    //                set[index] = step.Key;
    //                stack.Push(step);
    //                step = step.Left;
    //                index++;
    //            }
    //            step = stack.Pop();
    //            step = step.Right;
    //        }
    //        return set;
    //    }
    //    public void Put(K key, V value)
    //    {
    //        if (root == null)
    //        {
    //            root = new Node(key, value);
    //            size++;
    //            return;
    //        }
    //        Node newAdd = new Node(key, value);
    //        size++;
    //        Node step = root;
    //        while (step != null)
    //        {
    //            if (comparator.Compare(newAdd.Key, step.Key) < 0)
    //            {
    //                if (step.Left == null) { step.Left = newAdd; break; }
    //                else
    //                    step = step.Left;
    //            }
    //            else if (comparator.Compare(newAdd.Key, step.Key) > 0)
    //            {
    //                if (step.Right == null) { step.Right = newAdd; break; }
    //                else step = step.Right;
    //            }
    //            else
    //            {
    //                step.Value = value;
    //                size--;
    //                return;
    //            }
    //        }
    //    }
    //    public void Remove(K key)
    //    {
    //        if (comparator.Compare(key, root.Key) == 0 && root.Right == null && root.Left == null)
    //        {
    //            root = null;
    //            size--;
    //            return;
    //        }
    //        Node high = root;
    //        Node step = root;
    //        if (comparator.Compare(key, root.Key) < 0)
    //            step = root.Left;
    //        else if (comparator.Compare(key, root.Key) > 0)
    //            step = root.Right;
    //        while (step != null)
    //        {
    //            if (comparator.Compare(key, step.Key) == 0)
    //            {
    //                if (step.Left == null && step.Right == null)
    //                {
    //                    if (comparator.Compare(step.Key, high.Key) < 0)
    //                    {
    //                        size--;
    //                        high.Left = null;
    //                        return;
    //                    }
    //                    else
    //                    {
    //                        size--;
    //                        high.Right = null;
    //                        return;
    //                    }
    //                }
    //                else if ((step.Left == null && step.Right != null) || (step.Right == null && step.Left != null))
    //                {
    //                    if (step.Left != null)
    //                    {
    //                        step.Value = step.Left.Value;
    //                        step.Key = step.Left.Key;
    //                        step.Right = step.Left.Right;
    //                        step.Left = step.Left.Left;
    //                        size--;
    //                        return;
    //                    }
    //                    else if (step.Right != null)
    //                    {
    //                        step.Value = step.Right.Value;
    //                        step.Key = step.Right.Key;
    //                        step.Right = step.Right.Right;
    //                        step.Left = step.Right.Left;
    //                        size--;
    //                        return;
    //                    }
    //                }
    //                else if (step.Left != null && step.Right != null)
    //                {
    //                    Node? max = step.Left;
    //                    if (max.Right == null)
    //                        max = step.Left;
    //                    while (max.Right != null)
    //                        max = max.Right;
    //                    Node maxHigh = max;
    //                    if (maxHigh.Left != null)
    //                    {
    //                        step.Value = max.Value;
    //                        step.Key = max.Key;
    //                        maxHigh.Value = maxHigh.Left.Value;
    //                        maxHigh.Key = maxHigh.Left.Key;
    //                        maxHigh.Left = maxHigh.Left.Left;
    //                    }
    //                    else if (maxHigh.Left == null)
    //                    {
    //                        step.Value = max.Value;
    //                        step.Key = max.Key;
    //                        step.Left.Right = max.Left;
    //                    }
    //                    size--;
    //                    return;
    //                }
    //            }
    //            else if (comparator.Compare(key, step.Key) < 0)
    //            {
    //                high = step;
    //                step = step.Left;
    //            }
    //            else if (comparator.Compare(key, step.Key) > 0)
    //            {
    //                high = step;
    //                step = step.Right;
    //            }
    //        }
    //    }
    //    public int Size() => size;
    //    public K FirstKey()
    //    {
    //        if (root != null)
    //            return root.Key;
    //        return default(K);
    //    }
    //    public K LastKey()
    //    {
    //        Node step = root;
    //        while (step != null)
    //        {
    //            if (step.Right == null)
    //                return step.Key;
    //            step = step.Right;
    //        }
    //        return default(K);
    //    }
    //    public MyTreeMap<K, V> HeadMap(K end)
    //    {
    //        MyTreeMap<K, V> returnTree = new MyTreeMap<K, V>();
    //        Node step = root;
    //        Stack<Node> stack = new Stack<Node>();
    //        while (step != null || stack.Count > 0)
    //        {
    //            while (step != null)
    //            {
    //                if (comparator.Compare(step.Key, end) < 0)
    //                    returnTree.Put(step.Key, step.Value);
    //                stack.Push(step);
    //                step = step.Left;
    //            }
    //            if (stack.Count > 0)
    //            {
    //                step = stack.Pop();
    //                if (comparator.Compare(step.Key, end) >= 0)
    //                    break;
    //                step = step.Right;
    //            }
    //        }
    //        return returnTree;
    //    }
    //    public MyTreeMap<K, V> SubMap(K start, K end)
    //    {
    //        MyTreeMap<K, V> returnTree = new MyTreeMap<K, V>();
    //        Node step = root;
    //        Stack<Node> stack = new Stack<Node>();
    //        while (step != null || stack.Count > 0)
    //        {
    //            while (step != null)
    //            {
    //                if (comparator.Compare(step.Key, start) >= 0 && comparator.Compare(step.Key, end) < 0)
    //                    returnTree.Put(step.Key, step.Value);
    //                stack.Push(step);
    //                step = step.Left;
    //            }
    //            if (stack.Count > 0)
    //            {
    //                step = stack.Pop();
    //                step = step.Right;
    //            }
    //        }
    //        return returnTree;
    //    }
    //    public MyTreeMap<K, V> TailMap(K start)
    //    {
    //        MyTreeMap<K, V> returnTree = new MyTreeMap<K, V>();
    //        Node step = root;
    //        Stack<Node> stack = new Stack<Node>();
    //        while (step != null || stack.Count > 0)
    //        {
    //            while (step != null)
    //            {
    //                if (comparator.Compare(step.Key, start) >= 0)
    //                    returnTree.Put(step.Key, step.Value);
    //                stack.Push(step);
    //                step = step.Left;
    //            }
    //            if (stack.Count > 0)
    //            {
    //                step = stack.Pop();
    //                step = step.Right;
    //            }
    //        }
    //        return returnTree;
    //    }
    //    public IEnumerable<KeyValuePair<K, V>> FirstEntry()
    //    {
    //        yield return new KeyValuePair<K, V>(root.Key, root.Value);
    //    }
    //    public IEnumerable<KeyValuePair<K, V>> LastEntry()
    //    {
    //        Node step = root;
    //        while (step != null)
    //        {
    //            if (step.Right == null)
    //                yield return new KeyValuePair<K, V>(root.Key, root.Value);
    //            step = step.Right;
    //        }
    //    }
    //    public IEnumerable<KeyValuePair<K, V>> LowerEntry(K key)
    //    {
    //        Node step = root;
    //        while (step != null)
    //        {
    //            if (comparator.Compare(key, step.Key) < 0 || comparator.Compare(key, step.Key) == 0)
    //                step = step.Left;
    //            else
    //                yield return new KeyValuePair<K, V>(step.Key, step.Value);
    //        }
    //    }
    //    public IEnumerable<KeyValuePair<K, V>> FloorEntry(K key)
    //    {
    //        Node step = root;
    //        while (step != null)
    //        {
    //            if (comparator.Compare(key, step.Key) < 0)
    //                step = step.Left;
    //            else
    //                yield return new KeyValuePair<K, V>(step.Key, step.Value);
    //        }
    //    }
    //    public IEnumerable<KeyValuePair<K, V>> HigherEntry(K key)
    //    {
    //        Node step = root;
    //        while (step != null)
    //        {
    //            if (comparator.Compare(key, step.Key) > 0 || comparator.Compare(key, step.Key) == 0)
    //                step = step.Right;
    //            else
    //                yield return new KeyValuePair<K, V>(step.Key, step.Value);
    //        }
    //    }
    //    public IEnumerable<KeyValuePair<K, V>> CeilingEntry(K key)
    //    {
    //        Node step = root;
    //        while (step != null)
    //        {
    //            if (comparator.Compare(key, step.Key) > 0)
    //                step = step.Right;
    //            else
    //                yield return new KeyValuePair<K, V>(step.Key, step.Value);
    //        }
    //    }
    //    public K LowerKey(K key)
    //    {
    //        Node step = root;
    //        while (step != null)
    //        {
    //            if (comparator.Compare(key, step.Key) < 0 || comparator.Compare(key, step.Key) == 0)
    //                step = step.Left;
    //            else
    //                return step.Key;
    //        }
    //        return default(K);
    //    }
    //    public K FloorKey(K key)
    //    {
    //        Node step = root;
    //        while (step != null)
    //        {
    //            if (comparator.Compare(key, step.Key) < 0)
    //                step = step.Left;
    //            else
    //                return step.Key;
    //        }
    //        return default(K);
    //    }
    //    public K HigherKey(K key)
    //    {
    //        Node step = root;
    //        while (step != null)
    //        {
    //            if (comparator.Compare(key, step.Key) > 0 || comparator.Compare(key, step.Key) == 0)
    //                step = step.Right;
    //            else
    //                return step.Key;
    //        }
    //        return default(K);
    //    }
    //    public K CeilingKey(K key)
    //    {
    //        Node step = root;
    //        while (step != null)
    //        {
    //            if (comparator.Compare(key, step.Key) > 0)
    //                step = step.Right;
    //            else
    //                return step.Key;
    //        }
    //        return default(K);
    //    }
    //    public K PollFirstEntry()
    //    {
    //        K key = root.Key;
    //        Remove(root.Key);
    //        return key;
    //    }
    //    public K PollLastEntry()
    //    {
    //        K key = LastKey();
    //        Remove(key);
    //        return key;
    //    }
    //    public override string ToString()
    //    {
    //        string res = "";
    //        Node treeElement = root;
    //        Print("", treeElement);
    //        void Print(string space, Node root)
    //        {
    //            if (root == null)
    //                return;
    //            else
    //            {
    //                Print(space + '\t', root.Left);
    //                res += $"{space + root.Key}\n";
    //                Print(space + '\t', root.Right);
    //            }
    //        }
    //        return res;
    //    }
    //    public void PrintTree()
    //    {

    //        Console.WriteLine(this);
    //    }
    //}
    ////public class MyTreeMap<K, V> where K : IComparable<K>
    ////{
    ////    private class Node
    ////    {
    ////        public K key;
    ////        public V value;
    ////        public Node left;
    ////        public Node right;
    ////        public Node(K key, V value)
    ////        {
    ////            this.key = key;
    ////            this.value = value;
    ////        }
    ////    }
    ////    IComparer<K> cmp;
    ////    Node root;
    ////    int size;

    ////    public void Print()
    ////    {
    ////        if (root == null)
    ////        {
    ////            Console.WriteLine("Empty");
    ////            return;
    ////        }
    ////        Stack<Node> stack = new Stack<Node>();
    ////        stack.Push(root);
    ////        while (stack.Count > 0)
    ////        {
    ////            Node node = stack.Pop();
    ////            Console.WriteLine($"{node.key}: {node.value}");
    ////            if (node.right != null) stack.Push(node.right);
    ////            if (node.left != null) stack.Push(node.left);
    ////        }
    ////    }
    ////    private Node RemoveNode(Node node, K key)
    ////    {
    ////        if (node == null) return null;
    ////        if (node.key.CompareTo(key) < 0) node.left = RemoveNode(node.left, key);
    ////        else if (node.key.CompareTo(key) > 0) node.right = RemoveNode(node.right, key);
    ////        else
    ////        {
    ////            if (node.left == null) return node.right;
    ////            else if (node.right == null) return node.left;
    ////            else
    ////            {
    ////                Node tmp = GetMin(node.right);
    ////                node.key = tmp.key;
    ////                node.value = tmp.value;
    ////                node.right = RemoveNode(node.right, tmp.key);
    ////            }
    ////        }
    ////        return node;
    ////    }
    ////    private Node GetMin(Node node)
    ////    {
    ////        while (node != null) node = node.left;
    ////        return node;
    ////    }
    ////    private void IterKeySet(List<K> key)
    ////    {
    ////        Stack<Node> stack = new Stack<Node>();
    ////        Node curr = null;
    ////        while (curr != null || stack.Count > 0)
    ////        {
    ////            while (curr != null)
    ////            {
    ////                stack.Push(curr);
    ////                curr = curr.left;
    ////            }
    ////            curr = stack.Pop();
    ////            key.Add(curr.key);
    ////            curr = curr.right;
    ////        }
    ////    }
    ////    private void IterEntrySet(List<KeyValuePair<K, V>> entry)
    ////    {
    ////        Stack<Node> stack = new Stack<Node>();
    ////        Node curr = null;
    ////        while (curr != null || stack.Count > 0)
    ////        {
    ////            while (curr != null)
    ////            {
    ////                stack.Push(curr);
    ////                curr = curr.left;
    ////            }
    ////            curr = stack.Pop();
    ////            entry.Add(new KeyValuePair<K, V>(curr.key, curr.value));
    ////            curr = curr.right;
    ////        }
    ////    }
    ////    //1-4
    ////    public MyTreeMap() => root = null;
    ////    public MyTreeMap(Comparer<K> cmp) => this.cmp = cmp;
    ////    public void Clear() => size = 0;
    ////    public bool ContainsKey(K key) => Get(key) != null;
    ////    //5
    ////    public bool ContainsValue(V value)
    ////    {
    ////        if (root == null) return false;
    ////        Stack<Node> stack = new Stack<Node>();
    ////        stack.Push(root);
    ////        while (stack.Count > 0)
    ////        {
    ////            Node curr = stack.Pop();
    ////            if (curr.value.Equals(value)) return true;
    ////            if (curr.left != null) stack.Push(curr.left);
    ////            if (curr.right != null) stack.Push(curr.right);
    ////        }
    ////        return false;
    ////    }
    ////    //6
    ////    public List<KeyValuePair<K, V>> EntrySet()
    ////    {
    ////        List<KeyValuePair<K, V>> keyValuePairs = new List<KeyValuePair<K, V>>();
    ////        IterEntrySet(keyValuePairs);
    ////        return keyValuePairs;
    ////    }
    ////    //7
    ////    public V Get(K key)
    ////    {
    ////        Node curr = root;
    ////        while (curr != null)
    ////        {
    ////            if (key.CompareTo(curr.key) < 0) curr = curr.left;
    ////            else if (key.CompareTo(curr.key) > 0) curr = curr.right;
    ////            else return curr.value;
    ////        }
    ////        return default(V);
    ////    }
    ////    //8
    ////    public bool IsEmpty() => size == 0;
    ////    //9
    ////    public List<K> KeySet()
    ////    {
    ////        List<K> keys = new List<K>();
    ////        IterKeySet(keys);
    ////        return keys;
    ////    }
    ////    //10
    ////    public void Put(K key, V value)
    ////    {
    ////        if (root == null)
    ////        {
    ////            root = new Node(key, value);
    ////            size++;
    ////            return;
    ////        }
    ////        Node curr = root;
    ////        Node parent = null;
    ////        while (curr != null)
    ////        {
    ////            parent = curr;
    ////            if (key.CompareTo(curr.key) < 0) curr = curr.left;
    ////            else if (key.CompareTo(curr.key) > 0) curr = curr.right;
    ////            else { curr.value = value; return; }

    ////        }
    ////        Node newNode = new Node(key, value);
    ////        if (parent != null)
    ////        {
    ////            if (key.CompareTo(parent.key) < 0) parent.left = newNode;
    ////            else parent.right = newNode;
    ////        }
    ////        size++;
    ////    }
    ////    //11
    ////    //public void Remove(K key)
    ////    //{
    ////    //    Node curr = root;
    ////    //    Node parent = null, nodeToRemove = null;
    ////    //    bool isleft = false;
    ////    //    while (curr != null)
    ////    //    {
    ////    //        if (key.CompareTo(curr.key) < 0) { parent = curr; curr = curr.left; }
    ////    //        else if (key.CompareTo(curr.key) > 0) { parent = curr; curr = curr.right; }
    ////    //        else { nodeToRemove = curr; break; }
    ////    //    }
    ////    //    if (nodeToRemove == null) return;
    ////    //    //V valueToReturn = nodeToRemove.value;
    ////    //    if (nodeToRemove.left == null & nodeToRemove.right == null)
    ////    //    {
    ////    //        if (nodeToRemove == root) root = null;
    ////    //        else if (isleft) parent.left = null;
    ////    //        else parent.right = null;
    ////    //    }
    ////    //    else if (nodeToRemove.left == null)
    ////    //    {
    ////    //        if (nodeToRemove == root) root = nodeToRemove.right;
    ////    //        else if (isleft) parent.left = nodeToRemove.right;
    ////    //        else parent.right = nodeToRemove.right;
    ////    //    }
    ////    //    else if (nodeToRemove.right == null)
    ////    //    {
    ////    //        if (nodeToRemove == root) root = nodeToRemove.left;
    ////    //        else if (isleft) parent.left = nodeToRemove.left;
    ////    //        else parent.right = nodeToRemove.left;
    ////    //    }
    ////    //    else
    ////    //    {
    ////    //        Node child = GetMin(nodeToRemove.left);
    ////    //        nodeToRemove.key = child.key;
    ////    //        nodeToRemove.value = child.value;
    ////    //        Remove(child.key);
    ////    //    }
    ////    //    //return valueToReturn;
    ////    //}
    ////    //public V Remove(K key)
    ////    //{
    ////    //    Node curr = root;
    ////    //    Node parent = null;
    ////    //    Node nodeToRemove = null;
    ////    //    bool isleft = false;
    ////    //    while (curr != null)
    ////    //    {
    ////    //        if (key.CompareTo(curr.key) < 0)
    ////    //        {
    ////    //            parent = curr;
    ////    //            curr = curr.left;
    ////    //        }
    ////    //        else if (key.CompareTo(curr.key) > 0)
    ////    //        {
    ////    //            parent = curr;
    ////    //            curr = curr.right;
    ////    //        }
    ////    //        else
    ////    //        {
    ////    //            nodeToRemove = curr;
    ////    //            break;
    ////    //        }
    ////    //    }
    ////    //    if (nodeToRemove == null)
    ////    //        return default(V);
    ////    //    V valueToReturn = nodeToRemove.value;
    ////    //    if (nodeToRemove.left == null && nodeToRemove.right == null)
    ////    //    {
    ////    //        if (nodeToRemove == root)
    ////    //            root = null;
    ////    //        else if (isleft)
    ////    //            parent.left = null;
    ////    //        else parent.right = null;
    ////    //    }
    ////    //    else if (nodeToRemove.left == null)
    ////    //    {
    ////    //        if (nodeToRemove == root)
    ////    //            root = nodeToRemove.right;
    ////    //        else if (isleft)
    ////    //            parent.left = nodeToRemove.right;
    ////    //        else parent.right = nodeToRemove.right;
    ////    //    }
    ////    //    else if (nodeToRemove.right == null)
    ////    //    {
    ////    //        if (nodeToRemove == root)
    ////    //            root = nodeToRemove.left;
    ////    //        else if (isleft)
    ////    //            parent.left = nodeToRemove.left;
    ////    //        else parent.right = nodeToRemove.left;
    ////    //    }
    ////    //    else
    ////    //    {
    ////    //        Node child = GetMin(nodeToRemove.right);
    ////    //        nodeToRemove.key = child.key;
    ////    //        nodeToRemove.value = child.value;
    ////    //        Remove(child.key);
    ////    //    }
    ////    //    return valueToReturn;
    ////    //}
    ////    public void Remove(K key)
    ////    {
    ////        if (cmp.Compare(key, root.key) == 0 && root.right == null && root.left == null)
    ////        {
    ////            root = null;
    ////            size--;
    ////            return;
    ////        }
    ////        Node high = root;
    ////        Node step = root;
    ////        if (cmp.Compare(key, root.key) < 0)
    ////            step = root.left;
    ////        else if (cmp.Compare(key, root.key) > 0)
    ////            step = root.right;
    ////        while (step != null)
    ////        {
    ////            if (cmp.Compare(key, step.key) == 0)
    ////            {
    ////                if (step.left == null && step.right == null)
    ////                {
    ////                    if (cmp.Compare(step.key, high.key) < 0)
    ////                    {
    ////                        size--;
    ////                        high.left = null;
    ////                        return;
    ////                    }
    ////                    else
    ////                    {
    ////                        size--;
    ////                        high.right = null;
    ////                        return;
    ////                    }
    ////                }
    ////                else if ((step.left == null && step.right != null) || (step.right == null && step.left != null))
    ////                {
    ////                    if (step.left != null)
    ////                    {
    ////                        step.value = step.left.value;
    ////                        step.key = step.left.key;
    ////                        step.right = step.left.right;
    ////                        step.left = step.left.left;
    ////                        size--;
    ////                        return;
    ////                    }
    ////                    else if (step.right != null)
    ////                    {
    ////                        step.value = step.right.value;
    ////                        step.key = step.right.key;
    ////                        step.right = step.right.right;
    ////                        step.left = step.right.left;
    ////                        size--;
    ////                        return;
    ////                    }
    ////                }
    ////                else if (step.left != null && step.right != null)
    ////                {
    ////                    Node max = step.left;
    ////                    if (max.right == null)
    ////                        max = step.left;
    ////                    while (max.right != null)
    ////                        max = max.right;
    ////                    Node maxHigh = max;
    ////                    if (maxHigh.left != null)
    ////                    {
    ////                        step.value = max.value;
    ////                        step.key = max.key;
    ////                        maxHigh.value = maxHigh.left.value;
    ////                        maxHigh.key = maxHigh.left.key;
    ////                        maxHigh.left = maxHigh.left.left;
    ////                    }
    ////                    else if (maxHigh.left == null)
    ////                    {
    ////                        step.value = max.value;
    ////                        step.key = max.key;
    ////                        step.left.right = max.left;
    ////                    }
    ////                    size--;
    ////                    return;
    ////                }
    ////            }
    ////            else if (cmp.Compare(key, step.key) < 0)
    ////            {
    ////                high = step;
    ////                step = step.left;
    ////            }
    ////            else if (cmp.Compare(key, step.key) > 0)
    ////            {
    ////                high = step;
    ////                step = step.right;
    ////            }
    ////        }
    ////    }

    ////    //12-14
    ////    public int Size() => size;
    ////    public K FirstKey() => root.key;
    ////    public K LastKey() => root == null ? throw new InvalidOperationException() : GetMin(root).key;
    ////    //15
    ////    public MyTreeMap<K, V> HeadMap(K end)
    ////    {
    ////        MyTreeMap<K, V> head = new MyTreeMap<K, V>();
    ////        if (root == null) return head;
    ////        Stack<Node> stack = new Stack<Node>();
    ////        stack.Push(root);
    ////        while (stack.Count > 0)
    ////        {
    ////            Node curr = stack.Pop();
    ////            if (curr.key.CompareTo(end) < 0)
    ////            {
    ////                head.Put(curr.key, curr.value);
    ////                if (curr.left != null) stack.Push(curr.left);
    ////                if (curr.right != null) stack.Push(curr.right);
    ////            }
    ////            else if (curr.left != null) stack.Push(curr.left);
    ////        }
    ////        return head;
    ////    }
    ////    //16
    ////    public MyTreeMap<K, V> SubMap(K start, K end)
    ////    {
    ////        MyTreeMap<K, V> head = new MyTreeMap<K, V>();
    ////        if (root == null) return head;
    ////        Stack<Node> stack = new Stack<Node>();
    ////        stack.Push(root);
    ////        while (stack.Count > 0)
    ////        {
    ////            Node curr = stack.Pop();
    ////            if (curr.key.CompareTo(start) >= 0 && curr.key.CompareTo(end) < 0)
    ////            {
    ////                head.Put(curr.key, curr.value);
    ////                if (curr.left != null) stack.Push(curr.left);
    ////                if (curr.right != null) stack.Push(curr.right);
    ////            }
    ////            else if (curr.key.CompareTo(end) >= 0) if (curr.left != null) stack.Push(curr.left);
    ////                else if (curr.right != null) stack.Push(curr.right);
    ////        }
    ////        return head;
    ////    }
    ////    //17
    ////    public MyTreeMap<K, V> TailMap(K start)
    ////    {
    ////        MyTreeMap<K, V> head = new MyTreeMap<K, V>();
    ////        if (root == null) return head;
    ////        Stack<Node> stack = new Stack<Node>();
    ////        stack.Push(root);
    ////        while (stack.Count > 0)
    ////        {
    ////            Node curr = stack.Pop();
    ////            if (curr.key.CompareTo(start) >= 0)
    ////            {
    ////                head.Put(curr.key, curr.value);
    ////                if (curr.left != null) stack.Push(curr.left);
    ////                if (curr.right != null) stack.Push(curr.right);
    ////            }
    ////            else if (curr.right != null) stack.Push(curr.right);
    ////        }
    ////        return head;
    ////    }
    ////    //18
    ////    public KeyValuePair<K, V> LowerEntry(K key)
    ////    {
    ////        KeyValuePair<K, V>? entry = null;
    ////        if (root == null) return entry ?? default;
    ////        Stack<Node> stack = new Stack<Node>();
    ////        stack.Push(root);
    ////        while (stack.Count > 0)
    ////        {
    ////            Node curr = stack.Pop();
    ////            if (curr.key.CompareTo(key) < 0)
    ////            {
    ////                entry = new KeyValuePair<K, V>(curr.key, curr.value);
    ////                stack.Push(curr.left);
    ////                stack.Push(curr.right);
    ////            }
    ////            else stack.Push(curr.left);
    ////        }
    ////        return entry ?? default;
    ////    }
    ////    //19
    ////    public KeyValuePair<K, V> FloorEntry(K key)
    ////    {
    ////        KeyValuePair<K, V>? entry = null;
    ////        if (root == null) return entry ?? default;
    ////        Stack<Node> stack = new Stack<Node>();
    ////        stack.Push(root);
    ////        while (stack.Count > 0)
    ////        {
    ////            Node curr = stack.Pop();
    ////            if (curr.key.CompareTo(key) <= 0)
    ////            {
    ////                entry = new KeyValuePair<K, V>(curr.key, curr.value);
    ////                stack.Push(curr.left);
    ////                stack.Push(curr.right);
    ////            }
    ////            else stack.Push(curr.left);
    ////        }
    ////        return entry ?? default;
    ////    }
    ////    //20
    ////    public KeyValuePair<K, V> HigherEntry(K key)
    ////    {
    ////        Node curr = root;
    ////        KeyValuePair<K, V>? entry = null;
    ////        while (curr != null)
    ////        {
    ////            if (curr.key.CompareTo(key) > 0)
    ////            {
    ////                entry = new KeyValuePair<K, V>(curr.key, curr.value);
    ////                curr = curr.left;
    ////            }
    ////            else curr = curr.right;
    ////        }
    ////        return entry ?? default;
    ////    }
    ////    //21
    ////    public KeyValuePair<K, V> CeilingEntry(K key)
    ////    {
    ////        Node curr = root;
    ////        KeyValuePair<K, V>? entry = null;
    ////        while (curr != null)
    ////        {
    ////            if (curr.key.CompareTo(key) >= 0)
    ////            {
    ////                entry = new KeyValuePair<K, V>(curr.key, curr.value);
    ////                curr = curr.left;
    ////            }
    ////            else curr = curr.right;
    ////        }
    ////        return entry ?? default;
    ////    }
    ////    //22
    ////    public K LowerKey(K key)
    ////    {
    ////        Node curr = root;
    ////        K res = default(K);
    ////        while (curr != null)
    ////        {
    ////            if (key.CompareTo(curr.key) > 0) { res = curr.key; curr = curr.left; }
    ////            else curr = curr.right;
    ////        }
    ////        return res;
    ////    }
    ////    //23
    ////    public K FloorKey(K key)
    ////    {
    ////        Node curr = root;
    ////        K res = default(K);
    ////        while (curr != null)
    ////        {
    ////            if (key.CompareTo(curr.key) >= 0) { res = curr.key; curr = curr.left; }
    ////            else curr = curr.right;
    ////        }
    ////        return res;
    ////    }
    ////    //24
    ////    public K HigherKey(K key)
    ////    {
    ////        Node curr = root;
    ////        K res = default(K);
    ////        while (curr != null)
    ////        {
    ////            if (key.CompareTo(curr.key) < 0) { res = curr.key; curr = curr.left; }
    ////            else curr = curr.right;
    ////        }
    ////        return res;
    ////    }
    ////    //25
    ////    public K CeilingKey(K key)
    ////    {
    ////        Node curr = root;
    ////        K res = default(K);
    ////        while (curr != null)
    ////        {
    ////            if (key.CompareTo(curr.key) <= 0) { res = curr.key; curr = curr.left; }
    ////            else curr = curr.right;
    ////        }
    ////        return res;
    ////    }
    ////    //26
    ////    public KeyValuePair<K, V> PollFirstEntry()
    ////    {
    ////        if (root == null) return default;
    ////        KeyValuePair<K, V> entry = FirstEntry();
    ////        root = RemoveNode(root, entry.Key);
    ////        size--;
    ////        return entry;
    ////    }
    ////    //27
    ////    public KeyValuePair<K, V> PollLastEntry()
    ////    {
    ////        if (root == null) return default;
    ////        KeyValuePair<K, V> entry = LastEntry();
    ////        root = RemoveNode(root, entry.Key);
    ////        size--;
    ////        return entry;
    ////    }
    ////    //28
    ////    public KeyValuePair<K, V> FirstEntry()
    ////    {
    ////        if (root == null) return default;
    ////        Node minNode = GetMin(root);
    ////        return new KeyValuePair<K, V>(minNode.key, minNode.value);
    ////    }
    ////    //29
    ////    public KeyValuePair<K, V> LastEntry()
    ////    {
    ////        if (root == null) return default;
    ////        Node curr = root;
    ////        Node maxNode = null;
    ////        while (curr != null)
    ////        {
    ////            maxNode = curr;
    ////            curr = curr.right;
    ////        }
    ////        return new KeyValuePair<K, V>(maxNode.key, maxNode.value);
    ////    }
    ////}
}
