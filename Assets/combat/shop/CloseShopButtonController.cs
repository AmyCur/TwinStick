using UnityEngine;

namespace Combat.Shop{
    public class CloseShopButtonController : Singleton<CloseShopButtonController>
    {
        public void CloseShop(){
            StartCoroutine(ShopController.Instance.CloseShopMenu());
        }
    }
}