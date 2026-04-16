using NUnit.Framework;
using UnityEngine;
using Variables;

[TestFixture]
public class Vector2VariableTests
{
    private Vector2Variable vector2Variable;

    [SetUp]
    public void SetUp()
    {
        vector2Variable = ScriptableObject.CreateInstance<Vector2Variable>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(vector2Variable);
    }

    [Test]
    public void SetValue_ChangesValueAndInvokesEvents()
    {
        // Arrange
        bool valueChangedInvoked = false;
        bool xChangedInvoked = false;
        bool yChangedInvoked = false;
        Vector2 newValue = new Vector2(1.0f, 2.0f);
        vector2Variable.valueChanged += (val) => valueChangedInvoked = true;
        vector2Variable.xChanged += (val) => xChangedInvoked = true;
        vector2Variable.yChanged += (val) => yChangedInvoked = true;

        // Act
        vector2Variable.SetValue(newValue);

        // Assert
        Assert.AreEqual(newValue, vector2Variable.value);
        Assert.IsTrue(valueChangedInvoked);
        Assert.IsTrue(xChangedInvoked);
        Assert.IsTrue(yChangedInvoked);
    }

    [Test]
    public void SetValue_SameValue_DoesNotInvokeEvents()
    {
        // Arrange
        Vector2 initialValue = new Vector2(1.0f, 2.0f);
        vector2Variable.SetValue(initialValue);
        bool valueChangedInvoked = false;
        bool xChangedInvoked = false;
        bool yChangedInvoked = false;
        vector2Variable.valueChanged += (val) => valueChangedInvoked = true;
        vector2Variable.xChanged += (val) => xChangedInvoked = true;
        vector2Variable.yChanged += (val) => yChangedInvoked = true;

        // Act
        vector2Variable.SetValue(initialValue);

        // Assert
        Assert.AreEqual(initialValue, vector2Variable.value);
        Assert.IsFalse(valueChangedInvoked);
        Assert.IsFalse(xChangedInvoked);
        Assert.IsFalse(yChangedInvoked);
    }

    [Test]
    public void SetValue_OnlyXChanges_InvokesXEvent()
    {
        // Arrange
        vector2Variable.SetValue(new Vector2(1.0f, 2.0f));
        bool xChangedInvoked = false;
        vector2Variable.xChanged += (val) => xChangedInvoked = true;

        // Act
        vector2Variable.SetValue(new Vector2(3.0f, 2.0f));

        // Assert
        Assert.IsTrue(xChangedInvoked);
    }

    [Test]
    public void Equals_Vector2_ReturnsCorrectResult()
    {
        // Arrange
        vector2Variable.SetValue(new Vector2(1.0f, 2.0f));

        // Act & Assert
        Assert.IsTrue(vector2Variable.Equals(new Vector2(1.0f, 2.0f)));
        Assert.IsFalse(vector2Variable.Equals(new Vector2(3.0f, 4.0f)));
    }

    [Test]
    public void ToString_ReturnsValueAsString()
    {
        // Arrange
        vector2Variable.SetValue(new Vector2(1.5f, 2.5f));

        // Act
        string result = vector2Variable.ToString();

        // Assert
        Assert.AreEqual("(1.5, 2.5)", result);
    }
}