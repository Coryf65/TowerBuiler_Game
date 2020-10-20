using UnityEngine;
using UnityEngine.UI;

public class BuildingDemolishBtn : MonoBehaviour
{

    [SerializeField] private Building building;

    private void Awake()
    {
        // adding a destroy to our button
        transform.Find("Button").GetComponent<Button>().onClick.AddListener(() => {
            // when destroying we can get some resources back
            BuildingTypeSO buildingType = building.GetComponent<BuildingTypeHolder>().buildingType;
            foreach (ResourceAmount resourceAmount in buildingType.ConstructionResourceCost)
            {
                ResourceManager.TheResourceManager.AddResource(resourceAmount.resourceType, Mathf.FloorToInt(resourceAmount.amount * .6f)); // recoup 60%
            }

            // Remove the building
            Destroy(building.gameObject);
        });
    }
}
