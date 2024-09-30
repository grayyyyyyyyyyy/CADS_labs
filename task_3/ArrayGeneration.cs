using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArraysGeneration
{
    public static class ArrayGeneration
    {
        public static Random random = new Random();

        public static int[] ArrayType1(int n)
        {
            int[] array = new int[n];
            for (int i = 0; i < n; i++) array[i] = random.Next(0, 1000);
            return array;
        }

        public static int[] ArrayType2(int n)
        {
            int module = random.Next(1, n);
            int newLength = random.Next(2, n) % module;
            if (newLength < 2) newLength = 2;
            int[] array = new int[n];
            int countOfArray = 0;

            int i = 0;
            while (i < n)
            {
                int exp = random.Next(0, 1000);
                int elementBase = 0;
                countOfArray++;

                while (i < n && i < newLength * countOfArray)
                {
                    elementBase++;
                    array[i] = elementBase * exp;
                    i++;
                }
            }
            return array;
        }

        public static int[] ArrayType3(int n)
        {
            int[] array = new int[n];
            for (int i = 0; i < n; i++) array[i] = i;

            int countOfSwap = random.Next(0, n / 3);
            for (int i = 0; i < countOfSwap; i++)
            {
                int first = random.Next(0, array.Length - 1);
                int second = random.Next(0, array.Length - 1);
                int temp = array[first];
                array[first] = array[second];
                array[second] = temp;
            }
            return array;
        }

        public static int[] ArrayType4(int n)
        {
            int[] array = ArrayType3(n);
            int repeatIndex = random.Next(0, n - 1);
            int repeatCount = random.Next(0, n / 3);

            while (repeatCount > 0)
            {
                int randomIndex = random.Next(0, array.Length - 1);
                if (array[randomIndex] != array[repeatIndex])
                {
                    array[randomIndex] = array[repeatIndex];
                    repeatCount--;
                }

            }
            return array;
        }
    }
}
