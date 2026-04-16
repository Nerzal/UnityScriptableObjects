using System;
using UnityEngine;

namespace Variables {
  /// <summary>
  /// A ScriptableObject variable that holds a GameObject reference and provides
  /// notification when the value changes.
  /// </summary>
  [CreateAssetMenu(fileName = "NewGameObjectVariable", menuName = "Variables/GameObject", order = 0)]
  [Serializable]
  public class GameObjectVariable : BaseVariable<GameObject> {
    /// <summary>
    /// Checks if the current value equals the specified GameObject.
    /// </summary>
    /// <param name="other">The GameObject to compare against.</param>
    /// <returns>True if equal, false otherwise.</returns>
    public override bool Equals(GameObject other) {
      return this.value == other;
    }
  }
}