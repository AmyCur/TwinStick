using System;
using System.Collections.Generic;
using Combat;
using UnityEngine;

namespace Inventory{
    public class InventoryClickController : Singleton<InventoryClickController>{

        [SerializeField] InventoryClickedData currentlyClickedItem;
        bool debugSlotClicked = false;
   
        public InventoryController ic => InventoryController.Instance;

        public void PickupItem(InventoryClickedData clickedItem){
            currentlyClickedItem=clickedItem;
        }

        public void PutDownItem(){
            
        }

        public void SwapItem(){
            
        }

        public void HandleSlotClicked(int index, Type inventoryType) => HandleSlotClicked(new(index,inventoryType));

        public void HandleSlotClicked(InventoryClickedData clickedItem){
            if(debugSlotClicked) Debug.Log($"Slot {clickedItem.index} has been clicked in the {clickedItem.type} array");

            if(clickedItem.type==typeof(Relic)) clickedItem.item=InventoryController.Instance.relicInventory.inventory[clickedItem.index];
            if(clickedItem.type==typeof(Attack)) clickedItem.item=InventoryController.Instance.weaponInventory.inventory[clickedItem.index];

            if(currentlyClickedItem==null){
                if(clickedItem.type==typeof(Relic) && clickedItem.item!=null) PickupItem(clickedItem);
            }
            else{
                if (clickedItem.type == typeof(Relic)){
                    
                }
            }
        }
    }
}