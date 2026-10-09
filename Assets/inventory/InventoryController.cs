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
        public UIRelic[] relicInventory = {null,null,null,null,null,null,null,null,null};

        [Header("Utility Button Parameters")]
        public UIRelic itemToAdd;
        public int indexToChange;
        public ButtonPurpose purpose;


        public void AddItem(Item item, int index){
            if(relicInventory[index].item.itemName == "") relicInventory[index].item = item;
        }

        public void RemoveItem(int index){
            if(relicInventory[index] != null) relicInventory[index].item = null;
        }

        public void SwapItems(int i1, int i2){
            Item it1 = relicInventory[i1].item;
            Item it2 = relicInventory[i2].item;
            relicInventory[i1].item = it2;
            relicInventory[i2].item = it1;
        }

        public void UpdateIcons(){
            foreach(UIRelic item in relicInventory){
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
