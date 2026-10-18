using UnityEngine;

namespace Combat.Shop{
    public class CloseShopButtonController : MonoBehaviour
    {
        public void CloseShop(){
            StartCoroutine(ShopController.Instance.CloseShopMenu());
        }
    }
}