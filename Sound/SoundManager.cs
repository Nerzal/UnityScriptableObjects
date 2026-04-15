
using Events;
using UnityEngine;

namespace Sound
{
    /// <summary>
    /// Manages sound playback for a specific GameObject using an AudioSource.
    /// Listens to PlaySoundEventArgs and plays sounds only if the receiver matches this instance.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : MonoBehaviour {
        /// <summary>
        /// The AudioSource component used to play sounds.
        /// </summary>
        [SerializeField] private AudioSource _audioSource;

        /// <summary>
        /// Called when a PlaySoundEvent is raised. Plays the sound if this instance is the intended receiver.
        /// </summary>
        /// <param name="args">The arguments containing the sound clip and receiver ID.</param>
        public void OnSound(PlaySoundEventArgs args) {
            if (this.transform.GetInstanceID() != args.ReceiverInstanceID) {
                return;
            }

            this._audioSource.PlayOneShot(args.Sound);
        }
    }
}
