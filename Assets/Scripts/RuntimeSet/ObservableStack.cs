using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sets {
  /// <summary>
  /// A ScriptableObject-based observable stack that notifies listeners when items are pushed or popped.
  /// Implements IEnumerable for iteration and provides events for reactive programming.
  /// </summary>
  /// <typeparam name="T">The type of items stored in the stack.</typeparam>
  [CreateAssetMenu(fileName = "NewObservableStack", menuName = "Sets/Observable Stack", order = 0)]
  public class ObservableStack<T> : ObservableCollectionBase, IEnumerable<T> {
    /// <summary>
    /// The underlying list that stores the stack items (last item is top).
    /// </summary>
    [SerializeField]
    private List<T> items = new List<T>();

    /// <summary>
    /// Event invoked when an item is pushed onto the stack.
    /// </summary>
    public Action<T> itemPushed;

    /// <summary>
    /// Event invoked when an item is popped from the stack.
    /// </summary>
    public Action<T> itemPopped;

    /// <summary>
    /// Event invoked when the stack is cleared.
    /// </summary>
    public Action stackCleared;

    /// <summary>
    /// Gets the number of items in the stack.
    /// </summary>
    public int Count => items.Count;

    /// <summary>
    /// Gets a value indicating whether the stack is empty.
    /// </summary>
    public bool IsEmpty => items.Count == 0;

    /// <summary>
    /// Inserts an item at the top of the stack.
    /// </summary>
    /// <param name="item">The item to push onto the stack.</param>
    public void Push(T item) {
      items.Add(item);
      itemPushed?.Invoke(item);
    }

    /// <summary>
    /// Removes and returns the item at the top of the stack.
    /// </summary>
    /// <returns>The item that was popped.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the stack is empty.</exception>
    public T Pop() {
      if (IsEmpty) {
        throw new InvalidOperationException("Stack is empty");
      }

      int lastIndex = items.Count - 1;
      T item = items[lastIndex];
      items.RemoveAt(lastIndex);
      itemPopped?.Invoke(item);
      return item;
    }

    /// <summary>
    /// Returns the item at the top of the stack without removing it.
    /// </summary>
    /// <returns>The item at the top of the stack.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the stack is empty.</exception>
    public T Peek() {
      if (IsEmpty) {
        throw new InvalidOperationException("Stack is empty");
      }

      return items[items.Count - 1];
    }

    /// <summary>
    /// Attempts to pop an item from the stack.
    /// </summary>
    /// <param name="result">The popped item, or default if the stack is empty.</param>
    /// <returns>True if an item was popped, false otherwise.</returns>
    public bool TryPop(out T result) {
      if (IsEmpty) {
        result = default;
        return false;
      }

      result = Pop();
      return true;
    }

    /// <summary>
    /// Attempts to peek at the top item without removing it.
    /// </summary>
    /// <param name="result">The top item, or default if the stack is empty.</param>
    /// <returns>True if there is a top item, false otherwise.</returns>
    public bool TryPeek(out T result) {
      if (IsEmpty) {
        result = default;
        return false;
      }

      result = Peek();
      return true;
    }

    /// <summary>
    /// Removes all items from the stack.
    /// </summary>
    public void Clear() {
      items.Clear();
      stackCleared?.Invoke();
    }

    /// <summary>
    /// Determines whether the stack contains a specific item.
    /// </summary>
    /// <param name="item">The item to locate.</param>
    /// <returns>True if the item is found, false otherwise.</returns>
    public bool Contains(T item) {
      return items.Contains(item);
    }

    /// <summary>
    /// Returns an enumerator that iterates through the stack (top to bottom).
    /// </summary>
    /// <returns>An enumerator for the stack.</returns>
    public IEnumerator<T> GetEnumerator() {
      // Return items in reverse order (stack order: top first)
      for (int i = items.Count - 1; i >= 0; i--) {
        yield return items[i];
      }
    }

    /// <summary>
    /// Returns a non-generic enumerator for the stack.
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
        // Return in display order (top to bottom)
        return items[items.Count - 1 - index];
      }
      return null;
    }

    public override void ClearItems() {
      Clear();
    }
  }
}