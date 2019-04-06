using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

namespace Events {
  [CreateAssetMenu]
  public class GameEvent : ScriptableObject {
    /// <summary>
    /// The list of listeners that this event will notify if it is raised.
    /// </summary>
    private readonly List<GameEventListener> _eventListeners = new List<GameEventListener>();

    public void Raise() {
      for (int i = this._eventListeners.Count - 1; i >= 0; i--) {
        this._eventListeners[i].OnEventRaised();
      }
    }

    public void RegisterListener(GameEventListener listener) {
      if (!this._eventListeners.Contains(listener))
        this._eventListeners.Add(listener);
    }

    public void UnregisterListener(GameEventListener listener) {
      if (this._eventListeners.Contains(listener))
        this._eventListeners.Remove(listener);
    }
  }
}