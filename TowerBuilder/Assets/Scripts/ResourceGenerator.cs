using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGenerator : MonoBehaviour
{
    // going to make a resource per every set amount of time
    private ResourceGeneratorData resourceGeneratorData;
    private float timer;
    private float timerMax;

    private void Awake()
    {
        resourceGeneratorData = GetComponent<BuildingTypeHolder>().buildingType.resourceGenData;
        timerMax = resourceGeneratorData.timerMax;
    }

    private void Start()
    {
        int nearbyResourcesCount = GetNearbyResourceAmount(resourceGeneratorData, transform.position);

        if (nearbyResourcesCount == 0)
        {
            // No nodes nearby, might manage a circle display on UI ? widget?
            enabled = false;
        } else
        {
            timerMax = (resourceGeneratorData.timerMax / 2f) + resourceGeneratorData.timerMax * (1 - (float)nearbyResourcesCount / resourceGeneratorData.maxResourceAmount);
        }
        //Debug.Log("nearbyResourcesCount: " + nearbyResourcesCount + "; timerMax: " + timerMax);
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            timer += timerMax;
            ResourceManager.TheResourceManager.AddResource(resourceGeneratorData.resourceType, 1);
        }
    }

    public ResourceGeneratorData GetResourceGeneratorData()
    {
        return resourceGeneratorData;
    }

    public float GetTimerNormalized()
    {
        return timer / timerMax;
    }

    public float GetResourceGeneratedPerSecond()
    {
        return 1 / timerMax;
    }

    public static int GetNearbyResourceAmount(ResourceGeneratorData resourceGeneratorData, Vector3 position)
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(position, resourceGeneratorData.maxDetectionRadius);

        int nearbyResourcesCount = 0;
        foreach (Collider2D collider2D in collider2Ds)
        {
            ResourceNode resourceNode = collider2D.GetComponent<ResourceNode>();

            if (resourceNode != null)
            {
                // It's a Resource Node!
                if (resourceNode.resourceType == resourceGeneratorData.resourceType)
                {
                    // Same Type, Increase!
                    nearbyResourcesCount++;
                }
            }
        }

        // clamping to not make over a max amount
        nearbyResourcesCount = Mathf.Clamp(nearbyResourcesCount, 0, resourceGeneratorData.maxResourceAmount);

        return nearbyResourcesCount;
    }

}
