using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;
    private Transform barTransform;
    private Transform seperatorContainer;
    private Transform seperatorTemplate;
    float barSize;

    private void Awake()
    {
        barTransform = transform.Find("bar");
        seperatorContainer = transform.Find("seperatorContainer");
        seperatorTemplate = seperatorContainer.Find("seperatorTemplate");
        barSize = transform.Find("bar").Find("barSprite").localScale.x;
    }

    private void Start()
    {
        CreateHealthBarSeperator();
        
        healthSystem.OnDamageTaken += HealthSystem_OnDamageTaken;
        healthSystem.OnRepair += HealthSystem_OnRepair;
        healthSystem.OnMaxHPChanged += HealthSystem_OnMaxHPChanged;

        UpdateBar();
        UpdateBarVisibility();
    }

    private void HealthSystem_OnMaxHPChanged(object sender, System.EventArgs e)
    {
        CreateHealthBarSeperator();
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

    /// <summary>
    ///  Segment the Health bar into chunks of 10's
    /// </summary>
    private void CreateHealthBarSeperator()
    {
        seperatorTemplate.gameObject.SetActive(false);

        // can duplicate the code so here we do a cleanup of the segments were already created
        foreach (Transform sepratorTransform in seperatorContainer)
        {
            // Dont destroy our template obj
            if (sepratorTransform == seperatorTemplate) continue;
            Destroy(sepratorTransform.gameObject);
        }

        // size of one unit on the HP
        int healthPerSeperator = 10;
        float barOneHPAmountSize = barSize / healthSystem.GetMaxHP();
        int seperatorCount = Mathf.FloorToInt(healthSystem.GetMaxHP() / healthPerSeperator);

        for (int i = 1; i < seperatorCount; i++)
        {
            Transform speratorTransform = Instantiate(seperatorTemplate, seperatorContainer);
            seperatorTemplate.gameObject.SetActive(true);
            seperatorTemplate.localPosition = new Vector3(barOneHPAmountSize * i * healthPerSeperator, 0, 0);
        }
    }
}
