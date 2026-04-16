using NUnit.Framework;
using UnityEngine;
using Variables;

[TestFixture]
public class Vector4VariableTests
{
    private Vector4Variable vector4Variable;

    [SetUp]
    public void SetUp()
    {
        vector4Variable = ScriptableObject.CreateInstance<Vector4Variable>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(vector4Variable);
    }

    [Test]
    public void SetValue_VectorChanged_InvokesEvent()
    {
        bool invoked = false;
        vector4Variable.valueChanged += value => invoked = true;

        vector4Variable.SetValue(new Vector4(1f, 2f, 3f, 4f));

        Assert.IsTrue(invoked);
        Assert.AreEqual(new Vector4(1f, 2f, 3f, 4f), vector4Variable.value);
    }

    [Test]
    public void Equals_SameVector_ReturnsTrue()
    {
        var target = new Vector4(0.5f, 0.5f, 0.5f, 0.5f);
        vector4Variable.SetValue(target);

        Assert.IsTrue(vector4Variable.Equals(target));
    }
}
