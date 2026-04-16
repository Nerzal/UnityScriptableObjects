using NUnit.Framework;
using UnityEngine;
using Variables;

[TestFixture]
public class Vector3VariableTests
{
    private Vector3Variable vector3Variable;

    [SetUp]
    public void SetUp()
    {
        vector3Variable = ScriptableObject.CreateInstance<Vector3Variable>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(vector3Variable);
    }

    [Test]
    public void SetValue_ChangesValueAndInvokesEvents()
    {
        // Arrange
        bool valueChangedInvoked = false;
        bool xChangedInvoked = false;
        bool yChangedInvoked = false;
        bool zChangedInvoked = false;
        Vector3 newValue = new Vector3(1.0f, 2.0f, 3.0f);
        vector3Variable.valueChanged += (val) => valueChangedInvoked = true;
        vector3Variable.xChanged += (val) => xChangedInvoked = true;
        vector3Variable.yChanged += (val) => yChangedInvoked = true;
        vector3Variable.zChanged += (val) => zChangedInvoked = true;

        // Act
        vector3Variable.SetValue(newValue);

        // Assert
        Assert.AreEqual(newValue, vector3Variable.value);
        Assert.IsTrue(valueChangedInvoked);
        Assert.IsTrue(xChangedInvoked);
        Assert.IsTrue(yChangedInvoked);
        Assert.IsTrue(zChangedInvoked);
    }

    [Test]
    public void SetValue_SameValue_DoesNotInvokeEvents()
    {
        // Arrange
        Vector3 initialValue = new Vector3(1.0f, 2.0f, 3.0f);
        vector3Variable.SetValue(initialValue);
        bool valueChangedInvoked = false;
        bool xChangedInvoked = false;
        bool yChangedInvoked = false;
        bool zChangedInvoked = false;
        vector3Variable.valueChanged += (val) => valueChangedInvoked = true;
        vector3Variable.xChanged += (val) => xChangedInvoked = true;
        vector3Variable.yChanged += (val) => yChangedInvoked = true;
        vector3Variable.zChanged += (val) => zChangedInvoked = true;

        // Act
        vector3Variable.SetValue(initialValue);

        // Assert
        Assert.AreEqual(initialValue, vector3Variable.value);
        Assert.IsFalse(valueChangedInvoked);
        Assert.IsFalse(xChangedInvoked);
        Assert.IsFalse(yChangedInvoked);
        Assert.IsFalse(zChangedInvoked);
    }

    [Test]
    public void SetValue_OnlyXChanges_InvokesXEvent()
    {
        // Arrange
        vector3Variable.SetValue(new Vector3(1.0f, 2.0f, 3.0f));
        bool xChangedInvoked = false;
        vector3Variable.xChanged += (val) => xChangedInvoked = true;

        // Act
        vector3Variable.SetValue(new Vector3(4.0f, 2.0f, 3.0f));

        // Assert
        Assert.IsTrue(xChangedInvoked);
    }

    [Test]
    public void Equals_Vector3_ReturnsCorrectResult()
    {
        // Arrange
        vector3Variable.SetValue(new Vector3(1.0f, 2.0f, 3.0f));

        // Act & Assert
        Assert.IsTrue(vector3Variable.Equals(new Vector3(1.0f, 2.0f, 3.0f)));
        Assert.IsFalse(vector3Variable.Equals(new Vector3(4.0f, 5.0f, 6.0f)));
    }

    [Test]
    public void ToString_ReturnsValueAsString()
    {
        // Arrange
        vector3Variable.SetValue(new Vector3(1.5f, 2.5f, 3.5f));

        // Act
        string result = vector3Variable.ToString();

        // Assert
        Assert.AreEqual("(1.5, 2.5, 3.5)", result);
    }
}