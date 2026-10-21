using Player;
using UnityEngine;

namespace Combat
{
    public class FollowPlayer : MonoBehaviour
    {
        [SerializeField] Vector3 offset;

        void Update()
        {
            transform.position=PlayerController.Instance.transform.position+offset;
        }
    }
}