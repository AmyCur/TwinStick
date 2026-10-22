using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Inventory;
using System;
using System.Linq;
using Util;

namespace Combat.Shop{
    public partial class ShopController : Singleton<ShopController>{
        
        [SerializeField] List<ShopUIItem> shopUIItems = new();
        
        WaitForSeconds TimeBetweenItemLoad = new(0.3f);
        
        [Header("Possible Items")]
        static List<Relic> possibleRelics = new();
        static List<Weapon> possibleWeapons = new();

        [Header("Weights")]
        [SerializeField] float relicWeight = 60;
        [SerializeField] float weaponWeight = 40;

        [SerializeField] GameObject shopUI;        

        IEnumerator CreateShopMenu(){
            
            shopUI.SetActive(true); 

            foreach(ShopUIItem sui in shopUIItems){
                Debug.Log("hi");
                sui.itemData.item=null;
                Item ri = ChooseRandomItem();
                if(ri!=null) sui.CreateItemCard(ri);
                yield return TimeBetweenItemLoad;            
            }
                        
            TimeUtil.PauseGame();
        }

        public IEnumerator CloseShopMenu(){
            TimeUtil.ResumeGame();

            foreach(ShopUIItem sui in shopUIItems){
                sui.button.onClick.RemoveAllListeners();
            }
            
            shopUIItems.Reverse();

            foreach(ShopUIItem sui in shopUIItems){
                sui.RemoveItemCard();
                yield return TimeBetweenItemLoad;          
            }

            shopUIItems.Reverse();
            shopUI.SetActive(false);
        }
        
        public void TriggerShop(){
            foreach(BouncingBall b in GameObject.FindObjectsByType<BouncingBall>(FindObjectsSortMode.None)){
                b.DestroyBall();
            }
            StartCoroutine(CreateShopMenu());
        }

        void Start(){
            foreach(ShopUIItem sui in shopUIItems){
                sui.Init();
            }
            shopUI.SetActive(false);

            possibleRelics = Resources.LoadAll<Relic>("relics").ToList();
            possibleWeapons = Resources.LoadAll<Weapon>("weapons").ToList();
        }
    }
}