using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sets {
  /// <summary>
  /// A ScriptableObject-based observable queue that notifies listeners when items are enqueued or dequeued.
  /// Implements IEnumerable for iteration and provides events for reactive programming.
  /// </summary>
  /// <typeparam name="T">The type of items stored in the queue.</typeparam>
  [CreateAssetMenu(fileName = "NewObservableQueue", menuName = "Sets/Observable Queue", order = 0)]
  public class ObservableQueue<T> : ObservableCollectionBase, IEnumerable<T> {
    /// <summary>
    /// The underlying queue that stores the items.
    /// </summary>
    [SerializeField]
    private List<T> items = new List<T>();

    /// <summary>
    /// Event invoked when an item is enqueued.
    /// </summary>
    public Action<T> itemEnqueued;

    /// <summary>
    /// Event invoked when an item is dequeued.
    /// </summary>
    public Action<T> itemDequeued;

    /// <summary>
    /// Event invoked when the queue is cleared.
    /// </summary>
    public Action queueCleared;

    /// <summary>
    /// Gets the number of items in the queue.
    /// </summary>
    public int Count => items.Count;

    /// <summary>
    /// Gets a value indicating whether the queue is empty.
    /// </summary>
    public bool IsEmpty => items.Count == 0;

    /// <summary>
    /// Adds an item to the end of the queue.
    /// </summary>
    /// <param name="item">The item to enqueue.</param>
    public void Enqueue(T item) {
      items.Add(item);
      itemEnqueued?.Invoke(item);
    }

    /// <summary>
    /// Removes and returns the item at the front of the queue.
    /// </summary>
    /// <returns>The item that was dequeued.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the queue is empty.</exception>
    public T Dequeue() {
      if (IsEmpty) {
        throw new InvalidOperationException("Queue is empty");
      }

      T item = items[0];
      items.RemoveAt(0);
      itemDequeued?.Invoke(item);
      return item;
    }

    /// <summary>
    /// Returns the item at the front of the queue without removing it.
    /// </summary>
    /// <returns>The item at the front of the queue.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the queue is empty.</exception>
    public T Peek() {
      if (IsEmpty) {
        throw new InvalidOperationException("Queue is empty");
      }

      return items[0];
    }

    /// <summary>
    /// Attempts to dequeue an item from the queue.
    /// </summary>
    /// <param name="result">The dequeued item, or default if the queue is empty.</param>
    /// <returns>True if an item was dequeued, false otherwise.</returns>
    public bool TryDequeue(out T result) {
      if (IsEmpty) {
        result = default;
        return false;
      }

      result = Dequeue();
      return true;
    }

    /// <summary>
    /// Attempts to peek at the front item without removing it.
    /// </summary>
    /// <param name="result">The front item, or default if the queue is empty.</param>
    /// <returns>True if there is a front item, false otherwise.</returns>
    public bool TryPeek(out T result) {
      if (IsEmpty) {
        result = default;
        return false;
      }

      result = Peek();
      return true;
    }

    /// <summary>
    /// Removes all items from the queue.
    /// </summary>
    public void Clear() {
      items.Clear();
      queueCleared?.Invoke();
    }

    /// <summary>
    /// Determines whether the queue contains a specific item.
    /// </summary>
    /// <param name="item">The item to locate.</param>
    /// <returns>True if the item is found, false otherwise.</returns>
    public bool Contains(T item) {
      return items.Contains(item);
    }

    /// <summary>
    /// Returns an enumerator that iterates through the queue.
    /// </summary>
    /// <returns>An enumerator for the queue.</returns>
    public IEnumerator<T> GetEnumerator() {
      return items.GetEnumerator();
    }

    /// <summary>
    /// Returns a non-generic enumerator for the queue.
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