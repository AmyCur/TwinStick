using Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Combat.Shop{
    public class ShopUIItem{
        
        public GameObject sceneObject;
        
        public Image image{
            get{
                if(image!=null) return image;
                return sceneObject.GetComponent<Image>();
            }
        }

        public Button button{
            get{
                if(button!=null) return button;
                return sceneObject.GetComponent<Button>();
            }
        }

        public TMP_Text descriptionText
        {
            get
            {
                if(descriptionText!=null) return descriptionText;
                return sceneObject.GetComponent<TMP_Text>();
            }
        }

        public ShopItemData itemData{
            get{
                if(itemData!=null) return itemData;
                else if(sceneObject.GetComponent<ShopItemData>()!=null) return itemData = sceneObject.GetComponent<ShopItemData>();
                return itemData = sceneObject.AddComponent<ShopItemData>();
            }
            private set{
                itemData=value;
            }
        }
    
        public void CreateItemCard(Item targetItem){
            itemData.item=targetItem;
            image.sprite=itemData.item.sprite;
            descriptionText.text=itemData.item.description;
            sceneObject.SetActive(true);

        }

        public void RemoveItemCard()
        {
            sceneObject.SetActive(false);
        }

 
    
    }
}