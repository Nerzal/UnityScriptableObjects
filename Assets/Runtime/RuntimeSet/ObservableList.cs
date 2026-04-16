using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sets {
  /// <summary>
  /// A ScriptableObject-based observable list that notifies listeners when items are added, removed, inserted, or modified.
  /// Implements IList and IEnumerable for full list functionality with events.
  /// </summary>
  /// <typeparam name="T">The type of items stored in the list.</typeparam>
  [CreateAssetMenu(fileName = "NewObservableList", menuName = "Sets/Observable List", order = 0)]
  public class ObservableList<T> : ObservableCollectionBase, IList<T>, IEnumerable<T> {
    /// <summary>
    /// The underlying list that stores the items.
    /// </summary>
    [SerializeField]
    private List<T> items = new List<T>();

    /// <summary>
    /// Event invoked when an item is added to the list.
    /// </summary>
    public Action<T, int> itemAdded;

    /// <summary>
    /// Event invoked when an item is removed from the list.
    /// </summary>
    public Action<T, int> itemRemoved;

    /// <summary>
    /// Event invoked when an item is inserted into the list.
    /// </summary>
    public Action<T, int> itemInserted;

    /// <summary>
    /// Event invoked when an item is modified in the list.
    /// </summary>
    public Action<T, int> itemModified;

    /// <summary>
    /// Event invoked when the list is cleared.
    /// </summary>
    public Action listCleared;

    /// <summary>
    /// Gets or sets the item at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the item to get or set.</param>
    /// <returns>The item at the specified index.</returns>
    public T this[int index] {
      get => items[index];
      set {
        T oldItem = items[index];
        items[index] = value;
        itemModified?.Invoke(value, index);
      }
    }

    /// <summary>
    /// Gets the number of items in the list.
    /// </summary>
    public int Count => items.Count;

    /// <summary>
    /// Gets a value indicating whether the list is read-only.
    /// </summary>
    public bool IsReadOnly => false;

    /// <summary>
    /// Adds an item to the end of the list.
    /// </summary>
    /// <param name="item">The item to add.</param>
    public void Add(T item) {
      items.Add(item);
      itemAdded?.Invoke(item, items.Count - 1);
    }

    /// <summary>
    /// Inserts an item at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index at which the item should be inserted.</param>
    /// <param name="item">The item to insert.</param>
    public void Insert(int index, T item) {
      items.Insert(index, item);
      itemInserted?.Invoke(item, index);
    }

    /// <summary>
    /// Removes the first occurrence of the specified item from the list.
    /// </summary>
    /// <param name="item">The item to remove.</param>
    /// <returns>True if the item was successfully removed, false otherwise.</returns>
    public bool Remove(T item) {
      int index = items.IndexOf(item);
      if (index >= 0) {
        items.RemoveAt(index);
        itemRemoved?.Invoke(item, index);
        return true;
      }
      return false;
    }

    /// <summary>
    /// Removes the item at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the item to remove.</param>
    public void RemoveAt(int index) {
      T item = items[index];
      items.RemoveAt(index);
      itemRemoved?.Invoke(item, index);
    }

    /// <summary>
    /// Removes all items from the list.
    /// </summary>
    public void Clear() {
      items.Clear();
      listCleared?.Invoke();
    }

    /// <summary>
    /// Determines whether the list contains a specific item.
    /// </summary>
    /// <param name="item">The item to locate.</param>
    /// <returns>True if the item is found, false otherwise.</returns>
    public bool Contains(T item) {
      return items.Contains(item);
    }

    /// <summary>
    /// Searches for the specified item and returns the zero-based index of the first occurrence.
    /// </summary>
    /// <param name="item">The item to locate.</param>
    /// <returns>The zero-based index of the first occurrence, or -1 if not found.</returns>
    public int IndexOf(T item) {
      return items.IndexOf(item);
    }

    /// <summary>
    /// Copies the elements of the list to an array, starting at a particular array index.
    /// </summary>
    /// <param name="array">The one-dimensional array that is the destination.</param>
    /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
    public void CopyTo(T[] array, int arrayIndex) {
      items.CopyTo(array, arrayIndex);
    }

    /// <summary>
    /// Returns an enumerator that iterates through the list.
    /// </summary>
    /// <returns>An enumerator for the list.</returns>
    public IEnumerator<T> GetEnumerator() {
      return items.GetEnumerator();
    }

    /// <summary>
    /// Returns a non-generic enumerator for the list.
    /// </summary>
    /// <returns>A non-generic enumerator.</returns>
    IEnumerator IEnumerable.GetEnumerator() {
      return GetEnumerator();
    }

    // Editor support methods
    public override int GetCount() {
      return Count;
    }

    public override object GetItemAt(int index) {
      if (index >= 0 && index < items.Count) {
        return items[index];
      }
      return null;
    }

    public override void ClearItems() {
      Clear();
    }
  }
}