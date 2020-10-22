using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourcesUI : MonoBehaviour
{

    // the other approach by selecting in the engine
    //[SerializeField] private Transform resourceTemplate;
    private ResourceTypeListSO resourceList;
    private Dictionary<ResourceTypeSO, Transform> resourceTypeTransformDictionary;
    private float offestAmount = -100f;

    private void Awake()
    {
        resourceList = Resources.Load<ResourceTypeListSO>(typeof(ResourceTypeListSO).Name);

        resourceTypeTransformDictionary = new Dictionary<ResourceTypeSO, Transform>();

        Transform resourceTemplate = transform.Find("resourceTemplate"); // one approach to get the ui Gameobject, find by name string
        // want to hide this reference
        resourceTemplate.gameObject.SetActive(false);
        
        //int index = 0;

        foreach(ResourceTypeSO resourceType in resourceList.list)
        {
            Transform resourceTransform = Instantiate(resourceTemplate, transform);
            resourceTransform.gameObject.SetActive(true);
            resourceTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offestAmount, 0);

            // setting the image
            resourceTransform.Find("image").GetComponent<Image>().sprite = resourceType.sprite;

            resourceTypeTransformDictionary[resourceType] = resourceTransform;
            
            // need to position it now
            offestAmount -= 150f;
            //index++;
        }
    }

    private void Start()
    {
        ResourceManager.TheResourceManager.OnResourceAmountChanged += ResourceManager_OnResourceAmountChanged; ;
        UpdateResourceAmounts();
    }

    // The Event Listener
    private void ResourceManager_OnResourceAmountChanged(object sender, System.EventArgs e)
    {
        UpdateResourceAmounts();
    }

    // going to use events to update our UI resource counters

    private void UpdateResourceAmounts()
    {
        foreach (ResourceTypeSO resourceType in resourceList.list)
        {
            Transform resourceTransform = resourceTypeTransformDictionary[resourceType];
            
            // need to get how much
            int resourceAmount = ResourceManager.TheResourceManager.GetResourceAmount(resourceType);
            //setting resource amount
            resourceTransform.Find("text").GetComponent<TextMeshProUGUI>().SetText(resourceAmount.ToString());
        }
    }
}
