using Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Combat.Shop{
    [System.Serializable]
    public class ShopUIItem{
        
        public GameObject sceneObject;
        
        [HideInInspector] public Image image;
        [HideInInspector] public Button button;
        [HideInInspector] public TMP_Text descriptionText;
        [HideInInspector] public TMP_Text titleText;
        [HideInInspector] public ShopItemData itemData;
    
        public void CreateItemCard(Item targetItem){
            itemData.item=targetItem;
            titleText.text=targetItem.name;
            image.sprite=itemData.item.sprite;
            descriptionText.text=itemData.item.description;
            button.onClick.AddListener(() => itemData.GetItem());
            sceneObject.SetActive(true);
        }

        public void Init(){
            itemData = sceneObject.GetComponent<ShopItemData>();
            titleText = sceneObject.transform.GetChild(0).GetComponent<TMP_Text>();
            descriptionText = sceneObject.transform.GetChild(2).GetComponent<TMP_Text>();
            button = sceneObject.GetComponent<Button>();
            image =  sceneObject.transform.GetChild(1).GetComponent<Image>();
        }

        public void RemoveItemCard()
        {
            sceneObject.SetActive(false);
        }

 
    
    }
}