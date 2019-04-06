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
  [CreateAssetMenu(fileName = "NewIntVariable", menuName = "Variables/Int", order = 0)]
  [Serializable]
  public class IntVariable : BaseVariable<int> {

    public static bool operator < (IntVariable value1, IntVariable value2) {
      return (value1.value < value2.value);
    }

    public static bool operator > (IntVariable value1, IntVariable value2) {
      return (value1.value > value2.value);
    }

    public void ApplyChange(int amount) {
      if (amount == 0) {
        return;
      }

      this.value += amount;
      InvokeChange(this.value);
    }

    public void ApplyChange(IntVariable amount) {
      ApplyChange(amount.value);
    }

    /// <inheritdoc />
    public override string ToString() {
      return this.value.ToString();
    }

    /// <inheritdoc />
    public override bool Equals(int other) {
      return this.value == other;
    }
  }
}