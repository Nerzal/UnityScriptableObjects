using UnityEngine;
using UnityEditor;
using Sets;

namespace Sets.Editor {
  /// <summary>
  /// Custom inspector for ObservableCollection assets that displays current items and provides controls.
  /// </summary>
  [CustomEditor(typeof(ObservableCollectionBase), true)]
  public class ObservableCollectionEditor : UnityEditor.Editor {
    public override void OnInspectorGUI() {
      DrawDefaultInspector();

      ObservableCollectionBase collection = (ObservableCollectionBase)target;

      EditorGUILayout.Space();
      EditorGUILayout.LabelField("Runtime Controls", EditorStyles.boldLabel);

      EditorGUILayout.LabelField($"Current Count: {collection.GetCount()}");

      if (collection.GetCount() > 0) {
        EditorGUILayout.LabelField("Current Items:");
        EditorGUI.indentLevel++;

        for (int i = 0; i < collection.GetCount(); i++) {
          object item = collection.GetItemAt(i);
          EditorGUILayout.LabelField($"[{i}] {item?.ToString() ?? "null"}");
        }

        EditorGUI.indentLevel--;

        if (GUILayout.Button("Clear All Items")) {
          collection.ClearItems();
          Debug.Log($"Cleared all items from {collection.name}");
        }
      } else {
        EditorGUILayout.LabelField("No items in collection");
      }
    }
  }
}