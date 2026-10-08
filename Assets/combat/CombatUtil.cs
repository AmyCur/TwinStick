using System.Collections.Generic;
using System.Linq;
using Entities;
using Player;
using UnityEngine;
using GameDebug;
using System.Threading.Tasks;

namespace Combat{
    public static class CombatUtil{
        
        static GameObject hurtboxPrefab => Resources.Load<GameObject>("Debugging/DebugCube");

        static async void CreateDamageDebugBox(Vector3 scale, Vector3 position, int delay){
            GameObject hurtbox = null;
            
            hurtbox=GameObject.Instantiate(hurtboxPrefab, position, Quaternion.identity);
            hurtbox.transform.localScale=scale;
            
            await Task.Delay(delay);

            if(hurtbox!=null) GameObject.Destroy(hurtbox);
        }

        public static void CreateDamageBox(float damage, object dimensions, Vector3 position, EntityTypes eType=EntityTypes.enemy){
            List<Collider> colliders = new();

            if(dimensions is Vector3 v3){ 
                colliders = Physics.OverlapBox(position, v3, Quaternion.identity).ToList();
                if (CombatDebug.shouldDrawHurtBoxes){
                    CreateDamageDebugBox(v3, position, CombatDebug.HURTBOXLINGERTIME);
                }
            }
            else if(dimensions is float f) {
                colliders= Physics.OverlapBox(position, new(f,f,f), Quaternion.identity).ToList();
                if (CombatDebug.shouldDrawHurtBoxes){
                    CreateDamageDebugBox(new(f,f,f), position, CombatDebug.HURTBOXLINGERTIME);
                    
                }
            }
            else Debug.LogError($"The datatype of {nameof(dimensions)} is invalid");
            
            List<EntityController> entities = new();

            foreach(Collider col in colliders){
                EntityController ec = col.GetComponent<EntityController>();
                if (ec != null){
                    if(eType==EntityTypes.player && ec is PlayerController) ec.TakeDamage(damage);
                    else if(eType==EntityTypes.enemy && ec is EnemyController) ec.TakeDamage(damage);
                }
            }


   
        }
    }
}