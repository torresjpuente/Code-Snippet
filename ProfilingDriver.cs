//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:			ProfilingResearch
//	File Name:		    ProfilingDriver.cs
//	Description:	    This program will utilize the capabilities of the profiler for research.  Methods taken from
//                          power point.
//	Course:			    CSCI 2210-001 - Data Structures
//	Author:			    Julian Torres, torresjp@etsu.edu, Department of Computing, East Tennessee State University
//	Created:			Saturday, March 23, 2019
//	Copyright:		    Julian Torres, 2019
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProfilingResearch
{
    /// <summary>
    /// Driver class to be used to gather data for profiler research
    /// </summary>
    public static class ProfilingDriver
    {
        private static int N = 100;
        private static Random Random = new Random();
        private static List<int> list = new List<int>(N);
        
        /// <summary>
        /// Starting point of the execution
        /// </summary>
        public static void Main(String[] args)
        {
            Fill(list);

            //list.Sort();
            //list.Reverse();

            //This for loop randomizes 10% of the sorted list
            //for (int i = 0; i <= 45; i += 5)
            //{
            //    list.RemoveAt(i);
            //    list.Insert(i, Random.Next(1001));
            //}//end for

            List<int>[] testLists = new List<int>[8];

            for (int i = 0; i < testLists.Length; i++)
                testLists[i] = new List<int>(list);
            
            SinkSort(list);
            SelectionSort(testLists[0], N);
            InsertionSort(testLists[1]);
            testLists[2] = MergeSort(testLists[2]);
            OriginalQuickSort(testLists[3]);
            QuickMedianOfThreeSort(testLists[4]);
            ShellSort(testLists[5]);
            testLists[6] = CountingSort(testLists[6]);
            Radix10LSDSort(testLists[7]);
        }//end Main(String[])

        #region Fill
        /// <summary>
        /// Fills the list with random numbers for testing different sorting algorithms
        /// </summary>
        /// <param name="list">the list that needs to be filled with integers</param>
        private static void Fill(List<int> list)
        {
            for (int i = 0; i < N; i++)
                list.Add(Random.Next(1001));    // allow a max of up to 10000 integer value in the list to be tested 
        }//end Fill(List<int>)
        #endregion

        #region SinkSort
        /// <summary>
        /// Sorts a list of integers by comparing two integers each time
        /// and then "sinks" the higher integer down the list until each
        /// integer is in correct order
        /// </summary>
        /// <param name="list">the list that needs to be sorted</param>
        private static void SinkSort(List<int> list)
        {
            Boolean sorted = false;
            int pass = 0;

            while (!sorted && (pass < N))
            {
                pass++;
                sorted = true;

                for (int i = 0; i < N - pass; i++)
                {
                    if (list[i] > list[i + 1])
                    {
                        Swap(list, i, i + 1);
                        sorted = false;
                    }//end if
                }//end for
            }//end while
        }//end SinkSort(List<int>)
        #endregion

        #region Swap
        /// <summary>
        /// Swaps two integers in list of integers with each other's place
        /// </summary>
        /// <param name="list">the list that contains both integers</param>
        /// <param name="n">first integer to be swapped</param>
        /// <param name="m">second integer to be swapped</param>
        private static void Swap(List<int> list, int n, int m)
        {
            int temp = list[n];
            list[n] = list[m];
            list[m] = temp;
        }//end Swap(List<int>, int, int)
        #endregion

        #region SelectionSort
        /// <summary>
        /// Sort goes through each integer in the list and compares it to
        /// each following integer and sees if it is smaller and stops there 
        /// and is placed if indeed smaller
        /// </summary>
        /// <param name="list">the list to be sorted</param>
        /// <param name="n">number of integers in the list</param>
        private static void SelectionSort(List<int> list, int n)
        {
            if (n <= 1)
                return;

            int max = Max(list, n);

            if (list[max] != list[n - 1])
                Swap(list, max, n - 1);

            SelectionSort(list, n -1);
        }//end SelectionSort(List<int>, int)
        #endregion

        #region Max
        /// <summary>
        /// Determines the largest integer in the list to return
        /// </summary>
        /// <param name="list">the list to search through</param>
        /// <param name="n">the number of integers in the list to traverse</param>
        /// <returns> the largest integer in th list</returns>
        private static int Max(List<int> list, int n)
        {
            int max = 0;

            for (int i = 0; i < n; i++)
            {
                if (list[max] < list[i])
                    max = i;
            }//end for

            return max;
        }//end Max(List<int>, int)
        #endregion

        #region InsertionSort
        /// <summary>
        /// Sort places integers in a list coming before each other in order
        /// </summary>
        /// <param name="list">the list needing to be sorted</param>
        private static void InsertionSort(List<int> list)
        {
            int temp, j;

            for (int i = 1; i < N; i++)
            {
                temp = list[i];

                for (j = i; j > 0 && temp < list[j - 1]; j--)
                {
                    list[j] = list[j - 1];                                            
                }//end for
                            
                list[j] = temp;           
            }//end for
        }//end InsertionSort(List<int>)
        #endregion

        #region MergeSort
        /// <summary>
        /// Divide-and-Conquer method of sorting that divides the list into subsets
        /// that are then merged after sorting
        /// </summary>
        /// <param name="list">the list that needs to be sorted</param>
        /// <returns>list of sorted integers</returns>
        private static List<int> MergeSort(List<int> list)
        {
            if (list.Count <= 1)
                return list;

            List<int> result = new List<int>();
            List<int> left = new List<int>();
            List<int> right = new List<int>();

            int middle = list.Count / 2;

            for (int i = 0; i < middle; i++)
                left.Add(list[i]);

            for (int i = middle; i < list.Count; i++)
                right.Add(list[i]);


            left = MergeSort(left);
            right = MergeSort(right);

            if (left[left.Count - 1] <= right[0])
                return append(left, right);

   
            result = merge(left, right);

            return result;
        }//end MergeSort(List<int>)
        #endregion

        #region merge
        /// <summary>
        /// Method that actually merges two subsets of a list into one
        /// </summary>
        /// <param name="left"> the lower portion of a subset</param>
        /// <param name="right"> the upper portion of a subset</param>
        /// <returns>a single subset or whole merged list</returns>
        private static List<int> merge(List<int> left, List<int> right)
        {
            List<int> result = new List<int>();

            while (left.Count > 0 && right.Count > 0)
            {
                if (left[0] < right[0])
                {
                    result.Add(left[0]);
                    left.RemoveAt(0);
                }//end if
                else
                {
                    result.Add(right[0]);
                    right.RemoveAt(0);
                }//end else
            }//end while

            while (left.Count > 0)
            {
                result.Add(left[0]);
                left.RemoveAt(0);
            }//end while

            while (right.Count > 0)
            {
                result.Add(right[0]);
                right.RemoveAt(0);
            }//end while

            return result;
        }//end merge(List<int>, List<int>)
        #endregion

        #region append
        /// <summary>
        /// Quicker way of merging right side of the list to the left if necessary
        /// </summary>
        /// <param name="left">the lower portion of a subset</param>
        /// <param name="right">the upper portion of a subset</param>
        /// <returns>combined list of upper and lower portion in one list</returns>
        private static List<int> append(List<int> left, List<int> right)
        {
            List<int> result = new List<int>(left);

            foreach (int x in right)
                result.Add(x);

            return result;
        }//end append(List<int>, List<int>)
        #endregion

        #region OriginalQuickSort
        /// <summary>
        /// Sorts list based on a pivot and sorts left and right sides of it to be in order
        /// </summary>
        /// <param name="list">the list to be sorted</param>
        private static void OriginalQuickSort(List<int> list)
        {
            QuickSort(list, 0, list.Count - 1);
        }//end OriginalQuickSort(List<int>)
        #endregion

        #region QuickSort
        /// <summary>
        /// Actual method that sorts the list based on the starting position and ending position
        /// </summary>
        /// <param name="list">the list that needs to be sorted</param>
        /// <param name="start">beginning of the list</param>
        /// <param name="end">end of the list</param>
        private static void QuickSort(List<int> list, int start, int end)
        {
            int left = start;
            int right = end;

            if (left >= right)
                return;

            while (left < right)
            {
                while (list[left] <= list[right] && left < right)
                    right--;

                if (left < right)
                    Swap(list, left, right);

                while (list[left] <= list[right] && left < right)
                    left++;

                if (left < right)
                    Swap(list, left, right);
            }//end while

            QuickSort(list, start, left - 1);
            QuickSort(list, right + 1, end);
        }//end QuickSort(List<int>, int ,int)
        #endregion

        #region QuickMedianOfThreeSort
        /// <summary>
        /// This sort generally performs better than the OriginalQuickSort
        /// as it reduces the chance of choosing a pivot that is at the end 
        /// or the beginning of the list
        /// </summary>
        /// <param name="list">the list that needs to be sorted</param>
        private static void QuickMedianOfThreeSort(List<int> list)
        {
            QuickMedOfThreeSort(list, 0, list.Count - 1);
        }//end QuickMedianOfThreeSort(List<int>)
        #endregion

        #region QuickMedOfThreeSort
        /// <summary>
        /// Actual method that sorts based on median pivot
        /// </summary>
        /// <param name="list">the list that needs to be sorted</param>
        /// <param name="start">the beginning of the list</param>
        /// <param name="end">the end of the list</param>
        private static void QuickMedOfThreeSort(List<int> list, int start, int end)
        {
            const int cutoff = 10;

            if (start + cutoff > end)
                InsertSort(list, start, end);
            else
            {
                int middle = (start + end) / 2;
                if (list[middle] < list[start])
                    Swap(list, start, middle);
                if (list[end] < list[start])
                    Swap(list, start, end);
                if (list[end] < list[middle])
                    Swap(list, middle, end);

                int pivot = list[middle];
                Swap(list, middle, end - 1);

                int left, right;

                for (left = start, right = end - 1; ; )
                {
                    while (list[++left] < pivot)
                        ;
                    while (pivot < list[--right])
                        ;
                    if (left < right)
                        Swap(list, left, right);
                    else
                        break;
                }//end for

                Swap(list, left, end - 1);

                QuickMedOfThreeSort(list, start, left - 1);
                QuickMedOfThreeSort(list, left + 1, end);
            }//end else
        }//end QuickMedOfThreeSort(List<int>, int, int)
        #endregion

        #region InsertSort         
        /// <summary>
        /// Same as the InsertionSort just uses a starting and ending integer from the list
        /// </summary>
        /// <param name="list">the list that needs to be sorted</param>
        /// <param name="start">the beginning of the list</param>
        /// <param name="end">the end of the list</param>
        private static void InsertSort(List<int> list, int start, int end)
        {
            int temp, j;
            for (int i = start + 1; i <= end; i++)
            {
                temp = list[i];

                for (j = i; j > start && temp < list[j - 1]; j--)
                {
                    list[j] = list[j - 1];
                }//end for

                list[j] = temp;
            }//end for
        }//end InsertSort(List<int>, int, int)
        #endregion

        #region ShellSort
        /// <summary>
        /// Sorts list almost like QuickSort by dividing the list into subsets
        /// subsets become larger, and uses Insertion to sort the list
        /// </summary>
        /// <param name="list">the list that needs to be sorted</param>
        private static void ShellSort(List<int> list)
        {
            for (int gap = N / 2; gap > 0; gap = (gap == 2 ? 1 : (int) (gap / 2.2)))
            {
                int temp, j;
                for (int i = gap; i < N; i++)
                {
                    temp = list[i];

                    for (j = i; j >= gap && temp < list[j - gap]; j -= gap)
                    {
                        list[j] = list[j - gap];
                    }//end for

                    list[j] = temp;
                }//end for
            }//end for
        }//end ShellSort(List<int>)
        #endregion

        #region CountingSort
        /// <summary>
        /// Sort counts each instance of the value in the list and
        /// creates a separate array to hold the count and based on the
        /// number of occurrences of each item sorts the list
        /// </summary>
        /// <param name="list">the list that needs to be sorted</param>
        /// <returns>sorted list of integers</returns>
        private static List<int> CountingSort(List<int> list)
        {
            List<int> newList = new List<int>(list);

            int max = list.Max();

            int[] counts = new int[max + 1];

            for (int i = 0; i <= max; i++)
                counts[i] = 0;

            for (int j = 0; j < list.Count; j++)
                counts[list[j]]++;

            for (int j = 1; j <= max; j++)
                counts[j] += counts[j - 1];

            for (int j = 0; j < newList.Count; j++)
            {
                newList[counts[list[j]] - 1] = list[j];
                counts[list[j]]--;
            }//end for

            return newList;
        }//end CountingSort(List<int>)
        #endregion

        #region Radix10LSDSort
        /// <summary>
        /// Perform a Radix (base 10) sort using the least significant
        /// digit approach 
        /// </summary>
        /// <param name="list">The list to be sorted</param>
        /// <returns>Sorted list</returns>
        private static List<int> Radix10LSDSort(List<int> list)
        {
            List<List<int>> bin = new List<List<int>>(10);

            for (int i = 0; i < 10; i++)
                bin.Add(new List<int>(N));

            int numDigits = list.Max().ToString().Length;

            for (int j = 0; j < numDigits; j++)
            {
                for (int n = 0; n < list.Count; n++)
                    bin[Digit(list[n], j)].Add(list[n]);


                CopyToResult(bin, list);

                for (int i = 0; i < 10; i++)
                {
                    bin[i].Clear();
                }//end for
            }//end for

            return list;
        }//end Radix10LSDSort(List<int>)
        #endregion

        #region CopyToResult
        /// <summary>
        /// Copies the values of each bin back into the list, one
        /// bin at a time from bin 0 to bin 9
        /// </summary>
        /// <param name="bin">the list of bins</param>
        /// <param name="result">the list of integers</param>
        private static void CopyToResult(List<List<int>> bin, List<int> result)
        {
            result.Clear();

            for (int i = 0; i < 10; i++)
                foreach (int j in bin[i])
                    result.Add(j);
        }//end CopyToResult(List<List<int>>, List<int>)
        #endregion

        #region Digit
        /// <summary>
        /// Get the digit of value currently in digitPosition where the 
        /// positions are numbered from right to left.   That is, digit
        /// position 0 is the rightmost digit, position 1 is the 10's digit,
        /// position 2 is the 100's digit, and so forth
        /// </summary>
        /// <param name="value">The value from which we want the digit in
        /// position digitPosition from the right</param>
        /// <param name="digitPosition">The position of the digit we want</param>
        /// <returns>The designated digit of value</returns>
        private static int Digit(int value, int digitPosition)
        {
            return (value / (int) Math.Pow(10, digitPosition) % 10);
        }//end Digit(int, int)
        #endregion
    }//end ProfilingDriver
}