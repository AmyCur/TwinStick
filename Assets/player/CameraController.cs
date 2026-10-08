using UnityEngine;

namespace Player
{
    public class CameraController : MonoBehaviour{
        float cameraheight;

        void Start()
        {
            cameraheight=transform.position.y;
        }

        void Update()
        {
            transform.position=new(transform.position.x, cameraheight, transform.position.z);
        }
    }
}