using System;

namespace Variables {
  [Serializable]
  public abstract class BaseReference<TType, TVariable> where TVariable : BaseVariable<TType> {
    public bool useConstant = true;
    public TType constantValue;
    public TVariable variable;

    protected BaseReference() { }

    protected BaseReference(TType value) {
      this.useConstant = true;
      this.constantValue = value;
    }

    public TType Value {
      get { return this.useConstant ? this.constantValue : this.variable.value; }
    }

    public static implicit operator TType(BaseReference<TType, TVariable> reference) {
      return reference.Value;
    }
  }
}