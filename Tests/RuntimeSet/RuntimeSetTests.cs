using NUnit.Framework;
using UnityEngine;
using Sets;

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
    public void Add_ItemNotNull_AddsItem()
    {
        // Arrange
        var item = new GameObject();

        // Act
        runtimeSet.Add(item);

        // Assert
        Assert.AreEqual(1, runtimeSet.Count());
        Assert.IsTrue(runtimeSet.Contains(item));
    }

    [Test]
    public void Add_ItemNull_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<System.ArgumentNullException>(() => runtimeSet.Add(null));
    }

    [Test]
    public void Remove_ItemNotNull_RemovesItem()
    {
        // Arrange
        var item = new GameObject();
        runtimeSet.Add(item);

        // Act
        runtimeSet.Remove(item);

        // Assert
        Assert.AreEqual(0, runtimeSet.Count());
        Assert.IsFalse(runtimeSet.Contains(item));
    }

    [Test]
    public void Remove_ItemNull_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<System.ArgumentNullException>(() => runtimeSet.Remove(null));
    }

    [Test]
    public void AddRange_ItemsNotNull_AddsItems()
    {
        // Arrange
        var items = new[] { new GameObject(), new GameObject() };

        // Act
        runtimeSet.AddRange(items);

        // Assert
        Assert.AreEqual(2, runtimeSet.Count());
    }

    [Test]
    public void AddRange_ItemsNull_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<System.ArgumentNullException>(() => runtimeSet.AddRange(null));
    }

    [Test]
    public void Clear_RemovesAllItems()
    {
        // Arrange
        runtimeSet.Add(new GameObject());
        runtimeSet.Add(new GameObject());

        // Act
        runtimeSet.Clear();

        // Assert
        Assert.AreEqual(0, runtimeSet.Count());
    }

    [Test]
    public void Initialize_ResetsSet()
    {
        // Arrange
        runtimeSet.Add(new GameObject());

        // Act
        runtimeSet.Initialize();

        // Assert
        Assert.AreEqual(0, runtimeSet.Count());
        Assert.AreEqual(-1, runtimeSet.index);
    }

    // Concrete implementation for testing
    private class TestRuntimeSet : RuntimeSet<GameObject> { }
}