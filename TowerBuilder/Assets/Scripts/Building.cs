using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    HealthSystem healthSystem;
    BuildingTypeSO buildingType;
    private Transform buildingDemolishButton;
    private Transform buildingRepairButton;

    private void Awake()
    {
        buildingDemolishButton = transform.Find("pf_BuildingDemolishBtn");
        buildingRepairButton = transform.Find("pf_RepairButton");
        HideBuildingDemolishBtn();
        HideBuildingRepairBtn();
    }

    // Start is called before the first frame update
    void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        buildingType = GetComponent<BuildingTypeHolder>().buildingType;

        healthSystem.SetHPMax(buildingType.maxHP, updateHPAmount:true);

        healthSystem.OnDamageTaken += HealthSystem_OnDamageTaken;
        healthSystem.OnRepair += HealthSystem_OnRepair;
        healthSystem.OnDestroyed += HealthSystem_OnDestroyed;
    }

    private void HealthSystem_OnRepair(object sender, System.EventArgs e)
    {
        if (healthSystem.IsFullHP())
        {
            HideBuildingRepairBtn();
        }
    }

    private void HealthSystem_OnDamageTaken(object sender, System.EventArgs e)
    {
        ShowBuildingRepairBtn();
        SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingDamaged);
        ScreenShake.Instance.ShakeCamera();
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
        Instantiate(Resources.Load<Transform>("pfBuildingDestroyedParticles"), transform.position, Quaternion.identity);
        Destroy(gameObject);
        SoundManager.Instance.PlaySound(SoundManager.Sound.BuildingDestroyed);
        ScreenShake.Instance.ShakeCamera(6f, .12f);
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

    private void ShowBuildingRepairBtn()
    {
        if (buildingRepairButton != null)
        {
            buildingRepairButton.gameObject.SetActive(true);
        }
    }

    private void HideBuildingRepairBtn()
    {
        if (buildingRepairButton != null)
        {
            buildingRepairButton.gameObject.SetActive(false);
        }
    }
}
