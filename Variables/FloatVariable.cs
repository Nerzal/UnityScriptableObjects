using System;
using UnityEngine;

// ----------------------------------------------------------------------------
// Unite 2017 - Game Architecture with Scriptable Objects
// 
// Author: Ryan Hipple
// Date:   10/04/17
// Modified for LDJAM42
// Author: Nerzal
// ----------------------------------------------------------------------------

namespace Variables {
  [CreateAssetMenu(fileName = "NewFloatVariable", menuName = "Variables/Float", order = 0)]
  [Serializable]
  public class FloatVariable : BaseVariable<float> {
    public void ApplyChange(float amount) {
      if (amount.Equals(0)) {
        return;
      }

      this.value += amount;
      InvokeChange(this.value);
    }

    /// <inheritdoc />
    public override bool Equals(float other) {
      return other.Equals(this.value);
    }
  }
}