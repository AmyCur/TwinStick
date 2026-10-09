using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Inventory{
    public class InventoryModificationButton{

        static InventoryController ic => InventoryController.instance;

        [MenuItem("Tools/Inventory/Add item", false, -1)]
        public static void AddItem(){
            var addItemPopup = ScriptableObject.CreateInstance<AddItemPopup>();

        }
    }

    public class AddItemPopup : EditorWindow{
        
        static InventoryController ic => InventoryController.instance;

        void OnEnable(){
            GetWindow(typeof(AddItemPopup));
        }

        void OnGUI(){
      


            // EditorGUILayout.ObjectField(ic.itemToAdd as SerializedProperty)
            EditorGUILayout.LabelField("Hi");
            if(GUILayout.Button("Add Item")) ic.AddItem(ic.itemToAdd.item, ic.indexToChange);
        }
    }
}