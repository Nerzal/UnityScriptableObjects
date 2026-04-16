using NUnit.Framework;
using UnityEngine;
using Variables;

[TestFixture]
public class ColorVariableTests
{
    private ColorVariable colorVariable;

    [SetUp]
    public void SetUp()
    {
        colorVariable = ScriptableObject.CreateInstance<ColorVariable>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(colorVariable);
    }

    [Test]
    public void SetValue_ColorValueChanged_InvokesEvent()
    {
        bool invoked = false;
        colorVariable.valueChanged += value => invoked = true;

        colorVariable.SetValue(Color.red);

        Assert.IsTrue(invoked);
        Assert.AreEqual(Color.red, colorVariable.value);
    }

    [Test]
    public void Equals_SameColor_ReturnsTrue()
    {
        colorVariable.SetValue(new Color(0.2f, 0.4f, 0.6f));

        Assert.IsTrue(colorVariable.Equals(new Color(0.2f, 0.4f, 0.6f)));
    }
}
