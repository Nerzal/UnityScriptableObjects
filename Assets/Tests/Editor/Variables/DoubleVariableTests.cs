using NUnit.Framework;
using UnityEngine;
using Variables;

[TestFixture]
public class DoubleVariableTests
{
    private DoubleVariable doubleVariable;

    [SetUp]
    public void SetUp()
    {
        doubleVariable = ScriptableObject.CreateInstance<DoubleVariable>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(doubleVariable);
    }

    [Test]
    public void SetValue_ValueChanged_InvokesEvent()
    {
        bool invoked = false;
        doubleVariable.valueChanged += value => invoked = true;

        doubleVariable.SetValue(1.25);

        Assert.IsTrue(invoked);
        Assert.AreEqual(1.25, doubleVariable.value);
    }

    [Test]
    public void ApplyChange_AddsAmount()
    {
        doubleVariable.SetValue(2.5);

        doubleVariable.ApplyChange(1.5);

        Assert.AreEqual(4.0, doubleVariable.value);
    }
}
