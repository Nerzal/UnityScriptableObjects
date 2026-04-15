using NUnit.Framework;
using UnityEngine;
using Variables;

[TestFixture]
public class StringVariableTests
{
    private StringVariable stringVariable;

    [SetUp]
    public void SetUp()
    {
        stringVariable = ScriptableObject.CreateInstance<StringVariable>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(stringVariable);
    }

    [Test]
    public void SetValue_ChangesValueAndInvokesEvent()
    {
        // Arrange
        bool eventInvoked = false;
        string newValue = "Hello World";
        stringVariable.valueChanged += (val) => eventInvoked = true;

        // Act
        stringVariable.SetValue(newValue);

        // Assert
        Assert.AreEqual(newValue, stringVariable.value);
        Assert.IsTrue(eventInvoked);
    }

    [Test]
    public void SetValue_SameValue_DoesNotInvokeEvent()
    {
        // Arrange
        string initialValue = "Test";
        stringVariable.SetValue(initialValue);
        bool eventInvoked = false;
        stringVariable.valueChanged += (val) => eventInvoked = true;

        // Act
        stringVariable.SetValue(initialValue);

        // Assert
        Assert.AreEqual(initialValue, stringVariable.value);
        Assert.IsFalse(eventInvoked);
    }

    [Test]
    public void Equals_String_ReturnsCorrectResult()
    {
        // Arrange
        stringVariable.SetValue("TestString");

        // Act & Assert
        Assert.IsTrue(stringVariable.Equals("TestString"));
        Assert.IsFalse(stringVariable.Equals("DifferentString"));
        Assert.IsFalse(stringVariable.Equals(null)); // Edge case: null comparison
    }

    [Test]
    public void ToString_ReturnsValueAsString()
    {
        // Arrange
        stringVariable.SetValue("Sample Text");

        // Act
        string result = stringVariable.ToString();

        // Assert
        Assert.AreEqual("Sample Text", result);
    }
}