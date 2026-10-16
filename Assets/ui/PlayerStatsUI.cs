using UnityEngine.UI;
using UnityEngine;
using Player;

namespace UI{
    public class PlayerStatsUI : MonoBehaviour{
        public Bar healthBar;

        void Update(){
            healthBar.UpdateBar(Mathf.FloorToInt(PlayerController.Instance.health));
        }
    }
}