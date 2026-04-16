using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Events {
  /// <summary>
  /// An asynchronous ScriptableObject event that can be raised to notify all registered listeners.
  /// All listeners are notified concurrently using async/await patterns.
  /// </summary>
  [CreateAssetMenu(fileName = "NewAsyncGameEvent", menuName = "Events/Async Game Event", order = 0)]
  public class AsyncGameEvent : ScriptableObject {
    /// <summary>
    /// The list of listeners that this event will notify if it is raised.
    /// </summary>
    private readonly List<AsyncGameEventListener> _eventListeners = new List<AsyncGameEventListener>();

    /// <summary>
    /// Raises the event asynchronously, notifying all registered listeners concurrently.
    /// Returns a Task that completes when all listeners have finished processing.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task RaiseAsync() {
      if (_eventListeners.Count == 0) {
        return;
      }

      // Create tasks for all listeners
      var tasks = new List<Task>();
      for (int i = _eventListeners.Count - 1; i >= 0; i--) {
        tasks.Add(_eventListeners[i].OnEventRaisedAsync());
      }

      // Wait for all listeners to complete
      await Task.WhenAll(tasks);
    }

    /// <summary>
    /// Registers a listener to be notified when the event is raised.
    /// Prevents duplicate registrations.
    /// </summary>
    /// <param name="listener">The listener to register.</param>
    public void RegisterListener(AsyncGameEventListener listener) {
      if (!this._eventListeners.Contains(listener))
        this._eventListeners.Add(listener);
    }

    /// <summary>
    /// Unregisters a listener from being notified when the event is raised.
    /// </summary>
    /// <param name="listener">The listener to unregister.</param>
    public void UnregisterListener(AsyncGameEventListener listener) {
      if (this._eventListeners.Contains(listener))
        this._eventListeners.Remove(listener);
    }
  }
}
