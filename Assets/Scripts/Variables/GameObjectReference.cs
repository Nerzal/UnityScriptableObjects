using System;
using UnityEngine;

namespace Variables {
  /// <summary>
  /// A reference that can hold either a constant GameObject or a reference to a GameObjectVariable.
  /// </summary>
  [Serializable]
  public class GameObjectReference : BaseReference<GameObject, GameObjectVariable> {

  }
}