using System.Diagnostics;

namespace RBTreeLib
{
    public class MyComparator<T> : Comparer<T> where T : IComparable
    {
        public override int Compare(T? x, T? y)
        {
            return x!.CompareTo(y);
            throw new NotImplementedException();
        }
    }
    public class RBTree<K> where K : IComparable
    {
        public enum Color
        {
            Red,
            Black
        }
        private class Node
        {
            public K? Key = default;
            public Node? pred = null;
            public Node? left = null;
            public Node? right = null;
            public Color color = Color.Red;
        }
        Node? root = null;
        Node nil = new Node();
        int size = 0;
        IComparer<K> comparer = new MyComparator<K>();
        //1
        public RBTree()
        {
            nil.color = Color.Black;
        }
        //3
        public RBTree(IComparer<K> comparator)
        {
            nil.color = Color.Black;
            comparer = comparator;
        }
        //4
        public RBTree(K[] array)
        {
            nil.color = Color.Black;
            foreach (K element in array) Add(element);
        }
        //5
        public RBTree(SortedSet<K> array)
        {
            nil.color = Color.Black;
            foreach (K element in array) Add(element);
        }

        //6
        public void Add(K element)
        {
            if (root == null)
            {
                root = new Node();
                root.left = nil;
                root.right = nil;
                root.Key = element;
                root.color = Color.Black;
                size++;
                return;
            }
            Node addible = new Node();
            addible.left = nil;
            addible.right = nil;
            addible.Key = element;
            Node? step = root;
            while (step != nil)
            {
                if (comparer.Compare(step!.Key, element) == 0)
                {
                    step.Key = element;
                    return;
                }
                else if (comparer.Compare(element, step.Key) > 0)
                {
                    if (step.right == nil)
                    {
                        step.right = addible;
                        addible.pred = step;
                        size++;
                        Balance(addible);
                        return;
                    }
                    step = step.right;
                }
                else
                {
                    if (step.left == nil)
                    {
                        step.left = addible;
                        addible.pred = step;
                        size++;
                        Balance(addible);
                        return;
                    }
                    step = step.left;
                }
            }
        }
        private void Balance(Node step)
        {
            if (step.pred == null || step.pred.pred == null) return;
            Node dad = step.pred;
            Node grand = step.pred.pred;
            Node uncle = grand.left == dad ? grand.right! : grand.left!;
            if (step.color == Color.Red && dad.color == Color.Red)
            {
                Case1();
                Case2();
                Case3();
            }
            void Case1()
            {
                if (uncle.color == Color.Red)
                {
                    dad.color = Color.Black;
                    uncle.color = Color.Black;
                    grand.color = grand == root ? Color.Black : Color.Red;
                    Balance(grand);
                }
            }
            void Case2()
            {
                if (uncle.color == Color.Black && grand.left == dad && dad.right == step && step.color == Color.Red && dad.color == Color.Red)
                {
                    LeftSmallRotation(step, dad, grand);
                    Case3();
                }
                else if (uncle.color == Color.Black && grand.right == dad && dad.left == step && step.color == Color.Red && dad.color == Color.Red)
                {
                    RightSmallRotation(step, dad, grand);
                    Case3();
                }
            }
            void Case3()
            {
                if (uncle.color == Color.Black && grand.left == dad && dad.left == step && step.color == Color.Red && dad.color == Color.Red)
                {
                    RightBigRotation(step, dad, grand);
                    Balance(dad);
                }
                else if (uncle.color == Color.Black && grand.right == dad && dad.right == step && step.color == Color.Red && dad.color == Color.Red)
                {
                    LeftBigRotation(step, dad, grand);
                    Balance(dad);
                }
            }
        }
        void LeftSmallRotation(Node X, Node dad, Node grand)
        {
            Node leftX = X.left!;
            X.left = dad;
            dad.pred = X;
            dad.right = leftX;
            grand.left = X;
            X.pred = grand;
        }
        void RightSmallRotation(Node X, Node dad, Node grand)
        {
            Node rightX = X.right!;
            X.right = dad;
            dad.pred = X;
            dad.left = rightX;
            grand.right = X;
            X.pred = grand;
        }
        void RightBigRotation(Node X, Node dad, Node grand)
        {
            Node dadRight = dad.right!;
            if (grand.pred != null)
            {
                dad.pred = grand.pred;
                if (grand.pred.left == grand) grand.pred.left = dad;
                else grand.pred.right = dad;
            }
            else
            {
                dad.pred = null;
                root = dad;
            }
            dad.right = grand;
            grand.pred = dad;
            grand.left = dadRight;
            dadRight.pred = grand;
            dad.color = Color.Black;
            grand.color = Color.Red;
        }
        void LeftBigRotation(Node X, Node dad, Node grand)
        {
            Node dadLeft = dad.left!;
            if (grand.pred != null)
            {
                dad.pred = grand.pred;
                if (grand.pred.left == grand) grand.pred.left = dad;
                else grand.pred.right = dad;
            }
            else
            {
                dad.pred = null;
                root = dad;
            }
            dad.left = grand;
            grand.pred = dad;
            grand.right = dadLeft;
            dadLeft.pred = grand;
            dad.color = Color.Black;
            grand.color = Color.Red;
        }
        //7
        public void AddAll(K[] array)
        {
            foreach (K element in array) Add(element);
        }
        
        //8
        public void Clear()
        {
            root = null;
            size = 0;
        }
        //9
        public bool Contains(K element)
        {
            Node? step = root;
            while (step != null)
            {
                if (comparer.Compare(element, step.Key) == 0) return true;
                else if (comparer.Compare(element, step.Key) > 0) step = step.right;
                step = step!.left;
            }
            return false;
        }
        //10
        public bool ContaisAll(K[] array)
        {
            foreach (K element in array) if (!Contains(element)) return false;
            return true;
        }
        //11
        public bool IsEmpty() => size == 0 ? true : false;
        //12
        public void Remove(K element)
        {
            Removing(element);
            size--;
        }
        private void Removing(K element)
        {
            bool isDeleted = false;
            Node? step = root;
            //while (step != null)
            //{
            //    if (comparer.Compare(element, step.Key) == 0) break;
            //    else if (comparer.Compare(element, step.Key) > 0) step = step.right;
            //    else step = step.left;
            //}
            Stack<Node> stack = new Stack<Node>();
            while (step != null || stack.Count > 0)
            {
                while (step != null)
                {
                    stack.Push(step);
                    step = step.left;
                }
                step = stack.Pop();
                if (comparer.Compare(element, step.Key) == 0) break;
                step = step.right;
            }
            if (!isDeleted) RedNoChildren(step!);
            if (!isDeleted) TwoChildren(step!);
            if (!isDeleted) BlackOneChild(step!);
            if (!isDeleted) BlackNoChildren(step!);

            void RedNoChildren(Node current)
            {
                if (size == 1) root = nil;
                if (current.color == Color.Red && current.left == nil && current.right == nil)
                {
                    if (current.pred!.left == current) current.pred.left = nil;
                    else current.pred.right = nil;
                }
            }
            void TwoChildren(Node current)
            {
                if (current.left != nil && current.right != nil)
                {
                    Node minRight = current.right!;
                    while (minRight.left != nil) minRight = minRight.left!;
                    if (minRight.right == nil)
                    {
                        K temp = current.Key!;
                        current.Key = minRight.Key;
                        minRight.Key = temp;
                        Removing(minRight.Key!);
                        isDeleted = true;
                    }
                    else
                    {
                        K temp = current.Key!;
                        current.Key = minRight.Key;
                        minRight.Key = temp;
                        Removing(minRight.Key);
                        isDeleted = true;
                    }
                }
            }
            void BlackOneChild(Node current)
            {
                if (current.color == Color.Black && current.right != nil && current.left == nil)
                {
                    Node right = current.right!;
                    K temp = current.Key!;
                    current.Key = right.Key;
                    right.Key = temp;
                    Removing(right.Key);
                    isDeleted = true;
                }
                else if (current.color == Color.Black && current.right == nil && current.left != nil)
                {
                    Node left = current.left!;
                    K temp = current.Key!;
                    current.Key = left.Key;
                    left.Key = temp;
                    Removing(left.Key);
                    isDeleted = true;
                }
            }
            void BlackNoChildren(Node element)
            {
                if (element.color == Color.Black && element.left == nil && element.right == nil)
                    Rebalance(element);
            }
            void Rebalance(Node current, bool isDeleted = false)
            {
                if (!(current.color == Color.Black && current != root)) return;
                if (current.pred != null)
                {
                    Node dad = current.pred;
                    if (dad.left != nil && dad.right != nil)
                    {
                        Node brother = dad.left == current ? dad.right! : dad.left!;
                        Node leftBaby = brother.left!;
                        Node rightBaby = brother.right!; //начало ребаланса
                        if (dad.right == brother) // левая удаляемая вершина
                        {
                            if (brother.color == Color.Black && rightBaby.color == Color.Red) // черный брат, правый сын красный
                            {
                                Color topColor = dad.color;
                                LeftBigRotation(rightBaby, brother, dad);
                                rightBaby.color = Color.Black;
                                dad.color = Color.Black;
                                brother.color = topColor;
                                if (!isDeleted) dad.left = nil;
                            }
                            else if (brother.color == Color.Black && leftBaby.color == Color.Red) // черный брат левый сын красный
                            {
                                RightSmallRotation(leftBaby, brother, dad);
                                leftBaby.color = Color.Black;
                                dad.color = Color.Red;
                                Rebalance(current);
                            }
                            else if (brother.color == Color.Black && rightBaby.color == Color.Black && leftBaby.color == Color.Black) // оба ребенка брата черные
                            {
                                Color topColor = dad.color;
                                brother.color = Color.Red;
                                dad.color = Color.Black;
                                dad.left = nil;
                                if (topColor == Color.Red) return;
                                else Rebalance(dad, true);
                            }
                            else if (brother.color == Color.Red)
                            {
                                LeftBigRotation(rightBaby, brother, dad);
                                Rebalance(current);
                            }
                        }
                        else // правая удаляемая вершина
                        {
                            if (brother.color == Color.Black && leftBaby.color == Color.Red) // черный брат, левый сын красный
                            {
                                Color topColor = dad.color;
                                RightBigRotation(leftBaby, brother, dad);
                                leftBaby.color = Color.Black;
                                dad.color = Color.Black;
                                brother.color = topColor;
                                if (!isDeleted) dad.right = nil;
                            }
                            else if (brother.color == Color.Black && rightBaby.color == Color.Red) // черный брат правый сын красный
                            {
                                LeftSmallRotation(rightBaby, brother, dad);
                                rightBaby.color = Color.Black;
                                dad.color = Color.Red;
                                Rebalance(current);
                            }
                            else if (brother.color == Color.Black && leftBaby.color == Color.Black && rightBaby.color == Color.Black) // оба ребенка брата черные
                            {
                                Color topColor = dad.color;
                                brother.color = Color.Red;
                                dad.color = Color.Black;
                                if (!isDeleted) dad.right = nil;
                                if (topColor == Color.Red) return;
                                else Rebalance(dad, true);
                            }
                            else if (brother.color == Color.Red)
                            {
                                RightBigRotation(leftBaby, brother, dad);
                                Rebalance(current);
                            }
                        }
                    }
                }
            }
        }
        //13
        public void RemoveAll(K[] array)
        {
            foreach (K key in array) Remove(key);
        }
        //14
        public void RetainAll(K[] array)
        {
            K[] RBT = ToArray();
            foreach (K key in RBT)
            {
                if (!array.Contains(key)) Remove(key);
            }
        }
        //15
        public int Size() => size;
        //16
        public K[] ToArray()
        {
            Stack<Node> stack = new Stack<Node>();
            Node step = root!;
            K[] array = new K[size];
            int index = 0;
            while (step != null || stack.Count > 0)
            {
                while (step != null)
                {
                    array[index++] = step.Key!;
                    stack.Push(step);
                    step = step.left!;
                }
                step = stack.Pop();
                step = step.right!;
            }
            return array;
        }
        //17
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
                K[] RBT = ToArray();
                K[] array = new K[size + a.Length];
                int index = 0;
                foreach (K key in a) array[index++] = key;
                foreach (K key in RBT) array[index++] = key;
                return array;
            }
        }
        //18
        public K First()
        {
            Node step = root!;
            while (step.left != null) step = step.left;
            return step.Key!;
        }
        //19
        public K Last()
        {
            Node step = root!;
            while (step.right != null) step = step.right;
            return step.Key!;
        }
        //20
        public SortedSet<K> SubSet(K start, K end)
        {
            SortedSet<K> returnTree = new SortedSet<K>();
            Node step = root!;
            Stack<Node> stack = new Stack<Node>();
            while (step != null || stack.Count > 0)
            {
                while (step != null)
                {
                    if (comparer.Compare(step.Key, start) >= 0 && comparer.Compare(step.Key, end) < 0)
                        returnTree.Add(step.Key!);
                    stack.Push(step);
                    step = step.left!;
                }
                if (stack.Count > 0)
                {
                    step = stack.Pop();
                    step = step.right!;
                }
            }
            return returnTree;
        }
        //21
        public SortedSet<K> HeadSet(K start)
        {
            SortedSet<K> returnTree = new SortedSet<K>();
            Node step = root!;
            Stack<Node> stack = new Stack<Node>();
            while (step != null || stack.Count > 0)
            {
                while (step != null)
                {
                    if (comparer.Compare(step.Key, start) < 0) returnTree.Add(step.Key!);
                    stack.Push(step);
                    step = step.left!;
                }
                if (stack.Count > 0)
                {
                    step = stack.Pop();
                    step = step.right!;
                }
            }
            return returnTree;
        }
        //22
        public SortedSet<K> TailSet(K start)
        {
            SortedSet<K> returnTree = new SortedSet<K>();
            Node step = root!;
            Stack<Node> stack = new Stack<Node>();
            while (step != null || stack.Count > 0)
            {
                while (step != null)
                {
                    if (comparer.Compare(step.Key, start) >= 0) returnTree.Add(step.Key!);
                    stack.Push(step);
                    step = step.left!;
                }
                if (stack.Count > 0)
                {
                    step = stack.Pop();
                    step = step.right!;
                }
            }
            return returnTree;
        }
        //23
        public K? Ceiling(K element)
        {
            K[] array = ToArray();
            Array.Sort(array);
            foreach (K item in array) if (comparer.Compare(item, element) >= 0) return item;
            return default;
        }
        //24
        public K? Floor(K element)
        {
            K[] array = ToArray();
            Array.Sort(array);
            Array.Reverse(array);
            foreach (K item in array) if (comparer.Compare(item, element) <= 0) return item;
            return default;
        }
        //25
        public K? Highter(K element)
        {
            K max = First();
            if (comparer.Compare(max, element) > 0) return max;
            return default;
        }
        //26
        public K? Lower(K element)
        {
            K min = Last();
            if (comparer.Compare(min, element) < 0) return min;
            return default;
        }
        //27
        public RBTree<K> HeadSet(K upperBound, bool incl)
        {
            RBTree<K> head = new RBTree<K>();
            K[] array = ToArray();
            foreach (K item in array)
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
            K[] arrayStay = head.ToArray();
            RetainAll(arrayStay);
            return head;
        }
        //28
        public RBTree<K> SubSet(K lowerBound, K upperBound, bool lowIncl, bool highIncl)
        {
            RBTree<K> head = new RBTree<K>();
            K[] array = ToArray();
            foreach (K item in array)
                if (lowIncl && highIncl)
                    if (item.CompareTo(upperBound) <= 0 && item.CompareTo(lowerBound) >= 0) head.Add(item);
                    else if (!lowIncl && highIncl)
                        if (item.CompareTo(upperBound) < 0 && item.CompareTo(lowerBound) >= 0) head.Add(item);
                        else if (lowIncl && !highIncl)
                            if (item.CompareTo(upperBound) <= 0 && item.CompareTo(lowerBound) > 0) head.Add(item);
                            else if (!lowIncl && !highIncl)
                                if (item.CompareTo(upperBound) < 0 && item.CompareTo(lowerBound) > 0) head.Add(item);
            K[] arrayStay = head.ToArray();
            RetainAll(arrayStay);
            return head;
        }
        //29
        public RBTree<K> TailSet(K upperBound, bool incl)
        {
            RBTree<K> head = new RBTree<K>();
            K[] array = ToArray();
            foreach (K item in array)
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
            K[] arrayStay = head.ToArray();
            RetainAll(array);
            return head;
        }
        //30
        public K PollLast()
        {
            K el = Last();
            Remove(el);
            return el;
        }
        //31
        public K PollFirst()
        {
            K el = First();
            Remove(el);
            return el;
        }
        //32
        public IEnumerable<K> DescendingIterator() => CreateDescendingIterator(root!);
        private IEnumerable<K> CreateDescendingIterator(Node node)
        {
            if (node != null && node != nil)
            {
                foreach (var value in CreateDescendingIterator(node.right!))
                    yield return value;
                yield return node.Key!;
                foreach (var value in CreateDescendingIterator(node.left!))
                    yield return value;
            }
        }
        //33
        public SortedSet<K> DescendingSet()
        {
            var descendingSet = new SortedSet<K>(Comparer<K>.Create((x, y) => y.CompareTo(x)));
            var enumerator = DescendingIterator();
            foreach (var item in enumerator)
                descendingSet.Add(item);
            return descendingSet;
        }
        
        public void Print() => Printing(root!, "", false);
        private void Printing(Node node, string indent, bool isLast)
        {
            if (node == null)
            {
                Console.WriteLine("RBTree is Empty");
                return;
            }
            string clr = ""; Console.Write(indent);
            if (isLast)
            {
                Console.Write("`─"); indent += "  ";
            }
            else
            {
                Console.Write("|─");
                indent += "| ";
            }
            if (node.color == Color.Red) clr = "Red"; else clr = "Black";
            Console.WriteLine($"{node.Key}/{clr}"); if (node.left != nil)
                Printing(node.left!, indent, node.right == null); if (node.right != nil)
                Printing(node.right!, indent, true);
        }
    }
}