using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Combat;
using Unity.Collections;
using UnityEngine;

namespace Player
{
    public class JumpingController : MonoBehaviour{

        [Header("Grounded")]
        [SerializeField] float groundedDistance = 0.3f;
        [SerializeField] LayerMask playerMask;

        public static JumpingController instance;

        Animator animator;

        Rigidbody rb;

        public bool Grounded(){
            if(Physics.Raycast(transform.position, Vector3.down, groundedDistance, ~playerMask))
            {
                Debug.Log("Grounded");
                return true;
            }
            Debug.Log("Not Grounded");
            return false;
        }

        [Header("Forces")]

        public float jumpForce;
        
        IEnumerator CalculateFlipPoints(){

            List<Collider> enemiesFlippedOver = new();
            Debug.Log("Kickflip");
            yield return new WaitForSeconds(0.1f);
            while (!Grounded()){
                Collider[] belowObjects = Physics.OverlapBox(transform.position, new(transform.localScale.x*1.3f, 10_000 ,transform.localScale.z*1.3f));
                foreach(Collider col in belowObjects){
                    if(col.GetComponent<EnemyController>() && !enemiesFlippedOver.Contains(col)){
                        enemiesFlippedOver.Add(col);
                        Debug.Log($"Flipped over {col.name}");
                    }
                }
                yield return 0;
            }
        }


        async void DoASickFlip(){
            StartCoroutine(CalculateFlipPoints());
            animator.SetBool("Kickflip", true);
            await Task.Delay(200);
            animator.SetBool("Kickflip", false);
        }

        void Jump(){
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            DoASickFlip();
        }

        void Update()
        {
            if(Grounded() && Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }

        void Start()
        {
            rb=GetComponent<Rigidbody>();
            instance=this;
            animator=transform.GetChild(1).GetComponent<Animator>();
        }

        void OnDrawGizmos()
        {
            Gizmos.DrawRay(transform.position, Vector3.down);
        }
    }
}