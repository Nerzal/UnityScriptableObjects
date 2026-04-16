using System;
using UnityEngine;

namespace Variables {
  /// <summary>
  /// A ScriptableObject variable that holds a LayerMask value and provides
  /// notification when the value changes.
  /// </summary>
  [CreateAssetMenu(fileName = "NewLayerMaskVariable", menuName = "Variables/LayerMask", order = 0)]
  [Serializable]
  public class LayerMaskVariable : BaseVariable<LayerMask> {
    /// <summary>
    /// Checks if the current value equals the specified layer mask.
    /// </summary>
    /// <param name="other">The LayerMask to compare against.</param>
    /// <returns>True if equal, false otherwise.</returns>
    public override bool Equals(LayerMask other) {
      return this.value.Equals(other);
    }
  }
}