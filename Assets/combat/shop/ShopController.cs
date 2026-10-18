using UnityEngine;

namespace Combat.Shop{
    public class ShopController : Singleton<ShopController>{
        public void TriggerShop(){
            Debug.Log("Shop Triggered");
        }
    }
}