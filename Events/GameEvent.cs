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
  /// A ScriptableObject event that can be raised to notify all registered listeners.
  /// Provides a decoupled way to communicate between components without direct references.
  /// </summary>
  [CreateAssetMenu]
  public class GameEvent : ScriptableObject {
    /// <summary>
    /// The list of listeners that this event will notify if it is raised.
    /// </summary>
    private readonly List<GameEventListener> _eventListeners = new List<GameEventListener>();

    /// <summary>
    /// Raises the event, notifying all registered listeners in reverse order to handle removals safely.
    /// </summary>
    public void Raise() {
      for (int i = this._eventListeners.Count - 1; i >= 0; i--) {
        this._eventListeners[i].OnEventRaised();
      }
    }

    /// <summary>
    /// Registers a listener to be notified when the event is raised.
    /// Prevents duplicate registrations.
    /// </summary>
    /// <param name="listener">The listener to register.</param>
    public void RegisterListener(GameEventListener listener) {
      if (!this._eventListeners.Contains(listener))
        this._eventListeners.Add(listener);
    }

    /// <summary>
    /// Unregisters a listener from being notified when the event is raised.
    /// </summary>
    /// <param name="listener">The listener to unregister.</param>
    public void UnregisterListener(GameEventListener listener) {
      if (this._eventListeners.Contains(listener))
        this._eventListeners.Remove(listener);
    }
  }
}