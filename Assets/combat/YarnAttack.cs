using Player;
using UnityEngine;

namespace Combat
{
    [CreateAssetMenu(fileName = "New Yarn Attack", menuName = "Attacks/Create/Yarn Attack", order = 2)]
    public class YarnAttack : Weapon{
        GameObject yarnBallPrefab=> Resources.Load<GameObject>("weapon_objects/yarn");
        
        void CreateYarn()
        {
            GameObject yarn = Instantiate(yarnBallPrefab, PlayerController.Instance.transform.position, Quaternion.identity);
            yarn.GetComponent<BouncingBall>().damage=damage;
            yarn.GetComponent<BouncingBall>().speed=Random.Range(23,30);
        }

        public override void OnAttack(){
            CreateYarn();    
        }
    }
}