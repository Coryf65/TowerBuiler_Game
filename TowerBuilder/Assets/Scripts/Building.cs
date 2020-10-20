using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    HealthSystem healthSystem;
    BuildingTypeSO buildingType;
    private Transform buildingDemolishButton;

    private void Awake()
    {
        buildingDemolishButton = transform.Find("pf_BuildingDemolishBtn");
        HideBuildingDemolishBtn();
    }

    // Start is called before the first frame update
    void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        buildingType = GetComponent<BuildingTypeHolder>().buildingType;

        healthSystem.SetHPMax(buildingType.maxHP, updateHPAmount:true);
        healthSystem.OnDestroyed += HealthSystem_OnDestroyed;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            healthSystem.TakeDamage(50);
        }
    }

    private void HealthSystem_OnDestroyed(object sender, System.EventArgs e)
    {
        Destroy(gameObject);
    }

    /// <summary>
    ///  Handling the Demolish button using our collider
    /// </summary>
    private void OnMouseEnter()
    {
        ShowBuildingDemolishBtn();
    }

    /// <summary>
    /// Handling the Demolish button using our collider
    /// </summary>
    private void OnMouseExit()
    {
        HideBuildingDemolishBtn();
    }

    private void ShowBuildingDemolishBtn()
    {
        if (buildingDemolishButton != null)
        {
            buildingDemolishButton.gameObject.SetActive(true);
        }
    }

    private void HideBuildingDemolishBtn()
    {
        if (buildingDemolishButton != null)
        {
            buildingDemolishButton.gameObject.SetActive(false);
        }
    }
}
