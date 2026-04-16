using Events;
using UnityEditor;
using UnityEngine;
using Variables;

namespace Assets.Scripts.NoobyGames.Events.PropertyDrawer {
    /// <summary>
    /// Custom property drawer for IntGameEvent ScriptableObjects in the Unity Inspector.
    /// Provides an input field for the event argument and a button to raise the event.
    /// </summary>
    [CustomPropertyDrawer(typeof(IntGameEvent))]
    public class IntGameEventPropertyDrawer : UnityEditor.PropertyDrawer
    {
        private IntVariable _intVariable;
        private SerializedProperty _serializedIntVariable;
        public int instanceId;

        /// <summary>
        /// Draws the property in the Inspector, including an input field and a "Raise Event" button.
        /// </summary>
        /// <param name="position">The rectangle on the screen to use for the property GUI.</param>
        /// <param name="property">The SerializedProperty to make the custom GUI for.</param>
        /// <param name="label">The label of this property.</param>
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

        /// <summary>
        /// Calculates the height of the property in the Inspector.
        /// </summary>
        /// <param name="property">The SerializedProperty.</param>
        /// <param name="label">The label of this property.</param>
        /// <returns>The height in pixels.</returns>
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) + 80;
        }
    }
}