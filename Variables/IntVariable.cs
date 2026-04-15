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
  /// A ScriptableObject variable that holds an integer value and provides
  /// comparison operators and change application methods.
  /// </summary>
  [CreateAssetMenu(fileName = "NewIntVariable", menuName = "Variables/Int", order = 0)]
  [Serializable]
  public class IntVariable : BaseVariable<int> {

    /// <summary>
    /// Compares two IntVariable instances for less-than relationship.
    /// </summary>
    /// <param name="value1">The first IntVariable to compare.</param>
    /// <param name="value2">The second IntVariable to compare.</param>
    /// <returns>True if value1 is less than value2, false otherwise.</returns>
    public static bool operator < (IntVariable value1, IntVariable value2) {
      return (value1.value < value2.value);
    }

    /// <summary>
    /// Compares two IntVariable instances for greater-than relationship.
    /// </summary>
    /// <param name="value1">The first IntVariable to compare.</param>
    /// <param name="value2">The second IntVariable to compare.</param>
    /// <returns>True if value1 is greater than value2, false otherwise.</returns>
    public static bool operator > (IntVariable value1, IntVariable value2) {
      return (value1.value > value2.value);
    }

    /// <summary>
    /// Applies a change to the current value by adding the specified amount.
    /// Only applies if the amount is not zero.
    /// </summary>
    /// <param name="amount">The integer amount to add to the current value.</param>
    public void ApplyChange(int amount) {
      if (amount == 0) {
        return;
      }

      this.value += amount;
      InvokeChange(this.value);
    }

    /// <summary>
    /// Applies a change to the current value by adding the value of another IntVariable.
    /// </summary>
    /// <param name="amount">The IntVariable whose value to add.</param>
    public void ApplyChange(IntVariable amount) {
      ApplyChange(amount.value);
    }

    /// <summary>
    /// Returns a string representation of the current value.
    /// </summary>
    /// <returns>The string representation of the integer value.</returns>
    public override string ToString() {
      return this.value.ToString();
    }

    /// <summary>
    /// Checks if the current value equals the specified integer.
    /// </summary>
    /// <param name="other">The integer to compare against.</param>
    /// <returns>True if equal, false otherwise.</returns>
    public override bool Equals(int other) {
      return this.value == other;
    }
  }
}