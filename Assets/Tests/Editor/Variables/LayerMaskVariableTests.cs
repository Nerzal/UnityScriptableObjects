using NUnit.Framework;
using UnityEngine;
using Variables;

[TestFixture]
public class LayerMaskVariableTests
{
    private LayerMaskVariable layerMaskVariable;

    [SetUp]
    public void SetUp()
    {
        layerMaskVariable = ScriptableObject.CreateInstance<LayerMaskVariable>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(layerMaskVariable);
    }

    [Test]
    public void SetValue_WhenChanged_InvokesEvent()
    {
        bool invoked = false;
        layerMaskVariable.valueChanged += value => invoked = true;

        var mask = 1 << 3;
        layerMaskVariable.SetValue((LayerMask)mask);

        Assert.IsTrue(invoked);
        Assert.AreEqual((LayerMask)mask, layerMaskVariable.value);
    }
}
