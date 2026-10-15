using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventorySlotController : MonoBehaviour{

        enum InventoryType{
            relics,
            attacks    
        }

        [SerializeField] InventoryType iType;

        // public int GetSlotIndex(){
        //     if (iType == InventoryType.relics)
        //     {
        //         for(int i = 0; i < InventoryController.instance.relicInventory.sceneSlots.Length){
        //             return InventoryController.instance.relicInventory.sceneSlots[i].GetComponent<InventorySlotData>().index;
        //         }
              
        //     }
        // }

        // public void OnClick(){
        //     if(iType==InventoryType.relics) InventoryController.instance.relicInventory.SlotClicked();
        // }
    }
}