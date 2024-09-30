using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortsLib
{
    public static class Sorts
    {
        public static void Swap(ref int value1, ref int value2)
        {
            var temp = value1;
            value1 = value2;
            value2 = temp;
        }
        public static int GetNextStep(int s)
        {
            s = s * 1000 / 1247;
            return s > 1 ? s : 1;
        }
        public static int[] GetRandomArray(int length, int minValue, int maxValue)
        {
            var r = new Random();
            var outputArray = new int[length];
            for (var i = 0; i < outputArray.Length; i++)
            {
                outputArray[i] = r.Next(minValue, maxValue);
            }

            return outputArray;
        }
        public static int MinIndex(int[] array, int n)
        {
            int result = n;
            for (var i = n; i < array.Length; ++i)
            {
                if (array[i] < array[result])
                {
                    result = i;
                }
            }

            return result;
        }
        public static int MaxIndex(int[] array, int n)
        {
            int result = n;
            for (var i = n; i < array.Length; ++i)
            {
                if (array[i] > array[result])
                {
                    result = i;
                }
            }

            return result;
        }
        public static int MaxValue(int[] array)
        {
            int max = array[0];
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] > max) max = array[i];
            }
            return max;
        }

        public static int[] BubbleSort(int[] array, bool reversed = false)
        {
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length; j++)
                {
                    if (reversed)
                    {
                        if (array[i] > array[j])
                        {
                            int temp = array[i];
                            array[i] = array[j];
                            array[j] = temp;
                        }
                        continue;
                    }
                    if (array[i] < array[j])
                    {
                        int temp = array[i];
                        array[i] = array[j];
                        array[j] = temp;
                    }
                }
            }
            return array;
        }

        public static int[] ShakerSort(int[] array, bool reversed = false)
        {
            int left = 0;
            int right = array.Length;
            bool swapped = true;
            while (swapped)
            {
                swapped = false;
                for (int i = left; i < right - 1; i++)
                {
                    if ((!reversed && array[i] > array[i + 1]) || (reversed && array[i] < array[i + 1]))
                    {
                        int tmp = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = tmp;
                        swapped = true;
                    }
                }
                if (!swapped) break;
                right--;

                for (int i = right - 1; i > left - 1; i--)
                {
                    if ((!reversed && array[i] > array[i + 1]) || (reversed && array[i] < array[i + 1]))
                    {
                        int tmp = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = tmp;
                        swapped = true;
                    }
                }
                left++;
            }
            return array;
        }

        public static int[] GnomeSort(int[] array, bool reversed = false)
        {
            int left = 0;
            int right = array.Length-1;
            while (left <= right)
            {
                if ((!reversed && (left == 0 || array[left-1] <= array[left])) || (reversed && (left == 0 || array[left-1] >= array[left]))) left++;
                else
                {
                    int tmp = array[left];
                    array[left] = array[left + 1];
                    array[left + 1] = array[left];
                    left--;
                }
            }
            return array;
        }

        public static int[] InsertionSort(int[] array, bool reversed = false)
        {
            for (int i = 0; i < array.Length; i++)
            {
                int x; int m;
                for (int j = 1; j < array.Length; j++)
                {
                    x = array[i];
                    m = i;
                    while (m > 0 && ((!reversed && array[m - 1] > x) || (reversed && array[m - 1] < x)))
                    {
                        m--;
                        int tmp = array[m];
                        array[m] = array[m + 1];
                        array[m + 1] = tmp;
                    }
                }
            }
            return array;
        }

        public static int[] CombSort(int[] array, bool reversed = false)
        {
            int arrayLength = array.Length;
            int currentStep = arrayLength - 1;

            while (currentStep > 1)
            {
                for (int i = 0; i + currentStep < array.Length; i++)
                {
                    if (!reversed && array[i] > array[i + currentStep] || reversed && array[i] < array[i + currentStep])
                    {
                        Swap(ref array[i], ref array[i + currentStep]);
                    }
                }

                currentStep = GetNextStep(currentStep);
            }

            for (int i = 1; i < arrayLength; i++)
            {
                bool swapFlag = false;
                for (int j = 0; j < arrayLength - i; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        Swap(ref array[j], ref array[j + 1]);
                        swapFlag = true;
                    }
                }

                if (!swapFlag)
                {
                    break;
                }
            }

            return array;
        }

        public static int[] ShellSort(int[] array, bool reversed = false)
        {
            var d = array.Length / 2;
            while (d >= 1)
            {
                for (var i = d; i < array.Length; i++)
                {
                    var j = i;
                    while ((j >= d) && (!reversed && array[j - d] > array[j] || reversed && array[j - d] < array[j]))
                    {
                        Swap(ref array[j], ref array[j - d]);
                        j = j - d;
                    }
                }

                d = d / 2;
            }

            return array;
        }

        //public static int[] SelectionSort(int[] array, int currentIndex = 0, bool reversed = false)
        //{
        //    if (currentIndex == array.Length)
        //        return array;
        //    int index;
        //    if (!reversed) index = MinIndex(array, currentIndex);
        //    else index = MaxIndex(array, currentIndex);
        //    if (index != currentIndex)
        //    {
        //        Swap(ref array[index], ref array[currentIndex]);
        //    }

        //    return SelectionSort(array, currentIndex + 1, reversed);
        //}
        public static void Selection(int[] array, int currentIndex = 0, bool reversed = false)
        {
            if (currentIndex == array.Length)
                return;
            int index;
            if (!reversed) index = MinIndex(array, currentIndex);
            else index = MaxIndex(array, currentIndex);
            if (index != currentIndex)
            {
                Swap(ref array[index], ref array[currentIndex]);
            }
        }
        public static int[] SelectionSort(int[] array, bool reversed = false)
        {
            Selection(array);
            return array;
        }

        public static void Heapify(int[] array, int i, int n)
        {
            int largestIndex = i;
            int left = 2 * i + 1;
            int right = 2 * i + 2;

            if (left < n && array[left] > array[largestIndex]) largestIndex = left;
            if (right < n && array[right] > array[largestIndex]) largestIndex = right;

            if (largestIndex != i)
            {
                int temp = array[largestIndex];
                array[largestIndex] = array[i];
                array[i] = temp;

                Heapify(array, largestIndex, n);
            }
        }
        public static int[] HeapSort(int[] array, bool reversed = false)
        {
            int n = array.Length;

            for (int i = n / 2 - 1; i >= 0; i--) Heapify(array, i, n);

            for (int i = n - 1; i > 0; i--)
            {
                int temp = array[i];
                array[i] = array[0];
                array[0] = temp;

                Heapify(array, 0, i);
            }

            if (reversed)
            {
                for (int i = 0; i < n / 2; i++)
                {
                    int temp = (int)array[i];
                    array[i] = array[n - i - 1];
                    array[n - i - 1] = temp;
                }
            }
            return array;
        }

        public static void Quick(int[] array, int firstIndex = 0, int lastIndex = -1, bool reversed = false)
        {
            if (lastIndex < 0)
                lastIndex = array.Length - 1;
            if (firstIndex >= lastIndex)
                return;
            int middleIndex = (lastIndex - firstIndex) / 2 + firstIndex, currentIndex = firstIndex;
            Swap(ref array[firstIndex], ref array[middleIndex]);
            for (int i = firstIndex + 1; i <= lastIndex; ++i)
            {
                if (!reversed && array[i] <= array[firstIndex] || reversed && array[i] >= array[firstIndex])
                {
                    Swap(ref array[++currentIndex], ref array[i]);
                }
            }
            Swap(ref array[firstIndex], ref array[currentIndex]);
            Quick(array, firstIndex, currentIndex - 1);
            Quick(array, currentIndex + 1, lastIndex);
        }
        public static int[] QuickSort(int[] array, bool reversed = false)
        {
            QuickSort(array, reversed);
            return array;
        }

        public static void Merge(int[] array, int lowIndex, int midIndex, int highIndex)
        {
            var left = lowIndex;
            var right = midIndex + 1;
            var tempArray = new int[highIndex - lowIndex + 1];
            var index = 0;

            while ((left <= midIndex) && (right <= highIndex))
            {
                if (array[left] < array[right])
                {
                    tempArray[index] = array[left];
                    left++;
                }
                else
                {
                    tempArray[index] = array[right];
                    right++;
                }

                index++;
            }

            for (var i = left; i <= midIndex; i++)
            {
                tempArray[index] = array[i];
                index++;
            }

            for (var i = right; i <= highIndex; i++)
            {
                tempArray[index] = array[i];
                index++;
            }

            for (var i = 0; i < tempArray.Length; i++) array[lowIndex + i] = tempArray[i];
        }
        public static int[] MergeSort(int[] array, int lowIndex, int highIndex)
        {
            if (lowIndex < highIndex)
            {
                var middleIndex = (lowIndex + highIndex) / 2;
                MergeSort(array, lowIndex, middleIndex);
                MergeSort(array, middleIndex + 1, highIndex);
                Merge(array, lowIndex, middleIndex, highIndex);
            }
            return array;
        }
        public static int[] MergeSort(int[] array, bool reversed = false) //костыль
        {
            return MergeSort(array, 0, array.Length - 1);
        }

        public static int[] CountingSort(int[] array, bool reversed = false) //костыль
        {
            var min = array[0];
            var max = array[0];
            foreach (int element in array)
            {
                if (element > max)
                {
                    max = element;
                }
                else if (element < min)
                {
                    min = element;
                }
            }
            var correctionFactor = min != 0 ? -min : 0;
            max += correctionFactor;
            var count = new int[max + 1];
            for (var i = 0; i < array.Length; i++)
            {
                count[array[i] + correctionFactor]++;
            }
            var index = 0;
            for (var i = 0; i < count.Length; i++)
            {
                for (var j = 0; j < count[i]; j++)
                {
                    array[index] = i - correctionFactor;
                    index++;
                }
            }
            return array;
        }

        static void Radix(int[] array, int exp)
        {
            int loop = 0;
            int length = array.Length;
            int[] output = new int[length];
            int[] count = new int[10];
            for (loop = 0; loop < length; loop++) count[(array[loop] / exp) % 10]++;
            for (loop = 1; loop < 10; loop++) count[loop] += count[loop - 1];
            for (loop = length - 1; loop >= 0; loop--)
            {
                output[count[(array[loop] / exp) % 10] - 1] = array[loop];
                count[(array[loop] / exp) % 10]--;
            }
            for (loop = 0; loop < length; loop++) array[loop] = output[loop];
        }
        public static int[] RadixSort(int[] array, bool reversed = false)//костыль
        {
            int exp = 1;
            int max = MaxValue(array);
            int cond = 0;
            while (true)
            {
                cond = max / exp;
                if (cond <= 0) break;
                Radix(array, exp);
                exp = exp * 10;
            }
            return array;
        }
      
        static void BitonicSequenceSort(int[] array, int low, int count, bool reversed)
        {
            if (count > 1)
            {
                int k = count / 2;
                for (int i = low; i < low + k; i++)
                {
                    if (reversed ? array[i] > array[i + k] : array[i] < array[i + k])
                    {
                        int temp = array[i];
                        array[i] = array[i + k];
                        array[i + k] = temp;
                    }
                    int tmp = array[i];
                    array[i] = array[i + k];
                    array[i + k] = tmp;
                }
                BitonicSequenceSort(array, low, k, reversed);
                BitonicSequenceSort(array, low + k, k, reversed);
            }
        }
        static void BitonicSequenceCreate(int[] array, int low, int count, bool reversed)
        {
            if (count > 1)
            {
                int k = count / 2;
                BitonicSequenceCreate(array, low, k, true);
                BitonicSequenceCreate(array, low + k, k, false);
                BitonicSequenceSort(array, low, count, reversed);
            }
        }
        public static int[] BitonicSort(int[] array, bool reversed = false)
        {
            BitonicSequenceCreate(array, 0, array.Length, reversed);
            return array;
        }

        public class TreeNode
        {
            public TreeNode(int data)
            {
                Data = data;
            }
            public int Data { get; set; }
            public TreeNode Left { get; set; }
            public TreeNode Right { get; set; }
            public void Insert(TreeNode node)
            {
                if (node.Data < Data)
                {
                    if (Left == null) Left = node;
                    else Left.Insert(node);
                }
                else
                {
                    if (Right == null) Right = node;
                    else Right.Insert(node);
                }
            }
            public int[] Transform(List<int> elements = null)
            {
                if (elements == null) elements = new List<int>();
                if (Left != null) Left.Transform(elements);
                elements.Add(Data);

                if (Right != null) Right.Transform(elements);
                return elements.ToArray();
            }
            public static int[] Sort(int[] array, bool reversed = false)
            {
                TreeNode root = new TreeNode(array[0]);
                for (int i = 1; i < array.Length; i++) root.Insert(new TreeNode(array[i]));
                int[] newArray = root.Transform();
                if (!reversed) for (int i = 0; i < array.Length; i++) array[i] = newArray[i];
                else for (int i = array.Length - 1; i>= 0; i--) array[i] = newArray[i];
                return array;
            }
        }
    }
}
