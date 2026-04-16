# API Reference - Observable Collections

This document provides a comprehensive reference for all observable collection types in the Unity ScriptableObjects library.

## ObservableCollectionBase

Base class for all observable collections providing editor support methods.

### Methods

#### `int GetCount()`
Returns the number of items in the collection.

#### `object GetItemAt(int index)`
Returns the item at the specified index, or null if out of range.

#### `void ClearItems()`
Clears all items from the collection.

---

## ObservableList&lt;T&gt;

A ScriptableObject-based observable list implementing `IList<T>` and `IEnumerable<T>` with reactive events.

### Events

#### `Action<T, int> itemAdded`
Fired when an item is added to the list. Parameters: (item, index)

#### `Action<T, int> itemRemoved`
Fired when an item is removed from the list. Parameters: (item, index)

#### `Action<T, int> itemInserted`
Fired when an item is inserted into the list. Parameters: (item, index)

#### `Action<T, int> itemModified`
Fired when an item is modified in the list. Parameters: (item, index)

#### `Action listCleared`
Fired when the list is cleared.

### Properties

#### `T this[int index] { get; set; }`
Gets or sets the item at the specified index.

#### `int Count { get; }`
Gets the number of items in the list.

#### `bool IsReadOnly { get; }`
Always returns false.

### Methods

#### `void Add(T item)`
Adds an item to the end of the list.

#### `void Insert(int index, T item)`
Inserts an item at the specified index.

#### `bool Remove(T item)`
Removes the first occurrence of the specified item.

#### `void RemoveAt(int index)`
Removes the item at the specified index.

#### `void Clear()`
Removes all items from the list.

#### `bool Contains(T item)`
Determines whether the list contains the specified item.

#### `int IndexOf(T item)`
Returns the index of the first occurrence of the item.

#### `void CopyTo(T[] array, int arrayIndex)`
Copies the elements to an array starting at the specified index.

#### `IEnumerator<T> GetEnumerator()`
Returns an enumerator for the list.

---

## ObservableHashSet&lt;T&gt;

A ScriptableObject-based observable hash set implementing `ISet<T>` and `IEnumerable<T>` with reactive events.

### Events

#### `Action<T> itemAdded`
Fired when an item is added to the set.

#### `Action<T> itemRemoved`
Fired when an item is removed from the set.

#### `Action setCleared`
Fired when the set is cleared.

### Properties

#### `int Count { get; }`
Gets the number of items in the set.

#### `bool IsReadOnly { get; }`
Always returns false.

### Methods

#### `bool Add(T item)`
Adds an item to the set. Returns true if added, false if already present.

#### `bool Remove(T item)`
Removes the specified item from the set.

#### `void Clear()`
Removes all items from the set.

#### `bool Contains(T item)`
Determines whether the set contains the specified item.

#### `void CopyTo(T[] array, int arrayIndex)`
Copies the elements to an array starting at the specified index.

#### `void UnionWith(IEnumerable<T> other)`
Modifies the set to contain all elements present in itself or the other collection.

#### `void IntersectWith(IEnumerable<T> other)`
Modifies the set to contain only elements present in both collections.

#### `void ExceptWith(IEnumerable<T> other)`
Removes all elements in the other collection from this set.

#### `void SymmetricExceptWith(IEnumerable<T> other)`
Modifies the set to contain only elements present in either set but not both.

#### `bool IsSubsetOf(IEnumerable<T> other)`
Determines whether the set is a subset of the other collection.

#### `bool IsSupersetOf(IEnumerable<T> other)`
Determines whether the set is a superset of the other collection.

#### `bool Overlaps(IEnumerable<T> other)`
Determines whether the set overlaps with the other collection.

#### `bool SetEquals(IEnumerable<T> other)`
Determines whether the set contains the same elements as the other collection.

#### `IEnumerator<T> GetEnumerator()`
Returns an enumerator for the set.

---

## ObservableSortedSet&lt;T&gt;

A ScriptableObject-based observable sorted set implementing `ISet<T>` and `IEnumerable<T>` with automatic sorting.

### Events

#### `Action<T> itemAdded`
Fired when an item is added to the set.

#### `Action<T> itemRemoved`
Fired when an item is removed from the set.

#### `Action setCleared`
Fired when the set is cleared.

### Properties

#### `int Count { get; }`
Gets the number of items in the set.

#### `bool IsReadOnly { get; }`
Always returns false.

#### `T Min { get; }`
Gets the minimum value in the set.

#### `T Max { get; }`
Gets the maximum value in the set.

### Methods

#### `bool Add(T item)`
Adds an item to the set. Returns true if added, false if already present.

#### `bool Remove(T item)`
Removes the specified item from the set.

#### `void Clear()`
Removes all items from the set.

#### `bool Contains(T item)`
Determines whether the set contains the specified item.

#### `void CopyTo(T[] array, int arrayIndex)`
Copies the elements to an array starting at the specified index.

#### `void UnionWith(IEnumerable<T> other)`
Modifies the set to contain all elements present in itself or the other collection.

#### `void IntersectWith(IEnumerable<T> other)`
Modifies the set to contain only elements present in both collections.

#### `void ExceptWith(IEnumerable<T> other)`
Removes all elements in the other collection from this set.

#### `void SymmetricExceptWith(IEnumerable<T> other)`
Modifies the set to contain only elements present in either set but not both.

#### `bool IsSubsetOf(IEnumerable<T> other)`
Determines whether the set is a subset of the other collection.

#### `bool IsSupersetOf(IEnumerable<T> other)`
Determines whether the set is a superset of the other collection.

#### `bool Overlaps(IEnumerable<T> other)`
Determines whether the set overlaps with the other collection.

#### `bool SetEquals(IEnumerable<T> other)`
Determines whether the set contains the same elements as the other collection.

#### `SortedSet<T> GetViewBetween(T lowerValue, T upperValue)`
Returns a view of a subset within the specified range.

#### `T RemoveMin()`
Removes and returns the minimum value.

#### `T RemoveMax()`
Removes and returns the maximum value.

#### `IEnumerator<T> GetEnumerator()`
Returns an enumerator for the set.

---

## ObservableLinkedList&lt;T&gt;

A ScriptableObject-based observable doubly-linked list implementing `IEnumerable<T>` with node-based operations.

### Events

#### `Action<T> itemAdded`
Fired when an item is added to the list.

#### `Action<T> itemRemoved`
Fired when an item is removed from the list.

#### `Action listCleared`
Fired when the list is cleared.

### Properties

#### `int Count { get; }`
Gets the number of items in the list.

#### `LinkedListNode<T> First { get; }`
Gets the first node of the list.

#### `LinkedListNode<T> Last { get; }`
Gets the last node of the list.

### Methods

#### `LinkedListNode<T> AddLast(T value)`
Adds an item to the end of the list.

#### `LinkedListNode<T> AddFirst(T value)`
Adds an item to the beginning of the list.

#### `LinkedListNode<T> AddBefore(LinkedListNode<T> node, T value)`
Adds an item before the specified node.

#### `LinkedListNode<T> AddAfter(LinkedListNode<T> node, T value)`
Adds an item after the specified node.

#### `bool Remove(T value)`
Removes the first occurrence of the specified value.

#### `void Remove(LinkedListNode<T> node)`
Removes the specified node.

#### `void RemoveFirst()`
Removes the first node.

#### `void RemoveLast()`
Removes the last node.

#### `void Clear()`
Removes all items from the list.

#### `bool Contains(T value)`
Determines whether the list contains the specified value.

#### `LinkedListNode<T> Find(T value)`
Finds the first node containing the specified value.

#### `LinkedListNode<T> FindLast(T value)`
Finds the last node containing the specified value.

#### `void CopyTo(T[] array, int index)`
Copies the elements to an array starting at the specified index.

#### `IEnumerator<T> GetEnumerator()`
Returns an enumerator for the list.

---

## ObservableQueue&lt;T&gt;

A ScriptableObject-based observable FIFO queue implementing `IEnumerable<T>` with reactive events.

### Events

#### `Action<T> itemEnqueued`
Fired when an item is added to the queue.

#### `Action<T> itemDequeued`
Fired when an item is removed from the queue.

#### `Action queueCleared`
Fired when the queue is cleared.

### Properties

#### `int Count { get; }`
Gets the number of items in the queue.

### Methods

#### `void Enqueue(T item)`
Adds an item to the end of the queue.

#### `T Dequeue()`
Removes and returns the item at the front of the queue.

#### `T Peek()`
Returns the item at the front without removing it.

#### `bool TryDequeue(out T result)`
Attempts to dequeue an item.

#### `bool TryPeek(out T result)`
Attempts to peek at the front item.

#### `void Clear()`
Removes all items from the queue.

#### `bool Contains(T item)`
Determines whether the queue contains the specified item.

#### `void CopyTo(T[] array, int arrayIndex)`
Copies the elements to an array.

#### `T[] ToArray()`
Copies the queue to a new array.

#### `void TrimExcess()`
Sets the capacity to the actual number of elements.

#### `IEnumerator<T> GetEnumerator()`
Returns an enumerator for the queue.

---

## ObservableStack&lt;T&gt;

A ScriptableObject-based observable LIFO stack implementing `IEnumerable<T>` with reactive events.

### Events

#### `Action<T> itemPushed`
Fired when an item is pushed to the stack.

#### `Action<T> itemPopped`
Fired when an item is popped from the stack.

#### `Action stackCleared`
Fired when the stack is cleared.

### Properties

#### `int Count { get; }`
Gets the number of items in the stack.

### Methods

#### `void Push(T item)`
Adds an item to the top of the stack.

#### `T Pop()`
Removes and returns the item at the top of the stack.

#### `T Peek()`
Returns the item at the top without removing it.

#### `bool TryPop(out T result)`
Attempts to pop an item.

#### `bool TryPeek(out T result)`
Attempts to peek at the top item.

#### `void Clear()`
Removes all items from the stack.

#### `bool Contains(T item)`
Determines whether the stack contains the specified item.

#### `void CopyTo(T[] array, int arrayIndex)`
Copies the elements to an array.

#### `T[] ToArray()`
Copies the stack to a new array.

#### `void TrimExcess()`
Sets the capacity to the actual number of elements.

#### `IEnumerator<T> GetEnumerator()`
Returns an enumerator for the stack.

---

## Common Patterns

### Event Subscription
```csharp
public class MyComponent : MonoBehaviour {
    public ObservableList<Item> inventory;

    void Start() {
        inventory.itemAdded += OnItemAdded;
        inventory.itemRemoved += OnItemRemoved;
    }

    void OnItemAdded(Item item, int index) {
        Debug.Log($"Added {item.name} at index {index}");
    }

    void OnItemRemoved(Item item, int index) {
        Debug.Log($"Removed {item.name} from index {index}");
    }
}
```

### Editor Operations
All observable collections support runtime inspection and modification through custom editors:
- View current count and items
- Clear all items with a button
- Real-time updates in the Inspector

### Serialization
All collections are fully serializable as Unity ScriptableObjects and can be saved as assets or used at runtime.