using NUnit.Framework;
using UnityEngine;
using Variables;

[TestFixture]
public class QuaternionVariableTests
{
    private QuaternionVariable quaternionVariable;

    [SetUp]
    public void SetUp()
    {
        quaternionVariable = ScriptableObject.CreateInstance<QuaternionVariable>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(quaternionVariable);
    }

    [Test]
    public void SetValue_WhenChanged_InvokesEvent()
    {
        bool invoked = false;
        quaternionVariable.valueChanged += value => invoked = true;

        quaternionVariable.SetValue(Quaternion.Euler(0, 90, 0));

        Assert.IsTrue(invoked);
        Assert.AreEqual(Quaternion.Euler(0, 90, 0), quaternionVariable.value);
    }
}
