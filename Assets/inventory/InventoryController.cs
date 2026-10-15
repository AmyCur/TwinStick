using Combat;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Inventory{
    public sealed class InventoryController : Singleton<InventoryController>{

        [Header("Inventories")]
        public Inventory<Relic> relicInventory;
        public Inventory<Weapon> weaponInventory;

        public void HandleTrigger(TriggerCondition condition){
            foreach(Relic r in relicInventory.inventory){
                if (r != null){
                    if (r.triggerCondition == condition && !r.cooldown.onCD){
                        r.OnTrigger();
                    }
                }
            }
        }

        public void UpdateInventories(){
            relicInventory.UpdateUI();
            weaponInventory.UpdateUI();
        }

        void Start()
        {
            relicInventory.Init();
            weaponInventory.Init();
        }
        void OnValidate() => UpdateInventories();
    }
}