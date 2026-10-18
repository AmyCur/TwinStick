// using System.Collections.Generic;
// using UnityEngine;

// namespace Combat.Rounds{
//     public class SpawnPosition : MonoBehaviour{
//         static string enemyStorageLocationBase = "enemies/";
        
//         static string GetTypeResourceLocation(EnemyTypes type){
//             return enemyStorageLocationBase+type;
//         }

//         public Dictionary<EnemyTypes, GameObject> enemyTypeObjects = new()
//         {
//             {EnemyTypes.melee, Resources.Load<GameObject>(GetTypeResourceLocation(EnemyTypes.melee))},
//             {EnemyTypes.ranged, Resources.Load<GameObject>(GetTypeResourceLocation(EnemyTypes.ranged))},
//             {EnemyTypes.summoner, Resources.Load<GameObject>(GetTypeResourceLocation(EnemyTypes.summoner))},
//         };

//         public EnemyTypes enemyType;
//         GameObject targetPrefab => enemyTypeObjects[enemyType];

//     }
// }