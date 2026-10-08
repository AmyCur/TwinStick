using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class CombatController : MonoBehaviour{
        
        public List<Attack> attacks = new();
        [SerializeField] int currentAttack=0;

        public void Update()    {
            if (Input.GetKeyDown(KeyCode.Mouse0)){
                attacks[currentAttack].OnAttack();
            }
        }
    }
}