using UnityEngine;

namespace Events {
    public class PlaySoundEventListener : GenericGameEventListener<PlaySoundEventArgs>
    {
        [SerializeField] private PlaySoundEvent _gameEvent;
        [SerializeField] private PlaySoundUnityEvent _unityEvent;


        private void OnEnable()
        {
            _gameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            _gameEvent.UnregisterListener(this);
        }

        public override void OnEventRaised(PlaySoundEventArgs arg)
        {
            _unityEvent?.Invoke(arg);
        }
    }
}