using System;

bool IsSymmetric(int[][] matr)
{
    for (int i = 0; i< matr.Length; i++)
        for (int j = i+1; j< matr.Length; j++) if (matr[i][j] != matr[j][i]) return false;
    return true;
}
string path = "input.txt";
string? line;
int[][]? mas = null;
int[]? vector = null;
int n;
try
{
    StreamReader sr = new StreamReader(path);
    line = sr.ReadLine();
    n = Convert.ToInt32(line);

    mas = new int[n][];
    for (int i = 0; i < n; i++)
    {
        line = sr.ReadLine();
        mas[i] = line.Split(' ').Select(x => Convert.ToInt32(x)).ToArray();
    }

    Console.WriteLine("Исходная матрица:");
    for (int i = 0; i < mas.Length; i++)
    {
        foreach (int x in mas[i]) Console.Write(x + " ");
        Console.WriteLine();
    }

    line = sr.ReadLine();
    vector = line.Split(' ').Select(x => Convert.ToInt32(x)).ToArray();

    sr.Close();
} catch (Exception ex) { Console.WriteLine(ex.Message); }
    
Console.Write("Исходный вектор:");
foreach (int x in vector) Console.Write(x + " ");
Console.WriteLine();

if (!IsSymmetric(mas))
{
    Console.WriteLine("ERROR: Матрица не симметрична");
    return;
}

int sum = 0;
double ans = 0;
int[] vector2 = new int[mas.Length];
for (int i = 0; i< mas.Length; i++)
{
    sum = 0;
    for (int j =0; j < mas.Length; j++) sum += vector[j] * mas[i][j];
    vector2[i] = sum;
}
for (int i=0; i< mas.Length; i++)
{
    ans += vector2[i] * vector[i];
}
ans = Math.Sqrt(ans);
Console.Write($"Длина вектора: {ans}");
