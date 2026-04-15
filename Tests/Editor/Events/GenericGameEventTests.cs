using NUnit.Framework;
using UnityEngine;
using Events;

[TestFixture]
public class GenericGameEventTests
{
    private GenericGameEvent<string> genericEvent;
    private TestGenericListener listener1;
    private TestGenericListener listener2;

    [SetUp]
    public void SetUp()
    {
        genericEvent = ScriptableObject.CreateInstance<GenericGameEvent<string>>();
        var go1 = new GameObject();
        listener1 = go1.AddComponent<TestGenericListener>();
        listener1.GameEvent = genericEvent;

        var go2 = new GameObject();
        listener2 = go2.AddComponent<TestGenericListener>();
        listener2.GameEvent = genericEvent;
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(genericEvent);
        Object.DestroyImmediate(listener1.gameObject);
        Object.DestroyImmediate(listener2.gameObject);
    }

    [Test]
    public void Raise_WithArgument_NotifiesAllRegisteredListeners()
    {
        // Arrange
        string received1 = null;
        string received2 = null;
        listener1.OnRaised = (arg) => received1 = arg;
        listener2.OnRaised = (arg) => received2 = arg;

        // Act
        genericEvent.Raise("Test Message");

        // Assert
        Assert.AreEqual("Test Message", received1);
        Assert.AreEqual("Test Message", received2);
    }

    [Test]
    public void RegisterListener_AddsListener()
    {
        // Arrange
        var newListener = new GameObject().AddComponent<TestGenericListener>();
        newListener.GameEvent = genericEvent;
        string received = null;
        newListener.OnRaised = (arg) => received = arg;

        // Act
        genericEvent.RegisterListener(newListener);

        // Assert
        genericEvent.Raise("New Message");
        Assert.AreEqual("New Message", received);

        Object.DestroyImmediate(newListener.gameObject);
    }

    [Test]
    public void UnregisterListener_RemovesListener()
    {
        // Arrange
        string received = null;
        listener1.OnRaised = (arg) => received = arg;

        // Act
        genericEvent.UnregisterListener(listener1);

        // Assert
        genericEvent.Raise("Should Not Receive");
        Assert.IsNull(received);
    }
}

// Test helper class
public class TestGenericListener : GenericGameEventListener<string>
{
    public GenericGameEvent<string> GameEvent;
    public System.Action<string> OnRaised;

    private void OnEnable()
    {
        GameEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        GameEvent.UnregisterListener(this);
    }

    public override void OnEventRaised(string arg)
    {
        OnRaised?.Invoke(arg);
    }
}