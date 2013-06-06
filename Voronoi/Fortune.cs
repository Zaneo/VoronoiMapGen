/*
 * Copyright (c) 2013 Gareth Higgins

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
 * 
*/

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
