using UnityEngine;

namespace Combat{
    [RequireComponent(typeof(Rigidbody))]
    public class BouncingBall : MonoBehaviour{
        Rigidbody rb;
        public float speed=12f;
        [SerializeField] float killTime=50f;
        public float damage=500f;


        Vector3 moveDir;

        public void DestroyBall(){
            Destroy(gameObject);
        }

        void FixedUpdate(){
            rb.linearVelocity=moveDir.normalized*speed;
        }

        void OnCollisionEnter(Collision other){
            moveDir = Vector3.Reflect(new(moveDir.x,0,moveDir.z), other.GetContact(0).normal);

            if (other.collider.TryGetComponent<EnemyController>(out var ec)) ec.TakeDamage(damage);
        }

        

        void Start(){
            rb=GetComponent<Rigidbody>();
            transform.rotation=Quaternion.Euler(0,Random.Range(0,361),0);
            moveDir=transform.forward;
            Invoke(nameof(DestroyBall), killTime);
        }
    }
}