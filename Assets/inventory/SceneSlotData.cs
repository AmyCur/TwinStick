using System;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class SceneSlotData : MonoBehaviour{
        public int index;
        public Type inventoryType;
        Button button;

        public void OnClicked() {
            InventoryClickController.Instance.HandleSlotClicked(index, inventoryType);
        }
        
        public void Init(int index, Type inventoryType)
        {
            this.index=index;
            this.inventoryType=inventoryType;
        }

        void Start()
        {
            button=GetComponent<Button>();
            button.onClick.AddListener(() => OnClicked());
        }
    }
}