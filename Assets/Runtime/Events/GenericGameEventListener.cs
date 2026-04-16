using UnityEngine;

namespace Events {
  /// <summary>
  /// Abstract base class for MonoBehaviour listeners that respond to GenericGameEvent<T> raises.
  /// Subclasses must implement the OnEventRaised method to handle the event with a typed argument.
  /// </summary>
  /// <typeparam name="T">The type of the argument passed by the event.</typeparam>
  public abstract class GenericGameEventListener<T> : MonoBehaviour {
    /// <summary>
    /// Called when the associated GenericGameEvent is raised with an argument.
    /// Implement this method in subclasses to define the response behavior.
    /// </summary>
    /// <param name="o">The argument passed by the event.</param>
    public abstract void OnEventRaised(T o);
  }
}