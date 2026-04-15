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
  /// A ScriptableObject variable that holds a string value.
  /// </summary>
  [CreateAssetMenu(fileName = "NewStringVariable", menuName = "Variables/String", order = 0)]
  [Serializable]
  public class StringVariable : BaseVariable<string> {
    /// <summary>
    /// Checks if the current value equals the specified string using ordinal comparison.
    /// </summary>
    /// <param name="other">The string to compare against.</param>
    /// <returns>True if equal, false otherwise.</returns>
    public override bool Equals(string other) {
      return string.Compare(this.value, other, StringComparison.Ordinal) == 0;
    }
  }
}