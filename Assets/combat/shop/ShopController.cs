using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Inventory;
using System;
using System.Linq;

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

        Item ChooseRandomItem(){
            RemoveAllDuplicateItems();

            Weapon randomWeapon = possibleWeapons[UnityEngine.Random.Range(0, possibleWeapons.Count)];
            Relic randomRelic = possibleRelics[UnityEngine.Random.Range(0, possibleRelics.Count)];

            // If weapon inventory is full and relic inventory is not
            if(InventoryController.Instance.weaponInventory.Full() && !InventoryController.Instance.relicInventory.Full()) return randomRelic;
           
            // If weapon inventory is not full and relic inventory is
            if(!InventoryController.Instance.weaponInventory.Full() && InventoryController.Instance.relicInventory.Full()) return randomWeapon;

            float weight = UnityEngine.Random.Range(0, relicWeight+weaponWeight);
            if(weight <= weaponWeight) return possibleWeapons[UnityEngine.Random.Range(0, possibleWeapons.Count)];
            return possibleRelics[UnityEngine.Random.Range(0, possibleRelics.Count)]; 
        }

        IEnumerator CreateShopMenu(){
            shopUI.SetActive(true);
            foreach(ShopUIItem sui in shopUIItems){
                sui.CreateItemCard(ChooseRandomItem());
                yield return new WaitForSeconds(1f);            
            }
        }

        public IEnumerator CloseShopMenu(){
            shopUIItems.Reverse();

            foreach(ShopUIItem sui in shopUIItems){
                sui.RemoveItemCard();
                yield return new WaitForSeconds(1f);            
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