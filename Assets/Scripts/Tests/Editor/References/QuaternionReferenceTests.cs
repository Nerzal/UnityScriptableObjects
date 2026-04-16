using NUnit.Framework;
using UnityEngine;
using Variables;

[TestFixture]
public class QuaternionReferenceTests
{
    private QuaternionReference quaternionReference;
    private QuaternionVariable quaternionVariable;

    [SetUp]
    public void SetUp()
    {
        quaternionReference = new QuaternionReference();
        quaternionVariable = ScriptableObject.CreateInstance<QuaternionVariable>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(quaternionVariable);
    }

    [Test]
    public void Value_WhenUseConstant_ReturnsConstantValue()
    {
        quaternionReference.useConstant = true;
        quaternionReference.constantValue = Quaternion.Euler(0, 45, 0);

        Assert.AreEqual(Quaternion.Euler(0, 45, 0), quaternionReference.Value);
    }

    [Test]
    public void Value_WhenUseVariable_ReturnsVariableValue()
    {
        quaternionReference.useConstant = false;
        quaternionReference.variable = quaternionVariable;
        quaternionVariable.SetValue(Quaternion.Euler(90, 0, 0));

        Assert.AreEqual(Quaternion.Euler(90, 0, 0), quaternionReference.Value);
    }
}
