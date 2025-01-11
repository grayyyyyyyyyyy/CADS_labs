namespace MyTreeMapLib
{
    public class MyTreeMap<K, V> where K : IComparable
    {
        public class MyComparator<T> : Comparer<T> where T : IComparable
        {
            public override int Compare(T? x, T? y)
            {
                return x!.CompareTo(y);
                throw new NotImplementedException();
            }
        }
        private class Node
        {
            public K? Key { get; set; }
            public V? Value { get; set; }
            public Node? left { get; set; }
            public Node? right { get; set; }
            public Node(K Key, V Value)
            {
                this.Key = Key;
                this.Value = Value;
                left = null;
                right = null;
            }
        }
        IComparer<K> comparator;
        Node? root;
        int size = 0;
        public MyTreeMap()
        {
            root = null;
            comparator = new MyComparator<K>();
        }
        public MyTreeMap(IComparer<K> comparator)
        {
            root = null;
            this.comparator = comparator;
        }
        public void Clear()
        {
            root = null;
            size = 0;
        }
        public bool ContainsKey(K key)
        {
            Node? step = root;
            while (step != null)
            {
                if (comparator.Compare(key, step.Key) == 0) return true;
                else if (comparator.Compare(key, step.Key) > 0) step = step.right;
                step = step!.left;
            }
            return false;
        }
        public bool ContainsValue(V value)
        {
            Node? step = root;
            Stack<Node> stack = new Stack<Node>();
            while (step != null || stack.Count > 0)
            {
                while (step != null)
                {
                    stack.Push(step);
                    step = step.left;
                }
                step = stack.Pop();
                if (step.Value!.Equals(value)) return true;
                step = step.right;
            }
            return false;
        }
        public List<KeyValuePair<K, V>> EntrySet()
        {
            Node? step = root;
            Stack<Node> stack = new Stack<Node>();
            List<KeyValuePair<K, V>> list = new List<KeyValuePair<K, V>>();
            while (step != null || stack.Count > 0)
            {
                while (step != null)
                {
                    stack.Push(step);
                    step = step.left;
                }
                step = stack.Pop();
                list.Add(new KeyValuePair<K, V>(step!.Key!, step.Value!));
                step = step.right;
            }
            return list;
        }
        public V? Get(K key)
        {
            Node? step = root;
            while (step != null)
            {
                if (comparator.Compare(step.Key, key) == 0) return step.Value!;
                if (comparator.Compare(step.Key, key) < 0) step = step.right;
                else step = step.left;
            }
            return default;
        }
        public bool IsEmpty() => size == 0;
        public List<K> KeySet()
        {
            Node? step = root;
            List<K> list = new List<K>();
            Stack<Node> stack = new Stack<Node>();
            while (step != null || stack.Count > 0)
            {
                while (step != null)
                {
                    stack.Push(step);
                    step = step.left;
                }
                step = stack.Pop();
                list.Add(step.Key!);
                step = step.right;
            }
            return list;
        }
        public void Put(K key, V value)
        {
            if (root == null)
            {
                root = new Node(key, value);
                size++;
                return;
            }
            Node? step = root;
            while (step != null)
            {
                if (comparator.Compare(step.Key, key) == 0)
                {
                    step.Value = value;
                    return;
                }
                else if (comparator.Compare(key, step.Key) > 0)
                {
                    if (step.right == null)
                    {
                        step.right = new Node(key, value);
                        size++;
                        return;
                    }
                    step = step.right;
                }
                else
                {
                    if (step.left == null)
                    {
                        step.left = new Node(key, value);
                        size++;
                        return;
                    }
                    step = step.left;
                }
            }
        }
        public void Remove(K key)
        {
            if (comparator.Compare(root!.Key, key) == 0 && size == 1)
            {
                root = null;
                size = 0;
                return;
            }
            Node pred = root;
            Node step = root;
            if (comparator.Compare(key, step.Key) > 0) step = pred.right!;
            if (comparator.Compare(key, step.Key) < 0) step = pred.left!;
            while (step != null)
            {
                if (comparator.Compare(step.Key, key) == 0)
                {
                    if (step.left == null && step.right == null)
                    {
                        if (comparator.Compare(key, step.Key) > 0)
                        {
                            pred.right = null;
                            size--;
                            return;
                        }
                        else
                        {
                            pred.left = null;
                            size--;
                            return;
                        }
                    }
                    if (step.left == null && step.right != null || step.left != null && step.left == null)
                    {
                        if (step.left != null)
                        {
                            step.Value = step.left.Value;
                            step.Key = step.left.Key;
                            step.left = step.left.left;
                            step.right = step.left!.right;
                            size--;
                            return;
                        }
                        else
                        {
                            step.Value = step.right!.Value;
                            step.Key = step.right.Key;
                            step.left = step.right.left;
                            step.right = step.right.right;
                            size--;
                            return;
                        }
                    }
                    else
                    {
                        Node? minRight = step.right;
                        if (minRight!.left == null)
                        {
                            step.Value = minRight.Value;
                            step.Key = minRight.Key;
                            step.right = minRight.right;
                            size--;
                            return;
                        }
                        else
                        {
                            while (minRight.left != null)
                            {
                                pred = minRight;
                                minRight = minRight.left;
                            }
                            pred.left = null;
                            step.Value = minRight.Value;
                            step.Key = minRight.Key;
                            size--;
                            return;
                        }
                    }
                }
                pred = step;
                if (comparator.Compare(key, step.Key) > 0) step = step.right!;
                if (comparator.Compare(key, step.Key) < 0) step = step.left!;
            }
        }
        public int Size() => size;
        public K? FirstKey()
        {
            if (root != null) return root.Key!;
            return default;
        }
        public K? LastKey()
        {
            Node? step = root;
            while (step != null)
            {
                if (step.right == null) return step.Key;
                step = step.right;
            }
            return default;
        }
        private Node? LastKeyValue()
        {
            Node? step = root;
            while (step != null)
            {
                if (step.right == null) return step;
                step = step.right;
            }
            return null;
        }
        public MyTreeMap<K, V> HeadMap(K key)
        {
            Node? step = root;
            MyTreeMap<K, V> newTree = new MyTreeMap<K, V>();
            Stack<Node> stack = new Stack<Node>();
            while (step != null || stack.Count > 0)
            {
                while (step != null)
                {
                    if (comparator.Compare(key, step.Key) > 0) newTree.Put(step.Key!, step.Value!);
                    stack.Push(step);
                    step = step.left;
                }
                if (stack.Count > 0)
                {
                    step = stack.Pop();
                    if (comparator.Compare(key, step.Key) <= 0) break;
                    step = step.right;
                }
            }
            return newTree;
        }
        public MyTreeMap<K, V> SubMap(K start, K end)
        {
            Node? step = root;
            MyTreeMap<K, V> newTree = new MyTreeMap<K, V>();
            Stack<Node> stack = new Stack<Node>();
            while (step != null || stack.Count > 0)
            {
                while (step != null)
                {
                    if (comparator.Compare(end, step.Key) > 0 && comparator.Compare(start, step.Key) <= 0)
                        newTree.Put(step.Key!, step.Value!);
                    stack.Push(step);
                    step = step.left;
                }
                if (stack.Count > 0)
                {
                    step = stack.Pop();
                    step = step.right;
                }
            }
            return newTree;
        }
        public MyTreeMap<K, V> TailMap(K key)
        {
            Node? step = root;
            MyTreeMap<K, V> newTree = new MyTreeMap<K, V>();
            Stack<Node> stack = new Stack<Node>();
            while (step != null || stack.Count > 0)
            {
                while (step != null)
                {
                    if (comparator.Compare(key, step.Key) < 0) newTree.Put(step.Key!, step.Value!);
                    stack.Push(step);
                    step = step.left;
                }
                if (stack.Count > 0)
                {
                    step = stack.Pop();
                    step = step.right;
                }
            }
            return newTree;
        }
        public KeyValuePair<K?, V?> LowerEntry(K Key)
        {
            Node? step = root;
            Stack<Node> stack = new Stack<Node>();
            while (step != null || stack.Count > 0)
            {
                while (step != null)
                {
                    stack.Push(step);
                    if (comparator.Compare(Key, step.Key) > 0) return new KeyValuePair<K?, V?>(step.Key, step.Value);
                    step = step.left;
                }
                step = stack.Pop();
                step = step.right;
            }
            return new KeyValuePair<K?, V?>(default(K), default(V));
        }
        public KeyValuePair<K?, V?> FloorEntry(K Key)
        {
            Node? step = root;
            Stack<Node> stack = new Stack<Node>();
            while (step != null || stack.Count > 0)
            {
                while (step != null)
                {
                    stack.Push(step);
                    if (comparator.Compare(Key, step.Key) >= 0) return new KeyValuePair<K?, V?>(step.Key, step.Value);
                    step = step.left;
                }
                step = stack.Pop();
                step = step.right;
            }
            return new KeyValuePair<K?, V?>(default(K), default(V));
        }
        public KeyValuePair<K?, V?> HightEntry(K Key)
        {
            Node? step = root;
            Stack<Node> stack = new Stack<Node>();
            while (step != null || stack.Count > 0)
            {
                while (step != null)
                {
                    stack.Push(step);
                    if (comparator.Compare(Key, step.Key) < 0) return new KeyValuePair<K?, V?>(step.Key, step.Value);
                    step = step.left;
                }
                step = stack.Pop();
                step = step.right;
            }
            return new KeyValuePair<K?, V?>(default(K), default(V));
        }
        public KeyValuePair<K?, V?> CeilingEntry(K Key)
        {
            Node? step = root;
            Stack<Node> stack = new Stack<Node>();
            while (step != null || stack.Count > 0)
            {
                while (step != null)
                {
                    stack.Push(step);
                    if (comparator.Compare(Key, step.Key) <= 0) return new KeyValuePair<K?, V?>(step.Key, step.Value);
                    step = step.left;
                }
                step = stack.Pop();
                step = step.right;
            }
            return new KeyValuePair<K?, V?>(default(K), default(V));
        }
        public K? LowerKey(K Key)
        {
            Node? step = root;
            Stack<Node> stack = new Stack<Node>();
            while (step != null || stack.Count > 0)
            {
                while (step != null)
                {
                    stack.Push(step);
                    if (comparator.Compare(Key, step.Key) > 0) return step.Key;
                    step = step.left;
                }
                step = stack.Pop();
                step = step.right;
            }
            return default;
        }
        public K? FloorKey(K Key)
        {
            Node? step = root;
            Stack<Node> stack = new Stack<Node>();
            while (step != null || stack.Count > 0)
            {
                while (step != null)
                {
                    stack.Push(step);
                    if (comparator.Compare(Key, step.Key) >= 0) return step.Key;
                    step = step.left;
                }
                step = stack.Pop();
                step = step.right;
            }
            return default;
        }
        public K? HigherKey(K Key)
        {
            Node? step = root;
            Stack<Node> stack = new Stack<Node>();
            while (step != null || stack.Count > 0)
            {
                while (step != null)
                {
                    stack.Push(step);
                    if (comparator.Compare(Key, step.Key) < 0) return step.Key;
                    step = step.left;
                }
                step = stack.Pop();
                step = step.right;
            }
            return default;
        }
        public K? CeilingKey(K Key)
        {
            Node? step = root;
            Stack<Node> stack = new Stack<Node>();
            while (step != null || stack.Count > 0)
            {
                while (step != null)
                {
                    stack.Push(step);
                    if (comparator.Compare(Key, step.Key) <= 0) return step.Key;
                    step = step.left;
                }
                step = stack.Pop();
                step = step.right;
            }
            return default;
        }
        public KeyValuePair<K?, V?> PollFirstEntry()
        {
            if (size == 0) return new KeyValuePair<K?, V?>(default(K), default(V));
            Node? step = root;
            Remove(root!.Key!);
            return new KeyValuePair<K?, V?>(step!.Key, step.Value);
        }
        public KeyValuePair<K?, V?> PollLastEntry()
        {
            if (size == 0) return new KeyValuePair<K?, V?>(default(K), default(V));
            Node? step = LastKeyValue();
            Remove(root!.Key!);
            return new KeyValuePair<K?, V?>(step!.Key, step.Value);
        }
        public KeyValuePair<K?, V?> FirstEntry()
        {
            if (size == 0) return new KeyValuePair<K?, V?>(default(K), default(V));
            Node? step = root;
            return new KeyValuePair<K?, V?>(step!.Key, step.Value);
        }
        public KeyValuePair<K?, V?> LastEntry()
        {
            if (size == 0) return new KeyValuePair<K?, V?>(default(K), default(V));
            Node? step = LastKeyValue();
            return new KeyValuePair<K?, V?>(step!.Key, step.Value);
        }
        public void Print()
        {
            Node? step = root;
            Stack<Node> stack = new Stack<Node>();
            while (step != null || stack.Count > 0)
            {
                while (step != null)
                {
                    stack.Push(step);
                    step = step.left;
                }
                step = stack.Pop();
                Console.WriteLine($"{step.Key}, {step.Value}");
                step = step.right;
            }
            Console.WriteLine();
        }
    }
}