using NUnit.Framework;
using UnityEngine;
using Variables;

[TestFixture]
public class IntReferenceTests
{
    private IntReference intReference;
    private IntVariable intVariable;

    [SetUp]
    public void SetUp()
    {
        intReference = new IntReference();
        intVariable = ScriptableObject.CreateInstance<IntVariable>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(intVariable);
    }

    [Test]
    public void Value_WhenUseConstant_ReturnsConstantValue()
    {
        // Arrange
        intReference.useConstant = true;
        intReference.constantValue = 42;

        // Act
        int result = intReference.Value;

        // Assert
        Assert.AreEqual(42, result);
    }

    [Test]
    public void Value_WhenUseVariable_ReturnsVariableValue()
    {
        // Arrange
        intReference.useConstant = false;
        intReference.variable = intVariable;
        intVariable.SetValue(99);

        // Act
        int result = intReference.Value;

        // Assert
        Assert.AreEqual(99, result);
    }

    [Test]
    public void ImplicitOperator_ReturnsCorrectValue()
    {
        // Arrange
        intReference.useConstant = true;
        intReference.constantValue = 10;

        // Act
        int result = intReference; // Implicit cast

        // Assert
        Assert.AreEqual(10, result);
    }

    [Test]
    public void Constructor_WithValue_SetsConstant()
    {
        // Arrange & Act
        IntReference refWithValue = new IntReference(25);

        // Assert
        Assert.IsTrue(refWithValue.useConstant);
        Assert.AreEqual(25, refWithValue.constantValue);
    }
}