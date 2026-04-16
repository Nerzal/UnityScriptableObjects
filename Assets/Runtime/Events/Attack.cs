#pragma warning disable 649

namespace Events
{
    public class Attack
    {
        public int InstanceID;
        public float Damage;

        // Ctor
        public Attack(int instanceId, float damage)
        {
            InstanceID = instanceId;
            Damage = damage;
        }
    }
}