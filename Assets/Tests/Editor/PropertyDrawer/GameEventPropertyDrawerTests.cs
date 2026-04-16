using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using Events;
using Assets.Scripts.NoobyGames.Events.PropertyDrawer;

[TestFixture]
public class GameEventPropertyDrawerTests
{
    private GameEventPropertyDrawer drawer;
    private GameEvent gameEvent;

    [SetUp]
    public void SetUp()
    {
        drawer = new GameEventPropertyDrawer();
        gameEvent = ScriptableObject.CreateInstance<GameEvent>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(gameEvent);
    }

    [Test]
    public void GetPropertyHeight_ReturnsIncreasedHeight()
    {
        // Arrange
        SerializedProperty property = null; // Hard to mock, but for height test
        GUIContent label = new GUIContent("Test");

        // Act
        float height = drawer.GetPropertyHeight(property, label);

        // Assert
        Assert.AreEqual(80 + EditorGUIUtility.singleLineHeight, height); // base height + 80
    }

    // Note: OnGUI testing requires EditorWindow context.
    // For full GUI testing, use Unity Test Framework with [UnityTest] and EditorWindow.
    // Example (not implemented due to complexity):
    // [UnityTest]
    // public IEnumerator OnGUI_RaiseEventButton_CallsRaise()
    // {
    //     // Create EditorWindow, draw property, simulate button click
    //     yield return null;
    // }
}