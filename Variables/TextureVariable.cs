using System;
using UnityEngine;

namespace Variables {
  /// <summary>
  /// A ScriptableObject variable that holds a Texture value.
  /// </summary>
  [CreateAssetMenu(fileName = "NewTextureVariable", menuName = "Variables/Texture", order = 0)]
  [Serializable]
  public class TextureVariable : BaseVariable<Texture> {
    /// <summary>
    /// Checks if the current value equals the specified Texture.
    /// </summary>
    /// <param name="other">The Texture to compare against.</param>
    /// <returns>True if equal, false otherwise.</returns>
    public override bool Equals(Texture other) {
      return this.value.Equals(other);
    }
  }
}