using System;
using UnityEngine;

namespace Variables {
  /// <summary>
  /// A generic ScriptableObject variable for enum types.
  /// This is useful when you want a strongly typed enum asset.
  /// </summary>
  /// <typeparam name="TEnum">The enum type.</typeparam>
  [Serializable]
  public abstract class EnumVariable<TEnum> : BaseVariable<TEnum> where TEnum : struct, Enum {
    /// <summary>
    /// Checks if the current value equals the specified enum value.
    /// </summary>
    /// <param name="other">The enum value to compare against.</param>
    /// <returns>True if equal, false otherwise.</returns>
    public override bool Equals(TEnum other) {
      return this.value.Equals(other);
    }
  }
}