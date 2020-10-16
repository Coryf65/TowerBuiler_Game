using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceGeneratorOverlay : MonoBehaviour
{
    [SerializeField] private ResourceGenerator resourceGenerator;
    private Transform barTransform;

    // get the resource type
    private void Start()
    {
        ResourceGeneratorData resourceGeneratorData = resourceGenerator.GetResourceGeneratorData();
        barTransform = transform.Find("bar");

        //finding our sprite, everything has a transform, and setting to the correct icon
        transform.Find("icon").GetComponent<SpriteRenderer>().sprite = resourceGeneratorData.resourceType.sprite;        

        // setting the text Resource per second
        transform.Find("text").GetComponent<TextMeshPro>().SetText(resourceGenerator.GetResourceGeneratedPerSecond().ToString("F1"));
    }

    private void Update()
    {
        // now setting the bar
        barTransform.localScale = new Vector3(1 - resourceGenerator.GetTimerNormalized(), 0.5f, 0.5f);
    }
}
