using System;
using UnityEngine;

namespace Variables {
  /// <summary>
  /// A ScriptableObject variable that holds a Vector4 value and provides
  /// notification when the value changes.
  /// </summary>
  [CreateAssetMenu(fileName = "NewVector4Variable", menuName = "Variables/Vector4", order = 0)]
  [Serializable]
  public class Vector4Variable : BaseVariable<Vector4> {
    /// <summary>
    /// Checks if the current value equals the specified Vector4.
    /// </summary>
    /// <param name="other">The Vector4 to compare against.</param>
    /// <returns>True if equal, false otherwise.</returns>
    public override bool Equals(Vector4 other) {
      return this.value.Equals(other);
    }
  }
}