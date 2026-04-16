using NUnit.Framework;
using UnityEngine;
using Variables;

[TestFixture]
public class IntVariableTests
{
    private IntVariable intVariable;

    [SetUp]
    public void SetUp()
    {
        intVariable = ScriptableObject.CreateInstance<IntVariable>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(intVariable);
    }

    [Test]
    public void SetValue_ChangesValueAndInvokesEvent()
    {
        // Arrange
        bool eventInvoked = false;
        int newValue = 42;
        intVariable.valueChanged += (val) => eventInvoked = true;

        // Act
        intVariable.SetValue(newValue);

        // Assert
        Assert.AreEqual(newValue, intVariable.value);
        Assert.IsTrue(eventInvoked);
    }

    [Test]
    public void SetValue_SameValue_DoesNotInvokeEvent()
    {
        // Arrange
        int initialValue = 10;
        intVariable.SetValue(initialValue);
        bool eventInvoked = false;
        intVariable.valueChanged += (val) => eventInvoked = true;

        // Act
        intVariable.SetValue(initialValue);

        // Assert
        Assert.AreEqual(initialValue, intVariable.value);
        Assert.IsFalse(eventInvoked);
    }

    [Test]
    public void ApplyChange_Int_IncreasesValue()
    {
        // Arrange
        intVariable.SetValue(10);
        int amount = 5;

        // Act
        intVariable.ApplyChange(amount);

        // Assert
        Assert.AreEqual(15, intVariable.value);
    }

    [Test]
    public void ApplyChange_IntVariable_IncreasesValue()
    {
        // Arrange
        intVariable.SetValue(10);
        IntVariable amountVar = ScriptableObject.CreateInstance<IntVariable>();
        amountVar.SetValue(5);

        // Act
        intVariable.ApplyChange(amountVar);

        // Assert
        Assert.AreEqual(15, intVariable.value);
        Object.DestroyImmediate(amountVar);
    }

    [Test]
    public void ApplyChange_ZeroAmount_DoesNotChangeValue()
    {
        // Arrange
        intVariable.SetValue(10);

        // Act
        intVariable.ApplyChange(0);

        // Assert
        Assert.AreEqual(10, intVariable.value);
    }

    [Test]
    public void Operator_LessThan_ReturnsCorrectResult()
    {
        // Arrange
        IntVariable var1 = ScriptableObject.CreateInstance<IntVariable>();
        IntVariable var2 = ScriptableObject.CreateInstance<IntVariable>();
        var1.SetValue(5);
        var2.SetValue(10);

        // Act & Assert
        Assert.IsTrue(var1 < var2);
        Assert.IsFalse(var2 < var1);

        Object.DestroyImmediate(var1);
        Object.DestroyImmediate(var2);
    }

    [Test]
    public void Operator_GreaterThan_ReturnsCorrectResult()
    {
        // Arrange
        IntVariable var1 = ScriptableObject.CreateInstance<IntVariable>();
        IntVariable var2 = ScriptableObject.CreateInstance<IntVariable>();
        var1.SetValue(15);
        var2.SetValue(10);

        // Act & Assert
        Assert.IsTrue(var1 > var2);
        Assert.IsFalse(var2 > var1);

        Object.DestroyImmediate(var1);
        Object.DestroyImmediate(var2);
    }

    [Test]
    public void ToString_ReturnsValueAsString()
    {
        // Arrange
        intVariable.SetValue(123);

        // Act
        string result = intVariable.ToString();

        // Assert
        Assert.AreEqual("123", result);
    }

    [Test]
    public void Equals_Int_ReturnsCorrectResult()
    {
        // Arrange
        intVariable.SetValue(100);

        // Act & Assert
        Assert.IsTrue(intVariable.Equals(100));
        Assert.IsFalse(intVariable.Equals(99));
    }
}