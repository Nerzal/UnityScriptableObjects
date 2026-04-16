using NUnit.Framework;
using UnityEngine;
using Variables;

[TestFixture]
public class StringReferenceTests
{
    private StringReference stringReference;
    private StringVariable stringVariable;

    [SetUp]
    public void SetUp()
    {
        stringReference = new StringReference();
        stringVariable = ScriptableObject.CreateInstance<StringVariable>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(stringVariable);
    }

    [Test]
    public void Value_WhenUseConstant_ReturnsConstantValue()
    {
        stringReference.useConstant = true;
        stringReference.constantValue = "Hello";

        Assert.AreEqual("Hello", stringReference.Value);
    }

    [Test]
    public void Value_WhenUseVariable_ReturnsVariableValue()
    {
        stringReference.useConstant = false;
        stringReference.variable = stringVariable;
        stringVariable.SetValue("World");

        Assert.AreEqual("World", stringReference.Value);
    }
}
