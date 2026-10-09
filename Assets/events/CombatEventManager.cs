using System;
using Inventory;
using UnityEngine;
using UnityEngine.Events;

namespace Events{
    public class CombatEventManager : MonoBehaviour{
        public static CombatEventManager instance;
        public UnityEvent enemyKilled;
        public UnityEvent enemyDamaged;
        public UnityEvent playerDamaged;
        public UnityEvent playerKilled;
        public UnityEvent enemyJumped;
        
        UIRelic[] inventory => InventoryController.instance.relicInventory;


        public void TriggerAllOnEnemyJumped(){
            foreach(UIRelic item in inventory){
                if(item.item != null)
                {
                    if (item.item is Relic relic){
                        if(!relic.relicCooldown.onCD) relic.OnEnemyJumped();
                    }
                }
            }
        }

        public void TriggerAllEnemyKilled(){
            foreach(UIRelic item in inventory){
                if(item.item != null)
                {
                    if (item.item is  Relic relic){
                        if(!relic.relicCooldown.onCD) relic.OnEnemyKilled();
                    }
                }
            }
        }

        public void TriggerAllEnemyDamaged(){
            foreach(UIRelic item in inventory){
                if(item.item != null)
                {
                    if (item.item is  Relic relic){
                        if(!relic.relicCooldown.onCD) relic.OnEnemyDamaged();
                    }
                }
                
            }
        }

        public void TriggerAllPlayerDamaged(){
            foreach(UIRelic item in inventory){
                if(item.item != null)
                {
                    if (item.item is  Relic relic){
                        if(!relic.relicCooldown.onCD) relic.OnPlayerDamaged();
                    }
                }
            }
        }

        public void TriggerAllPlayerKilled(){
            foreach(UIRelic item in inventory){
                if(item.item != null)
                {
                    if (item.item is Relic relic){
                        if(!relic.relicCooldown.onCD) relic.OnPlayerKilled();
                    }
                }
            }
        }

        void Start(){
            enemyKilled.AddListener(TriggerAllEnemyKilled);
            enemyDamaged.AddListener(TriggerAllEnemyDamaged);
            playerKilled.AddListener(TriggerAllPlayerKilled);
            playerDamaged.AddListener(TriggerAllPlayerDamaged);
            enemyJumped.AddListener(TriggerAllOnEnemyJumped);


            instance=this;  
        }
    }
}