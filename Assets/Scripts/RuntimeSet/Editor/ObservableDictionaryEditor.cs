using UnityEngine;
using UnityEditor;
using Sets;
using System.Collections.Generic;

namespace Sets.Editor {
  /// <summary>
  /// Custom inspector for ObservableDictionary assets that displays current key-value pairs and provides controls.
  /// </summary>
  [CustomEditor(typeof(ObservableDictionary<,>), true)]
  public class ObservableDictionaryEditor : UnityEditor.Editor {
    public override void OnInspectorGUI() {
      DrawDefaultInspector();

      ObservableDictionaryBase dict = (ObservableDictionaryBase)target;

      EditorGUILayout.Space();
      EditorGUILayout.LabelField("Runtime Controls", EditorStyles.boldLabel);

      EditorGUILayout.LabelField($"Current Count: {dict.GetCount()}");

      if (dict.GetCount() > 0) {
        EditorGUILayout.LabelField("Current Items:");
        EditorGUI.indentLevel++;

        for (int i = 0; i < dict.GetCount(); i++) {
          var kvp = dict.GetItemAt(i);
          EditorGUILayout.LabelField($"{kvp.Key} -> {kvp.Value}");
        }

        EditorGUI.indentLevel--;

        if (GUILayout.Button("Clear All Items")) {
          dict.ClearItems();
          Debug.Log($"Cleared all items from {dict.name}");
        }
      } else {
        EditorGUILayout.LabelField("No items in dictionary");
      }
    }
  }

  // Base class for editor access
  public abstract class ObservableDictionaryBase : ScriptableObject {
    public abstract int GetCount();
    public abstract KeyValuePair<object, object> GetItemAt(int index);
    public abstract void ClearItems();
  }
}
