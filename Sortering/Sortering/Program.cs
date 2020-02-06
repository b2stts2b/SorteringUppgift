using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sortering
{
    class Program
    {
        static Stopwatch sw = new Stopwatch();
        static int listStorlek = 10000;

        static List<int> originalLista = new List<int>();
        //Skapa Listor
        static List<int> talLista1 = new List<int>();
        static List<int> talLista2 = new List<int>();
        static List<int> talLista3 = new List<int>();
        static List<int> talLista4 = new List<int>();
        
        static void Main(string[] args)
        {
            Console.WriteLine("Antal element: " + listStorlek + "\n");

           
            //Generera originalLista
            GenereraLista(originalLista);

            //Kopiera Listan
            CopyList(talLista1, originalLista);
            CopyList(talLista2, originalLista);
            CopyList(talLista3, originalLista);
            CopyList(talLista4, originalLista);

            //Sortera Och Mät Tid
            
            //Bubble Sort
            /*TestaBubbleSort(talLista1);    
            //Selection Sort
            TestaSelectionSort(talLista2);
            //Merge Sort
            TestaMergeSort(talLista3);*/
            //Quick Sort
            TestaQuickSort(talLista4);
        }

        //Testar quick sort
        static void TestaQuickSort(List<int> lista)
        {
            sw.Reset();
            sw.Start();
            QuickSort(lista, 0, lista.Count - 1);
            sw.Stop();
           
            double tid = ConvertToMilli(sw.ElapsedTicks);
            Console.WriteLine("Quick Sort: " + tid.ToString("0.##") + " ms.");

        }

        //Säter pivot på rätt plats (quick sort)
        static int Partitioning(List<int> A, int l, int h)
        {
            int pivot = A[h];

            // index of smaller element 
            int i = (l - 1);
            for (int j = l; j < h; j++)
            {
                // If current element is smaller  
                // than the pivot 
                if (A[j] < pivot)
                {
                    i++;

                    // swap arr[i] and arr[j] 
                    int temp = A[i];
                    A[i] = A[j];
                    A[j] = temp;
                }
            }

            // swap arr[i+1] and arr[high] (or pivot) 
            int temp1 = A[i + 1];
            A[i + 1] = A[h];
            A[h] = temp1;

            return i + 1;
        }

        //Quick Sort
        static void QuickSort(List<int> A, int l, int h)
        {
            if (l < h)
            {

                int p = Partitioning(A, l, h);

                QuickSort(A, l, p - 1);
                QuickSort(A, p + 1, h);

            }
        }

        //Kopierar en lista 
        static void CopyList(List<int> lista, List<int> origLista)
        {
            foreach (int num in origLista)
            {
                lista.Add(num);
            }
        }

        //Merge Sort
        static void MergeSort(int l, int r)
        {
            if (l < r)
            {
                int mid = (l + r)/2;
                MergeSort(l, mid);
                MergeSort(mid + 1, r);
                Merge(l, mid, r);
            }
        }

        //Merge
        static void Merge(int l, int mid, int r)
        {
            int i = l;
            int j = mid+1;

            List<int> tempList = new List<int>();

            while (i <= mid && j <= r)
            {
                if (talLista3[i] < talLista3[j])
                {
                    tempList.Add(talLista3[i]);
                    i++;
                }
                else
                {
                    tempList.Add(talLista3[j]);
                    j++;
                }
            }
            while (i <= mid)
            {
                tempList.Add(talLista3[i]);
                i++;
            }
            while (j <= r)
            {
                tempList.Add(talLista3[j]);
                j++;
            }


            for (int s = l;  s <= r; s++)
            {
                talLista3[s] = tempList[s-l];
            }

        }

        //Ta tid och skriv ut för Merge Sort
        static void TestaMergeSort(List<int> lista)
        {
            sw.Reset();
            sw.Start();
            MergeSort(0,lista.Count-1);
            sw.Stop();

            double tid = ConvertToMilli(sw.ElapsedTicks);
            Console.WriteLine("Merge Sort: " + tid.ToString("0.##") + " ms.");

        }


        //Ta tid och skriv ut tid för selectionSort
        static void TestaSelectionSort(List<int> lista)
        {
            sw.Reset();
            sw.Start();
            SelectionSort(lista);
            sw.Stop();

            double tid = ConvertToMilli(sw.ElapsedTicks);
            Console.WriteLine("Selection Sort: " + tid.ToString("0.##") + " ms.");
        }

        //SelectionSort
        static void SelectionSort(List<int> lista)
        {
            int smallIndex = 0;
            for (int i = 0; i < lista.Count - 1; i++)
            {
                smallIndex = i;
                for (int j = i+1; j < lista.Count; j++)
                {
                    if (lista[j] < lista[i])
                    {
                        smallIndex = j;
                    }
                }
                int temp = lista[smallIndex];
                lista[smallIndex] = lista[i];
                lista[i] = temp;
            }
        }

        //Ta tid och skriv ut tid för bubbleSort
        static void TestaBubbleSort(List<int> lista)
        {
            sw.Reset();
            sw.Start();
            BubbleSort(lista);
            sw.Stop();

            double tid = ConvertToMilli(sw.ElapsedTicks);
            Console.WriteLine("Bubble Sort: " + tid.ToString("0.##") + " ms.");
        }

        //BubbleSort
        static void BubbleSort(List<int> lista)
        {
            //vilket är det index för sista elementet som inte är sorterat
            for (int i = lista.Count-1; i > 0; i--)
            {
                //index för det första elementet vi kollar
                for (int j = 0; j < i; j++)
                {
                    //Är elemtet större än nästa
                    if (lista[j] > lista[j + 1])
                    {
                        //Byt element
                        int temp = lista[j];
                        lista[j] = lista[j + 1];
                        lista[j + 1] = temp;
                    }
                }
            }
        }

        //Generera listor
        static void GenereraLista(List<int> lista)
        {
            Random rand = new Random();
            for (int i = 0; i < listStorlek; i++)
            {
                lista.Add(rand.Next(0, listStorlek));
            }
        }

        //Omvandla ticks till millisekunder
        static double ConvertToMilli(long ticks)
        {
            long freq = Stopwatch.Frequency;
            return ((double)ticks*1000/freq);
        }
    }

}
