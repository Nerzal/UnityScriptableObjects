using System;
using UnityEngine;

namespace Variables {
  /// <summary>
  /// Abstract base class for ScriptableObject-based variables that hold a value of type TType
  /// and provide event notifications when the value changes.
  /// </summary>
  /// <typeparam name="TType">The type of the value stored in the variable.</typeparam>
  [Serializable]
  public abstract class BaseVariable<TType> : ScriptableObject, IEquatable<TType> {
    /// <summary>
    /// Event triggered when the value changes. Subscribers can react to value updates.
    /// </summary>
    public Action<TType> valueChanged;

#if UNITY_EDITOR
    [NonSerialized]
    [Multiline]
    public string developerDescription = "";
#endif

    /// <summary>
    /// The current value of the variable.
    /// </summary>
    public TType value;

    /// <summary>
    /// Called when the ScriptableObject is enabled (e.g., when loaded).
    /// Initializes the value to its default if it's a reference type.
    /// </summary>
    protected virtual void OnEnable() {
      // For reference types, ensure value is not null on load
      if (typeof(TType).IsClass && value == null) {
        value = default(TType);
      }
    }

    /// <summary>
    /// Sets the value and invokes the change event if the value actually changes.
    /// </summary>
    /// <param name="value">The new value to set.</param>
    public virtual void SetValue(TType value) {
      if (Equals(value)) {
        return;
      }

      this.value = value;
      InvokeChange(value);
    }

    /// <summary>
    /// Invokes the valueChanged event with the current value.
    /// </summary>
    /// <param name="value">The value to pass to the event.</param>
    protected void InvokeChange(TType value) {
      this.valueChanged?.Invoke(value);
    }

    /// <summary>
    /// Abstract method to check equality with another value of type TType.
    /// </summary>
    /// <param name="other">The value to compare against.</param>
    /// <returns>True if equal, false otherwise.</returns>
    public abstract bool Equals(TType other);
  }
}