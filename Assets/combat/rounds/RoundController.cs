
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Combat.Shop;
using GameDebug;
using Inventory;
using Player;
using UnityEngine;

namespace Combat.Rounds{
    public class RoundController : Singleton<RoundController>{
        
        [Header("Spawning")]
        [SerializeField] int enemiesToSpawn;
        public float minSpawnRange = 3f;
        public float maxSpawnRange = 6f;
        [Tooltip("In MS")] public float timeBetweenSpawns = 1000;
        [Tooltip("In MS")] public float timeAfterRoundCompleted = 1000;
        static string enemyStorageLocationBase = "enemies/";


        [Header("Rounds")]
        [SerializeField] int round;

        [Header("Enemies")]

        public List<EnemyController> aliveEnemies => GameObject.FindObjectsByType<EnemyController>(FindObjectsSortMode.None).ToList();
        
        public Dictionary<EnemyTypes, float> enemyWeights = new(){
            {EnemyTypes.melee, 60},
            {EnemyTypes.ranged, 30},
            {EnemyTypes.summoner, 10}
        };

        static string GetTypeResourceLocation(EnemyTypes type){
            return enemyStorageLocationBase+type;
        }


        public Dictionary<EnemyTypes, GameObject> enemyTypeObjects = new();

        GameObject GetTargetEnemy(){
            int weightValue = Random.Range(0, 101);
            float weights=0f;
            foreach(KeyValuePair<EnemyTypes, float> kvp in enemyWeights){
                weights+=kvp.Value;
                if(weightValue <= weights) return enemyTypeObjects[kvp.Key];
            }

            Debug.LogError("Failed to choose an enemy based on the weights");
            return null;
        }

        void SpawnEnemy(EnemyTypes overrideEnemyType=EnemyTypes.none, Vector3? overidePosition=null){
            Vector3 spawnPosition = overidePosition == null ? SpawnPosition() : (Vector3)overidePosition;
            GameObject enemyType = overrideEnemyType==EnemyTypes.none ? GetTargetEnemy() : enemyTypeObjects[overrideEnemyType];
            GameObject enemyObject = Instantiate(enemyType, spawnPosition, Quaternion.identity);

            aliveEnemies.Add(enemyObject.GetComponent<EnemyController>());
        }

        Vector3 SpawnPosition(){
            int rotation = Random.Range(0, 181);
            float spawnDistance = Random.Range(minSpawnRange, maxSpawnRange);

            int flipX=Random.Range(0,2);
            int flipZ=Random.Range(0,2);

            Vector3 positionFromPlayer = new Vector3(flipX==0 ? 1 : -1,0, flipZ == 0 ? 1 : -1);
            positionFromPlayer = Quaternion.Euler(0,0,rotation)*positionFromPlayer;
            positionFromPlayer = positionFromPlayer.normalized * spawnDistance;

            positionFromPlayer = PlayerController.Instance.transform.position+positionFromPlayer;
            return new(positionFromPlayer.x, 3.102405f+transform.localScale.z/2f, positionFromPlayer.z);
        }  
        
        IEnumerator WaitUntilRoundEnd(){
            
            yield return new WaitUntil(() => aliveEnemies.Count == 0);
            yield return new WaitForSeconds(timeAfterRoundCompleted/1000f);
            if(!InventoryController.Instance.relicInventory.Full() && !InventoryController.Instance.weaponInventory.Full()) ShopController.Instance.TriggerShop();
        }

        IEnumerator SpawnAllEnemies(){
            for(int i = 0; i < enemiesToSpawn; i++){
                SpawnEnemy();
                yield return new WaitForSeconds(timeBetweenSpawns/1000f);
            }

            StartCoroutine(WaitUntilRoundEnd());
        }      

        public void BeginRound(){
            round++;
            StartCoroutine(SpawnAllEnemies());
        }

        void Start(){
            enemyTypeObjects = new()
            {
                {EnemyTypes.melee, Resources.Load<GameObject>(GetTypeResourceLocation(EnemyTypes.melee))},
                {EnemyTypes.ranged, Resources.Load<GameObject>(GetTypeResourceLocation(EnemyTypes.ranged))},
                {EnemyTypes.summoner, Resources.Load<GameObject>(GetTypeResourceLocation(EnemyTypes.summoner))}
            };

            BeginRound();
        }

        void OnDrawGizmos(){
            if (CombatDebug.drawEnemySpawnSpheres){
                Gizmos.color=Color.red;
                Gizmos.DrawWireSphere(PlayerController.Instance.transform.position, minSpawnRange);
                Gizmos.color=Color.blue;
                Gizmos.DrawWireSphere(PlayerController.Instance.transform.position, maxSpawnRange);
            }
        }
    }

}