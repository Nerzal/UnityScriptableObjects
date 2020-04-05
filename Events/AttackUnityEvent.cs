using System;
using UnityEngine.Events;

#pragma warning disable 649

namespace Events {
    [Serializable]
    public class AttackUnityEvent : UnityEvent<Attack>
    {

    }
}