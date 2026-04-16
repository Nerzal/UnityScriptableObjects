using System;
using UnityEngine;

namespace Variables {
  /// <summary>
  /// A reference that can hold either a constant Vector4 value or a reference to a Vector4Variable.
  /// </summary>
  [Serializable]
  public class Vector4Reference : BaseReference<Vector4, Vector4Variable> {

  }
}