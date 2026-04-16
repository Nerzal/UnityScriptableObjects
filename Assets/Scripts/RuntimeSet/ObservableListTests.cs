using NUnit.Framework;
using UnityEngine;
using Sets;
using System.Collections.Generic;

namespace Tests.RuntimeSet {
    public class ObservableListTests {
        private ObservableList<int> list;
        private List<int> addedItems;
        private List<int> removedItems;
        private List<int> insertedItems;
        private List<int> modifiedItems;
        private bool cleared;

        [SetUp]
        public void SetUp() {
            list = ScriptableObject.CreateInstance<ObservableList<int>>();
            addedItems = new List<int>();
            removedItems = new List<int>();
            insertedItems = new List<int>();
            modifiedItems = new List<int>();
            cleared = false;

            list.itemAdded += (item, index) => addedItems.Add(item);
            list.itemRemoved += (item, index) => removedItems.Add(item);
            list.itemInserted += (item, index) => insertedItems.Add(item);
            list.itemModified += (item, index) => modifiedItems.Add(item);
            list.listCleared += () => cleared = true;
        }

        [TearDown]
        public void TearDown() {
            Object.DestroyImmediate(list);
        }

        [Test]
        public void Add_ItemAddedEventFired() {
            list.Add(1);
            Assert.AreEqual(1, list.Count);
            Assert.Contains(1, addedItems);
        }

        [Test]
        public void Insert_ItemInsertedEventFired() {
            list.Add(1);
            list.Insert(0, 2);
            Assert.AreEqual(2, list.Count);
            Assert.Contains(2, insertedItems);
            Assert.AreEqual(2, list[0]);
            Assert.AreEqual(1, list[1]);
        }

        [Test]
        public void Remove_ItemRemovedEventFired() {
            list.Add(1);
            list.Add(2);
            bool removed = list.Remove(1);
            Assert.IsTrue(removed);
            Assert.AreEqual(1, list.Count);
            Assert.Contains(1, removedItems);
            Assert.AreEqual(2, list[0]);
        }

        [Test]
        public void RemoveAt_ItemRemovedEventFired() {
            list.Add(1);
            list.Add(2);
            list.RemoveAt(0);
            Assert.AreEqual(1, list.Count);
            Assert.Contains(1, removedItems);
            Assert.AreEqual(2, list[0]);
        }

        [Test]
        public void Clear_ListClearedEventFired() {
            list.Add(1);
            list.Add(2);
            list.Clear();
            Assert.AreEqual(0, list.Count);
            Assert.IsTrue(cleared);
        }

        [Test]
        public void Indexer_Set_ItemModifiedEventFired() {
            list.Add(1);
            list[0] = 2;
            Assert.AreEqual(2, list[0]);
            Assert.Contains(2, modifiedItems);
        }

        [Test]
        public void Contains_ReturnsCorrectValue() {
            list.Add(1);
            Assert.IsTrue(list.Contains(1));
            Assert.IsFalse(list.Contains(2));
        }

        [Test]
        public void IndexOf_ReturnsCorrectIndex() {
            list.Add(1);
            list.Add(2);
            Assert.AreEqual(0, list.IndexOf(1));
            Assert.AreEqual(1, list.IndexOf(2));
            Assert.AreEqual(-1, list.IndexOf(3));
        }

        [Test]
        public void CopyTo_CopiesToArray() {
            list.Add(1);
            list.Add(2);
            int[] array = new int[2];
            list.CopyTo(array, 0);
            Assert.AreEqual(1, array[0]);
            Assert.AreEqual(2, array[1]);
        }

        [Test]
        public void GetEnumerator_ReturnsEnumerator() {
            list.Add(1);
            list.Add(2);
            var enumerator = list.GetEnumerator();
            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual(1, enumerator.Current);
            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreEqual(2, enumerator.Current);
            Assert.IsFalse(enumerator.MoveNext());
        }

        [Test]
        public void IsReadOnly_ReturnsFalse() {
            Assert.IsFalse(list.IsReadOnly);
        }

        [Test]
        public void GetCount_ReturnsCorrectCount() {
            list.Add(1);
            list.Add(2);
            Assert.AreEqual(2, list.GetCount());
        }

        [Test]
        public void GetItemAt_ReturnsCorrectItem() {
            list.Add(1);
            list.Add(2);
            Assert.AreEqual(1, list.GetItemAt(0));
            Assert.AreEqual(2, list.GetItemAt(1));
            Assert.IsNull(list.GetItemAt(2));
        }

        [Test]
        public void ClearItems_ClearsList() {
            list.Add(1);
            list.Add(2);
            list.ClearItems();
            Assert.AreEqual(0, list.GetCount());
            Assert.IsTrue(cleared);
        }
    }
}
