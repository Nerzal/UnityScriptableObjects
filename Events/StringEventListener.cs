using UnityEngine;

namespace Events {
  public class StringEventListener : GenericGameEventListener<string> {
    [SerializeField] private StringGameEvent _event; 
    [SerializeField] private StringUnityEvent _unityEvent;


    private void OnEnable() {
            _event.RegisterListener(this);
    }

    private void OnDisable() {
      _event.UnregisterListener(this);
    }

    public override void OnEventRaised(string arg) {
      _unityEvent?.Invoke(arg);
    }
  }
}