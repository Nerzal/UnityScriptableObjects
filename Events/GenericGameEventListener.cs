using UnityEngine;

namespace Events {
  public abstract class GenericGameEventListener<T> : MonoBehaviour {
    public abstract void OnEventRaised(T o);
  }
}