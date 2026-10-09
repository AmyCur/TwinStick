using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Inventory{

    public enum ButtonPurpose{
        add,
        remove
    }

    public class InventoryController : MonoBehaviour
    {
        public static InventoryController instance
        {
            get{return GameObject.Find("Player").GetComponent<InventoryController>();}
        }

        // 3x3 for 9 total
        public UIItem[] inventory = {null,null,null,null,null,null,null,null,null};

        [Header("Utility Button Parameters")]
        public UIItem itemToAdd;
        public int indexToChange;
        public ButtonPurpose purpose;


        public void AddItem(Item item, int index){
            if(inventory[index].item.itemName == "") inventory[index].item = item;
        }

        public void RemoveItem(int index){
            if(inventory[index] != null) inventory[index].item = null;
        }

        public void SwapItems(int i1, int i2){
            Item it1 = inventory[i1].item;
            Item it2 = inventory[i2].item;
            inventory[i1].item = it2;
            inventory[i2].item = it1;
        }

        public void UpdateIcons(){
            foreach(UIItem item in inventory){
                Image itemImg = item.sceneObj.transform.GetChild(0).GetComponent<Image>();
                itemImg.sprite=item.sprite;
                itemImg.enabled = itemImg.sprite!=null;
            }
        }

        void Start()
        {
            UpdateIcons();
        }


     
    }
}
