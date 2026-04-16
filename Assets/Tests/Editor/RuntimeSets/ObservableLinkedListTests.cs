using NUnit.Framework;
using UnityEngine;
using Sets;
using System.Collections.Generic;

namespace Tests.RuntimeSet {
  public class ObservableLinkedListTests {
    private ObservableLinkedList<int> list;
    private List<int> addedItems;
    private List<int> removedItems;
    private bool cleared;

    [SetUp]
    public void SetUp() {
      list = ScriptableObject.CreateInstance<ObservableLinkedList<int>>();
      addedItems = new List<int>();
      removedItems = new List<int>();
      cleared = false;

      list.itemAdded += item => addedItems.Add(item);
      list.itemRemoved += item => removedItems.Add(item);
      list.listCleared += () => cleared = true;
    }

    [TearDown]
    public void TearDown() {
      Object.DestroyImmediate(list);
    }

    [Test]
    public void AddLast_ItemAddedEventFired() {
      var node = list.AddLast(1);
      Assert.AreEqual(1, list.Count);
      Assert.AreEqual(1, node.Value);
      Assert.Contains(1, addedItems);
    }

    [Test]
    public void AddFirst_ItemAddedEventFired() {
      var node = list.AddFirst(1);
      Assert.AreEqual(1, list.Count);
      Assert.AreEqual(1, node.Value);
      Assert.AreEqual(list.First, node);
      Assert.Contains(1, addedItems);
    }

    [Test]
    public void AddBefore_ItemAddedEventFired() {
      var node1 = list.AddLast(1);
      var node2 = list.AddBefore(node1, 2);
      Assert.AreEqual(2, list.Count);
      Assert.AreEqual(2, node2.Value);
      Assert.AreEqual(list.First, node2);
      Assert.AreEqual(list.Last, node1);
      Assert.Contains(2, addedItems);
    }

    [Test]
    public void AddAfter_ItemAddedEventFired() {
      var node1 = list.AddLast(1);
      var node2 = list.AddAfter(node1, 2);
      Assert.AreEqual(2, list.Count);
      Assert.AreEqual(2, node2.Value);
      Assert.AreEqual(list.First, node1);
      Assert.AreEqual(list.Last, node2);
      Assert.Contains(2, addedItems);
    }

    [Test]
    public void Remove_Value_ItemRemovedEventFired() {
      list.AddLast(1);
      list.AddLast(2);
      bool removed = list.Remove(1);
      Assert.IsTrue(removed);
      Assert.AreEqual(1, list.Count);
      Assert.AreEqual(2, list.First.Value);
      Assert.Contains(1, removedItems);
    }

    [Test]
    public void Remove_Node_ItemRemovedEventFired() {
      var node1 = list.AddLast(1);
      var node2 = list.AddLast(2);
      list.Remove(node1);
      Assert.AreEqual(1, list.Count);
      Assert.AreEqual(2, list.First.Value);
      Assert.Contains(1, removedItems);
    }

    [Test]
    public void RemoveFirst_ItemRemovedEventFired() {
      list.AddLast(1);
      list.AddLast(2);
      list.RemoveFirst();
      Assert.AreEqual(1, list.Count);
      Assert.AreEqual(2, list.First.Value);
      Assert.Contains(1, removedItems);
    }

    [Test]
    public void RemoveLast_ItemRemovedEventFired() {
      list.AddLast(1);
      list.AddLast(2);
      list.RemoveLast();
      Assert.AreEqual(1, list.Count);
      Assert.AreEqual(1, list.First.Value);
      Assert.Contains(2, removedItems);
    }

    [Test]
    public void Clear_ListClearedEventFired() {
      list.AddLast(1);
      list.AddLast(2);
      list.Clear();
      Assert.AreEqual(0, list.Count);
      Assert.IsTrue(cleared);
    }

    [Test]
    public void Contains_ReturnsCorrectValue() {
      list.AddLast(1);
      Assert.IsTrue(list.Contains(1));
      Assert.IsFalse(list.Contains(2));
    }

    [Test]
    public void Find_ReturnsCorrectNode() {
      var node1 = list.AddLast(1);
      var node2 = list.AddLast(2);
      var found = list.Find(1);
      Assert.AreEqual(node1, found);
      Assert.AreEqual(1, found.Value);
    }

    [Test]
    public void FindLast_ReturnsCorrectNode() {
      var node1 = list.AddLast(1);
      var node2 = list.AddLast(1);
      var found = list.FindLast(1);
      Assert.AreEqual(node2, found);
      Assert.AreEqual(1, found.Value);
    }

    [Test]
    public void CopyTo_CopiesToArray() {
      list.AddLast(1);
      list.AddLast(2);
      int[] array = new int[2];
      list.CopyTo(array, 0);
      Assert.AreEqual(1, array[0]);
      Assert.AreEqual(2, array[1]);
    }

    [Test]
    public void GetEnumerator_ReturnsEnumerator() {
      list.AddLast(1);
      list.AddLast(2);
      var enumerator = list.GetEnumerator();
      Assert.IsTrue(enumerator.MoveNext());
      Assert.AreEqual(1, enumerator.Current);
      Assert.IsTrue(enumerator.MoveNext());
      Assert.AreEqual(2, enumerator.Current);
      Assert.IsFalse(enumerator.MoveNext());
    }

    [Test]
    public void First_ReturnsFirstNode() {
      var node1 = list.AddLast(1);
      var node2 = list.AddLast(2);
      Assert.AreEqual(node1, list.First);
      Assert.AreEqual(1, list.First.Value);
    }

    [Test]
    public void Last_ReturnsLastNode() {
      var node1 = list.AddLast(1);
      var node2 = list.AddLast(2);
      Assert.AreEqual(node2, list.Last);
      Assert.AreEqual(2, list.Last.Value);
    }

    [Test]
    public void GetCount_ReturnsCorrectCount() {
      list.AddLast(1);
      list.AddLast(2);
      Assert.AreEqual(2, list.GetCount());
    }

    [Test]
    public void GetItemAt_ReturnsCorrectItem() {
      list.AddLast(1);
      list.AddLast(2);
      Assert.AreEqual(1, list.GetItemAt(0));
      Assert.AreEqual(2, list.GetItemAt(1));
      Assert.IsNull(list.GetItemAt(2));
    }

    [Test]
    public void ClearItems_ClearsList() {
      list.AddLast(1);
      list.AddLast(2);
      list.ClearItems();
      Assert.AreEqual(0, list.GetCount());
      Assert.IsTrue(cleared);
    }
  }
}