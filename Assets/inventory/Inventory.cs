using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory{
    [System.Serializable]
    public class Inventory<T> where T : Item {
        public T[] inventory;
        public Image[] sceneSlots;
        
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

        public void UpdateUI(){
            for(int i = 0; i < sceneSlots.Length; i++){
                Image spriteImage = sceneSlots[i].transform.GetChild(0).GetComponent<Image>();
                spriteImage.sprite = inventory[i].sprite;
            }
        }

        public void Toggle(){
            sceneObject.SetActive(!sceneObject.activeInHierarchy);
        }
    }
}