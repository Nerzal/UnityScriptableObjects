using UnityEngine;

#pragma warning disable 649

namespace Events {
    /// <summary>
    /// A listener for AttackGameEvent that invokes a UnityEvent when the event is raised.
    /// Automatically registers and unregisters with the event on enable/disable.
    /// </summary>
    public class AttackGameEventListener : GenericGameEventListener<Attack>
    {
        /// <summary>
        /// The AttackGameEvent to listen to.
        /// </summary>
        [SerializeField] private AttackGameEvent _gameEvent;

        /// <summary>
        /// The UnityEvent to invoke when the event is raised, passing the Attack argument.
        /// </summary>
        [SerializeField] private AttackUnityEvent _unityEvent;

        /// <summary>
        /// Called when the script is enabled. Registers this listener with the event.
        /// </summary>
        private void OnEnable()
        {
            this._gameEvent.RegisterListener(this);
        }

        /// <summary>
        /// Called when the script is disabled. Unregisters this listener from the event.
        /// </summary>
        private void OnDisable()
        {
            this._gameEvent.UnregisterListener(this);
        }

        /// <summary>
        /// Called by the AttackGameEvent when raised. Invokes the UnityEvent with the Attack argument.
        /// </summary>
        /// <param name="arg">The Attack argument passed by the event.</param>
        public override void OnEventRaised(Attack arg)
        {
            this._unityEvent?.Invoke(arg);
        }
    }
}