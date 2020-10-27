using System;
using System.Collections;
using System.Collections.Generic;

namespace TechnocomShared.Collection
{

    [Serializable]
    public class GenericCollection<T> : IEnumerable<T>
    {
        private readonly List<T> _arItems = new List<T>();

        public T GetItem(int pos)
        { 
            return _arItems[pos]; 
        }

        public void AddItem(T item)
        { 
            _arItems.Add(item); 
        }

        public void ClearItems()
        { 
            _arItems.Clear(); 
        }

        public int Count
        { 
            get 
            { 
                return _arItems.Count; 
            } 
        }

        // IEnumerable<T> extends IEnumerable, therefore
        // we need to implement both versions of GetEnumerator().
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        { 
            return _arItems.GetEnumerator(); 
        }
        IEnumerator IEnumerable.GetEnumerator()
        { 
            return _arItems.GetEnumerator(); 
        }
    }
}
