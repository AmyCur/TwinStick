using UnityEngine;

namespace Entities
{
    public abstract class EntityController : MonoBehaviour
    {
        public float health;

        public virtual void TakeDamage(float damage){
            health-=damage;
            if(health <= 0) Die();
        }

        public virtual void Die() => GameObject.Destroy(gameObject);
    }    
}