using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class ScreenShake : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin cinemachinePerlinNoise;
    private float timer;
    private float timerMax;
    private float startingIntensity;

    public static ScreenShake Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        cinemachinePerlinNoise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Update()
    {
        if (timer < timerMax)
        {
            timer += Time.deltaTime;
            float amplitude = Mathf.Lerp(startingIntensity, 0f, timer / timerMax); // goes until it hits Zero
            cinemachinePerlinNoise.m_AmplitudeGain = amplitude;
        }
    }

    public void ShakeCamera(float intensity = 5f, float timerMax = .1f)
    {       
        this.timerMax = timerMax;
        timer = 0f;
        startingIntensity = intensity;
        cinemachinePerlinNoise.m_AmplitudeGain = intensity;
    }


}
