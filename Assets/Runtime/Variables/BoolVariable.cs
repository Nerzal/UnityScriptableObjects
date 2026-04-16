using System;
using UnityEngine;

namespace Variables {
  /// <summary>
  /// A ScriptableObject variable that holds a boolean value.
  /// </summary>
  [CreateAssetMenu(fileName = "NewBoolVariable", menuName = "Variables/Bool", order = 0)]
  [Serializable]
  public class BoolVariable : BaseVariable<bool> {
    /// <summary>
    /// Checks if the current value equals the specified boolean.
    /// </summary>
    /// <param name="other">The boolean to compare against.</param>
    /// <returns>True if equal, false otherwise.</returns>
    public override bool Equals(bool other) {
      return this.value == other;
    }
  }
}