using Events;
using UnityEditor;
using UnityEngine;
using Variables;

namespace Assets.Scripts.NoobyGames.Events.PropertyDrawer {
    [CustomPropertyDrawer(typeof(IntGameEvent))]
    public class IntGameEventPropertyDrawer : UnityEditor.PropertyDrawer
    {
        private IntVariable _intVariable;
        private SerializedProperty _serializedIntVariable;
        public int instanceId;

        // Draw the property inside the given rect
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Using BeginProperty / EndProperty on the parent property means that
            // prefab override logic works on the entire property.
            EditorGUI.BeginProperty(position, label, property);

            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            Rect buttonPosition = new Rect(position.x, position.y + 5, position.width, 30);
            Rect idInputPosition = new Rect(position.x, buttonPosition.y + 35, position.width, 25);

            this.instanceId = EditorGUI.IntField(idInputPosition, this.instanceId);

            bool clicked = GUI.Button(buttonPosition, "Raise Event");
            if (clicked)
            {
                IntGameEvent gameEvent = property.objectReferenceValue as IntGameEvent;
                gameEvent?.Raise(this.instanceId);
                if (gameEvent != null)
                {
                    Debug.Log("Successfully raised event");
                }
            }

            var newPos = new Rect(idInputPosition.x, idInputPosition.y + 35, position.width, 25);
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