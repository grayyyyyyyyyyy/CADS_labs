using HashMap;

public class MyHashSet<T> where T : IComparable
{
    MyHashMap<T, object> map;
    //1-4 (конструкторы)
    public MyHashSet() => map = new MyHashMap<T, object>();
    public MyHashSet(T[] a)
    {
        foreach (T item in a) map.Put(item, false);
    }
    public MyHashSet(int initialCapacity, float loadFactor) => map = new MyHashMap<T, object>(initialCapacity, loadFactor);
    public MyHashSet(int initialCapacity) => map = new MyHashMap<T, object>(initialCapacity);
    
    //5, 7, 8, 10, 11, 14, 15
    public void Add(T e) => map.Put(e, false);
    public void Clear() => map.Clear();
    public bool Contains(T o) => map.ContainsKey(o);
    public bool IsEmpty() => map.IsEmpty();
    public void Remove(T e) => map.Remove(e);
    public int Size() => map.Size();
    public T[] ToArray() => map.KeySet();
    //6
    public void AddAll(T[] a)
    {
        foreach (T item in a) map.Put(item, false);
    }
    //9 
    public bool ContainsAll(T[] a)
    {
        foreach (T item in a) if (!map.ContainsKey(item)) return false;
        return true;
    }
    //12
    public void RemoveAll(T[] a)
    {
        foreach (T item in a) map.Remove(item);
    }
    //13 
    public void RetainAll(T[] a)
    {
        T[] keys = map.KeySet();
        foreach (T key in keys) if (keys.Contains(key)) map.Remove(key);
    }
    //16
    public T[] ToArray(T[] array)
    {
        if (array == null) return ToArray();
        T[] newArray = new T[array.Length + map.Size()];
        int ind = 0;
        for (int i =0; i< array.Length; i++)
        {
            newArray[ind++] = array[i];
        }
        T[] a = map.KeySet();
        foreach (T e in a) newArray[ind++] = e;
        return newArray;
    }
    //17
    public T First()
    {
        T[] array = map.KeySet();
        T minimum = array[0];
        foreach (T item in array)
        {
            if (minimum.CompareTo(item) >= 0) minimum = item;
        }
        return minimum;
    }
    //18
    public T Last()
    {
        T[] array = map.KeySet();
        T maximum = array[0];
        foreach (T item in array)
        {
            if (maximum.CompareTo(item) <= 0) maximum = item;
        }
        return maximum;
    }
    //19
    public MyHashSet<T> SubSet(T fromElement, T toElement)
    {
        MyHashSet<T> subSet = new MyHashSet<T>();
        T[] array = map.KeySet();
        foreach (T item in array) if (item.CompareTo(fromElement) >= 0 && item.CompareTo(toElement) <= 0) subSet.Add(item);
        return subSet;
    }
    //20
    public MyHashSet<T> HeadSet(T toElement)
    {
        MyHashSet<T> headSet = new MyHashSet<T>();
        T[] array = map.KeySet();
        foreach (T item in array) if (item.CompareTo(toElement) <= 0) headSet.Add(item);
        return headSet;
    }
    //21
    public MyHashSet<T> tailSet(T fromElement)
    {
        MyHashSet<T> tailSet = new MyHashSet<T>();
        T[] array = map.KeySet();
        foreach (T item in array) if (item.CompareTo(fromElement) >= 0) tailSet.Add(item);
        return tailSet;
    }

}
public class Program()
{
    static void Main(string[] args)
    {
        MyHashSet<string> hashSet = new MyHashSet<string>();
        string[] array = { "one", "two", "three", "four", "five", "six", "seven", "eight", "one" };
        hashSet.AddAll(array);
        string[] array1 = hashSet.ToArray();
        foreach (string item in array1) Console.Write(item + " ");
        hashSet.Remove("one");
        Console.WriteLine();
        Console.WriteLine(hashSet.First());
        Console.WriteLine(hashSet.Last());
        Console.WriteLine(hashSet.Size());
        Console.WriteLine(hashSet.IsEmpty());

    }
}