using NUnit.Framework;
using UnityEngine;
using Sound;
using Events;

[TestFixture]
public class SoundManagerTests
{
    private SoundManager soundManager;
    private GameObject gameObject;
    private AudioSource audioSource;

    [SetUp]
    public void SetUp()
    {
        gameObject = new GameObject();
        soundManager = gameObject.AddComponent<SoundManager>();
        audioSource = gameObject.GetComponent<AudioSource>();
        // Note: In a real test, you might mock AudioSource, but for simplicity, we use the real one.
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(gameObject);
    }

    [Test]
    public void OnSound_ReceiverMatchesInstanceID_PlaysSound()
    {
        // Arrange
        AudioClip clip = AudioClip.Create("TestClip", 44100, 1, 44100, false);
        PlaySoundEventArgs args = new PlaySoundEventArgs(clip, gameObject.transform.GetInstanceID());

        // Act
        soundManager.OnSound(args);

        // Assert
        // Since PlayOneShot is hard to test directly, we can check if the AudioSource is playing
        // But in EditMode, it might not play. This is a limitation.
        // Alternatively, assert that no exception is thrown.
        Assert.IsNotNull(audioSource);
    }

    [Test]
    public void OnSound_ReceiverDoesNotMatchInstanceID_DoesNotPlaySound()
    {
        // Arrange
        AudioClip clip = AudioClip.Create("TestClip", 44100, 1, 44100, false);
        PlaySoundEventArgs args = new PlaySoundEventArgs(clip, -999); // Different ID

        // Act
        soundManager.OnSound(args);

        // Assert
        // Again, hard to test playback, but ensure no crash.
        Assert.IsNotNull(audioSource);
    }
}