using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Events {
  /// <summary>
  /// A listener component for AsyncGenericGameEvent that responds to typed event raises asynchronously.
  /// Attach this to GameObjects that need to react to async generic game events.
  /// </summary>
  /// <typeparam name="T">The type of the argument passed by the event.</typeparam>
  public class AsyncGenericGameEventListener<T> : MonoBehaviour {
    /// <summary>
    /// The event this listener is registered to.
    /// </summary>
    [Tooltip("The AsyncGenericGameEvent to listen to.")]
    public AsyncGenericGameEvent<T> Event;

    /// <summary>
    /// Unity event that will be invoked when the async event is raised, passing the typed argument.
    /// </summary>
    [Tooltip("Response to invoke when the event is raised.")]
    public UnityEvent<T> Response;

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
    /// Called when the event is raised with a typed argument. Invokes the response asynchronously.
    /// </summary>
    /// <param name="arg">The argument passed by the event.</param>
    /// <returns>A Task representing the asynchronous operation.</returns>
    public async Task OnEventRaisedAsync(T arg) {
      // Invoke the Unity event asynchronously
      await Task.Yield(); // Allow Unity to process other operations
      Response?.Invoke(arg);
    }
  }
}