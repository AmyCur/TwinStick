using System.Threading.Tasks;
using Player;
using UnityEngine;

namespace Inventory{
    [CreateAssetMenu(fileName = "Crazy Speed Relic", menuName = "Inventory/Relics/Create/Crazy Speed Relic")]
    public class CrazySpeedRelic : Relic{
        public override async void OnEnemyJumped(){
            jumpedCooldown.CD();
            PlayerController.instance.speed*=2f;
            await Task.Delay(1000);
            PlayerController.instance.speed/=2f;

        }
    }
}