using Player;
using UnityEngine;

namespace Combat
{
    [CreateAssetMenu(fileName = "New Claw Attack", menuName = "Attacks/Create/Claw Attack")]
    public class ClawAttack : Attack{
        
        [SerializeField] Vector3 attackSize;
        [SerializeField] float attackOutRegion=3f;

        public override void OnAttack(){
            Directions lookDirection = PlayerController.instance.lookDirection;

            float horizontalMult = lookDirection == Directions.left ? -1 : lookDirection == Directions.right ? 1 : 0;
            float verticalMult = lookDirection == Directions.down ? -1 : lookDirection == Directions.up ? 1 : 0;

            CombatUtil.CreateDamageBox(
                damage,
                attackSize, 
                PlayerController.instance.transform.position+new Vector3(horizontalMult*attackOutRegion,0,verticalMult*attackOutRegion)
            );
        }
    }
}