using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    // singleton property, can get public but only set by this class
    public static ResourceManager TheResourceManager { get; private set; }

    // creating a custom event, needs to have a listener 
    public event EventHandler OnResourceAmountChanged;
    private Dictionary<ResourceTypeSO, int> resourceAmountDictionary;

    [SerializeField] private List<ResourceAmount> startingResources;

    // Awake runs before start and one time, for initializtion that does not depend on other game obkect use awake
    private void Awake()
    {
        TheResourceManager = this;

        resourceAmountDictionary = new Dictionary<ResourceTypeSO, int>();

        ResourceTypeListSO resourceList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);

        foreach (ResourceTypeSO resource in resourceList.list)
        {
            // Init all to 0
            resourceAmountDictionary[resource] = 0;
        }

        foreach (ResourceAmount resourceAmount in startingResources)
        {
            // for setting starting resources
            AddResource(resourceAmount.resourceType, resourceAmount.amount);
        }

        //TestLogResourceAmountDicitionary();
    }

    //testing our add resource function
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ResourceTypeListSO resourceList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);
            AddResource(resourceList.list[0], 2);
            //TestLogResourceAmountDicitionary();
        }      
    }

    public int GetResourceAmount(ResourceTypeSO resourceType)
    {
        return resourceAmountDictionary[resourceType];
    }


    // testing our dictionary
    private void TestLogResourceAmountDicitionary()
    {
        foreach (ResourceTypeSO resourceType in resourceAmountDictionary.Keys)
        {
            Debug.Log(resourceType.nameString + ": " + resourceAmountDictionary[resourceType]);
        }
    }

    public void AddResource(ResourceTypeSO resourceType, int amount)
    {
        resourceAmountDictionary[resourceType] += amount;
        // calling using a Null conditional Operator, similar to example#1 
        OnResourceAmountChanged?.Invoke(this, EventArgs.Empty);
        // Example #1
        //if (OnResourceAmountChanged != null)
        //{
        //    OnResourceAmountChanged(this, EventArgs.Empty);
        //}
        //TestLogResourceAmountDicitionary();
    }

    public bool CanAfford(ResourceAmount[] resourceAmounts)
    {
        foreach (ResourceAmount resourceAmount in resourceAmounts)
        {
            if (GetResourceAmount(resourceAmount.resourceType) >= resourceAmount.amount)
            {
                // Can afford
            } else
            {
                // Cannot Afford
                return false;
            }
        }

        // Can afford
        return true;
    }

    /// <summary>
    ///  Spend our Resource that we need for a given Building
    /// </summary>
    /// <param name="resourceAmounts"></param>
    public void SpendResources(ResourceAmount[] resourceAmounts)
    {
        foreach (ResourceAmount resourceAmount in resourceAmounts)
        {
            resourceAmountDictionary[resourceAmount.resourceType] -= resourceAmount.amount;
        }
           
    }
}
