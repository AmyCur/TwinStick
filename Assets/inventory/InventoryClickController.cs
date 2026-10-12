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

        // TODO this!!! figure out how to easily reference the correct inventory without having to call an if every time
        dynamic Inventory<T>(){
            if(typeof(T) == typeof(Relic)) return InventoryController.Instance.relicInventory;
            if(typeof(T) == typeof(Attack)) return InventoryController.Instance.weaponInventory;
            else return null;
        }


        public void HandleSlotClicked(InventoryClickedData clickedItem){
            if(debugSlotClicked) Debug.Log($"Slot {clickedItem.index} has been clicked in the {clickedItem.type} array");

            if(currentlyClickedItem==null){
                PickupItem(clickedItem);
            }
            else{
                if (clickedItem.type == typeof(Relic)){
                    // InventoryController.Instance.relicInventory
                }
            }
        }
    }
}