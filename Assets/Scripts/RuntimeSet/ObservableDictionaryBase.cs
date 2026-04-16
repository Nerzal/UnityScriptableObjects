using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

namespace Sets {
  /// <summary>
  /// Base class for observable collections that provides editor support methods.
  /// </summary>
  public abstract class ObservableDictionaryBase : ScriptableObject {
    /// <summary>
    /// Gets the number of items in the collection.
    /// </summary>
    /// <returns>The number of items in the collection.</returns>
    public abstract int GetCount();

    /// <summary>
    /// Gets the item at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the item to get.</param>
    /// <returns>The item at the specified index, or null if the index is out of range.</returns>
    public abstract KeyValuePair<object, object> GetItemAt(int index);

    /// <summary>
    /// Clears all items from the collection.
    /// </summary>
    public abstract void ClearItems();
  }
}
