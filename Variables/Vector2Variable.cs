using System;
using UnityEngine;

namespace Variables {
  /// <summary>
  /// A ScriptableObject variable that holds a Vector2 value and provides
  /// individual component change events.
  /// </summary>
  [CreateAssetMenu(fileName = "NewVector2Variable", menuName = "Variables/Vector2", order = 0)]
  [Serializable]
  public class Vector2Variable : BaseVariable<Vector2> {
    /// <summary>
    /// Event triggered when the X component changes.
    /// </summary>
    public Action<float> xChanged;

    /// <summary>
    /// Event triggered when the Y component changes.
    /// </summary>
    public Action<float> yChanged;

    /// <summary>
    /// Sets the value and triggers component-specific events if components change.
    /// </summary>
    /// <param name="value">The new Vector2 value to set.</param>
    public override void SetValue(Vector2 value) {
      Vector2 oldValue = this.value;
      this.value = value;

      if (oldValue.x.CompareTo(this.value.x) != 0) {
        this.xChanged?.Invoke(value.x);
      }

      if (oldValue.x.CompareTo(this.value.y) != 0) {
        this.yChanged?.Invoke(value.y);
      }
    }

    /// <summary>
    /// Checks if the current value equals the specified Vector2.
    /// </summary>
    /// <param name="other">The Vector2 to compare against.</param>
    /// <returns>True if equal, false otherwise.</returns>
    public override bool Equals(Vector2 other) {
      return this.value.Equals(other);
    }
  }
}