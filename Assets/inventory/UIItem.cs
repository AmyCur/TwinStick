using UnityEngine;

namespace Inventory{
    [System.Serializable]
    public abstract class UIItem
    {
        public Item item;
        public GameObject sceneObj;
        public Sprite sprite;
    }

    [System.Serializable]
    public class UIRelic : UIItem{
        new public Relic item;
    }
}