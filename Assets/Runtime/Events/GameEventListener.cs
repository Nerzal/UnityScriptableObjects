using UnityEngine;
using UnityEngine.Events;

// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
//
// Author: Ryan Hipple
// Date:   10/04/17
// ----------------------------------------------------------------------------

namespace Events {
  /// <summary>
  /// A MonoBehaviour that listens to a GameEvent and invokes a UnityEvent response when the event is raised.
  /// Automatically registers and unregisters itself with the event on enable/disable.
  /// </summary>
  public class GameEventListener : MonoBehaviour {
    /// <summary>
    /// The GameEvent to register with and listen to.
    /// </summary>
    [Tooltip("Event to register with.")]
    public GameEvent Event;

    /// <summary>
    /// The UnityEvent response to invoke when the GameEvent is raised.
    /// </summary>
    [Tooltip("Response to invoke when Event is raised.")]
    public UnityEvent response;

    /// <summary>
    /// Called when the script is enabled. Registers this listener with the event.
    /// </summary>
    private void OnEnable() {
      this.Event.RegisterListener(this);
    }

    /// <summary>
    /// Called when the script is disabled. Unregisters this listener from the event.
    /// </summary>
    private void OnDisable() {
      if (this.Event == null) {
        return;
      }
      this.Event.UnregisterListener(this);
    }

    /// <summary>
    /// Called by the GameEvent when it is raised. Invokes the response UnityEvent.
    /// </summary>
    public void OnEventRaised() {
      this.response.Invoke();
    }
  }
}