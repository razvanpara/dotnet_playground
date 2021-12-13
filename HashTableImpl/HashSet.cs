using System;

namespace HashTableImpl
{
    public class HashSet<T>
    {
        private T[] _innerStorage;
        public int Count { get; private set; }
        public HashSet()
        {
            _innerStorage = new T[32];
        }
        public void Add(T item)
        {
            var currentItem = _innerStorage[GetIndex(item)];

            if (currentItem != null && !currentItem.Equals(item))
            { // resize on collision
                Resize();
            }

            _innerStorage[GetIndex(item)] = item;

        }
        public bool Contains(T item) => item.Equals(_innerStorage[GetIndex(item)]);
        private void Resize()
        {
            var oldStorage = _innerStorage;
            _innerStorage = new T[(int)Math.Round(oldStorage.Length * 1.2)];
            foreach (var item in oldStorage)
            {
                if (item != null)
                {
                    var newIndex = GetIndex(item);
                    _innerStorage[newIndex] = item;
                }
            }
        }
        private int GetIndex(T item) => Math.Abs(item.GetHashCode() % _innerStorage.Length);
    }
}
