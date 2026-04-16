using System;
using UnityEngine;

namespace Variables {
  /// <summary>
  /// A ScriptableObject variable that holds a Color value and provides
  /// notification when the color changes.
  /// </summary>
  [CreateAssetMenu(fileName = "NewColorVariable", menuName = "Variables/Color", order = 0)]
  [Serializable]
  public class ColorVariable : BaseVariable<Color> {
    /// <summary>
    /// Checks if the current color equals the specified color.
    /// </summary>
    /// <param name="other">The color to compare against.</param>
    /// <returns>True if equal, false otherwise.</returns>
    public override bool Equals(Color other) {
      return this.value.Equals(other);
    }
  }
}