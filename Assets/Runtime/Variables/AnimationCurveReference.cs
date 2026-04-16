using System;
using UnityEngine;

namespace Variables {
  /// <summary>
  /// A reference that can hold either a constant AnimationCurve value or a reference to an AnimationCurveVariable.
  /// </summary>
  [Serializable]
  public class AnimationCurveReference : BaseReference<AnimationCurve, AnimationCurveVariable> {

  }
}