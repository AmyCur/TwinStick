using System.Threading.Tasks;
using UnityEngine;

namespace Combat
{
    public abstract class Attack : ScriptableObject
    {
        public float damage;
        public float cooldown;
        [HideInInspector] public bool onCD;

        public abstract void OnAttack();
        public async void Cooldown(){
            onCD=true;
            await Task.Delay(Mathf.FloorToInt(cooldown*1000f));
            onCD=false;
        }
    }
}