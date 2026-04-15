using System.Collections.Generic;
using Sets;
using UnityEngine;

namespace Assets.TripleTribe.Scripts.RuntimeSet {
  /// <summary>
  /// A runtime set specifically for managing GameObject items, with card-specific functionality.
  /// Extends RuntimeSet<GameObject> to provide methods for handling card components.
  /// </summary>
  [CreateAssetMenu]
  public class CardGameObjectRuntimeSet : RuntimeSet<GameObject> {
    /// <summary>
    /// Retrieves all CardItem components from the GameObjects in the set.
    /// </summary>
    /// <returns>An enumerable collection of CardItem components found on the GameObjects.</returns>
    public IEnumerable<CardItem> GetCards() {
      List<CardItem> result = new List<CardItem>();
      foreach (GameObject item in this.items) {
        var card = item.GetComponent<CardItem>();
        result.Add(card);
      }
      return result;
    }

    /// <summary>
    /// Changes the draggable state of all DragAndDropController components on the GameObjects in the set.
    /// </summary>
    /// <param name="state">The new draggable state to set.</param>
    public void ChangeDraggableState(bool state) {
      foreach (GameObject gameObject in this.items) {
        var controller = gameObject.GetComponent<DragAndDropController>();
        if (controller == null) {
          continue;
        }

        controller.isActive = state;
      }
    }
  }
}
