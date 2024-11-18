namespace MyHeap
{
    public class Heap<T> where T : IComparable<T>
    {
        List<T> elements;

        public Heap(IEnumerable<T> elements)
        {
            this.elements = new List<T>(elements);
            MakeHeap();
        }
        void MakeHeap()
        {
            for (int i = elements.Count / 2; i >= 0; i--) Heapify(i);
        }

        private void Swap(int index_1, int index_2)
        {
            T tmp = elements[index_1];
            elements[index_1] = elements[index_2];
            elements[index_2] = tmp;
        }
        private int Parent(int index) => (index - 1) / 2;
        private int LeftChild(int index) => 2 * index + 1;
        private int RightChild(int index) => 2 * index + 2;
        private void Heapify(int index)
        {
            int largest = index;
            int left = LeftChild(index);
            int right = RightChild(index);
            if (left < elements.Count && elements[left].CompareTo(elements[largest]) > 0) largest = left;
            if (right < elements.Count && elements[right].CompareTo(elements[largest]) > 0) largest = right;
            if (largest != index)
            {
                Swap(index, largest);
                Heapify(largest);
            }
        }

        public T Max()
        {
            if (elements.Count == 0) throw new ArgumentException("Heap is empty");
            return elements[0];
        }
        public T RemoveMax()
        {
            if (elements.Count == 0) throw new ArgumentException("Heap is empty");

            T maxElement = elements[0];
            elements[0] = elements[elements.Count - 1];
            elements.RemoveAt(elements.Count - 1);
            Heapify(0);
            return maxElement;
        }
        public void IncreaseKey(int index, T value)
        {
            if (index < 0 || index > elements.Count) throw new ArgumentException("Index out of range");

            if (value.CompareTo(elements[index]) < 0)
            {
                Swap(index, Parent(index));
                index = Parent(index);
            }
        }
        public void Add(T element)
        {
            elements.Add(element);
            int index = elements.Count - 1;

            while (index > 0 && elements[Parent(index)].CompareTo(element) < 0)
            {
                Swap(index, Parent(index));
                index = Parent(index);
            }
        }
        public Heap<T> Merge(Heap<T> newHeap)
        {
            var mergedElements = new List<T>(elements);
            mergedElements.AddRange(newHeap.elements);
            return new Heap<T>(mergedElements.ToArray());
        }

    }

    public class Program
    {
        public static void Main(string[] args)
        {
            List<int> array = new List<int> { 10, 20, 5, 6, 1 };
            Heap<int> heap = new Heap<int>(array);
            Console.WriteLine($"Maximum of the heap is {heap.Max()}");

            Console.Write("Enter a number: ");
            int x = Convert.ToInt32(Console.ReadLine());
            heap.Add(x);
            Console.WriteLine($"Maximum of the heap after adding {x}: {heap.Max()}");

            x = heap.RemoveMax();
            Console.WriteLine($"Removed element is {x}");
            Console.WriteLine($"Current maximum element is {heap.Max()}");

        }
    }
}