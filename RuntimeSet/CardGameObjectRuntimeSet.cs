using System.Collections.Generic;
using Sets;
using UnityEngine;

namespace Assets.TripleTribe.Scripts.RuntimeSet {
  [CreateAssetMenu]
  public class CardGameObjectRuntimeSet : RuntimeSet<GameObject> {
    /// <summary>
    /// GetCards returns the current cards in the deck
    /// </summary>
    /// <returns>The list of cards which are currently in the deck</returns>
    public IEnumerable<CardItem> GetCards() {
      List<CardItem> result = new List<CardItem>();
      foreach (GameObject item in this.items) {
        var card = item.GetComponent<CardItem>();
        result.Add(card);
      }
      return result;
    }

    /// <summary>
    /// Changes the draggable state of the drag&drop controller
    /// </summary>
    /// <param name="state"></param>
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
