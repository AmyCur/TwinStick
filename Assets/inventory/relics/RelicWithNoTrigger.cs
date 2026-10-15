using System.Diagnostics;
using System.Threading.Tasks;
using Player;
using UnityEngine;

namespace Inventory{
    [CreateAssetMenu(fileName = "Relic with no trigger", menuName = "Inventory/Relic/Create/No Trigger Relic")]
    public class RelicWithNoTrigger: Relic{
        public override async void OnTrigger(){
            base.OnTrigger();
            PlayerController.instance.speed*=3f;
            await Task.Delay(2000);
            PlayerController.instance.speed/=3f;

        }
    }
}