using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateMod2.Utils {
    public class PriorityQueue<T> where T : IComparable {
        private T[] heap;
        private int tot;
        public PriorityQueue(int sz) {
            tot = 0;
            heap = new T[sz * 4];
        }
        public bool Empty { get => tot == 0; }
        public void Push(T val) {
            heap[++tot] = val;
            swim();
        }
        public T Pop() {
            T ret = heap[1];
            swap(1, tot--);
            sink();
            return ret;
        }
        private void swap(int i, int j) {
            T tmp = heap[i];
            heap[i] = heap[j];
            heap[j] = heap[i];
        }
        private void swim() {
            int k = tot;
            while (k > 1 && heap[k >> 1].CompareTo(heap[k]) > 0) {
                swap(k >> 1, k);
                k >>= 1;
            }
        }
        private void sink() {
            int k = 1;
            while ((k << 1) <= tot) {
                int j = k << 1;
                if (heap[k << 1].CompareTo(heap[k << 1 | 1]) > 0)
                    j++;
                if (heap[k].CompareTo(heap[j]) <= 0) break;
                swap(k, j);
                k = j;
            }
        }
    }
}
