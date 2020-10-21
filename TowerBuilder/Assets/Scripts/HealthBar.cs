using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;
    private Transform barTransform;

    private void Awake()
    {
        barTransform = transform.Find("bar");
    }

    private void Start()
    {
        healthSystem.OnDamageTaken += HealthSystem_OnDamageTaken;
        healthSystem.OnRepair += HealthSystem_OnRepair;
        UpdateBar();
        UpdateBarVisibility();
    }

    private void HealthSystem_OnRepair(object sender, System.EventArgs e)
    {
        UpdateBar();
        UpdateBarVisibility();
    }

    private void HealthSystem_OnDamageTaken(object sender, System.EventArgs e)
    {
        UpdateBar();
        UpdateBarVisibility();
    }

    private void UpdateBar()
    {
        barTransform.localScale = new Vector3(healthSystem.GetHPNormalized(), 1, 1);
    }

    private void UpdateBarVisibility()
    {
        if (healthSystem.IsFullHP())
        {
            gameObject.SetActive(false);
        } else
        {
            gameObject.SetActive(true);
        }
    }
}
