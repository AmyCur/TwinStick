using Entities;
using UnityEngine;

namespace Player{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : EntityController
    {
        public static PlayerController instance;
        Rigidbody rb;

        [Header("Movement")]

        public bool canMoveX;
        public bool canMoveY;
        public float speed = 12f;
        bool canMoveXY => canMoveX || canMoveY;

        public int[] lookDirection = {0, 0};

        void Start(){
            if(instance==null) instance=this;
            rb=GetComponent<Rigidbody>();
        }

        void Move(){
            float vInp = canMoveY ? Input.GetAxisRaw("Vertical") : 0;
            float hInp = canMoveY ? Input.GetAxisRaw("Horizontal") : 0;

            lookDirection[0]=Mathf.FloorToInt(vInp);
            lookDirection[1]=Mathf.FloorToInt(hInp);

            rb.linearVelocity=new(hInp*speed, rb.linearVelocity.y, vInp*speed);
        }

        void Update(){
            if(canMoveXY) Move();
        }
    }
}