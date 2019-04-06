using System;
using UnityEngine;

namespace Variables {
  [CreateAssetMenu(fileName = "NewSpriteVariable", menuName = "Variables/Sprite", order = 0)]
  [Serializable]
  public class SpriteVariable : BaseVariable<Sprite> {
    /// <inheritdoc />
    public override bool Equals(Sprite other) {
      return this.value.Equals(other);
    }
  }
}