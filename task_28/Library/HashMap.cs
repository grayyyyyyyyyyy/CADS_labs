using System;
using System.Collections.Generic;

namespace HashMap
{
    public class MyHashMap<Key, Value>
    {
        private class Entry
        {
            public Key key { get; set; }
            public Value value { get; set; }
            public Entry next { get; set; }
            public Entry(Key key, Value value)
            {
                this.key = key;
                this.value = value;
            }
        }
        Entry[] table;
        int size;
        double loadFactor;

        // helping methods
        private int GetHashCode(Key key) => Math.Abs(key.GetHashCode()) % table.Length;
        private int GetHashCode(Value value) => Math.Abs(value.GetHashCode()) % table.Length;
        private void HelpPut(Key key, Value value)
        {
            int index = GetHashCode(key);
            Entry step = table[index];
            if (step != null)
            {
                int fl = 1;
                while (step.next != null)
                {
                    if (step.key.Equals(key))
                    {
                        step.value = value;
                        fl = 0;
                    }
                    step = step.next;
                }
                if (step.key.Equals(key))
                {
                    step.value = value;
                    fl = 0;
                }
                if (fl == 1)
                {
                    Entry tmp = new Entry(key, value);
                    step.next = tmp;
                    step = tmp;
                    size++;
                }
            }
            else
            {
                Entry newTmp = new Entry(key, value);
                table[index] = newTmp;
                size++;
            }
        }
        private void PutInNew(Entry[] array, Key key, Value value)
        {
            int index = Math.Abs(key.GetHashCode()) % array.Length;
            Entry tmp = new Entry(key, value);
            if (array[index] != null)
            {
                Entry step = array[index];
                while (step.next != null)
                    step = step.next;
                step.next = tmp;
            }
            else
                array[index] = tmp;
            size++;
        }
        private void Resize()
        {
            Entry[] newArray = new Entry[table.Length * 3];
            int prevSize = size;
            size = 0;
            for (int i = 0; i < table.Length; i++)
                if (table[i] != null)
                {
                    Entry value = table[i];
                    while (value != null)
                    {
                        int index = Math.Abs(value.key.GetHashCode()) % newArray.Length;
                        Entry nextValue = value.next;
                        PutInNew(newArray, value.key, value.value);
                        value = nextValue;
                    }
                }
            table = newArray;
        }

        // 1
        public MyHashMap()
        {
            table = new Entry[16];
            size = 16;
            loadFactor = 0.75;
        }

        // 2
        public MyHashMap(int initialCapacity)
        {
            table = new Entry[initialCapacity];
            size = initialCapacity;
            loadFactor = 0.75;
        }

        // 3
        public MyHashMap(int initialCapacity, float loadFactorr)
        {
            table = new Entry[initialCapacity];
            size = initialCapacity;
            loadFactor = loadFactorr;
        }

        // 4, 9, 13
        public void Clear() => size = 0;
        public bool IsEmpty() => size == 0;
        public int Size() => size;

        // 5
        public bool ContainsKey(Key key)
        {
            int index = GetHashCode(key);
            Entry step = table[index];
            while (step != null)
            {
                if (step.key.Equals(key))
                    return true;
                step = step.next;
            }
            return false;
        }

        // 6
        public bool ContainsValue(Value value)
        {
            int index = GetHashCode(value);
            Entry step = table[index];
            while (step != null)
            {
                if (step.value.Equals(value))
                    return true;
                step = step.next;
            }
            return false;
        }

        // 7
        public IEnumerable<KeyValuePair<Key, Value>> EntrySet()
        {
            foreach (var entry in table)
            {
                for (var step = entry; step != null; step = step.next)
                    yield return new KeyValuePair<Key, Value>(step.key, step.value);
            }
        }

        // 8
        public Value Get(Key key)
        {
            int index = GetHashCode(key);
            Entry step = table[index];
            while (step != null)
            {
                if (step.key.Equals(key))
                    return step.value;
                step = step.next;
            }
            throw new KeyNotFoundException();
        }

        // 10
        public Key[] KeySet()
        {
            Key[] array = new Key[size];
            int index = 0;
            for (int i = 0; i < table.Length; i++)
                if (table[i] != null)
                {
                    Entry step = table[i];
                    while (step != null)
                    {
                        array[index++] = step.key;
                        step = step.next;
                    }
                }
            return array;
        }

        // 11
        public void Put(Key key, Value value)
        {
            double count = (double)(size + 1) / (double)table.Length;
            if (count >= loadFactor)
                Resize();
            HelpPut(key, value);
        }

        // 12
        public void Remove(Key key)
        {
            int index = GetHashCode(key);
            Entry step = table[index];
            if (step == null)
                return;
            if (step.key.Equals(key))
            {
                table[index] = table[index].next;
                size--;
                return;
            }
            Entry cur = table[index];
            Entry prev = null;
            while (cur != null)
            {
                if (cur.key.Equals(key))
                {
                    prev.next = cur.next;
                    size--;
                    return;
                }
                prev = cur;
                cur = cur.next;
            }
        }
    }
}