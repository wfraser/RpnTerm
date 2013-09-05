//
// Copyright © William R. Fraser 2013
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpnTerm
{
    public class CircularBuffer<T> : IEnumerable<T>
    {
        private int m_start;
        private int m_length;
        private T[] m_elements;

        public CircularBuffer(int size)
        {
            m_elements = new T[size];
            m_start = 0;
            m_length = 0;
        }

        public void PushEnd(T item)
        {
            if (m_length < m_elements.Length)
            {
                m_elements[m_start + m_length] = item;
                m_length++;
            }
            else
            {
                m_elements[m_start] = item;
                m_start++;
                if (m_start == m_length)
                {
                    m_start = 0;
                }
            }
        }

        public void Clear()
        {
            m_start = 0;
            m_length = 0;
        }

        public int Count()
        {
            return m_length;
        }

        public T Front
        {
            get
            {
                return m_elements[m_start];
            }
            set
            {
                m_elements[m_start] = value;
            }
        }

        public T End
        {
            get
            {
                if (m_start == 0)
                {
                    return m_elements[m_length - 1];
                }
                else
                {
                    return m_elements[m_start - 1];
                }
            }
            set
            {
                if (m_start == 0)
                {
                    m_elements[m_length - 1] = value;
                }
                else
                {
                    m_elements[m_start - 1] = value;
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public struct Enumerator : IEnumerator<T>, System.Collections.IEnumerator
        {
            public Enumerator(CircularBuffer<T> parent)
            {
                m_parent = parent;
                m_position = parent.m_start - 1;
                m_started = false;
                m_index = 0;
            }

            public T Current
            {
                get
                {
                    if (!m_started)
                    {
                        throw new InvalidOperationException("Not started yet.");
                    }
                    else if (m_index > m_parent.m_length)
                    {
                        throw new InvalidOperationException("Moved past the end of the collection.");
                    }

                    return m_parent.m_elements[m_position];
                }
            }

            object System.Collections.IEnumerator.Current
            {
                get { return Current; }
            }

            public bool MoveNext()
            {
                if (!m_started)
                {
                    m_started = true;
                }

                m_position++;
                if (m_position == m_parent.m_elements.Length)
                {
                    m_position = 0;
                }

                if (m_index == m_parent.m_length)
                {
                    return false;
                }
                else
                {
                    m_index++;
                    return true;
                }
            }

            public void Dispose()
            {
            }

            public void Reset()
            {
                m_position = m_parent.m_start - 1;
                m_index = 0;
                m_started = false;
            }

            private CircularBuffer<T> m_parent;
            private int m_position;
            private bool m_started;
            private int m_index;
        }
    }
}
