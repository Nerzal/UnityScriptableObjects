using System;
using UnityEngine;

namespace Variables {
  /// <summary>
  /// A ScriptableObject variable that holds a double value and provides
  /// change application methods.
  /// </summary>
  [CreateAssetMenu(fileName = "NewDoubleVariable", menuName = "Variables/Double", order = 0)]
  [Serializable]
  public class DoubleVariable : BaseVariable<double> {
    /// <summary>
    /// Applies a change to the current value by adding the specified amount.
    /// Only applies when the amount is not zero.
    /// </summary>
    /// <param name="amount">The double amount to add to the current value.</param>
    public void ApplyChange(double amount) {
      if (amount.Equals(0.0)) {
        return;
      }

      this.value += amount;
      InvokeChange(this.value);
    }

    /// <summary>
    /// Checks if the current value equals the specified double.
    /// </summary>
    /// <param name="other">The double value to compare against.</param>
    /// <returns>True if equal, false otherwise.</returns>
    public override bool Equals(double other) {
      return other.Equals(this.value);
    }
  }
}