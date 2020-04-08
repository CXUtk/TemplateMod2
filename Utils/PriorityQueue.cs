using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateMod2.Utils {
    /// <summary>
    /// 优先队列，使用小根堆实现，Pop，Push复杂度保证O(log n)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PriorityQueue<T> where T : IComparable {
        private T[] heap;
        private int tot;
        public PriorityQueue(int sz) {
            tot = 0;
            heap = new T[sz * 4];
        }
        public bool Empty { get => tot == 0; }

        /// <summary>
        /// 将元素放入小根堆
        /// </summary>
        /// <param name="val"></param>
        public void Push(T val) {
            heap[++tot] = val;
            swim();
        }

        /// <summary>
        /// 获取堆顶值
        /// </summary>
        public T Top => heap[1];

        /// <summary>
        /// 获取并弹出堆顶上的最小值
        /// </summary>
        /// <returns></returns>
        public T Pop() {
            T ret = heap[1];
            swap(1, tot--);
            sink();
            return ret;
        }
        private void swap(int i, int j) {
            T tmp = heap[i];
            heap[i] = heap[j];
            heap[j] = tmp;
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
