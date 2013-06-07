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
using System.Collections.Generic;
using System.Windows;

namespace VoronoiMapGen.Util
{

    class YCoordinateComparer : IComparer<Vector> {
        public int Compare(Vector x, Vector y) {
            return (int)(x.Y - y.Y);
        }
    }
    class PriorityQueue<T> {

        enum Child {
            Left,
            Right
        }

        private const int DefaultSize = 1024;
        private readonly IComparer<T> _comparer;

        private T[] _heap;

        public PriorityQueue(IComparer<T> comparer, int initialSize) {
            _comparer = comparer;
            _heap = new T[initialSize];
        }

        public PriorityQueue():this(Comparer<T>.Default, DefaultSize) {
        }

        private static void Swap(ref T one, ref T two) {
            T temp = one;
            one = two;
            two = temp;
        }

        public void Add(T item) {
            
            if (IsReadOnly)
                throw new AccessViolationException("Attempted to add item to PriorityQueue marked Readonly.");

            int indexH;
            int indexI;
            if (Count > _heap.Length) {
                Grow();
            }

            _heap[Count] = item;
            indexI = Count;
            indexH = GetParentIndex(Count);
            while (indexH >= 0 && _comparer.Compare(_heap[indexI], _heap[indexH]) > 0) {
                Swap(ref _heap[indexI], ref _heap[indexH]);
                indexI = indexH;
                indexH = GetParentIndex(indexH);
            }

            Count++;
        }

        public T Remove() {
            if (IsReadOnly)
                throw new AccessViolationException("Attempted to remove item to PriorityQueue marked Readonly.");

            T retvalue = _heap[0];
            _heap[0] = _heap[Count];
            Count--;

            int indexH;
            int indexT;
            int indexI = 0;

            
            while (true) {
                indexH = GetChildIndex(Child.Left, indexI);
                indexT = GetChildIndex(Child.Right, indexI);

                if (indexH > Count || indexT > Count){
                    break;
                }

                if (_comparer.Compare(_heap[indexT], _heap[indexH]) > 0)
                    indexH = indexT;

                

                if (_comparer.Compare(_heap[indexI], _heap[indexH]) >= 0) {
                    break;
                }
                Swap(ref _heap[indexI], ref _heap[indexH]);
                indexI = indexH;
            }


            return retvalue;

        }


        private static int GetParentIndex(int index){
            return ((index +1) >> 1) -1;
        }

        private static int GetChildIndex(Child side, int index) {
            switch (side) {
                case Child.Left:
                    return ((index + 1) << 1) - 1;
                    break;
                case Child.Right:
                    return (index + 1) << 1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("side");
            }
        }

        private void Grow() {

            if (_heap.Length == int.MaxValue - 1)
                throw new IndexOutOfRangeException("No more keys left to give out, remove some objects fromt the queue.");
            int size;
            if (_heap.Length > (int.MaxValue-1/2)) {
                size = int.MaxValue - 1;
            }
            else {
               size = _heap.Length*2;
            }

            Array.Resize(ref _heap, size);
        }

        public void Clear() {
            Count = 0;
        }

       

        public int Count { get; private set; }
        public bool IsReadOnly { get; private set; }

    } 
}
