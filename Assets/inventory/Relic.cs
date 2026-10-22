using System.Linq;

namespace Inventory
{
    [System.Serializable]
    public abstract class Relic : Item{

        new public int index
        {
            get
            {
                for(int i = 0; i < InventoryController.Instance.relicInventory.inventory.Length; i++){
                    if(InventoryController.Instance.relicInventory.inventory[i]==this) return i;
                }
                
                throw new System.Exception("We hebben een serieus probleem");
            }
            set{}
        }

        public virtual void OnTrigger(){
            cooldown.CD();
        }

        public override void ForceTrigger(){
            if (!forceTriggerCooldown.onCD)
            {
                OnTrigger();
                forceTriggerCooldown.CD();            
            }
        }

        public TriggerCondition triggerCondition;
    }
}