using Events;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.NoobyGames.Events.PropertyDrawer
{
    /// <summary>
    /// Custom property drawer for GameEvent ScriptableObjects in the Unity Inspector.
    /// Provides a button to raise the event directly from the editor.
    /// </summary>
    [CustomPropertyDrawer(typeof(GameEvent))]
    public class GameEventPropertyDrawer : UnityEditor.PropertyDrawer
    {

        /// <summary>
        /// Draws the property in the Inspector, including a "Raise Event" button.
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
