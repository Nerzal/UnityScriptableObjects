using System;

namespace Variables {
  /// <summary>
  /// A reference that can hold either a constant double value or a reference to a DoubleVariable.
  /// </summary>
  [Serializable]
  public class DoubleReference : BaseReference<double, DoubleVariable> {

  }
}