using NUnit.Framework;
using UnityEngine;
using Variables;

[TestFixture]
public class GameObjectVariableTests
{
    private GameObjectVariable gameObjectVariable;
    private GameObject testObject;

    [SetUp]
    public void SetUp()
    {
        gameObjectVariable = ScriptableObject.CreateInstance<GameObjectVariable>();
        testObject = new GameObject("TestObject");
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(gameObjectVariable);
        Object.DestroyImmediate(testObject);
    }

    [Test]
    public void SetValue_WhenChanged_InvokesEvent()
    {
        bool invoked = false;
        gameObjectVariable.valueChanged += value => invoked = true;

        gameObjectVariable.SetValue(testObject);

        Assert.IsTrue(invoked);
        Assert.AreEqual(testObject, gameObjectVariable.value);
    }
}
