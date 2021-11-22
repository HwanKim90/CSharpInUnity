using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GenericDelegate
{
    delegate int Compare<T>(T a, T b);

    class MainApp
    {
        public static int AscendCompare<T>(T a, T b) where T : IComparable<T>
        {
            return a.CompareTo(b);
        }

        public static int DescendCompare<T>(T a, T b) where T : IComparable<T>
        {
            return a.CompareTo(b) * -1;
        }

        public static void BubbleSort<T>(T[] DataSet, Compare<T> Comparer)
        {
            int i = 0;
            int j = 0;
            T temp;

            for (i = 0; i < DataSet.Length - 1; i++)
            {
                for (j = 0; j < DataSet.Length - (i + 1); j++)
                {
                    if (Comparer(DataSet[j], DataSet[j + 1]) > 0)
                    {
                        temp = DataSet[j + 1];
                        DataSet[j + 1] = DataSet[j];
                        DataSet[j] = temp;
                    }
                }
            }
        }
    }

    public class GenericDelegate : MonoBehaviour
    {   
        void Start()
        {
            int[] array = { 3, 7, 4, 2, 10 };

            Debug.Log("Sorting ascending...");
            MainApp.BubbleSort<int>(array, new Compare<int>(MainApp.AscendCompare));
            
            for (int i = 0; i < array.Length; i++)
            {
                Debug.Log($"{array[i]}");
            }

            string[] array2 = { "abc", "def", "ghi", "jkl", "mmo" };

            Debug.Log("Sorting Descending...");
            MainApp.BubbleSort<string>(array2, new Compare<string>(MainApp.DescendCompare));

            for (int i = 0; i < array2.Length; i++)
            {
                Debug.Log($"{array2[i]}");
            }
        }
    }

}

