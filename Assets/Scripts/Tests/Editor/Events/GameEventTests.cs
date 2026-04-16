using NUnit.Framework;
using UnityEngine;
using Events;

[TestFixture]
public class GameEventTests
{
    private GameEvent gameEvent;
    private GameEventListener listener1;
    private GameEventListener listener2;

    [SetUp]
    public void SetUp()
    {
        gameEvent = ScriptableObject.CreateInstance<GameEvent>();
        var go1 = new GameObject();
        listener1 = go1.AddComponent<GameEventListener>();
        listener1.Event = gameEvent;

        var go2 = new GameObject();
        listener2 = go2.AddComponent<GameEventListener>();
        listener2.Event = gameEvent;
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(gameEvent);
        Object.DestroyImmediate(listener1.gameObject);
        Object.DestroyImmediate(listener2.gameObject);
    }

    [Test]
    public void Raise_NotifiesAllRegisteredListeners()
    {
        // Arrange
        bool listener1Called = false;
        bool listener2Called = false;
        listener1.response.AddListener(() => listener1Called = true);
        listener2.response.AddListener(() => listener2Called = true);

        // Act
        gameEvent.Raise();

        // Assert
        Assert.IsTrue(listener1Called);
        Assert.IsTrue(listener2Called);
    }

    [Test]
    public void RegisterListener_AddsListener()
    {
        // Arrange
        var newListener = new GameObject().AddComponent<GameEventListener>();
        newListener.Event = gameEvent;

        // Act
        gameEvent.RegisterListener(newListener);

        // Assert
        bool called = false;
        newListener.response.AddListener(() => called = true);
        gameEvent.Raise();
        Assert.IsTrue(called);

        Object.DestroyImmediate(newListener.gameObject);
    }

    [Test]
    public void UnregisterListener_RemovesListener()
    {
        // Arrange
        bool called = false;
        listener1.response.AddListener(() => called = true);

        // Act
        gameEvent.UnregisterListener(listener1);

        // Assert
        gameEvent.Raise();
        Assert.IsFalse(called);
    }

    [Test]
    public void RegisterListener_Duplicate_DoesNotAddTwice()
    {
        // Arrange
        int callCount = 0;
        listener1.response.AddListener(() => callCount++);

        // Act
        gameEvent.RegisterListener(listener1); // Already registered in SetUp

        // Assert
        gameEvent.Raise();
        Assert.AreEqual(1, callCount);
    }
}