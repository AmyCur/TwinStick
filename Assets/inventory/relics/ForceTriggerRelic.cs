using System.Threading.Tasks;
using Player;
using UnityEngine;

namespace Inventory{
    [CreateAssetMenu(fileName = "New FT Relic", menuName = "Inventory/Relic/Create/Force Trigger Relic")]
    public class ForceTriggerRelic : Relic{
       
        public string[] forceTriggerSlots;

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
            
        }

        public override void OnTrigger(){
            foreach(string str in forceTriggerSlots){
                int index = ReadForceTriggerSlot(str);
                Relic[] invRelic = InventoryController.instance.relicInventory.inventory;
                if(index <= invRelic.Length && index>=0 && invRelic[index]!=null) invRelic[index].ForceTrigger();
            }
        }
    }
}