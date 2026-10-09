using UnityEngine;

namespace Inventory
{
    [System.Serializable]
    public abstract class Item : ScriptableObject
    {
        public string itemName;
        public string itemDescription;
    }
}