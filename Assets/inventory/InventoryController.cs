using Combat;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Inventory{
    public sealed class InventoryController : Singleton<InventoryController>{

        public Inventory<Relic> relicInventory;
        public Inventory<Attack> weaponInventory;

        void GetClickedData(){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hit, 10_000)){
                Debug.Log(hit.collider.name);
            }
        }


        void Update(){
            if(Input.GetKeyDown(KeyCode.Mouse0)) GetClickedData();
        }

        void Start(){
            relicInventory.UpdateUI();
            weaponInventory.UpdateUI();
        }
    }
}