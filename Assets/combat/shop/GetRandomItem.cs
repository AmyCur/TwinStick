using System.Collections.Generic;
using System.Linq;
using Inventory;
using UnityEngine;

namespace Combat.Shop
{
    public partial class ShopController : Singleton<ShopController>
    {                 
        // Removes all items that are already in the players inventory
        (List<Relic>, List<Weapon>) RemoveAllDuplicateItems(){
            // So its not a pointer
            List<Relic> pr = new(possibleRelics);
            List<Weapon> pw = new(possibleWeapons);


            foreach(Relic r in possibleRelics){
                foreach(Relic ir in InventoryController.Instance.relicInventory.inventory)
                {
                    if(ir!=null && r.name==ir.name) pr.Remove(r);
                }
            }

            foreach(Weapon w in possibleWeapons){
                foreach(Weapon iw in InventoryController.Instance.weaponInventory.inventory)
                {
                    if(iw!=null && w.name==iw.name) pw.Remove(w);
                }
            }

            
            Debug.Log($@"
                Possible Relics [{string.Join(",", pr)}],
                Current Relics [{string.Join<Relic>(", ", InventoryController.Instance.relicInventory.inventory)}]
            ");

             Debug.Log($@"
                Possible Weapons [{string.Join(",", pw)}],
                Current Weapons [{string.Join<Weapon>(", ", InventoryController.Instance.weaponInventory.inventory)}]
            ");



            return (pr, pw);
        }

        List<Item> GetAllCurrentlySoldItems(){
            List<Item> its = new();
            foreach(ShopUIItem sui in shopUIItems)
            {
                if(sui.itemData.item!=null) its.Add(sui.itemData.item);
            }

            return its;
        }

        (List<Relic>, List<Weapon>) GetPossibleItems((List<Relic> relics, List<Weapon> weapons) rw){
            List<Item> currentlySoldItems = GetAllCurrentlySoldItems();

            List<Weapon> pw = rw.weapons;
            List<Relic> pr = rw.relics;

            foreach(Item i in currentlySoldItems)
            {
                if(i is Relic r && pr.Contains(r)) pr.Remove(r);
                if(i is Weapon w && pw.Contains(w)) pw.Remove(w);
            }

            return (pr, pw);
        }

        Item ChooseRandomItem(){
            (List<Relic> relics, List<Weapon> weapons) removedDupes = RemoveAllDuplicateItems();
            (List<Relic> relics, List<Weapon> weapons) possibleItems = GetPossibleItems(removedDupes);

            Debug.Log($"Possible Relics: {string.Join(" | ", removedDupes.relics)}");
            Debug.Log($"Possible Weapons: {string.Join(" | ", removedDupes.weapons)}");       

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
            if(weight <= weaponWeight) return possibleItems.weapons[UnityEngine.Random.Range(0, possibleItems.weapons.Count)];
            return possibleItems.relics[UnityEngine.Random.Range(0, possibleItems.relics.Count)]; 
        }
    }
}