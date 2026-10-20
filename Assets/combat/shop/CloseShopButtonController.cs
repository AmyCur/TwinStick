using Combat.Rounds;
using UnityEngine;

namespace Combat.Shop{
    public class CloseShopButtonController : Singleton<CloseShopButtonController>{
        public void CloseShop(){
            RoundController.Instance.ProgressRound();
            StartCoroutine(ShopController.Instance.CloseShopMenu());
        }
    }
}