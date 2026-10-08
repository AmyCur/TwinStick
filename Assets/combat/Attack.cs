using UnityEngine;

namespace Combat
{
    public abstract class Attack : ScriptableObject
    {
        public float damage;
        public float cooldown;

        public abstract void OnAttack();
    }
}