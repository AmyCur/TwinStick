using System;
using System.Collections.Generic;
using Combat;
using UnityEngine;

namespace Inventory{
    public class InventoryClickController : Singleton<InventoryClickController>{


        [SerializeField] InventoryClickedData currentlyClickedRelic;
        [SerializeField] InventoryClickedData currentlyClickedWeapon;
        bool debugSlotClicked = false;
   
        public InventoryController ic => InventoryController.Instance;

        public void PickupItem(InventoryClickedData clickedItem){
            if(clickedItem.type == typeof(Relic)){
                currentlyClickedRelic=clickedItem;
            }
            if(clickedItem.type == typeof(Weapon)){
                currentlyClickedWeapon=clickedItem;
            }

            ic.UpdateInventories();
        }

        public void PutDownItem(InventoryClickedData clickedItem){
            if (clickedItem.type == typeof(Relic)){
                int i1 = currentlyClickedRelic.index;
                int i2 = clickedItem.index;

                ic.relicInventory.inventory[clickedItem.index] = (Relic)currentlyClickedRelic.item;
                ic.relicInventory.inventory[i1] = null;
                currentlyClickedRelic.item=null;
            }

            else if(clickedItem.type == typeof(Weapon)){
                int i1 = currentlyClickedWeapon.index;
                int i2 = clickedItem.index;

                ic.weaponInventory.inventory[clickedItem.index] = (Weapon)currentlyClickedWeapon.item;
                ic.weaponInventory.inventory[i1] = null;
                currentlyClickedWeapon.item=null;
            }

            ic.UpdateInventories();

        }

        public void SwapItem(InventoryClickedData clickedItem){

            if (clickedItem.type == typeof(Relic)){
                Relic i1 = ic.relicInventory.inventory[clickedItem.index];
                Relic i2 = ic.relicInventory.inventory[currentlyClickedRelic.index];

                ic.relicInventory.inventory[currentlyClickedRelic.index] = i1;
                ic.relicInventory.inventory[clickedItem.index] = i2;
                currentlyClickedRelic.item=null;
            }

            else if(clickedItem.type == typeof(Relic)){
                Weapon i1 = ic.weaponInventory.inventory[clickedItem.index];
                Weapon i2 = ic.weaponInventory.inventory[currentlyClickedWeapon.index];

                ic.weaponInventory.inventory[currentlyClickedWeapon.index] = i1;
                ic.weaponInventory.inventory[clickedItem.index] = i2;
                currentlyClickedWeapon.item=null;
            }

            ic.UpdateInventories();

        }

        public void HandleSlotClicked(int index, Type inventoryType) => HandleSlotClicked(new(index,inventoryType));

        public void HandleSlotClicked(InventoryClickedData clickedItem){
            if(debugSlotClicked) Debug.Log($"Slot {clickedItem.index} has been clicked in the {clickedItem.type} array");

            if(clickedItem.type==typeof(Relic)){
                clickedItem.item=ic.relicInventory.inventory[clickedItem.index];
            }
            
            if(clickedItem.type==typeof(Weapon)){
                clickedItem.item=ic.weaponInventory.inventory[clickedItem.index];
            }

            InventoryClickedData currentlyClickedItemData = clickedItem.type==typeof(Relic) ? currentlyClickedRelic : currentlyClickedWeapon;

            // If there is no item selected
            if(currentlyClickedItemData.item==null){
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