using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using Events;
using Assets.Scripts.NoobyGames.Events.PropertyDrawer;

[TestFixture]
public class IntGameEventPropertyDrawerTests
{
    private IntGameEventPropertyDrawer drawer;
    private IntGameEvent intGameEvent;

    [SetUp]
    public void SetUp()
    {
        drawer = new IntGameEventPropertyDrawer();
        intGameEvent = ScriptableObject.CreateInstance<IntGameEvent>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(intGameEvent);
    }

    [Test]
    public void GetPropertyHeight_ReturnsIncreasedHeight()
    {
        // Arrange
        SerializedProperty property = null; // Hard to mock
        GUIContent label = new GUIContent("Test");

        // Act
        float height = drawer.GetPropertyHeight(property, label);

        // Assert
        Assert.AreEqual(80 + EditorGUIUtility.singleLineHeight, height);
    }

    [Test]
    public void InstanceId_CanBeSet()
    {
        // Arrange
        int testId = 42;

        // Act
        drawer.instanceId = testId;

        // Assert
        Assert.AreEqual(testId, drawer.instanceId);
    }
}