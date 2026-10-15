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
            InventoryController.Instance.UpdateInventories();
        }

        public void PutDownItem(InventoryClickedData clickedItem){
            if (clickedItem.type == typeof(Relic)){
                int i1 = currentlyClickedItem.index;
                int i2 = clickedItem.index;

                InventoryController.Instance.relicInventory.inventory[clickedItem.index] = (Relic)currentlyClickedItem.item;
                InventoryController.Instance.relicInventory.inventory[i2] = null;
                

            }

            else if(clickedItem.type == typeof(Weapon)){
                int i1 = currentlyClickedItem.index;
                int i2 = clickedItem.index;

                InventoryController.Instance.weaponInventory.inventory[clickedItem.index] = (Weapon)currentlyClickedItem.item;
                InventoryController.Instance.weaponInventory.inventory[i2] = null;
            }

            currentlyClickedItem.item=null;
            InventoryController.Instance.UpdateInventories();

        }

        public void SwapItem(InventoryClickedData clickedItem){
            if (clickedItem.type == typeof(Relic)){
                Relic i1 = InventoryController.Instance.relicInventory.inventory[clickedItem.index];
                Relic i2 = InventoryController.Instance.relicInventory.inventory[currentlyClickedItem.index];

                InventoryController.Instance.relicInventory.inventory[currentlyClickedItem.index] = i1;
                InventoryController.Instance.relicInventory.inventory[clickedItem.index] = i2;
            }

            else if(clickedItem.type == typeof(Relic)){
                Weapon i1 = InventoryController.Instance.weaponInventory.inventory[clickedItem.index];
                Weapon i2 = InventoryController.Instance.weaponInventory.inventory[currentlyClickedItem.index];

                InventoryController.Instance.weaponInventory.inventory[currentlyClickedItem.index] = i1;
                InventoryController.Instance.weaponInventory.inventory[clickedItem.index] = i2;
            }

            currentlyClickedItem.item=null;
            InventoryController.Instance.UpdateInventories();

        }

        public void HandleSlotClicked(int index, Type inventoryType) => HandleSlotClicked(new(index,inventoryType));

        public void HandleSlotClicked(InventoryClickedData clickedItem){
            if(debugSlotClicked) Debug.Log($"Slot {clickedItem.index} has been clicked in the {clickedItem.type} array");

            if(clickedItem.type==typeof(Relic)) clickedItem.item=InventoryController.Instance.relicInventory.inventory[clickedItem.index];
            if(clickedItem.type==typeof(Weapon)) clickedItem.item=InventoryController.Instance.weaponInventory.inventory[clickedItem.index];

            // If there is no item selected
            if(currentlyClickedItem.item==null){
                // If the item youve clicked on is an item (because if not, youve just tried to swap two empty inventory slots, which would achieve nothing)
                if(clickedItem.item != null){
                    Debug.Log("PickupItem"); 
                    PickupItem(clickedItem);
                    
                }
            }
            else{        
                // If youre holding an item and click on an empty slot, put it down        
                if ( clickedItem.item == null){
                    Debug.Log("PutItemDown"); 
                    PutDownItem(clickedItem);
                }

                // If youre holding an item and click on another item, swap them
                else
                {
                    Debug.Log("SwapItem"); 
                    SwapItem(clickedItem);
                }
            }
        }
    }
}