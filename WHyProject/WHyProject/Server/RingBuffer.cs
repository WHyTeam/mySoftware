using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Comminucation.Server
{
    class RingBuffer
    {
        private int head;
        private int tail;
        private int count;

        private string[] array;
        private object lockObject;
     //   private TimeSpan waitSpan;

        private ManualResetEvent notEmptyEvent;
        private ManualResetEvent notFullEvent;

        ///<summary>
        /// Create a new RingBuffer with a specified size
        /// </summary>
        public RingBuffer(int size)
        {
            if(size <= 0)
                throw new ArgumentOutOfRangeException("size");

            array = new string[size];
            lockObject = new object();

            notFullEvent = new ManualResetEvent(true);
            notEmptyEvent = new ManualResetEvent(false);
        }

        ///<summary>
        /// Clear the buffer contents
        /// </summary>
        public void clear()
        {
            tail = 0;
            head = 0;
            count = 0;

            Array.Clear(array,0,array.Length);

            notFullEvent.Set();
            notEmptyEvent.Reset();
        }

        ///<summary>
        /// Write adds a string to the head of the ringbuffer
        /// </summary>
        public void WriteData(string value)
        {
            notFullEvent.WaitOne();
            lock(lockObject)
            {
                array[head] = value;
                head = (head + 1) % (array.Length);

                bool setEmpty = (count == 0);
                count += 1;
                if (IsFull)
                    notFullEvent.Reset();
                if (setEmpty)
                    notEmptyEvent.Set();
            }

        }

        public string ReadData()
        {
            string result = null;
            notEmptyEvent.WaitOne();

            if (!IsEmpty)
            {
                lock (lockObject)
                {
                    result = array[tail];
                    tail = (tail + 1) % array.Length;
                    bool setFull = IsFull;
                    count -= 1;

                    if (count == 0)
                    {
                        notEmptyEvent.Reset();
                    }
                    if (setFull)
                    {
                        notFullEvent.Set();
                    }
                }
            }
            return result;
        }

        public bool IsEmpty
        {
            get { return (count == 0); }
        }

        public bool IsFull
        {
            get { return (count == array.Length); }
        }

        public string this[int index]
        {
            get
            {
                if ((index < 0) || (index >= array.Length))
                {
                    throw new ArgumentOutOfRangeException("index");
                }
                return array[(tail + index) % array.Length];
            }
        }

    }
}
