using System;
using UnityEngine;

namespace Variables {
  /// <summary>
  /// A reference that can hold either a constant Quaternion value or a reference to a QuaternionVariable.
  /// </summary>
  [Serializable]
  public class QuaternionReference : BaseReference<Quaternion, QuaternionVariable> {

  }
}