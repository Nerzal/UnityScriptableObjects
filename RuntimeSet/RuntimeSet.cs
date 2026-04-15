using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
//
// Author: Ryan Hipple
// Date:   10/04/17
// Modified for LD42 Jam
// Author: Nerzal
// ----------------------------------------------------------------------------

namespace Sets {
  /// <summary>
  /// Abstract ScriptableObject representing a runtime set (collection) of items of type T.
  /// Provides methods for adding, removing, and managing items, with event notifications.
  /// Implements IEnumerable for iteration.
  /// </summary>
  /// <typeparam name="T">The type of items stored in the set.</typeparam>
  public abstract class RuntimeSet<T> : ScriptableObject, IEnumerable<T> {
    /// <summary>
    /// The list of items in the set.
    /// </summary>
    public List<T> items = new List<T>();

    /// <summary>
    /// The current index, typically used for tracking additions.
    /// </summary>
    public int index = -1;

    /// <summary>
    /// Event invoked when the items collection changes (add, remove, clear, initialize).
    /// </summary>
    public Action itemsChanged;

    /// <summary>
    /// Indexer to access items by index.
    /// </summary>
    /// <param name="i">The index of the item.</param>
    /// <returns>The item at the specified index.</returns>
    public T this[int i] {
      get { return this.items[i]; }
      set { this.items[i] = value; }
    }

    /// <summary>
    /// Initializes the set by clearing items and resetting the index.
    /// Invokes itemsChanged event.
    /// </summary>
    public void Initialize() {
      this.items = new List<T>();
      this.index = -1;
      this.Clear();
      this.itemsChanged?.Invoke();
    }

    /// <summary>
    /// Adds an item to the set if it's not already present.
    /// Increments the index and invokes itemsChanged.
    /// </summary>
    /// <param name="thing">The item to add.</param>
    /// <exception cref="ArgumentNullException">Thrown if thing is null.</exception>
    public virtual void Add(T thing) {
      if (thing == null) {
        throw new ArgumentNullException(nameof(thing));
      }
      if (this.items.Contains(thing)) {
        return;
      }

      this.items.Add(thing);
      this.index++;
      this.itemsChanged?.Invoke();
    }

    /// <summary>
    /// Adds a range of items to the set.
    /// Invokes itemsChanged after adding.
    /// </summary>
    /// <param name="things">The items to add.</param>
    /// <exception cref="ArgumentNullException">Thrown if things is null.</exception>
    public virtual void AddRange(IEnumerable<T> things) {
      if (things == null) {
        throw new ArgumentNullException(nameof(things));
      }
      this.items.AddRange(things);
      this.itemsChanged?.Invoke();
    }

    /// <summary>
    /// Removes an item from the set if present.
    /// Decrements the index and invokes itemsChanged.
    /// </summary>
    /// <param name="thing">The item to remove.</param>
    /// <exception cref="ArgumentNullException">Thrown if thing is null.</exception>
    public virtual void Remove(T thing) {
      if (thing == null) {
        throw new ArgumentNullException(nameof(thing));
      }
      if (!this.items.Contains(thing)) {
        return;
      }

      this.items.Remove(thing);
      this.index--;
      this.itemsChanged?.Invoke();
    }

    /// <summary>
    /// Clears all items from the set and invokes itemsChanged.
    /// </summary>
    public virtual void Clear() {
      this.items.Clear();
      this.itemsChanged?.Invoke();
    }

    /// <summary>
    /// Checks if the set contains the specified item.
    /// </summary>
    /// <param name="item">The item to check for.</param>
    /// <returns>True if the item is in the set, false otherwise.</returns>
    public bool Contains(T item) {
      foreach (T item1 in this.items) {
        if (item1.Equals(item)) {
          return true;
        }
      }

      return false;
    }

    /// <summary>
    /// Returns the number of items in the set.
    /// </summary>
    /// <returns>The count of items.</returns>
    public int Count() {
      return this.items.Count;
    }

    /// <summary>
    /// Returns an enumerator for iterating over the items.
    /// </summary>
    /// <returns>An enumerator for the items.</returns>
    public IEnumerator<T> GetEnumerator() {
      return this.items.GetEnumerator();
    }

    /// <summary>
    /// Returns a non-generic enumerator for the items.
    /// </summary>
    /// <returns>A non-generic enumerator.</returns>
    IEnumerator IEnumerable.GetEnumerator() {
      return GetEnumerator();
    }
  }
}