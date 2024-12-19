using HashMap;
using TreeMap;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ZedGraph;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

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
            Random random = new Random();
            switch (arrayInd)
            {
                case 0:
                    MyHashMap<int, int> hash = new MyHashMap<int, int>();
                    MyTreeMap<int, int> tree = new MyTreeMap<int, int>();
                    for (int size = 10; size <= 10000; size *= 10)
                    {
                        double sum1 = 0, sum2 = 0;
                        for (int i = 0; i < 20; i++)
                        {
                            for (int j = 0; j < size; j++)
                            {
                                int n = random.Next(1, size);
                                hash.Put(j, n);
                                tree.Put(j, n);
                            }
                            Stopwatch s1 = new Stopwatch();
                            Stopwatch s2 = new Stopwatch();
                            int tmp = random.Next(0, size - 1);

                            s1.Start();
                            for (int j = 0; j < size; j++) hash.Get(tmp);
                            s1.Stop();
                            sum1 += s1.ElapsedMilliseconds;

                            s2.Start();
                            for (int j = 0; j < size; j++) tree.Get(tmp);
                            s2.Stop();
                            sum2 += s2.ElapsedMilliseconds;
                        }
                        double res1 = sum1 / 20;
                        double res2 = sum2 / 20;
                        array1.Add(size, res1);
                        array2.Add(size, res2);
                        hash.Clear();
                        tree.Clear();
                        count = res1 > res2 ? (int)res1 : (int)res2;
 
                    }
                    break;

                case 1:
                    hash = new MyHashMap<int, int>();
                    tree = new MyTreeMap<int, int>();
                    for (int size = 10; size <= 10000; size *= 10)
                    {
                        double sum1 = 0, sum2 = 0;
                        for (int i = 0; i < 20; i++)
                        {
                            Stopwatch sw1 = new Stopwatch();
                            Stopwatch sw2 = new Stopwatch();
                            sw1.Start();
                            for (int j = 0; j < size; j++)
                            {
                                int n = random.Next(1, size);
                                hash.Put(j, n);
                            }
                            sw1.Stop();
                            sum1 += sw1.ElapsedMilliseconds;

                            sw2.Start();
                            for (int j = 0; j < size; j++)
                            {
                                int n = random.Next(1, size);
                                tree.Put(j, n);
                            }
                            sw2.Stop();
                            sum2 += sw2.ElapsedMilliseconds;
                        }
                        double res1 = sum1 / 20;
                        double res2 = sum2 / 20;
                        array1.Add(size, res1);
                        array2.Add(size, res2);
                        hash.Clear();
                        tree.Clear();
                        count = res1 > res2 ? (int)res1 : (int)res2;
                    }
                    break;

                case 2:
                    hash = new MyHashMap<int, int>();
                    tree = new MyTreeMap<int, int>();
                    for (int size = 10; size <= 10000; size *= 10)
                    {
                        double sum1 = 0, sum2 = 0;
                        for (int i = 0; i < 20; i++)
                        {
                            for (int j = 0; j < size; j++)
                            {
                                int n = random.Next(1, size);
                                hash.Put(j, n);
                                tree.Put(j, n);
                            }
                            Stopwatch sw1 = new Stopwatch();
                            Stopwatch sw2 = new Stopwatch();
                            sw1.Start();
                            for (int j = 0; j < size; j++)
                            {
                                int ind = random.Next(0, hash.Size() - 1);
                                hash.Get(ind);
                                hash.Put(ind, j);
                            }
                            sw1.Stop();
                            sum1 += sw1.ElapsedMilliseconds;

                            sw2.Start();
                            for (int j = 0; j < size; j++)
                            {
                                int ind = random.Next(0, tree.Size() - 1);
                                tree.Get(ind);
                                tree.Put(ind, j);
                            }
                            sw2.Stop();
                            sum2 += sw2.ElapsedMilliseconds;
                        }
                        double res1 = sum1 / 20;
                        double res2 = sum2 / 20;
                        array1.Add(size, res1);
                        array2.Add(size, res2);
                        hash.Clear();
                        tree.Clear();
                        count = res1 > res2 ? (int)res1 : (int)res2;
                    }
                    break;
                
            }
            pane.CurveList.Clear();
            pane.AddCurve("HashMap", array1, Color.Green, SymbolType.Circle);
            pane.AddCurve("TreeMap", array2, Color.Purple, SymbolType.Square);
            zedGraph.AxisChange();
            zedGraph.Invalidate();
            pane.XAxis.Scale.Min = 10;
            pane.XAxis.Scale.Max = 10000*1.05;
            pane.YAxis.Scale.Min = -1;
            pane.YAxis.Scale.Max = count*1.1;
            pane.XAxis.Title.Text = "Size";
            pane.YAxis.Title.Text = "Duration of execution:";
            pane.Title.Text = "Data structure efficieny:";

        }

    }
}

