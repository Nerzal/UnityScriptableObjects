using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
//
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

namespace Events {
  /// <summary>
  /// A generic ScriptableObject event that can be raised with a typed argument to notify all registered listeners.
  /// Provides type-safe event communication.
  /// </summary>
  /// <typeparam name="T">The type of the argument passed when the event is raised.</typeparam>
  public class GenericGameEvent<T> : ScriptableObject {
    /// <summary>
    /// The list of listeners that this event will notify if it is raised.
    /// </summary>
    private readonly List<GenericGameEventListener<T>> _eventListeners =
      new List<GenericGameEventListener<T>>();

    /// <summary>
    /// Raises the event with the specified argument, notifying all registered listeners in reverse order.
    /// </summary>
    /// <param name="arg">The argument to pass to the listeners.</param>
    public void Raise(T arg) {
      for (int i = _eventListeners.Count - 1; i >= 0; i--) {
        _eventListeners[i].OnEventRaised(arg);
      }
    }

    /// <summary>
    /// Registers a listener to be notified when the event is raised.
    /// Prevents duplicate registrations.
    /// </summary>
    /// <param name="listener">The listener to register.</param>
    public void RegisterListener(GenericGameEventListener<T> listener) {
      if (!_eventListeners.Contains(listener))
        _eventListeners.Add(listener);
    }

    /// <summary>
    /// Unregisters a listener from being notified when the event is raised.
    /// </summary>
    /// <param name="listener">The listener to unregister.</param>
    public void UnregisterListener(GenericGameEventListener<T> listener) {
      if (_eventListeners.Contains(listener))
        _eventListeners.Remove(listener);
    }
  }
}