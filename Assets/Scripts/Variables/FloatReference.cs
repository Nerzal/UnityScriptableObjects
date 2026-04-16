using System;
using Unity.VisualScripting;

namespace Variables {
    /// <summary>
    /// A reference that can hold either a constant float value or a reference to a FloatVariable.
    /// </summary>
    [Serializable]
    public class FloatReference : BaseReference<float, FloatVariable> {       
    }
}
