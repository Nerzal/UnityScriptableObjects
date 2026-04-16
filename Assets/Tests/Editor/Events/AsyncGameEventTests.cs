using NUnit.Framework;
using UnityEngine;
using Events;
using System.Threading.Tasks;

[TestFixture]
public class AsyncGameEventTests
{
    private AsyncGameEvent asyncGameEvent;
    private AsyncGameEventListener listener;

    [SetUp]
    public void SetUp()
    {
        asyncGameEvent = ScriptableObject.CreateInstance<AsyncGameEvent>();
        var go = new GameObject();
        listener = go.AddComponent<AsyncGameEventListener>();
        listener.Event = asyncGameEvent;
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(asyncGameEvent);
        if (listener != null) {
            Object.DestroyImmediate(listener.gameObject);
        }
    }

    [Test]
    public async Task RaiseAsync_NotifiesListener()
    {
        bool invoked = false;
        listener.Response.AddListener(() => invoked = true);

        await asyncGameEvent.RaiseAsync();

        Assert.IsTrue(invoked);
    }

    [Test]
    public async Task RaiseAsync_WithMultipleListeners_AllNotified()
    {
        var go2 = new GameObject();
        var listener2 = go2.AddComponent<AsyncGameEventListener>();
        listener2.Event = asyncGameEvent;

        bool invoked1 = false;
        bool invoked2 = false;
        listener.Response.AddListener(() => invoked1 = true);
        listener2.Response.AddListener(() => invoked2 = true);

        await asyncGameEvent.RaiseAsync();

        Assert.IsTrue(invoked1);
        Assert.IsTrue(invoked2);

        Object.DestroyImmediate(go2);
    }

    [Test]
    public async Task RaiseAsync_NoListeners_CompletesSuccessfully()
    {
        // Remove the default listener
        asyncGameEvent.UnregisterListener(listener);

        // Should not throw
        await asyncGameEvent.RaiseAsync();

        Assert.Pass();
    }
}
