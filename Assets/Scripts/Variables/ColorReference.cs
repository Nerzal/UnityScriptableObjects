using System;
using UnityEngine;

namespace Variables {
  /// <summary>
  /// A reference that can hold either a constant Color value or a reference to a ColorVariable.
  /// </summary>
  [Serializable]
  public class ColorReference : BaseReference<Color, ColorVariable> {

  }
}