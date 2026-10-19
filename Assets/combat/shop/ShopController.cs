using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Inventory;
using System;
using System.Linq;
using Util;

namespace Combat.Shop{
    public class ShopController : Singleton<ShopController>{
        
        [SerializeField] List<ShopUIItem> shopUIItems = new();
        
        [Header("Possible Items")]
        [SerializeField] List<Relic> possibleRelics = new();
        [SerializeField] List<Weapon> possibleWeapons = new();

        [Header("Weights")]
        [SerializeField] float relicWeight = 60;
        [SerializeField] float weaponWeight = 40;

        [SerializeField] GameObject shopUI;
         
        // Removes all items that are already in the players inventory
        void RemoveAllDuplicateItems(){
            Relic[] pr = new Relic[possibleRelics.Count];
            Weapon[] wp = new Weapon[possibleWeapons.Count];
            possibleRelics.CopyTo(pr);
            possibleWeapons.CopyTo(wp);

            foreach(Relic r in pr){
                if(InventoryController.Instance.relicInventory.inventory.Contains(r)) possibleRelics.Remove(r);
            }

            foreach(Weapon w in wp){
                if(InventoryController.Instance.weaponInventory.inventory.Contains(w)) possibleWeapons.Remove(w);
            }
        }

        bool ItemAlreadyInShop(Item item)
        {
            foreach(ShopUIItem sui in shopUIItems){
                if(sui.itemData.item == item) return true;
            }

            return false;
        }

        List<Item> GetAllCurrentlySoldItems(){
            List<Item> its = new();
            foreach(ShopUIItem sui in shopUIItems)
            {
                if(sui!=null) its.Add(sui.itemData.item);
            }

            return its;
        }


        (List<Relic>, List<Weapon>) GetPossibleItems()
        {
            List<Item> currentlySoldItems = GetAllCurrentlySoldItems();

            List<Weapon> pw = possibleWeapons;
            List<Relic> pr = possibleRelics;

            foreach(Item i in currentlySoldItems)
            {
                if(i is Relic r && pr.Contains(r)) pr.Remove(r);
                if(i is Weapon w && pw.Contains(w)) pw.Remove(w);
            }

            return (pr,pw);
        }

        Item ChooseRandomItem(){
            RemoveAllDuplicateItems();

            (List<Relic> relics, List<Weapon> weapons) possibleItems = GetPossibleItems();

            

            bool relicsPossible = possibleItems.relics.Count!=0;
            bool weaponsPossible = possibleItems.weapons.Count!=0;

            // If you have every item
            if(!relicsPossible && !weaponsPossible){
                return null;
            }

            Weapon randomWeapon = possibleItems.weapons[UnityEngine.Random.Range(0, possibleItems.weapons.Count)];
            Relic randomRelic = possibleItems.relics[UnityEngine.Random.Range(0, possibleItems.relics.Count)];

            // If weapon inventory is full and relic inventory is not
            if(InventoryController.Instance.weaponInventory.Full() && !InventoryController.Instance.relicInventory.Full()) return randomRelic;
           
            // If weapon inventory is not full and relic inventory is
            if(!InventoryController.Instance.weaponInventory.Full() && InventoryController.Instance.relicInventory.Full()) return randomWeapon;

            float weight = UnityEngine.Random.Range(0, relicWeight+weaponWeight);
            if(weight <= weaponWeight) return this.possibleWeapons[UnityEngine.Random.Range(0, this.possibleWeapons.Count)];
            return this.possibleRelics[UnityEngine.Random.Range(0, this.possibleRelics.Count)]; 
        }

        IEnumerator CreateShopMenu(){
            TimeUtil.PauseGame();
            shopUI.SetActive(true);
            foreach(ShopUIItem sui in shopUIItems){
                sui.itemData.item=null;
                Item ri = ChooseRandomItem();
                if(ri!=null) sui.CreateItemCard(ri);
                yield return new WaitForSeconds(0.3f);            
            }
        }

        public IEnumerator CloseShopMenu(){
            TimeUtil.ResumeGame();

            foreach(ShopUIItem sui in shopUIItems){
                sui.button.onClick.RemoveAllListeners();
            }
            
            shopUIItems.Reverse();

            foreach(ShopUIItem sui in shopUIItems){
                sui.RemoveItemCard();
                yield return new WaitForSeconds(0.3f);            
            }

            shopUIItems.Reverse();
            shopUI.SetActive(false);
        }
        
        public void TriggerShop(){
            StartCoroutine(CreateShopMenu());
        }

        void Start(){
            shopUI.SetActive(false);
            foreach(ShopUIItem sui in shopUIItems){
                sui.Init();
            }
        }
    }
}