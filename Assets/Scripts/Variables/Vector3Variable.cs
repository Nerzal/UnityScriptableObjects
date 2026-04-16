using System;
using UnityEngine;

namespace Variables {
  /// <summary>
  /// A ScriptableObject variable that holds a Vector3 value and provides
  /// individual component change events.
  /// </summary>
  [CreateAssetMenu(fileName = "NewVector3Variable", menuName = "Variables/Vector3", order = 0)]
  [Serializable]
  public class Vector3Variable : BaseVariable<Vector3> {
    /// <summary>
    /// Event triggered when the X component changes.
    /// </summary>
    public Action<float> xChanged;

    /// <summary>
    /// Event triggered when the Y component changes.
    /// </summary>
    public Action<float> yChanged;

    /// <summary>
    /// Event triggered when the Z component changes.
    /// </summary>
    public Action<float> zChanged;

    /// <summary>
    /// Sets the value and triggers component-specific events if components change.
    /// </summary>
    /// <param name="value">The new Vector3 value to set.</param>
    public override void SetValue(Vector3 value) {
      Vector3 oldValue = this.value;
      this.value = value;

      if (oldValue.x.CompareTo(this.value.x) != 0) {
        this.xChanged?.Invoke(value.x);
      }

      if (oldValue.y.CompareTo(this.value.y) != 0)
      {
          this.yChanged?.Invoke(value.y);
      }

      if (oldValue.z.CompareTo(this.value.z) != 0)
      {
          this.zChanged?.Invoke(value.z);
      }
}

    /// <summary>
    /// Checks if the current value equals the specified Vector3.
    /// </summary>
    /// <param name="other">The Vector3 to compare against.</param>
    /// <returns>True if equal, false otherwise.</returns>
    public override bool Equals(Vector3 other) {
      return this.value.Equals(other);
    }
  }
}