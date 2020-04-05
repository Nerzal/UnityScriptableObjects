using UnityEngine;

#pragma warning disable 649

namespace Events {

    [CreateAssetMenu(menuName = "Events/AttackGameEvent")]
    public class AttackGameEvent : GenericGameEvent<Attack>
    {

    }
}