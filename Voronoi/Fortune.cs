using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoronoiMapGen.Derp
{
    internal interface IPriorityQueue<T> : ICollection<T> {
        T FindMin();
        T DeleteMin();
        T FindMax();
        T DeleteMax();
        void Add(int priority, T value);
    }

    class PriorityQueue<T> : IPriorityQueue<T> {

        private const int DefaultSize = 1024;
        private IComparer<T> _comparer;

        private T[] _heap;

        public PriorityQueue(IComparer<T> comparer, int initialSize) {
            _comparer = comparer;
            _heap = new T[initialSize];
        }

        public PriorityQueue():this(Comparer<T>.Default, DefaultSize) {
        }

        private int GetIndex(int position) {
            return (int) Math.Log(position, 2);
        }
        public IEnumerator<T> GetEnumerator() {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public void Add(T item) {
            if (Count > _heap.Length) {
                throw new NotImplementedException("Grow");
            }
        }

        public void Clear() {
            throw new NotImplementedException();
        }

        public bool Contains(T item) {
            throw new NotImplementedException();
        }

        public void CopyTo(T[] array, int arrayIndex) {
            throw new NotImplementedException();
        }

        public bool Remove(T item) {
            throw new NotImplementedException();
        }

        public int Count { get; private set; }
        public bool IsReadOnly { get; private set; }
        public T FindMin() {
            throw new NotImplementedException();
        }

        public T DeleteMin() {
            throw new NotImplementedException();
        }

        public T FindMax() {
            throw new NotImplementedException();
        }

        public T DeleteMax() {
            throw new NotImplementedException();
        }

        public void Add(int priority, T value) {
            throw new NotImplementedException();
        }
    } 
}
