using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SortsLib;
using ArraysGeneration;
using System.IO;
using System.Reflection;
using ZedGraph;
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace task_3
{
    public partial class Form1 : Form
    {
        string pathArray;
        string pathTime;
        public void SetPath()
        {
            string appDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            pathArray = appDirectory + @"\ArrayFile.txt";
            pathTime = appDirectory + @"\TimeFile.txt";
        }
        int flag = 0;
        public Form1()
        {   
            InitializeComponent();
            GraphPane pane = zedGraphControl1.GraphPane;
            pane.CurveList.Clear();
            pane.XAxis.Title.Text = "Array size (elements)";
            pane.YAxis.Title.Text = "Execution time (ms)";
            pane.Title.Text = "Dependence of execution time on array size";
        }
        public void TimeOfSorting(Func<int, int[]> Generate, int length, params Func<int[], bool, int[]>[] SortMethods)
        {
            SetPath();
            double[] sumSpeed = new double[SortMethods.Length];
            for (int i = 0; i < 20; i++)
            {
                int[] myArray = Generate(length);
                //try
               // {
                    StreamWriter sw = File.AppendText(pathArray);
                    sw.WriteLine("Unsorted array: ");
                    foreach (int item in myArray) sw.Write(item.ToString() + " ");

                    int index = 0;
                    int[] sortedArray = null;
                    foreach (Func<int[], bool, int[]> Method in SortMethods)
                    {
                        Stopwatch timer = new Stopwatch();
                        timer.Start();
                        sortedArray = Method(myArray, false);
                        timer.Stop();
                        sumSpeed[index] += timer.ElapsedMilliseconds;
                        index++;
                    }
                    sw.WriteLine("Sorted array: ");
                    foreach (int member in sortedArray) sw.Write(member.ToString() + " ");
                    sw.Write("\n");
                    sw.Close();
               // }
                //catch
                //{
                //    label3.Text = "Error: append array to the file. Please try again";
                //}
            }
            try
            {
                StreamWriter sw = File.AppendText(pathTime);
                sw.Write(((double)(sumSpeed[0] / 20)).ToString());
                for (int i = 1; i < SortMethods.Length; i++) sw.Write(" " + ((double)(sumSpeed[i] / 20)).ToString());
                sw.WriteLine();
                sw.Close();
            }
            catch
            {
                label3.Text = "Error: append time to the file, please try again";
            }
        }
        int arrayInd = -1;
        int algorithmInd = -1;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            arrayInd = comboBox1.SelectedIndex;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            algorithmInd = comboBox2.SelectedIndex;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.BackColor = Color.Red;
            zedGraphControl1.GraphPane.CurveList.Clear(); // Очистить кривые
            zedGraphControl1.GraphPane.GraphObjList.Clear(); // Очистить объекты графика (если есть)
            zedGraphControl1.AxisChange(); // Обновить оси
            zedGraphControl1.Invalidate();
            SetPath();
            File.WriteAllText(pathTime, string.Empty);
            File.WriteAllText(pathArray, string.Empty);


            switch (arrayInd)
            {
                case -1:
                    label3.Text = "Choose the test array group";
                    break;
                case 0:
                    switch (algorithmInd)
                    {
                        case -1:
                            label3.Text = "Choose the sorting group";
                            break;
                        case 0:
                            for (int length = 10; length <= Math.Pow(10, 4); length *= 10)
                                TimeOfSorting(ArrayGeneration.ArrayType1, length, Sorts.BubbleSort, Sorts.InsertionSort, Sorts.SelectionSort, Sorts.ShakerSort, Sorts.GnomeSort);
                            flag = 1;
                            break;
                        case 1:
                            for (int length = 10; length <= Math.Pow(10, 5); length *= 10)
                                TimeOfSorting(ArrayGeneration.ArrayType1, length, Sorts.BitonicSort, Sorts.ShellSort/*, Sorts.TreeNode.Sort*/);
                            flag = 2;
                            break;
                        case 2:
                            for (int length = 10; length <= Math.Pow(10, 6); length *= 10)
                                TimeOfSorting(ArrayGeneration.ArrayType1, length, Sorts.CombSort, /*Sorts.HeapSort, Sorts.QuickSort,*/ Sorts.MergeSort, Sorts.CountingSort, Sorts.RadixSort);
                            flag = 3;
                            break;
                    }
                    break;
                case 1:
                    switch (algorithmInd)
                    {
                        case -1:
                            label3.Text = "Choose the sorting group";
                            break;
                        case 0:
                            for (int length = 10; length <= Math.Pow(10, 4); length *= 10)
                                TimeOfSorting(ArrayGeneration.ArrayType2, length, Sorts.BubbleSort, Sorts.InsertionSort, Sorts.SelectionSort, Sorts.ShakerSort, Sorts.GnomeSort);
                            flag = 1;
                            break;
                        case 1:
                            for (int length = 10; length <= Math.Pow(10, 5); length *= 10)
                                TimeOfSorting(ArrayGeneration.ArrayType2, length, Sorts.BitonicSort, Sorts.ShellSort/*, Sorts.TreeNode.Sort*/);
                            flag = 2;
                            break;
                        case 2:
                            for (int length = 10; length <= Math.Pow(10, 6); length *= 10)
                                TimeOfSorting(ArrayGeneration.ArrayType2, length, Sorts.CombSort,/* Sorts.HeapSort, Sorts.QuickSort,*/ Sorts.MergeSort, Sorts.CountingSort, Sorts.RadixSort);
                            flag = 3;
                            break;
                    }
                    break;
                case 2:
                    switch (algorithmInd)
                    {
                        case -1:
                            label3.Text = "Choose the sorting group";
                            break;
                        case 0:
                            for (int length = 10; length <= Math.Pow(10, 4); length *= 10)
                                TimeOfSorting(ArrayGeneration.ArrayType3, length, Sorts.BubbleSort, Sorts.InsertionSort, Sorts.SelectionSort, Sorts.ShakerSort, Sorts.GnomeSort);
                            flag = 1;
                            break;
                        case 1:
                            for (int length = 10; length <= Math.Pow(10, 5); length *= 10)
                                TimeOfSorting(ArrayGeneration.ArrayType3, length, Sorts.BitonicSort, Sorts.ShellSort/*, Sorts.TreeNode.Sort*/);
                            flag = 2;
                            break;
                        case 2:
                            for (int length = 10; length <= Math.Pow(10, 6); length *= 10)
                                TimeOfSorting(ArrayGeneration.ArrayType3, length, Sorts.CombSort, /*Sorts.HeapSort, Sorts.QuickSort,*/ Sorts.MergeSort, Sorts.CountingSort, Sorts.RadixSort);
                            flag = 3;
                            break;
                    }
                    break;
                case 3:
                    switch (algorithmInd)
                    {
                        case -1:
                            label3.Text = "Choose the sorting group";
                            break;
                        case 0:
                            for (int length = 10; length <= Math.Pow(10, 4); length *= 10)
                                TimeOfSorting(ArrayGeneration.ArrayType4, length, Sorts.BubbleSort, Sorts.InsertionSort, Sorts.SelectionSort, Sorts.ShakerSort, Sorts.GnomeSort);
                            flag = 1;
                            break;
                        case 1:
                            for (int length = 10; length <= Math.Pow(10, 5); length *= 10)
                                TimeOfSorting(ArrayGeneration.ArrayType4, length, Sorts.BitonicSort, Sorts.ShellSort/*, Sorts.TreeNode.Sort)*/);
                            flag = 2;
                            break;
                        case 2:
                            for (int length = 10; length <= Math.Pow(10, 6); length *= 10)
                                TimeOfSorting(ArrayGeneration.ArrayType4, length, Sorts.CombSort, /*Sorts.HeapSort, Sorts.QuickSort,*/ Sorts.MergeSort, Sorts.CountingSort, Sorts.RadixSort);
                            flag = 3;
                            break;
                    }
                    break;
            }
            pictureBox1.BackColor = Color.Green;
        }

       

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SetPath();
            List<List<double>> time = new List<List<double>>();
            try
            {
                StreamReader sr = new StreamReader(pathTime);
                string line = sr.ReadLine();

                while (line != null)
                {
                    List<double> speed = new List<double>();
                    string[] lineArray = line.Split(' ');
                    foreach (string str in lineArray) speed.Add(Convert.ToDouble(str));
                    time.Add(speed);
                    line = sr.ReadLine();
                }
                sr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            GraphPane pane = zedGraphControl1.GraphPane;
            pane.CurveList.Clear();
            pane.XAxis.Title.Text = "Array size (elements)";
            pane.YAxis.Title.Text = "Execution time (ms)";
            pane.Title.Text = "Dependence of execution time on array size";
            for (int i = 0; i < time[0].Count(); i++)
            {
                PointPairList pointList = new PointPairList();
                int x = 10;

                for (int j = 0; j < time.Count(); j++)
                {

                    pointList.Add(x, time[j][i]);
                    x *= 10;
                }
                switch (i)
                {
                    case 0:
                        if (flag == 1)
                        {
                            pane.XAxis.Scale.Max = 10000;
                            pane.AddCurve("BubbleSort: ", pointList, Color.Black, SymbolType.Default);
                        }
                        if (flag == 2)
                        {
                            pane.XAxis.Scale.Max = 100000;
                            pane.AddCurve("BitonicSort: ", pointList, Color.Black, SymbolType.Default);
                        }
                        if (flag == 3)
                        {
                            pane.XAxis.Scale.Max = 1000000;
                            pane.AddCurve("CombSort: ", pointList, Color.Black, SymbolType.Default);
                        }
                        break;
                    case 1:
                        if (flag == 1) pane.AddCurve("InsertionSort: ", pointList, Color.Green, SymbolType.Default);
                        if (flag == 2) pane.AddCurve("ShellSort: ", pointList, Color.Green, SymbolType.Default);
                        if (flag == 3) pane.AddCurve("MergeSort: ", pointList, Color.Green, SymbolType.Default);
                        break;
                    case 2:
                        if (flag == 1) pane.AddCurve("SelectionSort: ", pointList, Color.Blue, SymbolType.Default);
                        //if (flag == 2) pane.AddCurve("TreeSort: ", pointList, Color.Blue, SymbolType.Default);
                        if (flag == 3) pane.AddCurve("CountingSort: ", pointList, Color.Blue, SymbolType.Default);
                        break;
                    case 3:
                        if (flag == 1) pane.AddCurve("ShakerSort: ", pointList, Color.Pink, SymbolType.Default);
                        if (flag == 3) pane.AddCurve("RadixSort: ", pointList, Color.Pink, SymbolType.Default);
                        break;
                    case 4:
                        if (flag == 1) pane.AddCurve("GnomeSort: ", pointList, Color.Purple, SymbolType.Default);
                        //if (flag == 3) pane.AddCurve("CountingSort: ", pointList, Color.Purple, SymbolType.Default);
                        break;
                       // case 5:
                          //  if (flag == 3) pane.AddCurve("RadixSort: ", pointList, Color.SandyBrown, SymbolType.Default);
                          // break;
                }
                zedGraphControl1.AxisChange();
                zedGraphControl1.Invalidate();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SetPath();
            File.WriteAllText(pathTime, string.Empty);
            File.WriteAllText(pathArray, string.Empty);
            pictureBox1.BackColor = Color.Red;
        }

        
    }
}
