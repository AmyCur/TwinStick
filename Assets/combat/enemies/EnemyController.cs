using System.Threading.Tasks;
using Combat.Rounds;
using Entities;
using GameDebug;
using Inventory;
using Player;
using UnityEngine;
using UnityEngine.EventSystems;


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
            if(gameObject!=null) return Vector3.Distance(transform.position, GameObject.Find("Player").transform.position) <= attackRange;
            return false;
         
        }

        public override void TakeDamage(float damage){
            InventoryController.Instance.HandleTrigger(TriggerCondition.enemy_damaged);
            base.TakeDamage(damage);
        }

        public override void Die()
        {
            if(RoundController.Instance.aliveEnemies.Contains(this)) RoundController.Instance.aliveEnemies.Remove(this);
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

        void Update(){
            transform.position = new Vector3(transform.position.x, 3.102405f+transform.localScale.z/2f, transform.position.z);
        }

        void OnDrawGizmos(){
            if (CombatDebug.shouldDrawEnemyAttackRays){
                Gizmos.color=Color.softRed;
                Gizmos.DrawLine(transform.position, ((GameObject.Find("Player").transform.position-transform.position).normalized*attackRange)+transform.position);
            }
        }
    }
    
}