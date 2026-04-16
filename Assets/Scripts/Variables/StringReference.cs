using System;

namespace Variables {
  /// <summary>
  /// A reference that can hold either a constant string value or a reference to a StringVariable.
  /// </summary>
  [Serializable]
  public class StringReference : BaseReference<string, StringVariable> {

  }
}