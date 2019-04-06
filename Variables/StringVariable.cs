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
  [CreateAssetMenu(fileName = "NewStringVariable", menuName = "Variables/String", order = 0)]
  [Serializable]
  public class StringVariable : BaseVariable<string> {
    /// <inheritdoc />
    public override bool Equals(string other) {
      return string.Compare(this.value, other, StringComparison.Ordinal) == 0;
    }
  }
}