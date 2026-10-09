using System.Collections;
using UnityEngine;
using Util;

namespace Inventory
{
    public abstract class Relic : Item{
        public virtual void OnProc() => relicCooldown.CD();
        
        public void ProcAll(){
            OnEnemyDamaged();
            OnEnemyKilled();
            OnPlayerKilled();
            OnPlayerDamaged();
            OnEnemyJumped();
        }

        public virtual void OnEnemyDamaged(){}
        public virtual void OnEnemyKilled(){}
        public virtual void OnPlayerDamaged(){}
        public virtual void OnPlayerKilled(){}
        public virtual void OnEnemyJumped(){}

        public void ForceTrigger<T>(int slot){
            if(typeof(T) == typeof(Relic)){
                InventoryController.instance.relicInventory[slot].item
            }
        }

        
        public Cooldown relicCooldown=new();
         
        public void Init(){
            relicCooldown.onCD=false;
        }
    }
}