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

        public float damage=10f;
        
        [SerializeField] protected float attackRange=5f;

        protected bool PlayerInRange(){

            //! WARNING: Using this approach, enemies can shoot through walls
            return Vector3.Distance(transform.position, GameObject.Find("Player").transform.position) <= attackRange;
                
         
        }

        public override void TakeDamage(float damage){
            base.TakeDamage(damage);
        }

        public override void Die()
        {
            base.Die();
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