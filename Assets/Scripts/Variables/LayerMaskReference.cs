using System;
using UnityEngine;

namespace Variables {
  /// <summary>
  /// A reference that can hold either a constant LayerMask value or a reference to a LayerMaskVariable.
  /// </summary>
  [Serializable]
  public class LayerMaskReference : BaseReference<LayerMask, LayerMaskVariable> {

  }
}