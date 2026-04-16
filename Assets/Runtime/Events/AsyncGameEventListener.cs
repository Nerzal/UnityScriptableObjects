using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Events {
  /// <summary>
  /// A listener component for AsyncGameEvent that responds to event raises asynchronously.
  /// Attach this to GameObjects that need to react to async game events.
  /// </summary>
  public class AsyncGameEventListener : MonoBehaviour {
    /// <summary>
    /// The event this listener is registered to.
    /// </summary>
    [Tooltip("The AsyncGameEvent to listen to.")]
    public AsyncGameEvent Event;

    /// <summary>
    /// Unity event that will be invoked when the async event is raised.
    /// </summary>
    [Tooltip("Response to invoke when the event is raised.")]
    public UnityEvent Response;

    /// <summary>
    /// Called when the script is enabled. Registers this listener with the event.
    /// </summary>
    private void OnEnable() {
      if (Event != null) {
        Event.RegisterListener(this);
      }
    }

    /// <summary>
    /// Called when the script is disabled. Unregisters this listener from the event.
    /// </summary>
    private void OnDisable() {
      if (Event != null) {
        Event.UnregisterListener(this);
      }
    }

    /// <summary>
    /// Called when the event is raised. Invokes the response asynchronously.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task OnEventRaisedAsync() {
      // Invoke the Unity event asynchronously
      await Task.Yield(); // Allow Unity to process other operations
      Response?.Invoke();
    }
  }
}