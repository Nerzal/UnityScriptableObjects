# UnityScriptableObjects

A reusable Unity library for ScriptableObject-backed variables, references, events and runtime collections. This package is designed to simplify data-driven workflows, decouple game logic, and make shared state easy to manage across scenes and systems.

📖 **[API Reference](API_REFERENCE.md)** - Complete documentation for all classes and methods

## Features

- ScriptableObject variables for common Unity data types
- Reference wrappers for constant or asset-based values
- Event assets for decoupled communication (sync and async)
- Runtime sets and observable dictionaries for dynamic collections
- Custom inspectors for enhanced editor experience
- Unity 6.4 compatible

## Supported Variable Types

- `IntVariable`
- `FloatVariable`
- `BoolVariable`
- `StringVariable`
- `DoubleVariable`
- `ColorVariable`
- `Vector2Variable`
- `Vector3Variable`
- `Vector4Variable`
- `QuaternionVariable`
- `LayerMaskVariable`
- `AnimationCurveVariable`
- `GameObjectVariable`
- `SpriteVariable`
- `TextureVariable`

## Supported Reference Types

- `IntReference`
- `FloatReference`
- `DoubleReference`
- `StringReference`
- `QuaternionReference`
- `LayerMaskReference`
- `AnimationCurveReference`
- `GameObjectReference`
- `ColorReference`
- `Vector4Reference`

## Supported Collection Types

- `RuntimeSet<T>` - Dynamic collection for runtime management
- `ObservableDictionary<TKey, TValue>` - Reactive key-value storage with events
- `ObservableQueue<T>` - FIFO queue with reactive events
- `ObservableStack<T>` - LIFO stack with reactive events
- `ObservableList<T>` - Observable list with full IList functionality
- `ObservableHashSet<T>` - Observable hash set with set operations
- `ObservableSortedSet<T>` - Observable sorted set with ordering
- `ObservableLinkedList<T>` - Observable doubly-linked list

## Variables vs References

### When to use Variables?

Use `*Variable` assets when you want to:

- Store shared game state centrally
- Have the same value read and written by multiple components or scenes
- Enable debugging and balancing through editor assets
- Make values available independently of specific MonoBehaviours

Examples:

- `PlayerHealth` as an `IntVariable`
- `CameraPosition` as a `Vector3Variable`
- `AbilityColor` as a `ColorVariable`

### When to use References?

Use `*Reference` when you want flexible value sources:

- Choose between a constant value or a shared variable asset value
- Quickly switch between fixed value and shared asset in the inspector
- Parameterize components without creating multiple specialized variable fields

Examples:

- `weaponDamage` as a `FloatReference` (constant or defined by `FloatVariable`)
- `spawnPosition` as a `Vector3Reference` (fixed or from shared asset)
- `targetLayer` as a `LayerMaskReference`

### Using both together?

The most common pattern is:

- Define a variable as an asset
- Use references in components to use either a fixed value or asset-bound value

This keeps flexible configuration while maintaining central values you can edit in the editor.

## Editor Features

### Reference Property Drawers

All `*Reference` types (like `FloatReference`, `IntReference`, etc.) have custom property drawers that provide:

- A "Constant" toggle to switch between fixed values and variable assets
- Clean, intuitive UI for configuring references in the Inspector

### Event Editors

- **GameEvent**: Button to raise the event for testing (both in Editor and Play Mode)
- **GenericGameEvent<T>**: Input field for test arguments + raise button
- **AsyncGameEvent** & **AsyncGenericGameEvent**: Similar controls for async events

### Collection Editors

- **RuntimeSet<T>**: Displays current items, count, and provides Clear/Initialize buttons
- **ObservableDictionary<TKey, TValue>**: Shows key-value pairs and Clear button
- **ObservableQueue<T>**: Shows queued items and Clear button
- **ObservableStack<T>**: Shows stacked items and Clear button
- **ObservableList<T>**: Shows list items with indices and Clear button
- **ObservableHashSet<T>**: Shows set items and Clear button
- **ObservableSortedSet<T>**: Shows sorted items and Clear button
- **ObservableLinkedList<T>**: Shows linked list items and Clear button

## Installation

### Unity Package Manager

Add the package to your project via `Packages/manifest.json` or a local package reference.

Example `manifest.json` entry:

```json
"com.noobygames.unityscriptableobjects": "https://github.com/Nerzal/UnityScriptableObjects.git#main"
```

### Direct import

You can also import this repository as a custom package folder in Unity by copying it into your project and adding it as a package reference.

## Getting Started

### Create a new variable asset

1. Open Unity Editor
2. Right-click in the Project window
3. Select `Create > Variables > Int` or any other supported type

### Example: Using an `IntVariable`

```csharp
using Variables;

public class HealthManager : MonoBehaviour {
    public IntVariable playerHealth;

    private void Start() {
        playerHealth.valueChanged += OnHealthChanged;
    }

    public void Damage(int amount) {
        playerHealth.ApplyChange(-amount);
    }

    private void OnHealthChanged(int newHealth) {
        Debug.Log($"Player health is now {newHealth}");
    }
}
```

### Example: Using a reference wrapper

```csharp
using Variables;

public class DamageDealer : MonoBehaviour {
    public FloatReference damageAmount;

    public void DealDamage() {
        float amount = damageAmount.Value;
        Debug.Log($"Dealing {amount} damage");
    }
}
```

This lets you choose between a constant value and a shared variable asset in the inspector.

## Example Usage Patterns

### Shared game state

Use a `GameObjectVariable` or `Vector3Variable` to store values that multiple systems should read or update.

### Decoupled logic with events

Create event assets and let listeners subscribe to them. This avoids direct component references and supports modular scenes.

### Dynamic runtime collections

Use `RuntimeSet<T>` assets for collections that should update at runtime, like active enemies, pickups, or spawned objects.

Use observable collections for reactive data structures that notify when items change:

- `ObservableDictionary<TKey, TValue>` for reactive key-value storage
- `ObservableQueue<T>` for FIFO operations with events
- `ObservableStack<T>` for LIFO operations with events
- `ObservableList<T>` for indexed list operations with events
- `ObservableHashSet<T>` for unique item sets with set operations
- `ObservableSortedSet<T>` for automatically sorted unique items
- `ObservableLinkedList<T>` for efficient insertions/deletions with node-based access

### Example: Using ObservableQueue

```csharp
using Sets;

public class TaskManager : MonoBehaviour {
    public ObservableQueue<string> taskQueue;

    private void Start() {
        taskQueue.itemEnqueued += OnTaskEnqueued;
        taskQueue.itemDequeued += OnTaskDequeued;
    }

    public void AddTask(string task) {
        taskQueue.Enqueue(task);
    }

    public void ProcessNextTask() {
        if (taskQueue.Count > 0) {
            string task = taskQueue.Dequeue();
            Debug.Log($"Processing: {task}");
        }
    }

    private void OnTaskEnqueued(string task) {
        Debug.Log($"Task queued: {task}");
    }

    private void OnTaskDequeued(string task) {
        Debug.Log($"Task dequeued: {task}");
    }
}
```

### Example: Using ObservableStack

```csharp
using Sets;

public class UndoManager : MonoBehaviour {
    public ObservableStack<Command> undoStack;

    private void Start() {
        undoStack.itemPushed += OnCommandPushed;
        undoStack.itemPopped += OnCommandPopped;
    }

    public void ExecuteCommand(Command command) {
        command.Execute();
        undoStack.Push(command);
    }

    public void Undo() {
        if (undoStack.Count > 0) {
            Command command = undoStack.Pop();
            command.Undo();
        }
    }

    private void OnCommandPushed(Command command) {
        Debug.Log($"Command pushed to undo stack: {command}");
    }

    private void OnCommandPopped(Command command) {
        Debug.Log($"Command popped from undo stack: {command}");
    }
}
```

### Example: Using ObservableList

```csharp
using Sets;

public class InventoryList : MonoBehaviour {
    public ObservableList<Item> inventory;

    private void Start() {
        inventory.itemAdded += OnItemAdded;
        inventory.itemRemoved += OnItemRemoved;
        inventory.itemModified += OnItemModified;
    }

    public void AddItem(Item item) {
        inventory.Add(item);
    }

    public void RemoveItem(Item item) {
        inventory.Remove(item);
    }

    public void UpgradeItem(int index, Item upgradedItem) {
        inventory[index] = upgradedItem;
    }

    private void OnItemAdded(Item item, int index) {
        Debug.Log($"Item added at index {index}: {item.name}");
    }

    private void OnItemRemoved(Item item, int index) {
        Debug.Log($"Item removed from index {index}: {item.name}");
    }

    private void OnItemModified(Item item, int index) {
        Debug.Log($"Item modified at index {index}: {item.name}");
    }
}
```

### Example: Using ObservableHashSet

```csharp
using Sets;

public class UniqueTagsManager : MonoBehaviour {
    public ObservableHashSet<string> activeTags;

    private void Start() {
        activeTags.itemAdded += OnTagAdded;
        activeTags.itemRemoved += OnTagRemoved;
    }

    public void AddTag(string tag) {
        activeTags.Add(tag);
    }

    public void RemoveTag(string tag) {
        activeTags.Remove(tag);
    }

    public void UnionWith(IEnumerable<string> newTags) {
        activeTags.UnionWith(newTags);
    }

    private void OnTagAdded(string tag) {
        Debug.Log($"Tag added: {tag}");
    }

    private void OnTagRemoved(string tag) {
        Debug.Log($"Tag removed: {tag}");
    }
}
```

### Example: Using async events

```csharp
using Events;

public class AsyncEventExample : MonoBehaviour {
    public AsyncGameEvent levelLoadedEvent;

    private async void Start() {
        // Raise event asynchronously
        await levelLoadedEvent.RaiseAsync();

        // Continue with other initialization
        Debug.Log("Level loading completed");
    }
}
```

### Example: Using typed async events

```csharp
using Events;

public class PlayerScoreManager : MonoBehaviour {
    public AsyncGenericGameEvent<int> scoreChangedEvent;

    public async void AddScore(int points) {
        // Raise event with score asynchronously
        await scoreChangedEvent.RaiseAsync(points);

        Debug.Log($"Score updated: {points}");
    }
}
```

## Recommended workflow

- Create variable assets for any game state you want centralized
- Use references for flexible inspector values
- Use events to connect systems without hard references
- Keep game logic testable by reading from ScriptableObject assets instead of static globals

## Contributing

Contributions are welcome! Feel free to open issues or pull requests for:

- new variable or reference types
- bug fixes
- improved documentation

## License

This project is open source. Use it in your Unity projects and adapt it as needed.

---

```text
// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
//
// Author: Ryan Hipple
// Date:   10/04/17
// Modified and extended for LDJAM42 and stuff
// Author: Nerzal
// ----------------------------------------------------------------------------
```

