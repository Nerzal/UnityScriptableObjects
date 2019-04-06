using System;
using UnityEngine;

namespace Variables {
  [CreateAssetMenu(fileName = "NewTextureVariable", menuName = "Variables/Texture", order = 0)]
  [Serializable]
  public class TextureVariable : BaseVariable<Texture> {
    /// <inheritdoc />
    public override bool Equals(Texture other) {
      return this.value.Equals(other);
    }
  }
}