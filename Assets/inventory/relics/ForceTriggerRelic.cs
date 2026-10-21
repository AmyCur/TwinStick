using System.Threading.Tasks;
using Combat;
using Player;
using UnityEngine;

namespace Inventory{

    public enum InventoryTypes
    {
        relic,
        weapon
    }

    [CreateAssetMenu(fileName = "New FT Relic", menuName = "Inventory/Relic/Create/Force Trigger Relic")]
    public class ForceTriggerRelic : Relic{
       
        public string[] forceTriggerSlots;
        [SerializeField] InventoryTypes triggerType;
        

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
            foreach(string str in forceTriggerSlots){
                int index = ReadForceTriggerSlot(str);
                Relic[] invRelic = InventoryController.Instance.relicInventory.inventory;
                Weapon[] invWeapon = InventoryController.Instance.weaponInventory.inventory;
                
                if(triggerType==InventoryTypes.relic && index <= invRelic.Length && index>=0 && invRelic[index]!=null) invRelic[index].ForceTrigger();
                if(triggerType==InventoryTypes.weapon && index <= invWeapon.Length && index>=0 && invWeapon[index]!=null) invWeapon[index].ForceTrigger();
            }
            
            base.OnTrigger();
        }
    }
}