using System;
using UnityEngine;

namespace Variables {
  [CreateAssetMenu(fileName = "NewVector3Variable", menuName = "Variables/Vector3", order = 0)]
  [Serializable]
  public class Vector3Variable : BaseVariable<Vector3> {
    public Action<float> xChanged;
    public Action<float> yChanged;
    public Action<float> zChanged;

    public override void SetValue(Vector3 value) {
      Vector3 oldValue = this.value;
      this.value = value;

      if (oldValue.x.CompareTo(this.value.x) != 0) {
        this.xChanged?.Invoke(value.x);
      }

      if (oldValue.y.CompareTo(this.value.y) != 0)
      {
          this.yChanged?.Invoke(value.y);
      }

      if (oldValue.z.CompareTo(this.value.z) != 0)
      {
          this.zChanged?.Invoke(value.z);
      }
}

    /// <inheritdoc />
    public override bool Equals(Vector3 other) {
      return this.value.Equals(other);
    }
  }
}