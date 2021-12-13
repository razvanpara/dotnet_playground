using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HashTableImpl
{
    public class HashTable<T>
    {
        private class TableEntry
        {
            public string Key { get; private set; }
            public T Value { get; private set; }
            public TableEntry Next { get; set; }
            public TableEntry(string key, T value)
            {
                Key = key;
                Value = value;
            }
        }

        private TableEntry[] _innerStorage;
        public HashTable() { _innerStorage = new TableEntry[32]; }
        public HashTable(int size) { _innerStorage = new TableEntry[size]; }
        public override string ToString()
        {
            string value = "";
            value = string.Join(",", _innerStorage.Select(te =>
            {
                if (te != null)
                {
                    var sb = new StringBuilder();
                    sb.Append(te.Value);
                    while (te.Next != null)
                    {
                        sb.Append($", {te.Next.Value}");
                        te = te.Next;
                    }
                    return $"[{sb}]";
                }
                return string.Empty;
            }));
            return $"[{value}]";
        }

        public T this[string key]
        {
            get => Get(key);
            set => Add(key, value);
        }
        public T Get(string key)
        {
            var valueAtIndex = _innerStorage[GetIndex(key)];

            while (valueAtIndex != null && valueAtIndex.Key != key)
                valueAtIndex = valueAtIndex.Next;

            if (valueAtIndex is null) throw new Exception("Key not found");

            return valueAtIndex.Value;
        }
        public void Add(string key, T value)
        {

            var index = GetIndex(key);
            var newEntry = new TableEntry(key, value);

            var valueAtIndex = _innerStorage[index];

            if (valueAtIndex != null && ThrowIfKeyExists(key, valueAtIndex))
                newEntry.Next = valueAtIndex;

            _innerStorage[index] = newEntry;
        }
        private bool ThrowIfKeyExists(string key, TableEntry tableEntry) => key != tableEntry.Key ? true : throw new Exception($"key {key} already exists");
        private int GetIndex(string key)
        {
            var keyHash = Hashing.ByteFoldHash(key);
            var index = keyHash % _innerStorage.Length;
            return index;
        }
        public IEnumerable<KeyValuePair<string, T>> ToList()
        {
            return _innerStorage.SelectMany(kv =>
            {
                var list = new List<KeyValuePair<string, T>>();
                while (kv != null)
                {
                    list.Add(new KeyValuePair<string, T>(kv.Key, kv.Value));
                    kv = kv.Next;
                }
                return list;
            });
        }
    }
}
