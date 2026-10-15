using System.Collections.Generic;
using System.Linq;
using Inventory;
using Player;
using UnityEngine;

namespace Combat
{
    public class CombatController : MonoBehaviour{
        
        public List<Weapon> attacks = new();


            
        void GetAttacks() => attacks= InventoryController.Instance.weaponInventory.inventory.ToList();

        public void Update()    {

            GetAttacks();

            foreach(Weapon atk in attacks)
            {
                if(!atk.onCD) {
                    // If youre on the ground attack, otherwise youve missed the attack window and have to wait for the CD again
                    if (JumpingController.instance.Grounded()) atk.OnAttack();
                    atk.Cooldown();
                }
            }            
        }

        void OnValidate() => GetAttacks();

        void Start()
        {

            GetAttacks();
            
            foreach(Weapon atk in attacks){
                atk.onCD=false;
            }
        }
    }
}

