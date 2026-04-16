using System;
using UnityEngine;

namespace Variables {
  /// <summary>
  /// A reference that can hold either a constant enum value or a reference to an EnumVariable.
  /// </summary>
  /// <typeparam name="TEnum">The enum type.</typeparam>
  [Serializable]
  public abstract class EnumReference<TEnum, TVariable> : BaseReference<TEnum, TVariable>
    where TEnum : struct, Enum
    where TVariable : EnumVariable<TEnum> {

  }
}