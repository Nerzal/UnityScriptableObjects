using NUnit.Framework;
using UnityEngine;
using Variables;

[TestFixture]
public class SpriteVariableTests
{
    private SpriteVariable spriteVariable;

    [SetUp]
    public void SetUp()
    {
        spriteVariable = ScriptableObject.CreateInstance<SpriteVariable>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(spriteVariable);
    }

    [Test]
    public void SetValue_ChangesValueAndInvokesEvent()
    {
        // Arrange
        bool eventInvoked = false;
        Sprite newValue = Sprite.Create(Texture2D.whiteTexture, new Rect(0, 0, 1, 1), Vector2.zero);
        spriteVariable.valueChanged += (val) => eventInvoked = true;

        // Act
        spriteVariable.SetValue(newValue);

        // Assert
        Assert.AreEqual(newValue, spriteVariable.value);
        Assert.IsTrue(eventInvoked);

        // Cleanup
        Object.DestroyImmediate(newValue.texture);
        Object.DestroyImmediate(newValue);
    }

    [Test]
    public void SetValue_SameValue_DoesNotInvokeEvent()
    {
        // Arrange
        Sprite initialValue = Sprite.Create(Texture2D.whiteTexture, new Rect(0, 0, 1, 1), Vector2.zero);
        spriteVariable.SetValue(initialValue);
        bool eventInvoked = false;
        spriteVariable.valueChanged += (val) => eventInvoked = true;

        // Act
        spriteVariable.SetValue(initialValue);

        // Assert
        Assert.AreEqual(initialValue, spriteVariable.value);
        Assert.IsFalse(eventInvoked);

        // Cleanup
        Object.DestroyImmediate(initialValue.texture);
        Object.DestroyImmediate(initialValue);
    }

    [Test]
    public void Equals_Sprite_ReturnsCorrectResult()
    {
        // Arrange
        Sprite sprite1 = Sprite.Create(Texture2D.whiteTexture, new Rect(0, 0, 1, 1), Vector2.zero);
        Sprite sprite2 = Sprite.Create(Texture2D.whiteTexture, new Rect(0, 0, 1, 1), Vector2.zero);
        spriteVariable.SetValue(sprite1);

        // Act & Assert
        Assert.IsTrue(spriteVariable.Equals(sprite1));
        Assert.IsFalse(spriteVariable.Equals(sprite2));

        // Cleanup
        Object.DestroyImmediate(sprite1.texture);
        Object.DestroyImmediate(sprite1);
        Object.DestroyImmediate(sprite2.texture);
        Object.DestroyImmediate(sprite2);
    }
}