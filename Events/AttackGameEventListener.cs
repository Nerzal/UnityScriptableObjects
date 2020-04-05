using UnityEngine;

#pragma warning disable 649

namespace Events {
    public class AttackGameEventListener : GenericGameEventListener<Attack>
    {
        [SerializeField] private AttackGameEvent _gameEvent;
        [SerializeField] private AttackUnityEvent _unityEvent;


        private void OnEnable()
        {
            this._gameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            this._gameEvent.UnregisterListener(this);
        }

        public override void OnEventRaised(Attack arg)
        {
            this._unityEvent?.Invoke(arg);
        }
    }
}