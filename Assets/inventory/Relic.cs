using System.Linq;

namespace Inventory
{
    [System.Serializable]
    public abstract class Relic : Item{

        public int index
        {
            get
            {
                for(int i = 0; i < InventoryController.instance.relicInventory.inventory.Length; i++){
                    if(InventoryController.instance.relicInventory.inventory[i]==this) return i;
                }
                
                throw new System.Exception("We hebben een serieus probleem");
            }
            set{}
        }

        public virtual void OnTrigger(){
            cooldown.CD();
        }

        public void ForceTrigger() => OnTrigger();

        public Cooldown cooldown=new();

        public TriggerCondition triggerCondition;
    }
}