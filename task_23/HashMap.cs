using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashMap
{
    public class MyHashMap<K, V>
    {
        private class Entry
        {
            public K key;
            public V value;
            public Entry next;
            public Entry(K key, V value)
            {
                this.key = key;
                this.value = value;
            }
        }
        Entry[] table;
        int size;
        double loadFactor;

        //1
        public MyHashMap()
        {
            table = new Entry[16];
            size = 16;
            loadFactor = 0.75;
        }
        //2
        public MyHashMap(int initialCapacity)
        {
            table = new Entry[initialCapacity];
            size = initialCapacity;
            loadFactor = 0.75;
        }
        //3
        public MyHashMap(int initialCapacity, double loadFactor)
        {
            table = new Entry[initialCapacity];
            size = initialCapacity;
            this.loadFactor = loadFactor;
        }

        private int GetHashCode(K key) => Math.Abs(key.GetHashCode()) % table.Length;
        private int GetHashCode(V value) => Math.Abs(value.GetHashCode()) % table.Length;
        private void HelpPut(K key, V value)
        {
            int index = GetHashCode(key);
            Entry step = table[index];
            if (step != null)
            {
                int f1 = 1;
                while (step.next != null)
                {
                    if (step.key.Equals(key))
                    {
                        step.value = value;
                        f1 = 0;
                    }
                    step = step.next;
                }
                if (step.key.Equals(key))
                {
                    step.value = value;
                    f1 = 0;
                }
                if (f1 == 1)
                {
                    Entry tmp = new Entry(key, value);
                    step.next = tmp;
                    step = tmp;
                    size++;
                }
            }
            else
            {
                Entry tmp = new Entry(key, value);
                table[index] = tmp;
                size++;
            }
        }
        private void PutInNew(Entry[] entries, K key, V value)
        {
            int index = Math.Abs(key.GetHashCode()) % entries.Length;
            Entry tmp = new Entry(key, value);
            if (entries[index] != null)
            {
                Entry step = entries[index];
                while (step.next != null) step = step.next;
                step.next = tmp;
            }
            else entries[index] = tmp;
            size++;
        }
        private void Resize()
        {
            Entry[] newEntries = new Entry[table.Length * 3];
            int prevSize = size;
            size = 0;
            for (int i = 0; i < table.Length; i++)
            {
                if (table[i] != null)
                {
                    Entry value = table[i];
                    while (value != null)
                    {
                        int index = Math.Abs(value.key.GetHashCode()) % newEntries.Length;
                        Entry nextValue = value.next;
                        PutInNew(newEntries, value.key, value.value);
                        value = nextValue;
                    }
                }
            }
            table = newEntries;
        }
        //4
        public void Clear() => size = 0;

        //5
        public bool ContainsKey(K key)
        {
            int index = GetHashCode(key);
            Entry step = table[index];
            while (step != null)
            {
                if (step.key.Equals(key)) return true;
                step = step.next;
            }
            return false;
        }
        //6
        public bool ContainsValue(K value)
        {
            int index = GetHashCode(value);
            Entry step = table[index];
            while (step != null)
            {
                if (step.value.Equals(value)) return true;
                step = step.next;
            }
            return false;
        }
        //7
        public IEnumerable<KeyValuePair<K, V>> EntrySet()
        {
            foreach (var entry in table)
            {
                for (var step = entry; step != null; step = step.next) yield return new KeyValuePair<K, V>(step.key, step.value);
            }
        }
        //8
        public V Get(K key)
        {
            int index = GetHashCode(key);
            Entry step = table[index];
            while (step != null)
            {
                if (step.key.Equals(key)) return step.value;
                step = step.next;
            }
            throw new KeyNotFoundException();
        }
        //9
        public bool IsEmpty() => size == 0;
        //10
        public K[] KeySet()
        {
            K[] array = new K[size];
            int index = 0;
            for (int i = 0; i < table.Length; i++)
            {
                if (table[i] != null)
                {
                    Entry step = table[i];
                    while (step != null)
                    {
                        array[index++] = step.key;
                        step = step.next;
                    }
                }
            }
            return array;
        }
        //11
        public void Put(K key, V value)
        {
            double count = (double)(size + 1) / (double)table.Length;
            if (count >= loadFactor) Resize();
            HelpPut(key, value);
        }
        //12
        //public void Remove(K key)
        //{
        //    int index = GetHashCode(key);
        //    Entry step = table[index];
        //    if (step != null) return;
        //    if (step.key.Equals(key))
        //    {
        //        table[index] = table[index].next;
        //        size--;
        //        return;
        //    }
        //    Entry current = table[index];
        //    Entry previous = null;
        //    while (current != null)
        //    {
        //        if (current.key.Equals(key))
        //        {
        //            previous.next = current.next;
        //            size--;
        //            return;
        //        }
        //        previous = current;
        //        current = current.next;
        //    }
        //}
        public void Remove(K key)
        {
            int index = Math.Abs(GetHashCode(key)) % table.Length;
            Entry entry = table[index];
            if (entry == null) throw new ArgumentException("Данного ключа не обнаружено");
            if (entry.key!.Equals(key))
            {
                table[index] = entry.next!;
                size--;
                return;
            }
            while (entry.next != null)
            {
                if (entry.next.key!.Equals(key))
                {
                    entry.next = entry.next.next;
                    size--;
                    return;
                }
                entry = entry.next;
            }
        }
        //13
        public int Size() => size;

    }
}
