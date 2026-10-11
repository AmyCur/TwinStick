using Combat;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Inventory{
    public sealed class InventoryController : Singleton<InventoryController>{

        public Inventory<Relic> relicInventory;
        public Inventory<Attack> weaponInventory;

        public void HandleTrigger(TriggerCondition condition){
            foreach(Relic r in relicInventory.inventory){
                if (r != null){
                    if (r.triggerCondition == condition && !r.cooldown.onCD){
                        r.OnTrigger();
                    }
                }
                
            }
        }

        void Update(){
          
        }

        void Start(){
            relicInventory.UpdateUI();
            weaponInventory.UpdateUI();
        }
    }
}