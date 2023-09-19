using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IT481_Unit7
{
    class Program
    {
        private static Stopwatch stopWatch;

        private static bool debug = false;
        public static void Main(string[] args)
        {
            //Integer for each type
            //1-Bubble Sort
            //2-QuickSort

            int type = 1;

            //Bubble Sort//

            //Small Array Sort

            int[] smallArray = getArray(10, 100);

            int[] newSmallArray = new int[smallArray.Length];
            Array.Copy(smallArray, 0, newSmallArray, 0, newSmallArray.Length);

            int[] quickSmallArray = new int[newSmallArray.Length];
            Array.Copy(newSmallArray, 0, quickSmallArray, 0, quickSmallArray.Length);

            String size = "small";
            runSortArray(smallArray, size, type);

            //Medium Array Sort

            int[] mediumArray = getArray(1000, 10000);

            int[] newMediumArray = new int[mediumArray.Length];
            Array.Copy(mediumArray, 0, newMediumArray, 0, newMediumArray.Length);

            int[] quickMediumArray = new int[newMediumArray.Length];
            Array.Copy(newMediumArray, 0, quickMediumArray, 0, quickMediumArray.Length);

            size = "medium";
            runSortArray(mediumArray, size, type);

            //Large Array Sort

            int[] largeArray = getArray(10000, 1000000);

            int[] newLargeArray = new int[largeArray.Length];
            Array.Copy(largeArray, 0, newLargeArray, 0, newLargeArray.Length);

            int[] quickLargeArray = new int[newLargeArray.Length];
            Array.Copy(newLargeArray, 0, quickLargeArray, 0, quickLargeArray.Length);

            size = "large";
            runSortArray(largeArray, size, type);

            //New Small Array

            newSmallArray = onlyUniqueElements(newSmallArray);
            size = "new small unique";
            runSortArray(newSmallArray, size, type);

            //New Medium Array

            newMediumArray = onlyUniqueElements(newMediumArray);
            size = "new medium unique";
            runSortArray(newMediumArray, size, type);

            //New Large Array

            newLargeArray = onlyUniqueElements(newLargeArray);
            size = "new large unique";
            runSortArray(newLargeArray, size, type);

            //Quicksort//

            type = 2;

            size = "quick small";

            runSortArray(quickSmallArray, size, type);

            size = "quick medium";

            runSortArray(quickLargeArray, size, type);

            int[] arr = { 44, 88, 77, 22, 66, 11, 99, 00, 33 };
            int low = 0;
            int high = arr.Length - 1;
            quickSortAsc(arr, low, high);

            Console.WriteLine("Array after quick sort");

            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine(arr[i] + " ");
            }

            Console.Read();
        }

        private static int[] getArray(int size, int randomMazSize)
        {
            int[] myArray = new int[size];

            for (int i = 0; i < myArray.Length; i++)
            {
                myArray[i] = GetRandomNumber(1, randomMazSize);
            }

            return myArray;
        }

        private static void runSortArray(int[] arr, String size, int type)
        {
            long elapsedTime = 0;

            String sort = null;

            if (type == 1)
            {
                sort = "bubble";
            }
            else if (type == 2)
            {
                sort = "quick";
            }

            if (debug)
            {
                Console.WriteLine("Array before the " + sort + " sort");
                for (int i = 0; i < arr.Length; i++)
                {
                    Console.Write(arr[i] + " ");
                }
            }

            stopWatch = Stopwatch.StartNew();

            if (type == 1)
            {
                bubbleSort(arr);
            }
            else if (type == 2)
            {
                int low = 0;
                int high = arr.Length - 1;
                quickSortAsc(arr, low, high);
            }
            Console.WriteLine();

            if (debug)
            {
                Console.WriteLine("Array after the " + sort + " sort");

                for (int i = 0; i < arr.Length; i++)
                {
                    Console.Write(arr[i] + " ");
                }
            }

            stopWatch.Stop();

            elapsedTime = stopWatch.ElapsedTicks;

            long frequency = Stopwatch.Frequency;
            long nanosecondsPerTick = (1000L * 1000L * 1000L) / frequency;
            elapsedTime = elapsedTime * nanosecondsPerTick;

            Console.WriteLine("\n");

            Console.WriteLine("The run time is for the " + size + " array in nanoseconds is " + elapsedTime);
            Console.WriteLine("\n\n");
        }

        private static void bubbleSort(int[] intArray)
        {
            int temp = 0;

            for (int i = 0; i < intArray.Length; i++)
            {
                for (int j = 0; j < intArray.Length - 1; j++)
                {
                    if (intArray[j] > intArray[j + 1])
                    {
                        temp = intArray[j + 1];
                        intArray[j + 1] = intArray[j];
                        intArray[j] = temp;
                    }
                }
            }
        }

        private static int[] onlyUniqueElements(int[] inputArray)
        {
            HashSet<int> set = new HashSet<int>();

            int[] tmp = new int[inputArray.Length];
            int index = 0; 

            foreach (int i in inputArray)
                if (set.Add(i))
                    tmp[index++] = i;

            return set.ToArray();
        }

        private static void quickSortAsc(int[] x, int low, int high)
        {
            if (low < high)
            {
                int pr = partition(x, low, high);
                quickSortAsc(x, low, pr - 1);
                quickSortAsc(x, pr+1, high);
            }
        }

        private static int partition(int[] arr, int low, int high)
        {
            int p = low;
            int i = low;
            int temp = 0;

            for (int j = low + 1; j <= high; j++)
            {
                if (arr[j] <= arr[p])
                {
                    i++;

                    temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }

            temp = arr[i];
            arr[i] = arr[p];
            arr[p] = temp;
            return i;
        }

        private static readonly Random getrandom = new Random();

            public static int GetRandomNumber(int min,  int max)
            {
                lock (getrandom)
                {
                    return getrandom.Next(min, max);
                }
            }
    }
}
