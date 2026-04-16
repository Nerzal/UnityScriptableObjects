using UnityEngine;
using UnityEditor;
using Events;

namespace Events.Editor {
  /// <summary>
  /// Custom inspector for GenericGameEvent assets that provides a field to set the test argument and a button to raise the event.
  /// </summary>
  [CustomEditor(typeof(GenericGameEvent<>), true)]
  public class GenericGameEventEditor : UnityEditor.Editor {
    private object testArgument;

    public override void OnInspectorGUI() {
      DrawDefaultInspector();

      GenericGameEventBase genericEvent = (GenericGameEventBase)target;

      // Get the argument type
      System.Type argType = genericEvent.GetArgumentType();

      // Create field for test argument
      if (argType == typeof(int)) {
        testArgument = testArgument ?? 0;
        testArgument = EditorGUILayout.IntField("Test Argument", (int)testArgument);
      } else if (argType == typeof(float)) {
        testArgument = testArgument ?? 0f;
        testArgument = EditorGUILayout.FloatField("Test Argument", (float)testArgument);
      } else if (argType == typeof(string)) {
        testArgument = testArgument ?? "";
        testArgument = EditorGUILayout.TextField("Test Argument", (string)testArgument);
      } else if (argType == typeof(bool)) {
        testArgument = testArgument ?? false;
        testArgument = EditorGUILayout.Toggle("Test Argument", (bool)testArgument);
      } else if (argType == typeof(Vector3)) {
        testArgument = testArgument ?? Vector3.zero;
        testArgument = EditorGUILayout.Vector3Field("Test Argument", (Vector3)testArgument);
      } else if (argType == typeof(Color)) {
        testArgument = testArgument ?? Color.white;
        testArgument = EditorGUILayout.ColorField("Test Argument", (Color)testArgument);
      } else {
        EditorGUILayout.LabelField("Test Argument", $"Unsupported type: {argType.Name}");
        return;
      }

      if (GUILayout.Button("Raise Event")) {
        genericEvent.RaiseTest(testArgument);
        Debug.Log($"Raised event: {genericEvent.name} with argument: {testArgument}");
      }

      if (GUILayout.Button("Raise Event (Play Mode Only)")) {
        if (Application.isPlaying) {
          genericEvent.RaiseTest(testArgument);
          Debug.Log($"Raised event: {genericEvent.name} with argument: {testArgument}");
        } else {
          Debug.LogWarning("Event raising is only available in Play Mode");
        }
      }
    }
  }

  // Base class to access generic type information
  public abstract class GenericGameEventBase : ScriptableObject {
    public abstract System.Type GetArgumentType();
    public abstract void RaiseTest(object arg);
  }
}