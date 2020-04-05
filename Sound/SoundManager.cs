
using Events;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : MonoBehaviour {
        [SerializeField] private AudioSource _audioSource;

        public void OnSound(PlaySoundEventArgs args) {
            if (this.transform.GetInstanceID() != args.ReceiverInstanceID) {
                return;
            }

            this._audioSource.PlayOneShot(args.Sound);
        }
    }
}
