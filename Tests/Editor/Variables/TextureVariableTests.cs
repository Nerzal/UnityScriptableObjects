using NUnit.Framework;
using UnityEngine;
using Variables;

[TestFixture]
public class TextureVariableTests
{
    private TextureVariable textureVariable;

    [SetUp]
    public void SetUp()
    {
        textureVariable = ScriptableObject.CreateInstance<TextureVariable>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(textureVariable);
    }

    [Test]
    public void SetValue_ChangesValueAndInvokesEvent()
    {
        // Arrange
        bool eventInvoked = false;
        Texture newValue = Texture2D.whiteTexture;
        textureVariable.valueChanged += (val) => eventInvoked = true;

        // Act
        textureVariable.SetValue(newValue);

        // Assert
        Assert.AreEqual(newValue, textureVariable.value);
        Assert.IsTrue(eventInvoked);
    }

    [Test]
    public void SetValue_SameValue_DoesNotInvokeEvent()
    {
        // Arrange
        Texture initialValue = Texture2D.whiteTexture;
        textureVariable.SetValue(initialValue);
        bool eventInvoked = false;
        textureVariable.valueChanged += (val) => eventInvoked = true;

        // Act
        textureVariable.SetValue(initialValue);

        // Assert
        Assert.AreEqual(initialValue, textureVariable.value);
        Assert.IsFalse(eventInvoked);
    }

    [Test]
    public void Equals_Texture_ReturnsCorrectResult()
    {
        // Arrange
        Texture texture1 = Texture2D.whiteTexture;
        Texture texture2 = new Texture2D(1, 1);
        textureVariable.SetValue(texture1);

        // Act & Assert
        Assert.IsTrue(textureVariable.Equals(texture1));
        Assert.IsFalse(textureVariable.Equals(texture2));

        // Cleanup
        Object.DestroyImmediate(texture2);
    }
}