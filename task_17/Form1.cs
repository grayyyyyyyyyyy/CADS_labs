using ArrayList;
using LinkedList;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ZedGraph;

namespace task_17
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            GraphPane pane = zedGraph.GraphPane;
            pane.XAxis.Title.Text = "Size";
            pane.YAxis.Title.Text = "Duration of execution:";
            pane.Title.Text = "Data structure efficieny:";
        }

        int arrayInd = -1;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            arrayInd = comboBox1.SelectedIndex;
        }

        int count;
        private void button1_Click(object sender, EventArgs e)
        {
            PointPairList array1 = new PointPairList();
            PointPairList array2 = new PointPairList();
            GraphPane pane = zedGraph.GraphPane;
            pane.XAxis.Title.Text = "Size";
            pane.YAxis.Title.Text = "Duration of execution:";
            pane.Title.Text = "Data structure efficieny:";
            switch (arrayInd)
            {
                case 0:
                    MyArrayList<int> array_1 = new MyArrayList<int>();
                    MyLinkedList<int> array_2 = new MyLinkedList<int>();
                    for (int size = 10; size <= 10000; size *= 10)
                    {
                        double sum1 = 0, sum2 = 0;
                        for (int i = 0; i < 20; i++)
                        {
                            for (int j = 0; j < size; j++)
                            {
                                array_1.Add(j);
                                array_2.Add(j);
                            }
                            Random random = new Random();
                            Stopwatch s1 = new Stopwatch();
                            Stopwatch s2 = new Stopwatch();
                            int ind = random.Next(0, size - 1);

                            s1.Start();
                            for (int j = 0; j < size; j++) array_1.Get(j);
                            s1.Stop();
                            sum1 += s1.ElapsedMilliseconds;

                            s2.Start();
                            for (int j = 0; j < size; j++) array_2.Get(j);
                            s2.Stop();
                            sum2 += s2.ElapsedMilliseconds;
                        }
                        double res1 = sum1 / 20;
                        double res2 = sum2 / 20;
                        array1.Add(size, res1);
                        array2.Add(size, res2);
                        array_1.Clear();
                        array_2.Clear();
                        if (res1 > res2) count = (int)res1;
                        else count = (int)res2;
                    }
                    break;

                case 1:
                    array_1 = new MyArrayList<int>();
                    array_2 = new MyLinkedList<int>();
                    for (int size = 10; size <= 10000; size *= 10)
                    {
                        double sum1 = 0, sum2 = 0;
                        for (int i = 0; i < 20; i++)
                        {
                            for (int j = 0; j < size; j++)
                            {
                                array_1.Add(j);
                                array_2.Add(j);
                            }
                            Random random = new Random();
                            Stopwatch sw1 = new Stopwatch();
                            Stopwatch sw2 = new Stopwatch();
                            sw1.Start();
                            for (int j = 0; j < size; j++)
                            {
                                int number = random.Next(0, 100000);
                                int ind = random.Next(0, array_1.Size() - 1);
                                array_1.Set(ind, number);
                            }
                            sw1.Stop();
                            sum1 += sw1.ElapsedMilliseconds;

                            sw2.Start();
                            for (int j = 0; j < size; j++)
                            {
                                int number = random.Next(0, 100000);
                                int ind = random.Next(0, array_2.Size() - 1);
                                array_2.Set(ind, number);
                            }
                            sw2.Stop();
                            sum2 += sw2.ElapsedMilliseconds;
                        }
                        double result1 = sum1 / 20;
                        double result2 = sum2 / 20;
                        array1.Add(size, result1);
                        array2.Add(size, result2);
                        array_1.Clear();
                        array_2.Clear();
                        if (result1 > result2) count = (int)result1;
                        else count = (int)result2;
                    }
                    break;

                case 2:
                    array_1 = new MyArrayList<int>(10);
                    array_2 = new MyLinkedList<int>();
                    for (int size = 10; size <= 10000; size *= 10)
                    {
                        double sum1 = 0, sum2 = 0;
                        for (int i = 0; i < 20; i++)
                        {
                            Stopwatch sw1 = new Stopwatch();
                            Stopwatch sw2 = new Stopwatch();
                            sw1.Start();
                            for (int j = 0; j < size; j++)
                                array_1.Add(j);
                            sw1.Stop();
                            sum1 += sw1.ElapsedMilliseconds;

                            sw2.Start();
                            for (int j = 0; j < size; j++)
                                array_2.Add(j);
                            sw2.Stop();
                            sum2 += sw2.ElapsedMilliseconds;
                        }
                        double result1 = sum1 / 20;
                        double result2 = sum2 / 20;
                        array1.Add(size, result1);
                        array2.Add(size, result2);
                        array_1.Clear();
                        array_2.Clear();
                        if (result1 > result2) count = (int)result1;
                        else count = (int)result2;
                    }
                    break;
                case 3:
                    array_1 = new MyArrayList<int>(10);
                    array_2 = new MyLinkedList<int>();
                    for (int size = 10; size <= 10000; size *= 10)
                    {
                        double sum1 = 0, sum2 = 0;
                        for (int i = 0; i < 20; i++)
                        {
                            for (int j = 0; j < size; j++)
                            {
                                array_1.Add(j);
                                array_2.Add(j);
                            }
                            Random rand = new Random();
                            Stopwatch sw1 = new Stopwatch();
                            Stopwatch sw2 = new Stopwatch();
                            int tmp = rand.Next(0, size - 1);
                            sw1.Start();
                            for (int j = 0; j < size; j++)
                            {
                                int number = rand.Next(0, 100000);
                                int ind = rand.Next(0, array_1.Size() - 1);
                                array_1.Add(ind, number);
                            }
                            sw1.Stop();
                            sum1 += sw1.ElapsedMilliseconds;

                            sw2.Start();
                            for (int j = 0; j < size; j++)
                            {
                                int number = rand.Next(0, 100000);
                                int ind = rand.Next(0, array_2.Size() - 1);
                                array_2.Add(ind, number);
                            }
                            sw2.Stop();
                            sum2 += sw2.ElapsedMilliseconds;
                        }
                        double result1 = sum1 / 20;
                        double result2 = sum2 / 20;
                        array1.Add(size, result1);
                        array2.Add(size, result2);
                        array_1.Clear();
                        array_2.Clear();
                        if (result1 > result2) count = (int)result1;
                        else count = (int)result2;
                    }
                    break;
                case 4:
                    array_1 = new MyArrayList<int>(10);
                    array_2 = new MyLinkedList<int>();
                    for (int size = 10; size <= 10000; size *= 10)
                    {
                        double sum1 = 0, sum2 = 0;
                        for (int i = 0; i < 20; i++)
                        {
                            for (int j = 0; j < size; j++)
                            {
                                array_1.Add(j);
                                array_2.Add(j);
                            }
                            Random rand = new Random();
                            Stopwatch sw1 = new Stopwatch();
                            Stopwatch sw2 = new Stopwatch();
                            int tmp = rand.Next(0, size - 1);
                            sw1.Start();
                            for (int j = 0; j < size; j++)
                            {
                                int ind = rand.Next(0, array_1.Size() - 1);
                                array_1.Remove(ind);
                            }
                            sw1.Stop();
                            sum1 += sw1.ElapsedMilliseconds;

                            sw2.Start();
                            for (int j = 0; j < size; j++)
                            {
                                int ind = rand.Next(0, array_2.Size() - 1);
                                array_2.Remove(ind);
                            }
                            sw2.Stop();
                            sum2 += sw2.ElapsedMilliseconds;
                        }
                        double result1 = sum1 / 20;
                        double result2 = sum2 / 20;
                        array1.Add(size, result1);
                        array2.Add(size, result2);
                        array_1.Clear();
                        array_2.Clear();
                        if (result1 > result2) count = (int)result1;
                        else count = (int)result2;
                    }
                    break;
            }
            pane.CurveList.Clear();
            pane.AddCurve("ArrayList", array1, Color.Green, SymbolType.Circle);
            pane.AddCurve("LinkedList", array2, Color.Purple, SymbolType.Square);
            zedGraph.AxisChange();
            zedGraph.Invalidate();
            pane.XAxis.Scale.Min = 10;
            pane.XAxis.Scale.Max = 10000;
            pane.YAxis.Scale.Min = -1;
            pane.YAxis.Scale.Max = (int)count+10;
            pane.XAxis.Title.Text = "Size";
            pane.YAxis.Title.Text = "Duration of execution:";
            pane.Title.Text = "Data structure efficieny:";

        }

    }
}

