using UnityEngine;

namespace Events {
    public class PlaySoundEventListener : GenericGameEventListener<PlaySoundEventArgs>
    {
        [SerializeField] private PlaySoundEvent _gameEvent;
        [SerializeField] private PlaySoundUnityEvent _unityEvent;


        private void OnEnable()
        {
            this._gameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            this._gameEvent.UnregisterListener(this);
        }

        public override void OnEventRaised(PlaySoundEventArgs arg)
        {
            this._unityEvent?.Invoke(arg);
        }
    }
}