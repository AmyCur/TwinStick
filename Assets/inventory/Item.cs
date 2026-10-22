using Combat;
using UnityEngine;

namespace Inventory{
    public abstract class Item : ScriptableObject{
        [Header("Data")]
        [TextArea(5, 30)]
        public string description;
        public Sprite sprite;

        [Header("Functionality")]
        public Cooldown cooldown = new(null);
        public Cooldown forceTriggerCooldown = new(null, 10f);
        public int index;
        public Rarity rarity=Rarity.common;

        public abstract void ForceTrigger();
    }
}