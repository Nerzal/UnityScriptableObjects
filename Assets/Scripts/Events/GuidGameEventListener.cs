using System;
using UnityEngine;

namespace Events {
  public class GuidGameEventListener : GenericGameEventListener<Guid> {
    [SerializeField] private GuidGameEvent _event;
    [SerializeField] private GuidUnityEvent _unityEvent;


    private void OnEnable() {
      this._event.RegisterListener(this);
    }

    private void OnDisable() {
      this._event.UnregisterListener(this);
    }

    public override void OnEventRaised(Guid arg) {
      this._unityEvent?.Invoke(arg);
    }
  }
}