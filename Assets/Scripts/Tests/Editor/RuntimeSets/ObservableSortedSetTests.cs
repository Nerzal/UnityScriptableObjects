using NUnit.Framework;
using UnityEngine;
using Sets;
using System.Collections.Generic;

namespace Tests.RuntimeSet {
  public class ObservableSortedSetTests {
    private ObservableSortedSet<int> set;
    private List<int> addedItems;
    private List<int> removedItems;
    private bool cleared;

    [SetUp]
    public void SetUp() {
      set = ScriptableObject.CreateInstance<ObservableSortedSet<int>>();
      addedItems = new List<int>();
      removedItems = new List<int>();
      cleared = false;

      set.itemAdded += item => addedItems.Add(item);
      set.itemRemoved += item => removedItems.Add(item);
      set.setCleared += () => cleared = true;
    }

    [TearDown]
    public void TearDown() {
      Object.DestroyImmediate(set);
    }

    [Test]
    public void Add_ItemAddedEventFired() {
      bool added = set.Add(1);
      Assert.IsTrue(added);
      Assert.AreEqual(1, set.Count);
      Assert.Contains(1, addedItems);
    }

    [Test]
    public void Add_DuplicateItem_NoEventFired() {
      set.Add(1);
      bool added = set.Add(1);
      Assert.IsFalse(added);
      Assert.AreEqual(1, set.Count);
      Assert.AreEqual(1, addedItems.Count); // Only added once
    }

    [Test]
    public void Remove_ItemRemovedEventFired() {
      set.Add(1);
      bool removed = set.Remove(1);
      Assert.IsTrue(removed);
      Assert.AreEqual(0, set.Count);
      Assert.Contains(1, removedItems);
    }

    [Test]
    public void Remove_NonExistentItem_NoEventFired() {
      bool removed = set.Remove(1);
      Assert.IsFalse(removed);
      Assert.AreEqual(0, removedItems.Count);
    }

    [Test]
    public void Clear_SetClearedEventFired() {
      set.Add(1);
      set.Add(2);
      set.Clear();
      Assert.AreEqual(0, set.Count);
      Assert.IsTrue(cleared);
    }

    [Test]
    public void Contains_ReturnsCorrectValue() {
      set.Add(1);
      Assert.IsTrue(set.Contains(1));
      Assert.IsFalse(set.Contains(2));
    }

    [Test]
    public void Min_ReturnsMinimumValue() {
      set.Add(3);
      set.Add(1);
      set.Add(2);
      Assert.AreEqual(1, set.Min);
    }

    [Test]
    public void Max_ReturnsMaximumValue() {
      set.Add(3);
      set.Add(1);
      set.Add(2);
      Assert.AreEqual(3, set.Max);
    }

    [Test]
    public void RemoveMin_RemovesAndReturnsMinimum() {
      set.Add(3);
      set.Add(1);
      set.Add(2);
      int min = set.RemoveMin();
      Assert.AreEqual(1, min);
      Assert.AreEqual(2, set.Count);
      Assert.IsFalse(set.Contains(1));
      Assert.Contains(1, removedItems);
    }

    [Test]
    public void RemoveMax_RemovesAndReturnsMaximum() {
      set.Add(3);
      set.Add(1);
      set.Add(2);
      int max = set.RemoveMax();
      Assert.AreEqual(3, max);
      Assert.AreEqual(2, set.Count);
      Assert.IsFalse(set.Contains(3));
      Assert.Contains(3, removedItems);
    }

    [Test]
    public void GetViewBetween_ReturnsCorrectView() {
      set.Add(1);
      set.Add(2);
      set.Add(3);
      set.Add(4);
      set.Add(5);
      var view = set.GetViewBetween(2, 4);
      Assert.AreEqual(3, view.Count);
      Assert.IsTrue(view.Contains(2));
      Assert.IsTrue(view.Contains(3));
      Assert.IsTrue(view.Contains(4));
    }

    [Test]
    public void UnionWith_AddsNewItems() {
      set.Add(1);
      set.UnionWith(new int[] { 2, 3 });
      Assert.AreEqual(3, set.Count);
      Assert.IsTrue(set.Contains(1));
      Assert.IsTrue(set.Contains(2));
      Assert.IsTrue(set.Contains(3));
      Assert.Contains(2, addedItems);
      Assert.Contains(3, addedItems);
    }

    [Test]
    public void IntersectWith_KeepsCommonItems() {
      set.Add(1);
      set.Add(2);
      set.Add(3);
      set.IntersectWith(new int[] { 2, 3, 4 });
      Assert.AreEqual(2, set.Count);
      Assert.IsTrue(set.Contains(2));
      Assert.IsTrue(set.Contains(3));
      Assert.IsFalse(set.Contains(1));
      Assert.Contains(1, removedItems);
    }

    [Test]
    public void ExceptWith_RemovesSpecifiedItems() {
      set.Add(1);
      set.Add(2);
      set.Add(3);
      set.ExceptWith(new int[] { 2 });
      Assert.AreEqual(2, set.Count);
      Assert.IsTrue(set.Contains(1));
      Assert.IsTrue(set.Contains(3));
      Assert.IsFalse(set.Contains(2));
      Assert.Contains(2, removedItems);
    }

    [Test]
    public void SymmetricExceptWith_TogglesItems() {
      set.Add(1);
      set.Add(2);
      set.SymmetricExceptWith(new int[] { 2, 3 });
      Assert.AreEqual(2, set.Count);
      Assert.IsTrue(set.Contains(1));
      Assert.IsTrue(set.Contains(3));
      Assert.IsFalse(set.Contains(2));
      Assert.Contains(2, removedItems);
      Assert.Contains(3, addedItems);
    }

    [Test]
    public void IsSubsetOf_ReturnsCorrectValue() {
      set.Add(1);
      set.Add(2);
      Assert.IsTrue(set.IsSubsetOf(new int[] { 1, 2, 3 }));
      Assert.IsFalse(set.IsSubsetOf(new int[] { 1, 3 }));
    }

    [Test]
    public void IsSupersetOf_ReturnsCorrectValue() {
      set.Add(1);
      set.Add(2);
      set.Add(3);
      Assert.IsTrue(set.IsSupersetOf(new int[] { 1, 2 }));
      Assert.IsFalse(set.IsSupersetOf(new int[] { 1, 2, 4 }));
    }

    [Test]
    public void Overlaps_ReturnsCorrectValue() {
      set.Add(1);
      set.Add(2);
      Assert.IsTrue(set.Overlaps(new int[] { 2, 3 }));
      Assert.IsFalse(set.Overlaps(new int[] { 3, 4 }));
    }

    [Test]
    public void SetEquals_ReturnsCorrectValue() {
      set.Add(1);
      set.Add(2);
      Assert.IsTrue(set.SetEquals(new int[] { 1, 2 }));
      Assert.IsTrue(set.SetEquals(new int[] { 2, 1 }));
      Assert.IsFalse(set.SetEquals(new int[] { 1, 2, 3 }));
    }

    [Test]
    public void CopyTo_CopiesToArray() {
      set.Add(1);
      set.Add(3);
      set.Add(2);
      int[] array = new int[3];
      set.CopyTo(array, 0);
      Assert.AreEqual(1, array[0]);
      Assert.AreEqual(2, array[1]);
      Assert.AreEqual(3, array[2]);
    }

    [Test]
    public void GetEnumerator_ReturnsEnumerator() {
      set.Add(1);
      set.Add(3);
      set.Add(2);
      var enumerator = set.GetEnumerator();
      Assert.IsTrue(enumerator.MoveNext());
      Assert.AreEqual(1, enumerator.Current);
      Assert.IsTrue(enumerator.MoveNext());
      Assert.AreEqual(2, enumerator.Current);
      Assert.IsTrue(enumerator.MoveNext());
      Assert.AreEqual(3, enumerator.Current);
      Assert.IsFalse(enumerator.MoveNext());
    }

    [Test]
    public void IsReadOnly_ReturnsFalse() {
      Assert.IsFalse(set.IsReadOnly);
    }

    [Test]
    public void GetCount_ReturnsCorrectCount() {
      set.Add(1);
      set.Add(2);
      Assert.AreEqual(2, set.GetCount());
    }

    [Test]
    public void GetItemAt_ReturnsCorrectItem() {
      set.Add(1);
      set.Add(3);
      set.Add(2);
      Assert.AreEqual(1, set.GetItemAt(0));
      Assert.AreEqual(2, set.GetItemAt(1));
      Assert.AreEqual(3, set.GetItemAt(2));
    }

    [Test]
    public void ClearItems_ClearsSet() {
      set.Add(1);
      set.Add(2);
      set.ClearItems();
      Assert.AreEqual(0, set.GetCount());
      Assert.IsTrue(cleared);
    }
  }
}