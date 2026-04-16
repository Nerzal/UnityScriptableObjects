using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Events {
  /// <summary>
  /// A generic asynchronous ScriptableObject event that can be raised with a typed argument to notify all registered listeners.
  /// All listeners are notified concurrently using async/await patterns.
  /// </summary>
  /// <typeparam name="T">The type of the argument passed when the event is raised.</typeparam>
  [CreateAssetMenu(fileName = "NewAsyncGenericGameEvent", menuName = "Events/Async Generic Game Event", order = 0)]
  public class AsyncGenericGameEvent<T> : ScriptableObject {
    /// <summary>
    /// The list of listeners that this event will notify if it is raised.
    /// </summary>
    private readonly List<AsyncGenericGameEventListener<T>> _eventListeners =
      new List<AsyncGenericGameEventListener<T>>();

    /// <summary>
    /// Raises the event asynchronously with the specified argument, notifying all registered listeners concurrently.
    /// Returns a Task that completes when all listeners have finished processing.
    /// </summary>
    /// <param name="arg">The argument to pass to the listeners.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task RaiseAsync(T arg) {
      if (_eventListeners.Count == 0) {
        return;
      }

      // Create tasks for all listeners
      var tasks = new List<Task>();
      for (int i = _eventListeners.Count - 1; i >= 0; i--) {
        tasks.Add(_eventListeners[i].OnEventRaisedAsync(arg));
      }

      // Wait for all listeners to complete
      await Task.WhenAll(tasks);
    }

    /// <summary>
    /// Registers a listener to be notified when the event is raised.
    /// Prevents duplicate registrations.
    /// </summary>
    /// <param name="listener">The listener to register.</param>
    public void RegisterListener(AsyncGenericGameEventListener<T> listener) {
      if (!_eventListeners.Contains(listener))
        _eventListeners.Add(listener);
    }

    /// <summary>
    /// Unregisters a listener from being notified when the event is raised.
    /// </summary>
    /// <param name="listener">The listener to unregister.</param>
    public void UnregisterListener(AsyncGenericGameEventListener<T> listener) {
      if (_eventListeners.Contains(listener))
        _eventListeners.Remove(listener);
    }
  }
}