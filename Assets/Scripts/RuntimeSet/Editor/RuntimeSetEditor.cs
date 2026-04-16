using UnityEngine;
using UnityEditor;
using Sets;

namespace Sets.Editor {
  /// <summary>
  /// Custom inspector for RuntimeSet assets that displays current items and provides controls.
  /// </summary>
  [CustomEditor(typeof(RuntimeSet<>), true)]
  public class RuntimeSetEditor : UnityEditor.Editor {
    public override void OnInspectorGUI() {
      DrawDefaultInspector();

      RuntimeSetBase runtimeSet = (RuntimeSetBase)target;

      EditorGUILayout.Space();
      EditorGUILayout.LabelField("Runtime Controls", EditorStyles.boldLabel);

      EditorGUILayout.LabelField($"Current Count: {runtimeSet.GetCount()}");

      if (runtimeSet.GetCount() > 0) {
        EditorGUILayout.LabelField("Current Items:");
        EditorGUI.indentLevel++;

        for (int i = 0; i < runtimeSet.GetCount(); i++) {
          object item = runtimeSet.GetItemAt(i);
          EditorGUILayout.LabelField($"[{i}] {item?.ToString() ?? "null"}");
        }

        EditorGUI.indentLevel--;

        if (GUILayout.Button("Clear All Items")) {
          runtimeSet.ClearItems();
          Debug.Log($"Cleared all items from {runtimeSet.name}");
        }
      } else {
        EditorGUILayout.LabelField("No items in set");
      }

      if (GUILayout.Button("Initialize Set")) {
        runtimeSet.InitializeSet();
        Debug.Log($"Initialized {runtimeSet.name}");
      }
    }
  }

  // Base class for editor access
  public abstract class RuntimeSetBase : ScriptableObject {
    public abstract int GetCount();
    public abstract object GetItemAt(int index);
    public abstract void ClearItems();
    public abstract void InitializeSet();
  }
}