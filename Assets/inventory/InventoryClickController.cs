using System;
using UnityEngine;

namespace Inventory{
    public class InventoryClickController : Singleton<InventoryClickController>{
        public InventoryController ic => InventoryController.Instance;

        public void HandleSlotClicked(int index, Type inventoryType){
            Debug.Log($"Slot {index} has been clicked in the {inventoryType} array");
        }
    }
}