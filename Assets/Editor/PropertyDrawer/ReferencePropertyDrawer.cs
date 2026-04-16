using UnityEngine;
using UnityEditor;
using Variables;

namespace Variables.Editor {
  /// <summary>
  /// Custom inspector for Reference types that provides a toggle between constant and variable values.
  /// </summary>
  [CustomPropertyDrawer(typeof(FloatReference))]
  [CustomPropertyDrawer(typeof(IntReference))]
  [CustomPropertyDrawer(typeof(StringReference))]
  [CustomPropertyDrawer(typeof(DoubleReference))]
  [CustomPropertyDrawer(typeof(ColorReference))]
  [CustomPropertyDrawer(typeof(Vector4Reference))]
  [CustomPropertyDrawer(typeof(QuaternionReference))]
  [CustomPropertyDrawer(typeof(LayerMaskReference))]
  [CustomPropertyDrawer(typeof(AnimationCurveReference))]
  [CustomPropertyDrawer(typeof(GameObjectReference))]
  public class ReferencePropertyDrawer : PropertyDrawer {
    private const float ToggleWidth = 80f;
    private const float Spacing = 2f;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
      EditorGUI.BeginProperty(position, label, property);

      // Get the useConstant property
      SerializedProperty useConstantProp = property.FindPropertyRelative("useConstant");

      // Calculate rects
      Rect toggleRect = new Rect(position.x, position.y, ToggleWidth, EditorGUIUtility.singleLineHeight);
      Rect valueRect = new Rect(position.x + ToggleWidth + Spacing, position.y,
                               position.width - ToggleWidth - Spacing, EditorGUIUtility.singleLineHeight);

      // Draw toggle
      bool useConstant = useConstantProp.boolValue;
      useConstant = EditorGUI.ToggleLeft(toggleRect, "Constant", useConstant);
      useConstantProp.boolValue = useConstant;

      // Draw appropriate field based on toggle
      if (useConstant) {
        // Draw constant value field
        SerializedProperty constantValueProp = property.FindPropertyRelative("constantValue");
        EditorGUI.PropertyField(valueRect, constantValueProp, GUIContent.none);
      } else {
        // Draw variable field
        SerializedProperty variableProp = property.FindPropertyRelative("variable");
        EditorGUI.PropertyField(valueRect, variableProp, GUIContent.none);
      }

      EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
      return EditorGUIUtility.singleLineHeight;
    }
  }
}