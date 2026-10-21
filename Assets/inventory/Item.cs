using UnityEngine;

namespace Inventory{
    public abstract class Item : ScriptableObject{
        [Header("Data")]
        [TextArea(5, 30)]
        public string description;
        public Sprite sprite;

        [Header("Functionality")]
        public Cooldown cooldown = new(null);
        public int index;

        public abstract void ForceTrigger();
    }
}