using System;
using UnityEngine;

namespace Variables {
  [CreateAssetMenu(fileName = "NewBoolVariable", menuName = "Variables/Bool", order = 0)]
  [Serializable]
  public class BoolVariable : BaseVariable<bool> {
    /// <inheritdoc />
    public override bool Equals(bool other) {
      return this.value == other;
    }
  }
}