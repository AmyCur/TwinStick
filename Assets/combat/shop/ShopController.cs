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
        (List<Relic>, List<Weapon>) RemoveAllDuplicateItems(){
            List<Relic> pr = possibleRelics;
            List<Weapon> pw = possibleWeapons;
            
            for(int i = 0; i < possibleRelics.Count; i++){
                if(InventoryController.Instance.relicInventory.inventory.Contains(possibleRelics[i])) pr.RemoveAt(i);
            }

            for(int i = 0; i < possibleWeapons.Count; i++){
                if(InventoryController.Instance.weaponInventory.inventory.Contains(possibleWeapons[i])) pw.RemoveAt(i);
            }

            

            return (pr,pw);
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


        (List<Relic>, List<Weapon>) GetPossibleItems((List<Relic> relics, List<Weapon> weapons) rw)
        {
            List<Item> currentlySoldItems = GetAllCurrentlySoldItems();

            List<Weapon> pw = rw.weapons;
            List<Relic> pr = rw.relics;

            foreach(Item i in currentlySoldItems)
            {
                if(i is Relic r && pr.Contains(r)) pr.Remove(r);
                if(i is Weapon w && pw.Contains(w)) pw.Remove(w);
            }

        return (pr,pw);
        }

        Item ChooseRandomItem(){
            (List<Relic> relics, List<Weapon> weapons) removedDupes = RemoveAllDuplicateItems();
            (List<Relic> relics, List<Weapon> weapons) possibleItems = GetPossibleItems(removedDupes);


           


            Debug.Log($"Possible Relics: {string.Join(" | ", possibleItems.relics)}");
            Debug.Log($"Possible Weapons: {string.Join(" | ", possibleItems.weapons)}");

            

            bool relicsPossible = possibleItems.relics.Count!=0;
            bool weaponsPossible = possibleItems.weapons.Count!=0;

            // If you have every item
            if(!relicsPossible && !weaponsPossible){
                return null;
            }

            Weapon randomWeapon = possibleItems.weapons.Count != 0 ? possibleItems.weapons[UnityEngine.Random.Range(0, possibleItems.weapons.Count)] : null;
            Relic randomRelic = possibleItems.relics.Count != 0 ? possibleItems.relics[UnityEngine.Random.Range(0, possibleItems.relics.Count)] : null;

            // If weapon inventory is full, or there are no possible weapons, and relic inventory is not
            if((InventoryController.Instance.weaponInventory.Full() || possibleItems.weapons.Count==0) && !InventoryController.Instance.relicInventory.Full()) return randomRelic;
           
            // If weapon inventory is not full and relic inventory is
            if(!InventoryController.Instance.weaponInventory.Full() && (InventoryController.Instance.relicInventory.Full() || possibleItems.relics.Count==0)) return randomWeapon;

            float weight = UnityEngine.Random.Range(0, relicWeight+weaponWeight);
            if(weight <= weaponWeight) return this.possibleWeapons[UnityEngine.Random.Range(0, this.possibleWeapons.Count)];
            return this.possibleRelics[UnityEngine.Random.Range(0, this.possibleRelics.Count)]; 
        }

        IEnumerator CreateShopMenu(){
            shopUI.SetActive(true);
            foreach(ShopUIItem sui in shopUIItems){
                Debug.Log("hi");
                sui.itemData.item=null;
                Item ri = ChooseRandomItem();
                if(ri!=null) sui.CreateItemCard(ri);
                yield return new WaitForSeconds(0.3f);            
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
                yield return new WaitForSeconds(0.3f);            
            }

            shopUIItems.Reverse();
            shopUI.SetActive(false);
        }
        
        public void TriggerShop(){
            StartCoroutine(CreateShopMenu());
        }

        void Start(){
            foreach(ShopUIItem sui in shopUIItems){
                sui.Init();
            }
            shopUI.SetActive(false);
        }
    }
}