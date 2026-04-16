using NUnit.Framework;
using UnityEngine;
using Events;
using System.Threading.Tasks;

[TestFixture]
public class AsyncGenericGameEventTests
{
    private AsyncGenericGameEvent<string> asyncGenericGameEvent;
    private AsyncGenericGameEventListener<string> listener;

    [SetUp]
    public void SetUp()
    {
        asyncGenericGameEvent = ScriptableObject.CreateInstance<AsyncGenericGameEvent<string>>();
        var go = new GameObject();
        listener = go.AddComponent<AsyncGenericGameEventListener<string>>();
        listener.Event = asyncGenericGameEvent;
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(asyncGenericGameEvent);
        if (listener != null) {
            Object.DestroyImmediate(listener.gameObject);
        }
    }

    [Test]
    public async Task RaiseAsync_WithArgument_NotifiesListener()
    {
        string receivedArg = null;
        listener.Response.AddListener((arg) => receivedArg = arg);

        await asyncGenericGameEvent.RaiseAsync("Test Message");

        Assert.AreEqual("Test Message", receivedArg);
    }

    [Test]
    public async Task RaiseAsync_WithMultipleListeners_AllReceiveArgument()
    {
        var go2 = new GameObject();
        var listener2 = go2.AddComponent<AsyncGenericGameEventListener<string>>();
        listener2.Event = asyncGenericGameEvent;

        string receivedArg1 = null;
        string receivedArg2 = null;
        listener.Response.AddListener((arg) => receivedArg1 = arg);
        listener2.Response.AddListener((arg) => receivedArg2 = arg);

        await asyncGenericGameEvent.RaiseAsync("Shared Message");

        Assert.AreEqual("Shared Message", receivedArg1);
        Assert.AreEqual("Shared Message", receivedArg2);

        Object.DestroyImmediate(go2);
    }

    [Test]
    public async Task RaiseAsync_IntType_WorksCorrectly()
    {
        var intEvent = ScriptableObject.CreateInstance<AsyncGenericGameEvent<int>>();
        var go = new GameObject();
        var intListener = go.AddComponent<AsyncGenericGameEventListener<int>>();
        intListener.Event = intEvent;

        int receivedValue = 0;
        intListener.Response.AddListener((value) => receivedValue = value);

        await intEvent.RaiseAsync(42);

        Assert.AreEqual(42, receivedValue);

        Object.DestroyImmediate(intEvent);
        Object.DestroyImmediate(go);
    }
}
