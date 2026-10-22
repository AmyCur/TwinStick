using Combat;
using UnityEngine;

namespace Inventory{

    public enum ForceTriggerType : byte{
        index,
        offset,
        rarity,
        random
    }

    [System.Serializable]
    public class ForceTriggerCondition{
        public ForceTriggerType triggerType;
        public int index;
        public int offset;
        public Rarity rarity;
    }
}