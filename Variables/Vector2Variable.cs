using System;
using UnityEngine;

namespace Variables {
  [CreateAssetMenu(fileName = "NewVector2Variable", menuName = "Variables/Vector2", order = 0)]
  [Serializable]
  public class Vector2Variable : BaseVariable<Vector2> {
    public Action<float> xChanged;
    public Action<float> yChanged;

    public override void SetValue(Vector2 value) {
      Vector2 oldValue = this.value;
      this.value = value;

      if (oldValue.x.CompareTo(this.value.x) != 0) {
        this.xChanged?.Invoke(value.x);
      }

      if (oldValue.x.CompareTo(this.value.y) != 0) {
        this.yChanged?.Invoke(value.y);
      }
    }

    /// <inheritdoc />
    public override bool Equals(Vector2 other) {
      return this.value.Equals(other);
    }
  }
}