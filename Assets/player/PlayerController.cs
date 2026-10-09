using System.Threading.Tasks;
using Entities;
using Events;
using UnityEngine;

namespace Player{
    [RequireComponent(typeof(Rigidbody))]
    public sealed class PlayerController : EntityController
    {
        public static PlayerController instance
        {
            get{return GameObject.Find("Player").GetComponent<PlayerController>();}
            private set{}
        }
        Rigidbody rb;

        public Directions lookDirection;

        [Header("Movement")]

        public bool canMoveX;
        public bool canMoveY;
        public float speed = 12f;
        bool canMoveXY => canMoveX || canMoveY;

        [Header("Iframes")]

        public int IframeTime=500;
        [SerializeField] bool iframeOver;


        void Start(){
            rb=GetComponent<Rigidbody>();
        }

        void Move(){
            float vInp = canMoveY ? Input.GetAxisRaw("Vertical") : 0;
            float hInp = canMoveY ? Input.GetAxisRaw("Horizontal") : 0;

            rb.linearVelocity=new(hInp*speed, rb.linearVelocity.y, vInp*speed);
        }

        Directions DetermineLookDirection(){
            
            if(Input.GetAxisRaw("Horizontal") == 1) return Directions.right;
            if(Input.GetAxisRaw("Horizontal") == -1) return Directions.left;
            if(Input.GetAxisRaw("Vertical") == -1) return Directions.down;
            if(Input.GetAxisRaw("Vertical") == 1) return Directions.up;

            // If nothings pressed, dont change anything
            return lookDirection;
        }

        public override async void TakeDamage(float damage)
        {
            if(iframeOver){
                CombatEventManager.instance.playerDamaged.Invoke();
                base.TakeDamage(damage);
                iframeOver=false;
                await Task.Delay(IframeTime);
                iframeOver=true;
            }
        }

        public override void Die()
        {
            CombatEventManager.instance.playerKilled.Invoke();
            base.Die();
        }

        void FixedUpdate(){
            if(canMoveXY) Move();
        }

        void Update(){
            lookDirection=DetermineLookDirection();
        }
    }
}