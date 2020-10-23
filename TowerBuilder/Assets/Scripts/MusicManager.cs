using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource audioSource;
    private float volume = .5f; // 50%

    public void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        volume = PlayerPrefs.GetFloat("musicVolume", .5f); // get players music levels
        audioSource.volume = volume;
    }

    public void IncreaseVolume()
    {
        volume += .1f; // 10%
        volume = Mathf.Clamp01(volume); // clamps between 0 and 1
        audioSource.volume = volume;
        PlayerPrefs.SetFloat("musicVolume", volume); // saving players music levels
    }

    public void DecreaseVolume()
    {
        volume -= .1f; // 10%
        volume = Mathf.Clamp01(volume); // clamps between 0 and 1
        audioSource.volume = volume;
        PlayerPrefs.SetFloat("musicVolume", volume); // saving players music levels
    }

    public float GetCurrentVolume()
    {
        return volume;
    }
}
