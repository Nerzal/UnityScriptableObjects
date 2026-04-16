using System;
using UnityEngine;

namespace Variables {
  /// <summary>
  /// A ScriptableObject variable that holds an AnimationCurve value and provides
  /// notification when the value changes.
  /// </summary>
  [CreateAssetMenu(fileName = "NewAnimationCurveVariable", menuName = "Variables/AnimationCurve", order = 0)]
  [Serializable]
  public class AnimationCurveVariable : BaseVariable<AnimationCurve> {
    /// <summary>
    /// Checks if the current value equals the specified animation curve.
    /// </summary>
    /// <param name="other">The AnimationCurve to compare against.</param>
    /// <returns>True if equal, false otherwise.</returns>
    public override bool Equals(AnimationCurve other) {
      return this.value == other;
    }
  }
}