using Combat;
using Player;
using UnityEngine;

[CreateAssetMenu(fileName = "Tail Attack", menuName = "Attacks/Create/Tail Attack", order = 1)]
public class TailAttack : ClawAttack
{
    public override void OnAttack(){
            Directions lookDirection = PlayerController.Instance.lookDirection;

            float horizontalMult = lookDirection == Directions.left ? 1 : lookDirection == Directions.right ? -1 : 0;
            float verticalMult = lookDirection == Directions.down ? -1 : lookDirection == Directions.up ? 1 : 0;

            CombatUtil.CreateDamageBox(
                damage,
                attackSize, 
                PlayerController.Instance.transform.position+new Vector3(horizontalMult*attackOutRegion, 0, -verticalMult*attackOutRegion)
            );
        }
}
