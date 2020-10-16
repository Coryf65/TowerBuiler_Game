using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResourceGeneratorData
{
    public float timerMax;
    public ResourceTypeSO resourceType;
    // radius of detection zone of nodes
    public float maxDetectionRadius;
    // Cannot gether more than this number of nodes
    public int maxResourceAmount;
}
