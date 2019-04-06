using UnityEngine;

namespace Events {
  public class StringEventListener : GenericGameEventListener<string> {
    [SerializeField] private StringGameEvent _event; 
    [SerializeField] private StringUnityEvent _unityEvent;


    private void OnEnable() {
      this._event.RegisterListener(this);
    }

    private void OnDisable() {
      this._event.UnregisterListener(this);
    }

    public override void OnEventRaised(string arg) {
      this._unityEvent?.Invoke(arg);
    }
  }
}