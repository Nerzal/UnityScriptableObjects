using UnityEngine;

#pragma warning disable 649

namespace Events {
    /// <summary>
    /// A specific GenericGameEvent for Attack events, allowing typed event raising with Attack arguments.
    /// </summary>
    [CreateAssetMenu(menuName = "Events/AttackGameEvent")]
    public class AttackGameEvent : GenericGameEvent<Attack>
    {

    }
}