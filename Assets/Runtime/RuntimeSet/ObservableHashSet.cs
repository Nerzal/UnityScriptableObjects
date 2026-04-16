using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sets {
    /// <summary>
    /// A ScriptableObject-based observable hash set that notifies listeners when items are added or removed.
    /// Implements ISet and IEnumerable for full set functionality with events.
    /// </summary>
    /// <typeparam name="T">The type of items stored in the set.</typeparam>
    [CreateAssetMenu(fileName = "NewObservableHashSet", menuName = "Sets/Observable HashSet", order = 0)]
    public class ObservableHashSet<T> : ObservableCollectionBase, ISet<T>, IEnumerable<T> {
        /// <summary>
        /// The underlying hash set that stores the items.
        /// </summary>
        [SerializeField]
        private HashSet<T> items = new HashSet<T>();

        /// <summary>
        /// Event invoked when an item is added to the set.
        /// </summary>
        public Action<T> itemAdded;

        /// <summary>
        /// Event invoked when an item is removed from the set.
        /// </summary>
        public Action<T> itemRemoved;

        /// <summary>
        /// Event invoked when the set is cleared.
        /// </summary>
        public Action setCleared;

        /// <summary>
        /// Gets the number of items in the set.
        /// </summary>
        public int Count => items.Count;

        /// <summary>
        /// Gets a value indicating whether the set is read-only.
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// Adds an item to the set.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <returns>True if the item was added, false if it was already present.</returns>
        public bool Add(T item) {
            if (items.Add(item)) {
                itemAdded?.Invoke(item);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Removes the specified item from the set.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <returns>True if the item was removed, false if it was not present.</returns>
        public bool Remove(T item) {
            if (items.Remove(item)) {
                itemRemoved?.Invoke(item);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Removes all items from the set.
        /// </summary>
        public void Clear() {
            items.Clear();
            setCleared?.Invoke();
        }

        /// <summary>
        /// Determines whether the set contains a specific item.
        /// </summary>
        /// <param name="item">The item to locate.</param>
        /// <returns>True if the item is found, false otherwise.</returns>
        public bool Contains(T item) {
            return items.Contains(item);
        }

        /// <summary>
        /// Copies the elements of the set to an array, starting at a particular array index.
        /// </summary>
        /// <param name="array">The one-dimensional array that is the destination.</param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        public void CopyTo(T[] array, int arrayIndex) {
            items.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the set.
        /// </summary>
        /// <returns>An enumerator for the set.</returns>
        public IEnumerator<T> GetEnumerator() {
            return items.GetEnumerator();
        }

        /// <summary>
        /// Returns a non-generic enumerator for the set.
        /// </summary>
        /// <returns>A non-generic enumerator.</returns>
        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        /// <summary>
        /// Modifies the current set so that it contains all elements that are present in itself, the specified collection, or both.
        /// </summary>
        /// <param name="other">The collection to compare to the current set.</param>
        public void UnionWith(IEnumerable<T> other) {
            foreach (T item in other) {
                Add(item);
            }
        }

        /// <summary>
        /// Modifies the current set so that it contains only elements that are also in a specified collection.
        /// </summary>
        /// <param name="other">The collection to compare to the current set.</param>
        public void IntersectWith(IEnumerable<T> other) {
            HashSet<T> otherSet = new HashSet<T>(other);
            List<T> toRemove = new List<T>();

            foreach (T item in items) {
                if (!otherSet.Contains(item)) {
                    toRemove.Add(item);
                }
            }

            foreach (T item in toRemove) {
                Remove(item);
            }
        }

        /// <summary>
        /// Removes all elements in the specified collection from the current set.
        /// </summary>
        /// <param name="other">The collection of items to remove from the set.</param>
        public void ExceptWith(IEnumerable<T> other) {
            foreach (T item in other) {
                Remove(item);
            }
        }

        /// <summary>
        /// Modifies the current set so that it contains only elements that are present either in the current set or in the specified collection, but not both.
        /// </summary>
        /// <param name="other">The collection to compare to the current set.</param>
        public void SymmetricExceptWith(IEnumerable<T> other) {
            HashSet<T> otherSet = new HashSet<T>(other);

            // Remove items that are in both sets
            foreach (T item in otherSet) {
                if (items.Contains(item)) {
                    Remove(item);
                } else {
                    Add(item);
                }
            }
        }

        /// <summary>
        /// Determines whether the current set is a subset of a specified collection.
        /// </summary>
        /// <param name="other">The collection to compare to the current set.</param>
        /// <returns>True if the current set is a subset of other, false otherwise.</returns>
        public bool IsSubsetOf(IEnumerable<T> other) {
            return items.IsSubsetOf(other);
        }

        /// <summary>
        /// Determines whether the current set is a superset of a specified collection.
        /// </summary>
        /// <param name="other">The collection to compare to the current set.</param>
        /// <returns>True if the current set is a superset of other, false otherwise.</returns>
        public bool IsSupersetOf(IEnumerable<T> other) {
            return items.IsSupersetOf(other);
        }

        /// <summary>
        /// Determines whether the current set overlaps with the specified collection.
        /// </summary>
        /// <param name="other">The collection to compare to the current set.</param>
        /// <returns>True if the current set overlaps with other, false otherwise.</returns>
        public bool Overlaps(IEnumerable<T> other) {
            return items.Overlaps(other);
        }

        /// <summary>
        /// Determines whether the current set and the specified collection contain the same elements.
        /// </summary>
        /// <param name="other">The collection to compare to the current set.</param>
        /// <returns>True if the current set is equal to other, false otherwise.</returns>
        public bool SetEquals(IEnumerable<T> other) {
            return items.SetEquals(other);
        }

        // Editor support methods
        public override int GetCount() {
            return Count;
        }

        public override object GetItemAt(int index) {
            // HashSet doesn't have indexing, so we convert to list for editor display
            if (index >= 0 && index < items.Count) {
                List<T> list = new List<T>(items);
                return list[index];
            }
            return null;
        }

        public override void ClearItems() {
            Clear();
        }

        public bool IsProperSubsetOf(IEnumerable<T> other) {
            throw new NotImplementedException();
        }

        public bool IsProperSupersetOf(IEnumerable<T> other) {
            throw new NotImplementedException();
        }

        void ICollection<T>.Add(T item) {
            throw new NotImplementedException();
        }
    }
}
