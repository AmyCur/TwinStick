using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory{
    [System.Serializable]
    public class Inventory<T> where T : Item {
        public T[] inventory;
        public Image[] sceneSlots;
        [HideInInspector] public InventoryClickedData currentlyClickedItem;
        
        [Tooltip("The object which holds all of the scene slots")]
        public GameObject sceneObject;

        public bool UIIsActive => sceneObject.activeInHierarchy;
        
        public void Add(T item){
            // Look for available spot in the inventory
            for(int i = 0; i < inventory.Length; i++){
                // If available add the item
                if (inventory[i] == null){
                    inventory[i]=item;
                    break;
                }
            }
        }

        public void Remove(int index){
            inventory[index] = default(T);
        }

        public void Swap(int i1, int i2){
            T t1 = inventory[i1];
            T t2 = inventory[i2];

            inventory[i1]=t2;
            inventory[i2]=t1;
        }

        public bool Full(){
            
            foreach(T t in inventory){
                if(t==default(T)) return false;
            }

            return true;
        }

        public void UpdateUI(){
            for(int i = 0; i < sceneSlots.Length; i++){
                Image spriteImage = sceneSlots[i].transform.GetChild(0).GetComponent<Image>();
                spriteImage.sprite = inventory[i]?.sprite != null ? inventory[i].sprite : Resources.Load<Sprite>("nullFallbackSprite");    
                spriteImage.enabled=inventory[i]!=null;
                
                if(inventory[i]!=null) inventory[i].cooldown.cdBar = sceneSlots[i].transform.GetChild(1).GetComponent<Image>();
            }
        }

        public void Toggle(){
            sceneObject.SetActive(!sceneObject.activeInHierarchy);
        }

        public void AddSceneSlotData(){
            for(int i = 0 ; i < sceneSlots.Length; i++){
                SceneSlotData scd = sceneSlots[i].gameObject.AddComponent<SceneSlotData>();
                scd.Init(i, typeof(T));
            }
        }

        public void PickupItem(InventoryClickedData clickedItem){
            currentlyClickedItem=clickedItem;      

            UpdateUI();
        }

        public void PutDownItem(InventoryClickedData clickedItem){
            
            int i1 = currentlyClickedItem.index;
            int i2 = clickedItem.index;

            inventory[clickedItem.index] = (T)currentlyClickedItem.item;
            inventory[i1] = null;
            currentlyClickedItem.item=null;

            UpdateUI();
        }

        public void SwapItem(InventoryClickedData clickedItem){
            T i1 = inventory[clickedItem.index];
            T i2 =inventory[currentlyClickedItem.index];

            inventory[currentlyClickedItem.index] = i1;
            inventory[clickedItem.index] = i2;
            currentlyClickedItem.item=null;

            UpdateUI();
        }

        public void HandleSlotClicked(InventoryClickedData clickedItem){
            // if(debugSlotCli) Debug.Log($"Slot {clickedItem.index} has been clicked in the {clickedItem.type} array");
            clickedItem.item=inventory[clickedItem.index];

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


        public void Init(){
            UpdateUI();
            AddSceneSlotData();
        }
    }
}