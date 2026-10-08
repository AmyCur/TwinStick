using UnityEngine;

namespace Entities
{
    public abstract class EntityController : MonoBehaviour
    {
        public float health;

        public void TakeDamage(float damage){
            health-=damage;
        }
    }    
}