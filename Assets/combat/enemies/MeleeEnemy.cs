using UnityEngine.AI;
using UnityEngine;
using Player;

namespace Combat{
    [RequireComponent(typeof(NavMeshAgent))]
    public class MeleeEnemy : EnemyController{
        
        public NavMeshAgent navAgent;
        
        public override void Attack()
        {
            base.Attack();
            throw new System.NotImplementedException();
        }
        public override void Chase()
        {
            base.Chase();
            navAgent.destination=PlayerController.instance.transform.position;
        }
        protected override void Start()
        {
            navAgent=GetComponent<NavMeshAgent>();
            base.Start();
        }
    }
}