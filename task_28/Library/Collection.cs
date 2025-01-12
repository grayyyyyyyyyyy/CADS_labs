using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HashMap;
using Library;

namespace Library
{
    public interface MyCollection<T> where T : IComparable
    {
        void Add(T item);
        void AddAll(MyCollection<T> array);
        void Clear();
        bool Contains(T item);
        bool ContainsAll(MyCollection<T> array);
        bool IsEmpty();
        void Remove(T item);
        void RemoveAll(MyCollection<T> array);
        void RetainAll(MyCollection<T> array);
        int Size();
        T[] ToArray();
        T[] ToArray(T[] array);
        MyIteratorList<T> IteratorList();
        MyIteratorList<T> IteratorSet();
    }
    public interface MyList<T> : MyCollection<T> where T : IComparable
    {
        void Add(int index, T item);
        void AddAll(int index, MyCollection<T> array);
        T Get(int index);
        int IndexOf(T item);
        int LastIndexOf(T item);
        T Remove(int index);
        void Set(int index, T item);
        T[] SubList(int fromIndex, int toIndex);
    }
    public interface MyQueue<T> : MyCollection<T> where T : IComparable
    {
        T Element();
        bool Offer(T item);
        T Peek();
        T Poll();
    }
    public interface MyDeque<T> : MyCollection<T> where T : IComparable
    {
        void AddFirst(T item);
        void AddLast(T item);
        T GetFirst();
        T GetLast();
        bool OfferFirst(T item);
        bool OfferLast(T item);
        T Pop();
        void Push(T item);
        T PeekFirst();
        T PeekLast();
        T PollFirst();
        T PollLast();
        T RemoveFirst();
        T RemoveLast();
        bool RemoveFirstOccurrance(T item);
        bool RemoveLastOccurrance(T item);
    }
    public interface MySet<T> : MyCollection<T> where T : IComparable
    {
        T First();
        T Last();
        MySet<T> SubSet(T fromElement, T toElement);
        MySet<T> HeadSet(T toElement);
        MySet<T> TailSet(T fromElement);
    }
    public interface MySortedSet<T> : MyCollection<T> where T : IComparable
    {
        T First();
        T Last();
        MySet<T> SubSet(T fromElement, T toElement);
        MySet<T> HeadSet(T toElement);
        MySet<T> TailSet(T fromElement);
    }
    public interface MyNavigableSet<T> : MySet<T> where T : IComparable
    {
        T LowerEntry(T key);
        T FloorEntry(T key);
        T HigherEntry(T key);
        T CeilingEntry(T key);
        T LowerKey(T key);
        T FloorKey(T key);
        T HigherKey(T key);
        T CeilingKey(T key);
        T PollFirstEntry();
        T PollLastEntry();
        T FirstEntry();
        T LastEntry();
    }
    public interface MyMap<K, V>
    {
        void Clear();
        bool ContainsKey(K key);
        bool ContainsValue(V value);
        Tuple<K, V>[] EntrySet();
        V Get(K key);
        bool IsEmpty();
        K[] KeySet();
        void Put(K key, V value);
        void PutAll(MyHashMap<K, V> map);
        void Remove(K key);
        int Size();
        V Values();
    }
    public interface MySortedMap<K, V> : MyMap<K, V>
    {
        K FirstKey();
        K LastKey();
        MySortedMap<K, V> HeadMap(K end);
        MySortedMap<K, V> SubMap(K start, K end);
        MySortedMap<K, V> TailMap(K start);
    }
    public interface MyNavigableMap<K, V> : MySortedMap<K, V>
    {
        Tuple<K, V> LowerEntry(K key);
        Tuple<K, V> FloorEntry(K key);
        Tuple<K, V> HigherEntry(K key);
        Tuple<K, V> CeilingEntry(K key);
        K LowerKey(K key);
        K FloorKey(K key);
        K HigherKey(K key);
        K CeilingKey(K key);
        Tuple<K, V> PollFirstEntry();
        Tuple<K, V> PollLastEntry();
        Tuple<K, V> FirstEntry();
        Tuple<K, V> LastEntry();
    }
}