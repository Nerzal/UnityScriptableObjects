using NUnit.Framework;
using UnityEngine;
using Sets;
using System.Collections.Generic;

[TestFixture]
public class RuntimeSetTests
{
    private TestRuntimeSet runtimeSet;

    [SetUp]
    public void SetUp()
    {
        runtimeSet = ScriptableObject.CreateInstance<TestRuntimeSet>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(runtimeSet);
    }

    [Test]
    public void Add_AddsItemAndInvokesEvent()
    {
        // Arrange
        bool eventInvoked = false;
        runtimeSet.itemsChanged += () => eventInvoked = true;
        string item = "TestItem";

        // Act
        runtimeSet.Add(item);

        // Assert
        Assert.AreEqual(1, runtimeSet.Count());
        Assert.IsTrue(runtimeSet.Contains(item));
        Assert.IsTrue(eventInvoked);
        Assert.AreEqual(0, runtimeSet.index); // index starts at -1, increments to 0
    }

    [Test]
    public void Add_DuplicateItem_DoesNotAdd()
    {
        // Arrange
        runtimeSet.Add("Item");
        bool eventInvoked = false;
        runtimeSet.itemsChanged += () => eventInvoked = true;

        // Act
        runtimeSet.Add("Item");

        // Assert
        Assert.AreEqual(1, runtimeSet.Count());
        Assert.IsFalse(eventInvoked);
    }

    [Test]
    public void Remove_RemovesItemAndInvokesEvent()
    {
        // Arrange
        runtimeSet.Add("Item");
        bool eventInvoked = false;
        runtimeSet.itemsChanged += () => eventInvoked = true;

        // Act
        runtimeSet.Remove("Item");

        // Assert
        Assert.AreEqual(0, runtimeSet.Count());
        Assert.IsFalse(runtimeSet.Contains("Item"));
        Assert.IsTrue(eventInvoked);
        Assert.AreEqual(-1, runtimeSet.index); // decrements back
    }

    [Test]
    public void Remove_NonExistentItem_DoesNothing()
    {
        // Arrange
        bool eventInvoked = false;
        runtimeSet.itemsChanged += () => eventInvoked = true;

        // Act
        runtimeSet.Remove("NonExistent");

        // Assert
        Assert.AreEqual(0, runtimeSet.Count());
        Assert.IsFalse(eventInvoked);
    }

    [Test]
    public void Clear_RemovesAllItemsAndInvokesEvent()
    {
        // Arrange
        runtimeSet.Add("Item1");
        runtimeSet.Add("Item2");
        bool eventInvoked = false;
        runtimeSet.itemsChanged += () => eventInvoked = true;

        // Act
        runtimeSet.Clear();

        // Assert
        Assert.AreEqual(0, runtimeSet.Count());
        Assert.IsTrue(eventInvoked);
    }

    [Test]
    public void Initialize_ResetsSet()
    {
        // Arrange
        runtimeSet.Add("Item");
        bool eventInvoked = false;
        runtimeSet.itemsChanged += () => eventInvoked = true;

        // Act
        runtimeSet.Initialize();

        // Assert
        Assert.AreEqual(0, runtimeSet.Count());
        Assert.AreEqual(-1, runtimeSet.index);
        Assert.IsTrue(eventInvoked);
    }

    [Test]
    public void Indexer_GetsAndSetsItem()
    {
        // Arrange
        runtimeSet.Add("Original");

        // Act & Assert
        Assert.AreEqual("Original", runtimeSet[0]);
        runtimeSet[0] = "Modified";
        Assert.AreEqual("Modified", runtimeSet[0]);
    }

    [Test]
    public void GetEnumerator_EnumeratesItems()
    {
        // Arrange
        runtimeSet.Add("Item1");
        runtimeSet.Add("Item2");

        // Act
        var enumerator = runtimeSet.GetEnumerator();
        enumerator.MoveNext();
        string first = enumerator.Current;
        enumerator.MoveNext();
        string second = enumerator.Current;

        // Assert
        Assert.AreEqual("Item1", first);
        Assert.AreEqual("Item2", second);
    }

    [Test]
    public void LargeList_AddManyItems_PerformsCorrectly()
    {
        // Arrange
        const int largeCount = 1000;
        var largeList = new List<string>();
        for (int i = 0; i < largeCount; i++)
        {
            largeList.Add($"Item{i}");
        }

        // Act
        runtimeSet.AddRange(largeList);

        // Assert
        Assert.AreEqual(largeCount, runtimeSet.Count());
        Assert.IsTrue(runtimeSet.Contains("Item0"));
        Assert.IsTrue(runtimeSet.Contains($"Item{largeCount - 1}"));
    }
}

// Test helper class
public class TestRuntimeSet : RuntimeSet<string> { }
