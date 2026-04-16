using NUnit.Framework;
using UnityEngine;
using Variables;

[TestFixture]
public class DoubleReferenceTests
{
    private DoubleReference doubleReference;
    private DoubleVariable doubleVariable;

    [SetUp]
    public void SetUp()
    {
        doubleReference = new DoubleReference();
        doubleVariable = ScriptableObject.CreateInstance<DoubleVariable>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(doubleVariable);
    }

    [Test]
    public void Value_WhenUseConstant_ReturnsConstantValue()
    {
        doubleReference.useConstant = true;
        doubleReference.constantValue = 12.75;

        Assert.AreEqual(12.75, doubleReference.Value);
    }

    [Test]
    public void Value_WhenUseVariable_ReturnsVariableValue()
    {
        doubleReference.useConstant = false;
        doubleReference.variable = doubleVariable;
        doubleVariable.SetValue(3.14159);

        Assert.AreEqual(3.14159, doubleReference.Value);
    }
}
