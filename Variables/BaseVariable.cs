using System;
using UnityEngine;

namespace Variables {
  [Serializable]
  public abstract class BaseVariable<TType> : ScriptableObject, IEquatable<TType> {
    public Action<TType> valueChanged;

#if UNITY_EDITOR
    [NonSerialized]
    [Multiline]
    public string developerDescription = "";
#endif

    public TType value;

    public virtual void SetValue(TType value) {
      if (Equals(value)) {
        return;
      }

      this.value = value;
      InvokeChange(value);
    }

    protected void InvokeChange(TType value) {
      this.valueChanged?.Invoke(value);
    }

    /// <inheritdoc />
    public abstract bool Equals(TType other);
  }
}