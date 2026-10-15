using System.Threading.Tasks;
using Inventory;
using UnityEngine;

namespace Combat
{
    public abstract class Weapon : Item
    {
        public float damage;
        public abstract void OnAttack();
        
    }
}