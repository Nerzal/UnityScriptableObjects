using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sets {
  /// <summary>
  /// A ScriptableObject-based observable linked list that notifies listeners when items are added, removed, or modified.
  /// Implements ILinkedList and IEnumerable for full linked list functionality with events.
  /// </summary>
  /// <typeparam name="T">The type of items stored in the linked list.</typeparam>
  [CreateAssetMenu(fileName = "NewObservableLinkedList", menuName = "Sets/Observable LinkedList", order = 0)]
  public class ObservableLinkedList<T> : ObservableCollectionBase, IEnumerable<T> {
    /// <summary>
    /// The underlying linked list that stores the items.
    /// </summary>
    [SerializeField]
    private LinkedList<T> items = new LinkedList<T>();

    /// <summary>
    /// Event invoked when an item is added to the list.
    /// </summary>
    public Action<T> itemAdded;

    /// <summary>
    /// Event invoked when an item is removed from the list.
    /// </summary>
    public Action<T> itemRemoved;

    /// <summary>
    /// Event invoked when the list is cleared.
    /// </summary>
    public Action listCleared;

    /// <summary>
    /// Gets the number of items in the list.
    /// </summary>
    public int Count => items.Count;

    /// <summary>
    /// Gets the first node of the list.
    /// </summary>
    public LinkedListNode<T> First => items.First;

    /// <summary>
    /// Gets the last node of the list.
    /// </summary>
    public LinkedListNode<T> Last => items.Last;

    /// <summary>
    /// Adds an item to the end of the list.
    /// </summary>
    /// <param name="value">The item to add.</param>
    /// <returns>The LinkedListNode containing the added item.</returns>
    public LinkedListNode<T> AddLast(T value) {
      LinkedListNode<T> node = items.AddLast(value);
      itemAdded?.Invoke(value);
      return node;
    }

    /// <summary>
    /// Adds an item to the beginning of the list.
    /// </summary>
    /// <param name="value">The item to add.</param>
    /// <returns>The LinkedListNode containing the added item.</returns>
    public LinkedListNode<T> AddFirst(T value) {
      LinkedListNode<T> node = items.AddFirst(value);
      itemAdded?.Invoke(value);
      return node;
    }

    /// <summary>
    /// Adds an item before the specified node.
    /// </summary>
    /// <param name="node">The node before which to insert the new item.</param>
    /// <param name="value">The item to add.</param>
    /// <returns>The LinkedListNode containing the added item.</returns>
    public LinkedListNode<T> AddBefore(LinkedListNode<T> node, T value) {
      LinkedListNode<T> newNode = items.AddBefore(node, value);
      itemAdded?.Invoke(value);
      return newNode;
    }

    /// <summary>
    /// Adds an item after the specified node.
    /// </summary>
    /// <param name="node">The node after which to insert the new item.</param>
    /// <param name="value">The item to add.</param>
    /// <returns>The LinkedListNode containing the added item.</returns>
    public LinkedListNode<T> AddAfter(LinkedListNode<T> node, T value) {
      LinkedListNode<T> newNode = items.AddAfter(node, value);
      itemAdded?.Invoke(value);
      return newNode;
    }

    /// <summary>
    /// Removes the first occurrence of the specified item from the list.
    /// </summary>
    /// <param name="value">The item to remove.</param>
    /// <returns>True if the item was successfully removed, false otherwise.</returns>
    public bool Remove(T value) {
      if (items.Remove(value)) {
        itemRemoved?.Invoke(value);
        return true;
      }
      return false;
    }

    /// <summary>
    /// Removes the specified node from the list.
    /// </summary>
    /// <param name="node">The node to remove.</param>
    public void Remove(LinkedListNode<T> node) {
      T value = node.Value;
      items.Remove(node);
      itemRemoved?.Invoke(value);
    }

    /// <summary>
    /// Removes the first node from the list.
    /// </summary>
    public void RemoveFirst() {
      if (items.First != null) {
        T value = items.First.Value;
        items.RemoveFirst();
        itemRemoved?.Invoke(value);
      }
    }

    /// <summary>
    /// Removes the last node from the list.
    /// </summary>
    public void RemoveLast() {
      if (items.Last != null) {
        T value = items.Last.Value;
        items.RemoveLast();
        itemRemoved?.Invoke(value);
      }
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
    /// <param name="value">The item to locate.</param>
    /// <returns>True if the item is found, false otherwise.</returns>
    public bool Contains(T value) {
      return items.Contains(value);
    }

    /// <summary>
    /// Finds the first node that contains the specified value.
    /// </summary>
    /// <param name="value">The value to locate.</param>
    /// <returns>The first LinkedListNode that contains the specified value, if found; otherwise, null.</returns>
    public LinkedListNode<T> Find(T value) {
      return items.Find(value);
    }

    /// <summary>
    /// Finds the last node that contains the specified value.
    /// </summary>
    /// <param name="value">The value to locate.</param>
    /// <returns>The last LinkedListNode that contains the specified value, if found; otherwise, null.</returns>
    public LinkedListNode<T> FindLast(T value) {
      return items.FindLast(value);
    }

    /// <summary>
    /// Copies the elements of the list to an array, starting at a particular array index.
    /// </summary>
    /// <param name="array">The one-dimensional array that is the destination.</param>
    /// <param name="index">The zero-based index in array at which copying begins.</param>
    public void CopyTo(T[] array, int index) {
      items.CopyTo(array, index);
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
      // LinkedList doesn't have indexing, so we convert to list for editor display
      if (index >= 0 && index < items.Count) {
        List<T> list = new List<T>(items);
        return list[index];
      }
      return null;
    }

    public override void ClearItems() {
      Clear();
    }
  }
}