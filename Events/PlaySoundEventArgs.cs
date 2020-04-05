using UnityEngine;

namespace Events
{

    // PlaySoundEventArgs are usd as argument for playSoundEventArgs
    public class PlaySoundEventArgs
    {
        // ReceiverInstanceID is the instanceID of the receiver object that plays the sound
        public int ReceiverInstanceID;

        // Sound is the audioClip to play on the receiver
        public AudioClip Sound;

        // PlaySoundEventArgs is a ctor
        public PlaySoundEventArgs(int receiverInstanceId, AudioClip sound)
        {
            this.ReceiverInstanceID = receiverInstanceId;
            this.Sound = sound;
        }
    }
}