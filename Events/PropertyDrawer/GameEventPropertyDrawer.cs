using Events;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.NoobyGames.Events.PropertyDrawer
{
    [CustomPropertyDrawer(typeof(GameEvent))]
    public class GameEventPropertyDrawer : UnityEditor.PropertyDrawer
    {

        // Draw the property inside the given rect
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Using BeginProperty / EndProperty on the parent property means that
            // prefab override logic works on the entire property.
            EditorGUI.BeginProperty(position, label, property);

            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            Rect buttonPosition = new Rect(position.x, position.y + 5, position.width, 30);
          
            bool clicked = GUI.Button(buttonPosition, "Raise Event");
            if (clicked)
            {
                GameEvent gameEvent = property.objectReferenceValue as GameEvent;
                gameEvent?.Raise();
                if (gameEvent != null)
                {
                    Debug.Log("Successfully raised event");
                }
            }

            var newPos = new Rect(buttonPosition.x, buttonPosition.y + 35, position.width, 25);
            EditorGUI.PropertyField(newPos, property, GUIContent.none);

            // Set indent back to what it was
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) + 80;
        }
    }
}
