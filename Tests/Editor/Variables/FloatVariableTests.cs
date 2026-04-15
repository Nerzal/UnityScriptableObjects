using NUnit.Framework;
using UnityEngine;
using Variables;

[TestFixture]
public class FloatVariableTests
{
    private FloatVariable floatVariable;

    [SetUp]
    public void SetUp()
    {
        floatVariable = ScriptableObject.CreateInstance<FloatVariable>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(floatVariable);
    }

    [Test]
    public void SetValue_ChangesValueAndInvokesEvent()
    {
        // Arrange
        bool eventInvoked = false;
        float newValue = 42.5f;
        floatVariable.valueChanged += (val) => eventInvoked = true;

        // Act
        floatVariable.SetValue(newValue);

        // Assert
        Assert.AreEqual(newValue, floatVariable.value);
        Assert.IsTrue(eventInvoked);
    }

    [Test]
    public void SetValue_SameValue_DoesNotInvokeEvent()
    {
        // Arrange
        float initialValue = 10.0f;
        floatVariable.SetValue(initialValue);
        bool eventInvoked = false;
        floatVariable.valueChanged += (val) => eventInvoked = true;

        // Act
        floatVariable.SetValue(initialValue);

        // Assert
        Assert.AreEqual(initialValue, floatVariable.value);
        Assert.IsFalse(eventInvoked);
    }

    [Test]
    public void ApplyChange_IncreasesValue()
    {
        // Arrange
        floatVariable.SetValue(10.0f);
        float amount = 5.5f;

        // Act
        floatVariable.ApplyChange(amount);

        // Assert
        Assert.AreEqual(15.5f, floatVariable.value);
    }

    [Test]
    public void ApplyChange_ZeroAmount_DoesNotChangeValue()
    {
        // Arrange
        floatVariable.SetValue(10.0f);

        // Act
        floatVariable.ApplyChange(0.0f);

        // Assert
        Assert.AreEqual(10.0f, floatVariable.value);
    }

    [Test]
    public void Equals_Float_ReturnsCorrectResult()
    {
        // Arrange
        floatVariable.SetValue(100.5f);

        // Act & Assert
        Assert.IsTrue(floatVariable.Equals(100.5f));
        Assert.IsFalse(floatVariable.Equals(99.5f));
        Assert.IsFalse(floatVariable.Equals(float.NaN)); // Edge case: NaN comparison
    }

    [Test]
    public void ToString_ReturnsValueAsString()
    {
        // Arrange
        floatVariable.SetValue(123.45f);

        // Act
        string result = floatVariable.ToString();

        // Assert
        Assert.AreEqual("123.45", result);
    }
}