using System;
using UnityEngine;

namespace Variables {
  /// <summary>
  /// A ScriptableObject variable that holds a Sprite value.
  /// </summary>
  [CreateAssetMenu(fileName = "NewSpriteVariable", menuName = "Variables/Sprite", order = 0)]
  [Serializable]
  public class SpriteVariable : BaseVariable<Sprite> {
    /// <summary>
    /// Checks if the current value equals the specified Sprite.
    /// </summary>
    /// <param name="other">The Sprite to compare against.</param>
    /// <returns>True if equal, false otherwise.</returns>
    public override bool Equals(Sprite other) {
      return this.value.Equals(other);
    }
  }
}