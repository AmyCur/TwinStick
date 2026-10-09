using UnityEngine;

namespace Inventory{
    public abstract class Item : ScriptableObject{
        public string description;
        public Sprite sprite;
    }
}