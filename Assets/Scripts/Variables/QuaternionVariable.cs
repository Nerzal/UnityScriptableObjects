using System;
using UnityEngine;

namespace Variables {
  /// <summary>
  /// A ScriptableObject variable that holds a Quaternion value and provides
  /// notification when the value changes.
  /// </summary>
  [CreateAssetMenu(fileName = "NewQuaternionVariable", menuName = "Variables/Quaternion", order = 0)]
  [Serializable]
  public class QuaternionVariable : BaseVariable<Quaternion> {
    /// <summary>
    /// Checks if the current value equals the specified quaternion.
    /// </summary>
    /// <param name="other">The quaternion to compare against.</param>
    /// <returns>True if equal, false otherwise.</returns>
    public override bool Equals(Quaternion other) {
      return this.value.Equals(other);
    }
  }
}