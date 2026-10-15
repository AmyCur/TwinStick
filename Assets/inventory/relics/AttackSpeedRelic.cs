using System.Diagnostics;
using System.Threading.Tasks;
using Combat;
using Player;
using UnityEngine;

namespace Inventory{
    [CreateAssetMenu(fileName = "Attack Speed Relic", menuName = "Inventory/Relic/Create/Attack Speed Relic")]
    public class AttackSpeedRelic: Relic{
        public float attackSpeedIncrease = 1f;

        public override  void OnTrigger(){
            WeaponsGlobals.attackSpeed+=attackSpeedIncrease;
            base.OnTrigger();
        }
    }
}