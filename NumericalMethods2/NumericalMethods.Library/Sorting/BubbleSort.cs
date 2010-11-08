using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumericalMethods.Sorting
{
    public class BubbleSort
    {
        public BubbleSort(int[] a)
        {
            int x = a.Length;
            q_sort(a, 0, x - 1);
        }
        private void q_sort(int[] a, int left, int right)
        {
            int pivot, l_hold, r_hold;

            l_hold = left;
            r_hold = right;
            pivot = a[left];

            while (left < right)
            {
                while ((a[right] >= pivot) && (left < right))
                {
                    right--;
                }

                if (left != right)
                {
                    a[left] = a[right];
                    left++;
                }

                while ((a[left] <= pivot) && (left < right))
                {
                    left++;
                }

                if (left != right)
                {
                    a[right] = a[left];
                    right--;
                }
            }

            a[left] = pivot;
            pivot = left;
            left = l_hold;
            right = r_hold;

            if (left < pivot)
            {
                q_sort(a, left, pivot - 1);
            }

            if (right > pivot)
            {
                q_sort(a, pivot + 1, right);
            }
        }
    }

    public class mergeSort
    {
        // array of integers to hold values
        private int[] a = new int[100];
        private int[] b = new int[100];

        // number of elements in array
        private int x;

        // Merge Sort Algorithm
        public void sortArray()
        {
            m_sort(0, x - 1);
        }

        public void m_sort(int left, int right)
        {
            int mid;

            if (right > left)
            {
                mid = (right + left) / 2;
                m_sort(left, mid);
                m_sort(mid + 1, right);

                merge(left, mid + 1, right);
            }
        }

        public void merge(int left, int mid, int right)
        {
            int i, left_end, num_elements, tmp_pos;

            left_end = mid - 1;
            tmp_pos = left;
            num_elements = right - left + 1;

            while ((left <= left_end) && (mid <= right))
            {
                if (a[left] <= a[mid])
                {
                    b[tmp_pos] = a[left];
                    tmp_pos = tmp_pos + 1;
                    left = left + 1;
                }
                else
                {
                    b[tmp_pos] = a[mid];
                    tmp_pos = tmp_pos + 1;
                    mid = mid + 1;
                }
            }

            while (left <= left_end)
            {
                b[tmp_pos] = a[left];
                left = left + 1;
                tmp_pos = tmp_pos + 1;
            }

            while (mid <= right)
            {
                b[tmp_pos] = a[mid];
                mid = mid + 1;
                tmp_pos = tmp_pos + 1;
            }

            for (i = 0; i < num_elements; i++)
            {
                a[right] = b[right];
                right = right - 1;
            }
        }
    }


    public class HeapSort
    {
        /*
        public static void Main()
        {
            // Instantiate an instance of the class
            arraySort mySort = new arraySort();

            // Get the number of elements to store in the array
            Console.Write("Number of elements in the array (less than 100) : ");
            string s = Console.ReadLine();
            mySort.x = Int32.Parse(s);

            // Array header
            Console.WriteLine("");
            Console.WriteLine("-----------------------");
            Console.WriteLine(" Enter array elements  ");
            Console.WriteLine("-----------------------");

            // Get array elements
            for (int i = 0; i < mySort.x; i++)
            {
                Console.Write("<{0}> ", i + 1);
                string s1 = Console.ReadLine();
                mySort.a[i] = Int32.Parse(s1);
            }

            // Sort the array
            mySort.sortArray();

            // Output sorted array
            Console.WriteLine("");
            Console.WriteLine("-----------------------");
            Console.WriteLine(" Sorted array elements ");
            Console.WriteLine("-----------------------");

            for (int j = 0; j < mySort.x; j++)
            {
                Console.WriteLine(mySort.a[j]);
            }

            // Here to stop app from closing
            Console.WriteLine("\n\nPress Return to exit.");
            Console.Read();
        }
         **/
        // array of integers to hold values
        private int[] a = new int[100];

        // number of elements in array
        private int x;

        // Heap Sort Algorithm
        public void sortArray()
        {
            int i;
            int temp;

            for (i = (x / 2) - 1; i >= 0; i--)
            {
                siftDown(i, x);
            }

            for (i = x - 1; i >= 1; i--)
            {
                temp = a[0];
                a[0] = a[i];
                a[i] = temp;
                siftDown(0, i - 1);
            }
        }

        public void siftDown(int root, int bottom)
        {
            bool done = false;
            int maxChild;
            int temp;

            while ((root * 2 <= bottom) && (!done))
            {
                if (root * 2 == bottom)
                    maxChild = root * 2;
                else if (a[root * 2] > a[root * 2 + 1])
                    maxChild = root * 2;
                else
                    maxChild = root * 2 + 1;

                if (a[root] < a[maxChild])
                {
                    temp = a[root];
                    a[root] = a[maxChild];
                    a[maxChild] = temp;
                    root = maxChild;
                }
                else
                {
                    done = true;
                }
            }
        }

    }

    public static class SortAlgorithms
    {
        public static void BubbleSort(this int[] a)
        {
            for (int i = a.Length - 1; i > 0; i--)
            {
                for (int j = 0; j < i; j++)
                {
                    if (a[j] > a[j + 1])
                    {
                        int tmp = a[j];
                        a[j] = a[j + 1]; //{{1}}
                        a[j + 1] = tmp;
                    }
                }
            }
        }

        public static void BuubleSearch2(this int[] a)
        {
            int x = a.Length;
            int i;
            int j;
            int temp;

            for (i = (x - 1); i >= 0; i--)
            {
                for (j = 1; j <= i; j++)
                {
                    if (a[j - 1] > a[j])
                    {
                        temp = a[j - 1];
                        a[j - 1] = a[j];
                        a[j] = temp;
                    }
                }
            }
        }

        public static void SelectionSort(this int[] a)
        {
            for (int i = 0; i < a.Length - 1; i++)
            {
                int min = i;
                for (int j = i + 1; j < a.Length; j++)
                {
                    if (a[j] < a[min])
                    {
                        min = j;
                    }
                }
                if (i != min)
                {
                    int swap = a[i];
                    a[i] = a[min];
                    a[min] = swap;
                }
            }
        }

        public static void SelectionSort2(this int[] a)
        {
            int x = a.Length;
            int i, j;
            int min, temp;

            for (i = 0; i < x - 1; i++)
            {
                min = i;

                for (j = i + 1; j < x; j++)
                {
                    if (a[j] < a[min])
                    {
                        min = j;
                    }
                }

                temp = a[i];
                a[i] = a[min];
                a[min] = temp;
            }
        }

        public static void InsertionSort(this int[] m, int a, int b)
        {
            int t;
            int i, j;
            for (i = a; i < b; i++)
            {
                t = m[i];
                for (j = i - 1; j >= a && m[j] > t; j--)
                    m[j + 1] = m[j];
                m[j + 1] = t;
            }

        }

        public static void InsertionSort2(this int[] a)
        {
            int x = a.Length;

            int i;
            int j;
            int index;

            for (i = 1; i < x; i++)
            {
                index = a[i];
                j = i;

                while ((j > 0) && (a[j - 1] > index))
                {
                    a[j] = a[j - 1];
                    j = j - 1;
                }

                a[j] = index;
            }
        }
    

        // Shell Sort Algorithm
        public static void ShellSort(this int[] a)
        {
            int x = a.Length;
            int i, j, increment, temp;

            increment = 3;

            while (increment > 0)
            {
                for (i = 0; i < x; i++)
                {
                    j = i;
                    temp = a[i];

                    while ((j >= increment) && (a[j - increment] > temp))
                    {
                        a[j] = a[j - increment];
                        j = j - increment;
                    }

                    a[j] = temp;
                }

                if (increment / 2 != 0)
                {
                    increment = increment / 2;
                }
                else if (increment == 1)
                {
                    increment = 0;
                }
                else
                {
                    increment = 1;
                }
            }
        }

       

    }
}
