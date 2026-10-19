using Inventory;
using UnityEngine;

namespace Combat.Shop
{
    public class ShopItemData : MonoBehaviour{
        public Item item;

        public void GetItem(){
            if(item is Relic relic) InventoryController.Instance.relicInventory.Add(relic);
            if(item is Weapon weapon) InventoryController.Instance.weaponInventory.Add(weapon);

            CloseShopButtonController.Instance.CloseShop();

            if(item is Weapon)
            {
                foreach(Weapon w in InventoryController.Instance.weaponInventory.inventory){
                    if(w!=null) w.cooldown.onCD=false;
                }
            }

            if(item is Relic){
                foreach(Relic r in InventoryController.Instance.relicInventory.inventory){
                    if(r!=null) r.cooldown.onCD=false;
                }
            }
        }
    }
}