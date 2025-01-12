using Library;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TreeSet
{
    public class MyComparator<T> : Comparer<T> where T : IComparable<T>
    {
        public override int Compare(T x, T y)
        {
            return x.CompareTo(y);
            throw new NotImplementedException();
        }
    }
    public class MyTreeSet<K> where K : IComparable<K>
    {
        public MyIteratorSet<K> ListIterator() => new MyItr(this);
        public class MyItr : MyIteratorSet<K>
        {
            MyTreeSet<K> treeSet;
            K cursor;
            Node current;
            public K Cursor => cursor;
            int lenght = 0;
            Stack<Node> stack = new Stack<Node>();
            public MyItr(MyTreeSet<K> array)
            {
                treeSet = array;
                current = treeSet.nil;
            }
            public K Next()
            {
                if (current == treeSet.nil && lenght == 0)
                    current = treeSet.root;
                while (current != treeSet.nil)
                {
                    stack.Push(current);
                    current = current.left;
                }
                current = stack.Pop();
                cursor = current.Key;
                current = current.right;
                lenght++;
                return cursor;
            }
            public bool HasNext()
            {
                if (lenght >= treeSet.Size())
                    return false;
                return true;
            }
            public void Remove()
            {
                if (treeSet.Contains(cursor))
                {
                    treeSet.Remove(cursor);
                    current = treeSet.root;
                    stack.Clear();
                    lenght--;
                }
            }
        }
        private class Node
        {
            public Node left = null;
            public Node right = null;
            public Node prev = null;
            public Color color = Color.Red;
            public K Key;
        }
        public enum Color
        {
            Black,
            Red
        }
        Node root = null;
        Node nil = new Node();
        IComparer<K> comparer = new MyComparator<K>();
        int size;
        public MyTreeSet()
        {
            nil.color = Color.Black;
            size = 0;
        }
        public MyTreeSet(IComparer<K> comparer)
        {
            nil.color = Color.Black;
            this.comparer = comparer;
        }
        public MyTreeSet(K[] a)
        {
            foreach (var item in a)
                Add(item);
        }
        public MyTreeSet(SortedSet<K> E)
        {
            foreach (K k in E) Add(k);
        }
        public void Clear()
        {
            root = null;
            size = 0;
        }
        public bool Contains(K key)
        {
            Node step = root;
            while (step != null)
            {
                if (comparer.Compare(key, step.Key) < 0)
                    step = step.left;
                if (comparer.Compare(key, step.Key) > 0)
                    step = step.right;
                if (key.Equals(step.Key)) return true;
            }
            return false;
        }
        public bool ContainsAll(K[] a)
        {
            foreach (K k in a)
                if (!Contains(k))
                    return false;
            return true;
        }
        public bool IsEmpty() => size == 0;
        public int Size() => size;
        public K[] ToArray()
        {
            K[] array = new K[size];
            array = DFS();
            return array;
        }
        public K[] ToArray(K[] a)
        {
            if (a == null)
            {
                a = new K[size];
                a = ToArray();
                return a;
            }
            else
            {
                K[] arr = new K[size + a.Length];
                K[] arrT = new K[size];
                arrT = ToArray();
                int index = 0;
                foreach (K k in a)
                    arr[index++] = k;
                foreach (K k in arrT)
                    arr[index++] = k;
                return arr;
            }
        }
        public K[] DFS()
        {
            Stack<Node> stack = new Stack<Node>();
            Node step = root;
            K[] arr = new K[size];
            int index = 0;
            while (step != null || stack.Count > 0)
            {
                while (step != null)
                {
                    arr[index++] = step.Key;
                    stack.Push(step);
                    step = step.left;
                }
                step = stack.Pop();
                step = step.right;
            }
            return arr;
        }
        public IEnumerable<K> DFSO()
        {
            Stack<Node> stack = new Stack<Node>();
            Node step = root;
            while (step != null || stack.Count > 0)
            {
                while (step != null)
                {
                    if (step != nil)
                    {
                        stack.Push(step);
                    }
                    step = step.left;
                }
                step = stack.Pop();
                yield return step.Key;
                step = step.right;
            }
        }
        public K FirstKey()
        {
            if (root != null)
                return root.Key;
            return default(K);
        }
        public K LastKey()
        {
            Node step = root;
            while (step.right != null)
            {
                step = step.right;
            }
            return step.Key;
        }
        public void Add(K key)
        {
            if (root == null)
            {
                root = new Node();
                root.Key = key;
                root.left = nil;
                root.right = nil;
                root.color = Color.Black;
                size++;
                return;
            }
            Node newAdd = new Node();
            Node step = root;
            newAdd.Key = key;
            size++;
            while (true)
            {
                if (comparer.Compare(newAdd.Key, step.Key) < 0)
                {
                    if (step.left == nil)
                    {
                        newAdd.left = nil;
                        newAdd.right = nil;
                        step.left = newAdd;
                        newAdd.prev = step;
                        BalanceTree(newAdd);
                        break;
                    }
                    else
                        step = step.left;
                }
                else if (comparer.Compare(newAdd.Key, step.Key) > 0)
                {
                    if (step.right == nil)
                    {
                        newAdd.left = nil;
                        newAdd.right = nil;
                        step.right = newAdd;
                        newAdd.prev = step;
                        BalanceTree(newAdd);
                        break;
                    }
                    else step = step.right;
                }
                else
                {
                    step.Key = key;
                    size--;
                    return;
                }
            }
        }
        public void AddAll(K[] a)
        {
            foreach (K k in a) Add(k);
        }
        public SortedSet<K> SubSet(K start, K end)
        {
            SortedSet<K> returnTree = new SortedSet<K>();
            Node step = root;
            Stack<Node> stack = new Stack<Node>();
            while (step != null || stack.Count > 0)
            {
                while (step != null)
                {
                    if (comparer.Compare(step.Key, start) >= 0 && comparer.Compare(step.Key, end) < 0)
                        returnTree.Add(step.Key);
                    stack.Push(step);
                    step = step.left;
                }
                if (stack.Count > 0)
                {
                    step = stack.Pop();
                    step = step.right;
                }
            }
            return returnTree;
        }
        public SortedSet<K> HeadSet(K start)
        {
            SortedSet<K> returnTree = new SortedSet<K>();
            Node step = root;
            Stack<Node> stack = new Stack<Node>();
            while (step != null || stack.Count > 0)
            {
                while (step != null)
                {
                    if (comparer.Compare(step.Key, start) < 0)
                        returnTree.Add(step.Key);
                    stack.Push(step);
                    step = step.left;
                }
                if (stack.Count > 0)
                {
                    step = stack.Pop();
                    step = step.right;
                }
            }
            return returnTree;
        }
        public SortedSet<K> TailSet(K start)
        {
            SortedSet<K> returnTree = new SortedSet<K>();
            Node step = root;
            Stack<Node> stack = new Stack<Node>();
            while (step != null || stack.Count > 0)
            {
                while (step != null)
                {
                    if (comparer.Compare(step.Key, start) >= 0)
                        returnTree.Add(step.Key);
                    stack.Push(step);
                    step = step.left;
                }
                if (stack.Count > 0)
                {
                    step = stack.Pop();
                    step = step.right;
                }
            }
            return returnTree;
        }
        public void Remove(K key)
        {
            while (root.prev != null)
                root = root.prev;
            Node step = root;
            size--;
            while (step != null)
            {
                if (comparer.Compare(key, step.Key) < 0)
                    step = step.left;
                if (comparer.Compare(key, step.Key) > 0)
                    step = step.right;
                if (key.Equals(step.Key)) break;
            }
            if (step.color == Color.Red && step.left == nil && step.right == nil)
            {
                if (step.prev.left == step)
                    step.prev.left = nil;
                else step.prev.right = nil;
            }
            else if (step.left != nil && step.right != nil)
            {
                Node max = step.left;
                if (max.right == nil)
                {
                    if (max.color == Color.Red)//работает
                    {
                        step.Key = max.Key;
                        max.prev.left = nil;
                    }
                    else if (max.color == Color.Black)//работает
                    {
                        step.Key = max.Key;

                        //max.prev.right = nil;
                        if (max.left == nil && max.right == nil)
                        {
                            step.Key = max.Key;
                            DelLeftBlack(max);
                            max.prev.left = nil;
                        }
                        else
                        {
                            if (max.left != nil)
                            {
                                K keyy = max.Key;
                                max.Key = max.left.Key;
                                max.left.Key = keyy;
                                max = max.left;
                                if (max.prev.left == max)
                                    max.prev.left = nil;
                                else max.prev.right = nil;
                            }
                            else
                            {
                                K keyy = max.Key;
                                max.Key = max.right.Key;
                                max.right.Key = keyy;
                                max = max.right;
                                if (max.prev.left == max)
                                    max.prev.left = nil;
                                else max.prev.right = nil;
                            }
                        }
                    }
                }
                else
                {
                    while (max.right != nil)
                        max = max.right;
                    if (max.color == Color.Red)//работает
                    {
                        step.Key = max.Key;
                        max.prev.right = nil;
                    }
                    else if (max.color == Color.Black)//не работает
                    {
                        if (max.left == nil && max.right == nil)
                        {
                            step.Key = max.Key;
                            DelRightBlack(max);
                            max.prev.right = nil;
                        }
                        else
                        {
                            step.Key = max.Key;
                            //max.prev.right = nil;
                            if (max.left != nil)
                            {
                                K keyy = max.Key;
                                max.Key = max.left.Key;
                                max.left.Key = keyy;
                                max = max.left;
                                if (max.prev.left == max)
                                    max.prev.left = nil;
                                else max.prev.right = nil;
                            }
                            else
                            {
                                K keyy = max.Key;
                                max.Key = max.right.Key;
                                max.right.Key = keyy;
                                max = max.right;
                                if (max.prev.left == max)
                                    max.prev.left = nil;
                                else max.prev.right = nil;
                            }
                        }
                    }
                }
            }
            else if (step.color == Color.Black && ((step.left == nil && step.right != nil) || (step.left != nil && step.right == nil)))
            {

                if (step.left != nil)
                {
                    K keyy = step.Key;
                    step.Key = step.left.Key;
                    step.left.Key = keyy;
                    step = step.left;
                    if (step.prev.left == step)
                        step.prev.left = nil;
                    else step.prev.right = nil;
                }
                else
                {
                    K keyy = step.Key;
                    step.Key = step.right.Key;
                    step.right.Key = keyy;
                    step = step.right;
                    if (step.prev.left == step)
                        step.prev.left = nil;
                    else step.prev.right = nil;
                }
            }
            else if (step.color == Color.Black && step.left == nil && step.right == nil)
            {
                if (step.prev.left == step)
                {
                    step.prev.left = nil;
                    DelLeftBlack(step);
                }
                else if (step.prev.right == step)
                {
                    step.prev.right = nil;
                    DelRightBlack(step);
                }
            }
            void DelLeftBlack(Node del)
            {
                Node dad = del.prev;
                if (dad == null)
                {
                    root = new Node();
                    return;
                }
                Node bro = dad.right;
                if (bro.color == Color.Black)
                {
                    if (bro.left.color == Color.Red || bro.right.color == Color.Red)
                    {
                        if (bro.right.color == Color.Red)
                        {
                            bro.color = dad.color;
                            dad.color = Color.Black;
                            bro.right.color = Color.Black;
                            LeftRotation(bro);
                        }
                        else if (bro.left.color == Color.Red && bro.right.color == Color.Black)
                        {
                            Color tmp = bro.color;
                            bro.color = bro.left.color;
                            bro.left.color = tmp;
                            Node BrotherLeft = bro.left;
                            RightRotation(BrotherLeft);
                            DelLeftBlack(del);
                            return;
                        }
                    }
                    else if (bro.left.color == Color.Black && bro.right.color == Color.Black)
                    {
                        bro.color = Color.Red;
                        if (dad.color == Color.Red)
                            dad.color = Color.Black;
                        else
                        {
                            dad.color = Color.Black;
                            if (dad.prev != null)
                            {
                                if (dad.prev.left == dad)
                                    DelLeftBlack(dad);
                                else if (dad.prev.right == dad) DelRightBlack(dad);
                            }
                        }
                    }
                }
                else if (dad.color == Color.Black && bro.color == Color.Red)
                {
                    dad.color = Color.Red;
                    bro.color = Color.Black;
                    LeftRotation(bro);
                }
            }
            void DelRightBlack(Node del)
            {
                Node dad = del.prev;
                if (dad == null)
                {
                    root = new Node();
                    return;
                }
                Node bro = dad.left;
                if (bro.color == Color.Black)
                {
                    if (bro.left.color == Color.Red || bro.right.color == Color.Red)
                    {
                        if (bro.left.color == Color.Red)
                        {
                            bro.color = dad.color;
                            dad.color = Color.Black;
                            bro.left.color = Color.Black;
                            RightRotation(bro);
                        }
                        else if (bro.left.color == Color.Black && bro.right.color == Color.Red)
                        {
                            Color tmp = bro.color;
                            bro.color = bro.left.color;
                            bro.right.color = tmp;
                            Node BrotherRight = bro.right;
                            LeftRotation(BrotherRight);
                            DelRightBlack(del);
                            return;
                        }
                    }
                    else if (bro.left.color == Color.Black && bro.right.color == Color.Black)
                    {
                        bro.color = Color.Red;
                        if (dad.color == Color.Red)
                            dad.color = Color.Black;
                        else
                        {
                            dad.color = Color.Black;
                            if (dad.prev != null)
                            {
                                if (dad.prev.left == dad)
                                    DelLeftBlack(dad);
                                else if (dad.prev.right == dad) DelRightBlack(dad);
                            }
                        }
                    }
                }
                else if (dad.color == Color.Black && bro.color == Color.Red)
                {
                    dad.color = Color.Red;
                    bro.color = Color.Black;
                    RightRotation(bro);
                }
            }
        }
        public void RemoveAll(K[] a)
        {
            foreach (K k in a) Remove(k);
        }
        public void RetainAll(K[] a)
        {
            K[] array = ToArray();
            foreach (K item in array)
            {
                if (!array.Contains(item))
                    Remove(item);
            }
        }
        public K Ceiling(K key)
        {
            foreach (K item in DFSO())
            {
                if (item.CompareTo(key) >= 0) return item;
            }
            return default(K);
        }
        public K Floor(K key)
        {
            K pred = nil.Key;
            foreach (K item in DFSO())
            {

                if (item.CompareTo(key) >= 0) return pred;
                pred = item;
            }
            return default(K);
        }
        public K Higher(K key)
        {
            K pred = nil.Key;
            foreach (K item in DFSO())
            {

                if (item.CompareTo(key) > 0) return pred;
                pred = item;
            }
            return default(K);
        }
        public K Lower(K key)
        {
            K pred = nil.Key;
            foreach (K item in DFSO())
            {

                if (item.CompareTo(key) < 0) return pred;
                pred = item;
            }
            return default(K);
        }
        public MyTreeSet<K> HeadSet(K upperBound, bool incl)
        {
            MyTreeSet<K> head = new MyTreeSet<K>();
            foreach (K item in DFSO())
            {
                if (incl)
                {
                    if (item.CompareTo(upperBound) <= 0) head.Add(item);
                }
                else
                {
                    if (item.CompareTo(upperBound) < 0) head.Add(item);
                }
            }
            K[] array = head.ToArray();
            MyTreeSet<K> ret = new MyTreeSet<K>();
            RetainAll(array);
            return head;
        }
        public MyTreeSet<K> SubSet(K lowerBound, K upperBound, bool lowIncl, bool highIncl)
        {
            MyTreeSet<K> head = new MyTreeSet<K>();
            foreach (K item in DFSO())
                if (lowIncl && highIncl)
                    if (item.CompareTo(upperBound) <= 0 && item.CompareTo(lowerBound) >= 0) head.Add(item);
                    else if (!lowIncl && highIncl)
                        if (item.CompareTo(upperBound) < 0 && item.CompareTo(lowerBound) >= 0) head.Add(item);
                        else if (lowIncl && !highIncl)
                            if (item.CompareTo(upperBound) <= 0 && item.CompareTo(lowerBound) > 0) head.Add(item);
                            else if (!lowIncl && !highIncl)
                                if (item.CompareTo(upperBound) < 0 && item.CompareTo(lowerBound) > 0) head.Add(item);
            K[] array = head.ToArray();
            MyTreeSet<K> ret = new MyTreeSet<K>();
            RetainAll(array);
            return head;
        }
        public MyTreeSet<K> TailSet(K upperBound, bool incl)
        {
            MyTreeSet<K> head = new MyTreeSet<K>();
            foreach (K item in DFSO())
            {
                if (incl)
                {
                    if (item.CompareTo(upperBound) >= 0) head.Add(item);
                }
                else
                {
                    if (item.CompareTo(upperBound) > 0) head.Add(item);
                }
            }
            K[] array = head.ToArray();
            MyTreeSet<K> ret = new MyTreeSet<K>();
            RetainAll(array);
            return head;
        }
        public K PollLast()
        {
            K el = LastKey();
            Remove(el);
            return el;
        }
        public K PollFirst()
        {
            Node step = root;
            while (step.left != null)
            {
                step = step.left;
            }
            Remove(step.Key);
            return step.Key;
        }
        private void BalanceTree(Node addNode)
        {
            if (addNode.prev == null || addNode.prev.prev == null) return;
            Node dad = addNode.prev;
            Node grand = dad.prev;
            Node uncle = grand.left == dad ? grand.right : grand.left;
            if (dad.color == Color.Red && addNode.color == Color.Red)
            {
                Case1();
                Case2();
                Case3();
            }
            void Case1()
            {
                if (addNode.prev.color == Color.Red && addNode.color == Color.Red && uncle.color == Color.Red && uncle != null)
                {
                    dad.color = Color.Black;
                    uncle.color = Color.Black;
                    if (grand.prev == null) { grand.color = Color.Black; }
                    else grand.color = Color.Red;
                    BalanceTree(grand);
                }
            }
            void Case2()
            {
                if (dad.right == addNode && uncle.color == Color.Black && addNode.color == Color.Red && dad.color == Color.Red && grand.left == dad)
                {
                    LeftRotation(addNode);
                    BalanceTree(dad);
                }
                else if (dad.left == addNode && uncle.color == Color.Black && addNode.color == Color.Red && dad.color == Color.Red && grand.right == dad) { RightRotation(addNode); BalanceTree(dad); }
            }
            void Case3()
            {
                if (grand.left == dad && uncle.color == Color.Black && dad.left == addNode && addNode.color == Color.Red && dad.color == Color.Red)
                {
                    grand.left = dad.right;
                    dad.right.prev = grand;
                    dad.right = grand;
                    dad.prev = grand.prev;
                    if (grand.prev != null && grand.prev.left == grand) grand.prev.left = dad;
                    else if (grand.prev != null && grand.prev.right == grand) grand.prev.right = dad;
                    grand.prev = dad;
                    grand.color = Color.Red;
                    dad.color = Color.Black;
                    BalanceTree(dad);
                }
                if (grand.right == dad && uncle.color == Color.Black && dad.right == addNode && addNode.color == Color.Red && dad.color == Color.Red)
                {
                    grand.right = dad.left;
                    dad.left.prev = grand;
                    dad.left = grand;
                    dad.prev = grand.prev;
                    if (grand.prev != null && grand.prev.left == grand) grand.prev.left = dad;
                    else if (grand.prev != null && grand.prev.right == grand) grand.prev.right = dad;
                    grand.prev = dad;
                    grand.color = Color.Red;
                    dad.color = Color.Black;
                    BalanceTree(dad);
                }
            }
        }
        private void LeftRotation(Node rotAdd)
        {
            Node Parent = rotAdd.prev;
            if (Parent == null) return;
            Node Grand = rotAdd.prev.prev;
            Node rotLeft = rotAdd.left;
            rotAdd.left = rotAdd.prev;
            Parent.right = rotLeft;
            rotAdd.prev = Grand;
            Parent.prev = rotAdd;
            rotLeft.prev = Parent;
            if (Grand != null && Grand.left == Parent)
                Grand.left = rotAdd;
            else if (Grand != null && Grand.right == Parent)
                Grand.right = rotAdd;
        }
        private void RightRotation(Node rotAdd)
        {
            Node Parent = rotAdd.prev;
            if (Parent == null) return;
            Node Grand = rotAdd.prev.prev;
            Node rotRight = rotAdd.right;
            rotAdd.right = rotAdd.prev;
            Parent.left = rotRight;
            rotAdd.prev = Grand;
            Parent.prev = rotAdd;
            rotRight.prev = Parent;
            if (Grand != null && Grand.left == Parent)
                Grand.left = rotAdd;
            else if (Grand != null && Grand.right == Parent)
                Grand.right = rotAdd;
        }
        public IEnumerable<K> GetIterator() => CreateDescendingIterator(root);
        private IEnumerable<K> CreateDescendingIterator(Node node)
        {
            if (node != null && node != nil)
            {
                foreach (var value in CreateDescendingIterator(node.right))
                    yield return value;
                yield return node.Key;
                foreach (var value in CreateDescendingIterator(node.left))
                    yield return value;
            }
        }
        public SortedSet<K> DescendingSet()
        {
            var descendingSet = new SortedSet<K>(Comparer<K>.Create((x, y) => y.CompareTo(x)));
            var enumerator = GetIterator();
            foreach (var item in enumerator)
                descendingSet.Add(item);
            return descendingSet;
        }
        public void Print()
        {
            if (size == 0)
                return;
            while (root.prev != null)
                root = root.prev;
            Pprint(root);
            void Pprint(Node roo)
            {
                if (roo == nil)
                    return;
                else
                {
                    if (roo == root)
                    {
                        Console.WriteLine(roo.color.ToString() + " " + roo.Key.ToString());
                        Pprint(roo.left);
                        Pprint(roo.right);
                    }
                    else
                    {
                        Console.WriteLine(roo.color.ToString() + " " + roo.Key.ToString());
                        Pprint(roo.left);
                        Pprint(roo.right);
                    }

                }
            }
        }
    }
}