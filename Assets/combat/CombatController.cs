using System.Collections.Generic;
using Player;
using UnityEngine;

namespace Combat
{
    public class CombatController : MonoBehaviour{
        
        public List<Attack> attacks = new();

        public void Update()    {
            foreach(Attack atk in attacks)
            {
                if(!atk.onCD) {
                    // If youre on the ground attack, otherwise youve missed the attack window and have to wait for the CD again
                    if (JumpingController.instance.Grounded()) atk.OnAttack();
                    
                    
                    atk.Cooldown();
                }
            }            
        }

        void Start()
        {
            foreach(Attack atk in attacks){
                atk.onCD=false;
            }
        }
    }
}

