using NUnit.Framework;
using UnityEngine;
using Variables;

[TestFixture]
public class AnimationCurveVariableTests
{
    private AnimationCurveVariable animationCurveVariable;

    [SetUp]
    public void SetUp()
    {
        animationCurveVariable = ScriptableObject.CreateInstance<AnimationCurveVariable>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(animationCurveVariable);
    }

    [Test]
    public void SetValue_WhenChanged_InvokesEvent()
    {
        bool invoked = false;
        animationCurveVariable.valueChanged += value => invoked = true;

        var curve = AnimationCurve.Linear(0, 0, 1, 1);
        animationCurveVariable.SetValue(curve);

        Assert.IsTrue(invoked);
        Assert.AreEqual(curve, animationCurveVariable.value);
    }
}
