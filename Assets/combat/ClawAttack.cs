using Player;
using UnityEngine;

namespace Combat
{
    [CreateAssetMenu(fileName = "New Claw Attack", menuName = "Attacks/Create/Claw Attack")]
    public class ClawAttack : Attack{
        
        [SerializeField] Vector3 attackSize;
        [SerializeField] float attackOutRegion=3f;

        public override void OnAttack(){
            int[] lookDirection = PlayerController.instance.lookDirection;
            CombatUtil.CreateDamageBox(
                damage,
                attackSize, 
                PlayerController.instance.transform.position+new Vector3(lookDirection[1]*attackOutRegion,0,lookDirection[0]*attackOutRegion)
            );
        }
    }
}