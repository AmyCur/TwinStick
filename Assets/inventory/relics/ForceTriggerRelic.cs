using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Combat;
using Player;
using UnityEngine;

namespace Inventory{

    public enum InventoryTypes{
        relic,
        weapon
    }

    [CreateAssetMenu(fileName = "New FT Relic", menuName = "Inventory/Relic/Create/Force Trigger Relic")]
    public class ForceTriggerRelic : Relic{
       
        public ForceTriggerCondition[] forceTriggerConditions;
        [SerializeField] InventoryTypes triggerType;
        Item[] targetInventory => triggerType == InventoryTypes.relic ? InventoryController.Instance.relicInventory.inventory : InventoryController.Instance.weaponInventory.inventory;

        public int ReadForceTriggerSlot(string slot){
            if(slot.Length == 1) return int.Parse(slot);
            else{
                //! This code contains NO CHECKING FOR ERRORS so if i input something wrong
                //! it will get super mad (Solution: Dont input wrong)
                int amount = int.Parse(slot[1].ToString());
                int mult = slot[0] == '-' ? -1 : 1;
                amount*=mult;
                return index+amount;
            }

            //! DO NOT PUT A LOOP HERE IT WILL CAUSE A MEMORY LEAK FOR SOME REASON
           
            
        }

        public override void OnTrigger(){
            foreach(ForceTriggerCondition ftc in forceTriggerConditions){
                switch (ftc.triggerType)
                {
                    case ForceTriggerType.index:
                        if(targetInventory[ftc.index]!=null) targetInventory[ftc.index].ForceTrigger();
                    break;
                    case ForceTriggerType.offset:
                        if(
                            targetInventory[index+ftc.offset] != null && 
                            targetInventory.Length > index+ftc.offset && 
                            index+ftc.offset > 0
                        ) targetInventory[index+ftc.offset].ForceTrigger();
                    break;
                    case ForceTriggerType.rarity:
                        foreach(Item i in targetInventory){
                            if(i.rarity==ftc.rarity) i.ForceTrigger();
                        }
                    break;
                    case ForceTriggerType.random:
                        // Get all non null items and then get a random one of them
                        int random =  Random.Range(0, targetInventory.OfType<Item>().Count());
                        targetInventory.OfType<Item>().ToList()[random].ForceTrigger();                        
                    break;
                }
            }
            
            base.OnTrigger();
        }
    }
}