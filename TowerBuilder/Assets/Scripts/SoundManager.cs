using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance { get; private set; }

    // List of all our sounds in Resources folder
    // Could have done a List<>
    public enum Sound
    {
        BuildingPlaced,
        BuildingDamaged,
        BuildingDestroyed,
        EnemyDie,
        EnemyHit,
        GameOver,
    }

    private AudioSource audioSource;
    private Dictionary<Sound, AudioClip> soundClipDictionary;
    private float volume = .5f; // 50%


    private void Awake()
    {
        Instance = this;

        audioSource = GetComponent<AudioSource>();

        soundClipDictionary = new Dictionary<Sound, AudioClip>();

        foreach (Sound sound in System.Enum.GetValues(typeof(Sound)))
        {
            soundClipDictionary[sound] = Resources.Load<AudioClip>(sound.ToString());
        }
    }

    public void PlaySound(Sound sound)
    {
        audioSource.PlayOneShot(soundClipDictionary[sound], volume);
    }

    public void IncreaseVolume()
    {
        volume += .1f; // 10%
        volume = Mathf.Clamp01(volume); // clamps between 0 and 1
    }

    public void DecreaseVolume()
    {
        volume -= .1f; // 10%
        volume = Mathf.Clamp01(volume); // clamps between 0 and 1
    }

    public float GetCurrentVolume()
    {
        return volume;
    }
}
