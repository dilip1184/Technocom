using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace TechnocomShared.Collection
{
    public class IndexedDictionary<TKey, TValue> : ConcurrentDictionary<TKey, TValue>
    {
        private List<TKey> keylist;

        /// <summary>
        /// Gets or sets the value associated at the specified index.
        /// </summary>
        /// <param name="index">The index of the value to get or set.</param>
        /// <returns>
        /// Returns the Value property of the <see cref="System.Collections.Concurrent.ConcurrentDictionary"/>
        /// at the specified index.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">index is a null reference (Nothing in Visual Basic).</exception>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">The property is retrieved and index does not exist in the collection.</exception>
        /// <exception cref="System.IndexOutOfRangeException">The property is retrieved and index does not exist in the collection.</exception>
        public TValue this[int index]
        {
            get { return this[keylist[index]]; }
            set { this[keylist[index]] = value; }
        }

        /// <summary>
        /// Gets or sets the value associated for the specified key.
        /// </summary>
        /// <param name="key">The key of the value to get or set.</param>
        /// <returns>
        /// Returns the Value property of the <see cref="System.Collections.Concurrent.ConcurrentDictionary"/>
        /// for the specified key.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">key is a null reference (Nothing in Visual Basic).</exception>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">The property is retrieved and key does not exist in the collection.</exception>
        /// <exception cref="System.IndexOutOfRangeException">The property is retrieved and index does not exist in the collection.</exception>
        public new TValue this[TKey key]
        {
            get { return base[key]; }
            set { base[key] = value; }
        }

        /// <summary>
        /// Adds a key/value pair to the <see cref="System.Collections.Concurrent.ConcurrentDictionary"/>
        /// if the key does not already exist, or updates a key/value pair in the <see cref="System.Collections.Concurrent.ConcurrentDictionary"/>
        /// if the key already exists.
        /// </summary>
        /// <param name="key">The key to be added or whose value should be updated</param>
        /// <param name="addValueFactory">The function used to generate a value for an absent key</param>
        /// <param name="updateValueFactory">The function used to generate a new value for an existing key based on the key's existing value</param>
        /// <returns>
        /// The new value for the key. This will be either be the result of addValueFactory
        /// (if the key was absent) or the result of updateValueFactory (if the key was
        /// present).
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// key is a null reference (Nothing in Visual Basic).-or-addValueFactory is
        /// a null reference (Nothing in Visual Basic).-or-updateValueFactory is a null
        /// reference (Nothing in Visual Basic).
        /// </exception>
        /// <exception cref="System.OverflowException">The dictionary contains too many elements.</exception>
        public new TValue AddOrUpdate(TKey key, Func<TKey, TValue> addValueFactory, Func<TKey, TValue, TValue> updateValueFactory)
        {
            if (!ContainsKey(key))
                keylist.Add(key);

            return base.AddOrUpdate(key, addValueFactory, updateValueFactory);
        }

        /// <summary>
        /// Adds a key/value pair to the System.Collections.Concurrent.ConcurrentDictionary<TKey,TValue>
        /// if the key does not already exist, or updates a key/value pair in the <see cref="System.Collections.Concurrent.ConcurrentDictionary"/>
        /// if the key already exists.
        /// </summary>
        /// <param name="key">The key to be added or whose value should be updated</param>
        /// <param name="addValue">The value to be added for an absent key</param>
        /// <param name="updateValueFactory">The function used to generate a new value for an existing key based on the key's existing value</param>
        /// <returns>
        /// The new value for the key. This will be either be addValue (if the key was
        /// absent) or the result of updateValueFactory (if the key was present).
        /// </returns>
        /// <exception cref="System.ArgumentNullException">key is a null reference (Nothing in Visual Basic).-or-updateValueFactory is a null reference (Nothing in Visual Basic).</exception>
        /// <exception cref="System.OverflowException">The dictionary contains too many elements.</exception>
        public new TValue AddOrUpdate(TKey key, TValue addValue, Func<TKey, TValue, TValue> updateValueFactory)
        {
            if (!ContainsKey(key))
                keylist.Add(key);

            return base.AddOrUpdate(key, addValue, updateValueFactory);
        }

        /// <summary>
        /// Removes all keys and values from the <see cref="System.Collections.Concurrent.ConcurrentDictionary"/>.
        /// </summary>
        public new void Clear()
        {
            keylist.Clear();
            base.Clear();
        }

        /// <summary>
        /// Adds a key/value pair to the <see cref="System.Collections.Concurrent.ConcurrentDictionary"/>
        /// if the key does not already exist.
        /// </summary>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="valueFactory">The function used to generate a value for the key</param>
        /// <returns>
        /// The value for the key. This will be either the existing value for the key
        /// if the key is already in the dictionary, or the new value for the key as
        /// returned by valueFactory if the key was not in the dictionary.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">key is a null reference (Nothing in Visual Basic).-or-valueFactory is a null reference (Nothing in Visual Basic).</exception>
        /// <exception cref="System.OverflowException">The dictionary contains too many elements.</exception>
        public new TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory)
        {
            if (!ContainsKey(key))
                keylist.Add(key);

            return base.GetOrAdd(key, valueFactory);
        }

        /// <summary>
        /// Adds a key/value pair to the <see cref="System.Collections.Concurrent.ConcurrentDictionary"/>
        /// if the key does not already exist.
        /// </summary>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value">the value to be added, if the key does not already exist</param>
        /// <returns>
        /// The value for the key. This will be either the existing value for the key
        /// if the key is already in the dictionary, or the new value if the key was
        /// not in the dictionary.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">key is a null reference (Nothing in Visual Basic).</exception>
        /// <exception cref="System.OverflowException">The dictionary contains too many elements.</exception>
        public new TValue GetOrAdd(TKey key, TValue value)
        {
            if (!ContainsKey(key))
                keylist.Add(key);

            return base.GetOrAdd(key, value);
        }

        /// <summary>
        /// Attempts to add the specified key and value to the <see cref="System.Collections.Concurrent.ConcurrentDictionary"/>.
        /// </summary>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value">
        /// The value of the element to add. The value can be a null reference (Nothing
        /// in Visual Basic) for reference types.
        /// </param>
        /// <returns>
        /// true if the key/value pair was added to the System.Collections.Concurrent.ConcurrentDictionary<TKey,TValue>
        /// successfully; otherwise, false.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">key is null reference (Nothing in Visual Basic).</exception>
        /// <exception cref="System.OverflowException">The System.Collections.Concurrent.ConcurrentDictionary<TKey,TValue> contains too many elements.</exception>
        public new bool TryAdd(TKey key, TValue value)
        {
            if (base.TryAdd(key, value))
            {
                keylist.Add(key);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Attempts to remove and return the the value with the specified key from the
        /// <see cref="System.Collections.Concurrent.ConcurrentDictionary"/>.
        /// </summary>
        /// <param name="key">The key of the element to remove and return.</param>
        /// <param name="value">
        /// When this method returns, value contains the object removed from the <see cref="System.Collections.Concurrent.ConcurrentDictionary"/>
        /// or the default value of if the operation failed.
        /// </param>
        /// <returns>true if an object was removed successfully; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">key is a null reference (Nothing in Visual Basic).</exception>
        public new bool TryRemove(TKey key, out TValue value)
        {
            if (base.TryRemove(key, out value))
            {
                keylist.Remove(key);
                return true;
            }
            return false;
        }

        public IndexedDictionary()
            : base()
        {
            keylist = new List<TKey>();
        }

        public IndexedDictionary(IEqualityComparer<TKey> comparer)
            : base(comparer)
        {
            keylist = new List<TKey>();
        }
    }
}