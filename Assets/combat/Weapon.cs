using System.Threading.Tasks;
using Inventory;
using UnityEngine;

namespace Combat
{
    public abstract class Weapon : Item
    {
        [Header("Weapon")]
        public float damage;
        public abstract void OnAttack();

        new public int index
        {
            get
            {
                for(int i = 0; i < InventoryController.Instance.weaponInventory.inventory.Length; i++){
                    if(InventoryController.Instance.weaponInventory.inventory[i]==this) return i;
                }
                
                throw new System.Exception("We hebben een serieus probleem");
            }
            set{}
        }

        public override void ForceTrigger() => OnAttack();
    }
}