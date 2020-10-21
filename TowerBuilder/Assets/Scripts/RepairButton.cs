using UnityEngine;
using UnityEngine.UI;

public class RepairButton : MonoBehaviour
{

    [SerializeField] private HealthSystem buildingHealthSystem;
    [SerializeField] private ResourceTypeSO goldResource;

    private void Awake()
    {
        // adding a destroy to our button
        transform.Find("Button").GetComponent<Button>().onClick.AddListener(() => {
            int missingHP = buildingHealthSystem.GetMaxHP() - buildingHealthSystem.GetCurrentHP();
            int repairCost = missingHP / 2;

            ResourceAmount[] resourceAmountCost = new ResourceAmount[] { new ResourceAmount { resourceType = goldResource, amount = repairCost } };

            if (ResourceManager.TheResourceManager.CanAfford(resourceAmountCost))
            {
                ResourceManager.TheResourceManager.SpendResources(resourceAmountCost);
                // have the resources to repair
                buildingHealthSystem.FullRepair();
            } else
            {
                // broke
                ToolTipUI.Instance.Show($"Cannot Afford: {repairCost} {goldResource.name} to repair {missingHP} HP", new ToolTipUI.ToolTipTimer { timer = 2f });
            }
        });
    }
}
