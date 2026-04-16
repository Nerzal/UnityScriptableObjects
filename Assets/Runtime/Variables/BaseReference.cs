using System;

namespace Variables {
  /// <summary>
  /// Abstract base class for references that can hold either a constant value or a reference to a ScriptableObject variable.
  /// Provides flexibility in Unity Inspector to switch between constant and variable values without code changes.
  /// </summary>
  /// <typeparam name="TType">The type of the value (e.g., int, float).</typeparam>
  /// <typeparam name="TVariable">The type of the ScriptableObject variable that holds the value.</typeparam>
  [Serializable]
  public abstract class BaseReference<TType, TVariable> where TVariable : BaseVariable<TType> {
    /// <summary>
    /// If true, uses the constant value; if false, uses the variable's value.
    /// </summary>
    public bool useConstant = true;

    /// <summary>
    /// The constant value to use when useConstant is true.
    /// </summary>
    public TType constantValue;

    /// <summary>
    /// The ScriptableObject variable to reference when useConstant is false.
    /// </summary>
    public TVariable variable;

    /// <summary>
    /// Default constructor.
    /// </summary>
    protected BaseReference() { }

    /// <summary>
    /// Constructor that initializes with a constant value.
    /// </summary>
    /// <param name="value">The constant value to set.</param>
    protected BaseReference(TType value) {
      this.useConstant = true;
      this.constantValue = value;
    }

    /// <summary>
    /// Gets the current value, either from the constant or the variable.
    /// </summary>
    public TType Value {
      get { return this.useConstant ? this.constantValue : this.variable.value; }
    }

    /// <summary>
    /// Implicit operator to allow direct use as TType.
    /// </summary>
    /// <param name="reference">The reference to convert.</param>
    /// <returns>The current value.</returns>
    public static implicit operator TType(BaseReference<TType, TVariable> reference) {
      return reference.Value;
    }
  }
}