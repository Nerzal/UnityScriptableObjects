using NUnit.Framework;
using UnityEngine;
using Sets;
using System.Collections.Generic;

[TestFixture]
public class ObservableDictionaryTests
{
    private ObservableDictionary<string, int> dictionary;

    [SetUp]
    public void SetUp()
    {
        dictionary = ScriptableObject.CreateInstance<ObservableDictionary<string, int>>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(dictionary);
    }

    [Test]
    public void Add_ItemAdded_InvokesEvent()
    {
        string addedKey = null;
        int addedValue = 0;
        dictionary.itemAdded += (key, value) => {
            addedKey = key;
            addedValue = value;
        };

        dictionary.Add("test", 42);

        Assert.AreEqual("test", addedKey);
        Assert.AreEqual(42, addedValue);
        Assert.AreEqual(1, dictionary.Count);
    }

    [Test]
    public void Remove_ItemRemoved_InvokesEvent()
    {
        dictionary.Add("test", 42);

        string removedKey = null;
        int removedValue = 0;
        dictionary.itemRemoved += (key, value) => {
            removedKey = key;
            removedValue = value;
        };

        bool result = dictionary.Remove("test");

        Assert.IsTrue(result);
        Assert.AreEqual("test", removedKey);
        Assert.AreEqual(42, removedValue);
        Assert.AreEqual(0, dictionary.Count);
    }

    [Test]
    public void Indexer_Get_ReturnsCorrectValue()
    {
        dictionary.Add("key1", 100);
        dictionary.Add("key2", 200);

        Assert.AreEqual(100, dictionary["key1"]);
        Assert.AreEqual(200, dictionary["key2"]);
    }

    [Test]
    public void Indexer_Set_ExistingKey_InvokesModifiedEvent()
    {
        dictionary.Add("key", 100);

        string modifiedKey = null;
        int modifiedValue = 0;
        dictionary.itemModified += (key, value) => {
            modifiedKey = key;
            modifiedValue = value;
        };

        dictionary["key"] = 200;

        Assert.AreEqual("key", modifiedKey);
        Assert.AreEqual(200, modifiedValue);
        Assert.AreEqual(200, dictionary["key"]);
    }

    [Test]
    public void Indexer_Set_NewKey_AddsItem()
    {
        string addedKey = null;
        int addedValue = 0;
        dictionary.itemAdded += (key, value) => {
            addedKey = key;
            addedValue = value;
        };

        dictionary["newKey"] = 300;

        Assert.AreEqual("newKey", addedKey);
        Assert.AreEqual(300, addedValue);
        Assert.AreEqual(300, dictionary["newKey"]);
    }

    [Test]
    public void Clear_InvokesClearedEvent()
    {
        dictionary.Add("key1", 1);
        dictionary.Add("key2", 2);

        bool cleared = false;
        dictionary.dictionaryCleared += () => cleared = true;

        dictionary.Clear();

        Assert.IsTrue(cleared);
        Assert.AreEqual(0, dictionary.Count);
    }

    [Test]
    public void ContainsKey_ReturnsCorrectResult()
    {
        dictionary.Add("existing", 1);

        Assert.IsTrue(dictionary.ContainsKey("existing"));
        Assert.IsFalse(dictionary.ContainsKey("nonexistent"));
    }

    [Test]
    public void TryGetValue_ReturnsCorrectResult()
    {
        dictionary.Add("key", 42);

        Assert.IsTrue(dictionary.TryGetValue("key", out int value));
        Assert.AreEqual(42, value);

        Assert.IsFalse(dictionary.TryGetValue("missing", out int missingValue));
        Assert.AreEqual(0, missingValue); // default for int
    }

    [Test]
    public void GetEnumerator_IteratesCorrectly()
    {
        dictionary.Add("a", 1);
        dictionary.Add("b", 2);
        dictionary.Add("c", 3);

        var items = new System.Collections.Generic.List<KeyValuePair<string, int>>();
        foreach (var kvp in dictionary) {
            items.Add(kvp);
        }

        Assert.AreEqual(3, items.Count);
        Assert.IsTrue(items.Exists(kvp => kvp.Key == "a" && kvp.Value == 1));
        Assert.IsTrue(items.Exists(kvp => kvp.Key == "b" && kvp.Value == 2));
        Assert.IsTrue(items.Exists(kvp => kvp.Key == "c" && kvp.Value == 3));
    }
}
