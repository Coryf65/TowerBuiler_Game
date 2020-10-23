using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ChromaticAbberationEffect : MonoBehaviour
{
    private Volume volume;

    public static ChromaticAbberationEffect Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        volume = GetComponent<Volume>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (volume.weight > 0)
        {
            float decreaseSpeed = 1f;
            volume.weight -= Time.deltaTime * decreaseSpeed;
        }
    }

    public void SetWeight(float weight)
    {
        volume.weight = weight;
    }
}
