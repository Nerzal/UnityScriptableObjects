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
  /// <summary>
  /// A ScriptableObject variable that holds a float value and provides
  /// change application methods.
  /// </summary>
  [CreateAssetMenu(fileName = "NewFloatVariable", menuName = "Variables/Float", order = 0)]
  [Serializable]
  public class FloatVariable : BaseVariable<float> {
    /// <summary>
    /// Applies a change to the current value by adding the specified amount.
    /// Only applies if the amount is not zero (using Equals for float comparison).
    /// </summary>
    /// <param name="amount">The float amount to add to the current value.</param>
    public void ApplyChange(float amount) {
      if (amount.Equals(0)) {
        return;
      }

      this.value += amount;
      InvokeChange(this.value);
    }

    /// <summary>
    /// Checks if the current value equals the specified float.
    /// </summary>
    /// <param name="other">The float to compare against.</param>
    /// <returns>True if equal, false otherwise.</returns>
    public override bool Equals(float other) {
      return other.Equals(this.value);
    }
  }
}