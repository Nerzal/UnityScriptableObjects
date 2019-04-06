using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

namespace Events {
  public class GenericGameEvent<T> : ScriptableObject {
    /// <summary>
    /// The list of listeners that this event will notify if it is raised.
    /// </summary>
    private readonly List<GenericGameEventListener<T>> _eventListeners =
      new List<GenericGameEventListener<T>>();

    public void Raise(T arg) {
      for (int i = this._eventListeners.Count - 1; i >= 0; i--) {
        this._eventListeners[i].OnEventRaised(arg);
      }
    }

    public void RegisterListener(GenericGameEventListener<T> listener) {
      if (!this._eventListeners.Contains(listener))
        this._eventListeners.Add(listener);
    }

    public void UnregisterListener(GenericGameEventListener<T> listener) {
      if (this._eventListeners.Contains(listener))
        this._eventListeners.Remove(listener);
    }
  }
}