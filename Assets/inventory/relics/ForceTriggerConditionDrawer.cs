using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Inventory
{
    [CustomPropertyDrawer(typeof(ForceTriggerCondition))]
    public class ForceTriggerConditionDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);


            Rect triggerTypePos = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight, position.width, EditorGUIUtility.singleLineHeight);
            SerializedProperty triggerProperty = property.FindPropertyRelative("triggerType");
            EditorGUI.PropertyField(triggerTypePos, triggerProperty);

            ForceTriggerType triggerType = (ForceTriggerType)triggerProperty.enumValueIndex;

            if (triggerType == ForceTriggerType.index){
                Rect indexPos = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight*3, position.width, EditorGUIUtility.singleLineHeight);
                SerializedProperty indexProperty = property.FindPropertyRelative("index");
                EditorGUI.PropertyField(indexPos, indexProperty);                
            }

            else if (triggerType == ForceTriggerType.offset){
                Rect offsetPos = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight*3, position.width, EditorGUIUtility.singleLineHeight);
                SerializedProperty offsetProperty = property.FindPropertyRelative("offset");
                EditorGUI.PropertyField(offsetPos, offsetProperty);                
            }

            else if (triggerType == ForceTriggerType.rarity){
                Rect rarityPos = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight*3, position.width, EditorGUIUtility.singleLineHeight);
                SerializedProperty rarityProperty = property.FindPropertyRelative("rarity");
                EditorGUI.PropertyField(rarityPos, rarityProperty);                
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 5;
        }
    }
}