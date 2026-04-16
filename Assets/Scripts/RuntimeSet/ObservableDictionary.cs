using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sets {
  /// <summary>
  /// A ScriptableObject-based observable dictionary that notifies listeners when items are added, removed, or modified.
  /// Implements IEnumerable for iteration and provides events for reactive programming.
  /// </summary>
  /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
  /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
  [CreateAssetMenu(fileName = "NewObservableDictionary", menuName = "Sets/Observable Dictionary", order = 0)]
  public class ObservableDictionary<TKey, TValue> : ObservableDictionaryBase, IEnumerable<KeyValuePair<TKey, TValue>> {
    /// <summary>
    /// The underlying dictionary that stores the key-value pairs.
    /// </summary>
    [SerializeField]
    private List<TKey> keys = new List<TKey>();

    [SerializeField]
    private List<TValue> values = new List<TValue>();

    /// <summary>
    /// Event invoked when an item is added to the dictionary.
    /// </summary>
    public Action<TKey, TValue> itemAdded;

    /// <summary>
    /// Event invoked when an item is removed from the dictionary.
    /// </summary>
    public Action<TKey, TValue> itemRemoved;

    /// <summary>
    /// Event invoked when an item is modified in the dictionary.
    /// </summary>
    public Action<TKey, TValue> itemModified;

    /// <summary>
    /// Event invoked when the dictionary is cleared.
    /// </summary>
    public Action dictionaryCleared;

    /// <summary>
    /// Gets or sets the value associated with the specified key.
    /// </summary>
    /// <param name="key">The key of the value to get or set.</param>
    /// <returns>The value associated with the specified key.</returns>
    public TValue this[TKey key] {
      get {
        int index = keys.IndexOf(key);
        if (index >= 0) {
          return values[index];
        }
        throw new KeyNotFoundException($"Key '{key}' not found in dictionary.");
      }
      set {
        int index = keys.IndexOf(key);
        if (index >= 0) {
          TValue oldValue = values[index];
          values[index] = value;
          itemModified?.Invoke(key, value);
        } else {
          Add(key, value);
        }
      }
    }

    /// <summary>
    /// Gets the number of key-value pairs in the dictionary.
    /// </summary>
    public int Count => keys.Count;

    /// <summary>
    /// Gets a collection containing the keys in the dictionary.
    /// </summary>
    public IEnumerable<TKey> Keys => keys;

    /// <summary>
    /// Gets a collection containing the values in the dictionary.
    /// </summary>
    public IEnumerable<TValue> Values => values;

    /// <summary>
    /// Adds the specified key and value to the dictionary.
    /// </summary>
    /// <param name="key">The key of the element to add.</param>
    /// <param name="value">The value of the element to add.</param>
    public void Add(TKey key, TValue value) {
      if (keys.Contains(key)) {
        throw new ArgumentException($"Key '{key}' already exists in dictionary.");
      }

      keys.Add(key);
      values.Add(value);
      itemAdded?.Invoke(key, value);
    }

    /// <summary>
    /// Removes the value with the specified key from the dictionary.
    /// </summary>
    /// <param name="key">The key of the element to remove.</param>
    /// <returns>True if the element was successfully removed, false otherwise.</returns>
    public bool Remove(TKey key) {
      int index = keys.IndexOf(key);
      if (index >= 0) {
        TValue removedValue = values[index];
        keys.RemoveAt(index);
        values.RemoveAt(index);
        itemRemoved?.Invoke(key, removedValue);
        return true;
      }
      return false;
    }

    /// <summary>
    /// Determines whether the dictionary contains the specified key.
    /// </summary>
    /// <param name="key">The key to locate in the dictionary.</param>
    /// <returns>True if the dictionary contains an element with the specified key, false otherwise.</returns>
    public bool ContainsKey(TKey key) {
      return keys.Contains(key);
    }

    /// <summary>
    /// Attempts to get the value associated with the specified key.
    /// </summary>
    /// <param name="key">The key of the value to get.</param>
    /// <param name="value">When this method returns, contains the value associated with the specified key, if the key is found; otherwise, the default value for the type of the value parameter.</param>
    /// <returns>True if the dictionary contains an element with the specified key, false otherwise.</returns>
    public bool TryGetValue(TKey key, out TValue value) {
      int index = keys.IndexOf(key);
      if (index >= 0) {
        value = values[index];
        return true;
      }
      value = default;
      return false;
    }

    /// <summary>
    /// Removes all keys and values from the dictionary.
    /// </summary>
    public void Clear() {
      keys.Clear();
      values.Clear();
      dictionaryCleared?.Invoke();
    }

    /// <summary>
    /// Returns an enumerator that iterates through the dictionary.
    /// </summary>
    /// <returns>An enumerator for the dictionary.</returns>
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() {
      for (int i = 0; i < keys.Count; i++) {
        yield return new KeyValuePair<TKey, TValue>(keys[i], values[i]);
      }
    }

    /// <summary>
    /// Returns a non-generic enumerator for the dictionary.
    /// </summary>
    /// <returns>A non-generic enumerator.</returns>
    IEnumerator IEnumerable.GetEnumerator() {
      return GetEnumerator();
    }

    // Editor support methods
    public override int GetCount() {
      return Count;
    }

    public override KeyValuePair<object, object> GetItemAt(int index) {
      if (index >= 0 && index < keys.Count) {
        return new KeyValuePair<object, object>(keys[index], values[index]);
      }
      return default;
    }

    public override void ClearItems() {
      Clear();
    }
  }
}
