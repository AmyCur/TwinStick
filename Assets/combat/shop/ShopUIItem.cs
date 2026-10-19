using Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Combat.Shop{
    [System.Serializable]
    public class ShopUIItem{
        
        public GameObject sceneObject;
        
        public Image image;
        public Button button;
        public TMP_Text descriptionText;
        public TMP_Text titleText;
        public ShopItemData itemData;
    
        public void CreateItemCard(Item targetItem){
            itemData.item=targetItem;
            titleText.text=targetItem.name;
            image.sprite=itemData.item.sprite;
            descriptionText.text=itemData.item.description;
            sceneObject.SetActive(true);

        }

        public void Init(){
            itemData = sceneObject.GetComponent<ShopItemData>();
            titleText = sceneObject.transform.GetChild(0).GetComponent<TMP_Text>();
            descriptionText = sceneObject.transform.GetChild(2).GetComponent<TMP_Text>();
            button = sceneObject.transform.GetChild(3).GetComponent<Button>();
            image =  sceneObject.transform.GetChild(1).GetComponent<Image>();
            button.onClick.AddListener(() => itemData.GetItem());
        }

        public void RemoveItemCard()
        {
            sceneObject.SetActive(false);
        }

 
    
    }
}