using UnityEngine;
using UnityEditor;
using Events;

namespace Events.Editor {
  /// <summary>
  /// Custom inspector for GameEvent assets that provides a button to raise the event for testing.
  /// </summary>
  [CustomEditor(typeof(GameEvent))]
  public class GameEventEditor : UnityEditor.Editor {
    public override void OnInspectorGUI() {
      DrawDefaultInspector();

      GameEvent gameEvent = (GameEvent)target;

      if (GUILayout.Button("Raise Event")) {
        gameEvent.Raise();
        Debug.Log($"Raised event: {gameEvent.name}");
      }

      if (GUILayout.Button("Raise Event (Play Mode Only)")) {
        if (Application.isPlaying) {
          gameEvent.Raise();
          Debug.Log($"Raised event: {gameEvent.name}");
        } else {
          Debug.LogWarning("Event raising is only available in Play Mode");
        }
      }
    }
  }
}