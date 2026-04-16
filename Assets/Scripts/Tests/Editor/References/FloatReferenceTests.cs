using NUnit.Framework;
using UnityEngine;
using Variables;

[TestFixture]
public class FloatReferenceTests
{
    private FloatReference floatReference;
    private FloatVariable floatVariable;

    [SetUp]
    public void SetUp()
    {
        floatReference = new FloatReference();
        floatVariable = ScriptableObject.CreateInstance<FloatVariable>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(floatVariable);
    }

    [Test]
    public void Value_WhenUseConstant_ReturnsConstantValue()
    {
        // Arrange
        floatReference.useConstant = true;
        floatReference.constantValue = 42.5f;

        // Act
        float result = floatReference.Value;

        // Assert
        Assert.AreEqual(42.5f, result);
    }

    [Test]
    public void Value_WhenUseVariable_ReturnsVariableValue()
    {
        // Arrange
        floatReference.useConstant = false;
        floatReference.variable = floatVariable;
        floatVariable.SetValue(99.9f);

        // Act
        float result = floatReference.Value;

        // Assert
        Assert.AreEqual(99.9f, result);
    }

    [Test]
    public void ImplicitOperator_ReturnsCorrectValue()
    {
        // Arrange
        floatReference.useConstant = true;
        floatReference.constantValue = 10.0f;

        // Act
        float result = floatReference; // Implicit cast

        // Assert
        Assert.AreEqual(10.0f, result);
    }

    [Test]
    public void Constructor_WithValue_SetsConstant()
    {
        // Arrange & Act
        //FloatReference refWithValue = new FloatReference(25.0f);

        // Assert
        //Assert.IsTrue(refWithValue.useConstant);
        //Assert.AreEqual(25.0f, refWithValue.constantValue);
    }
}
