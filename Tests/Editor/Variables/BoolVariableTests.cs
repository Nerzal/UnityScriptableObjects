using NUnit.Framework;
using UnityEngine;
using Variables;

[TestFixture]
public class BoolVariableTests
{
    private BoolVariable boolVariable;

    [SetUp]
    public void SetUp()
    {
        boolVariable = ScriptableObject.CreateInstance<BoolVariable>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(boolVariable);
    }

    [Test]
    public void SetValue_ChangesValueAndInvokesEvent()
    {
        // Arrange
        bool eventInvoked = false;
        bool newValue = true;
        boolVariable.valueChanged += (val) => eventInvoked = true;

        // Act
        boolVariable.SetValue(newValue);

        // Assert
        Assert.AreEqual(newValue, boolVariable.value);
        Assert.IsTrue(eventInvoked);
    }

    [Test]
    public void SetValue_SameValue_DoesNotInvokeEvent()
    {
        // Arrange
        bool initialValue = false;
        boolVariable.SetValue(initialValue);
        bool eventInvoked = false;
        boolVariable.valueChanged += (val) => eventInvoked = true;

        // Act
        boolVariable.SetValue(initialValue);

        // Assert
        Assert.AreEqual(initialValue, boolVariable.value);
        Assert.IsFalse(eventInvoked);
    }

    [Test]
    public void Equals_Bool_ReturnsCorrectResult()
    {
        // Arrange
        boolVariable.SetValue(true);

        // Act & Assert
        Assert.IsTrue(boolVariable.Equals(true));
        Assert.IsFalse(boolVariable.Equals(false));
    }

    [Test]
    public void ToString_ReturnsValueAsString()
    {
        // Arrange
        boolVariable.SetValue(true);

        // Act
        string result = boolVariable.ToString();

        // Assert
        Assert.AreEqual("True", result); // Note: bool.ToString() returns "True" or "False"
    }
}