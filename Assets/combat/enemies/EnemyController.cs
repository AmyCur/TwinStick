using System.Threading.Tasks;
using Entities;
using GameDebug;
using Player;
using UnityEngine;

namespace Combat{
    public abstract class EnemyController : EntityController{
        
        public bool canAttack=true;
        public bool canChase=true;

        protected bool shouldAttack => canAttack && PlayerInRange();
        protected bool shouldChase => canChase && !shouldAttack;
        
        [SerializeField] protected float attackRange=5f;

        protected bool PlayerInRange(){
            if(Physics.Raycast(transform.position, GameObject.Find("Player").transform.position-transform.position,out RaycastHit hit, attackRange)){
                return hit.distance>=attackRange;
            }

            return false;
        }

        async void DecideState(){
            if(shouldAttack) Attack();
            else if(shouldChase) Chase();
            else if (CombatDebug.enemiesShouldBroadcastState) Debug.LogWarning($"{transform.name} is idle");
            await Task.Delay(100);
            DecideState();
        }

        public virtual void Chase(){
            if(CombatDebug.enemiesShouldBroadcastState) Debug.Log($"{transform.name} is chasing!");
        }
        public virtual void Attack(){
            if(CombatDebug.enemiesShouldBroadcastState) Debug.Log($"{transform.name} is attacking!");
        }
        
        protected virtual void Start(){
            DecideState();
        }

        void OnDrawGizmos(){
            if (CombatDebug.shouldDrawEnemyAttackRays){
                Gizmos.color=Color.softRed;
                Gizmos.DrawLine(transform.position, ((GameObject.Find("Player").transform.position-transform.position).normalized*attackRange)+transform.position);
            }
        }
    }
    
}