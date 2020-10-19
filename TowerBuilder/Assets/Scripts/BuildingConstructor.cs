using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingConstructor : MonoBehaviour
{

    public static BuildingConstructor Create(Vector3 position, BuildingTypeSO buildingType)
    {
        // Getting our Prefab
        Transform pf_BuildingConstruction = Resources.Load<Transform>("pf_BuildingConstruction");
        // Create an instantiate
        Transform buildingConstructionTransform = Instantiate(pf_BuildingConstruction, position, Quaternion.identity);
        // returning this referece to this object
        BuildingConstructor buildingConstruction = buildingConstructionTransform.GetComponent<BuildingConstructor>();
        buildingConstruction.SetBuildingType(buildingType);

        return buildingConstruction;
    }    

    private BuildingTypeSO buildingType;
    private float constructionTimer;
    private float constructionTimerMax;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    private BuildingTypeHolder buildingTypeHolder;
    private Material constructionMaterial;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = transform.Find("sprite").GetComponent<SpriteRenderer>();
        buildingTypeHolder = GetComponent<BuildingTypeHolder>();
        constructionMaterial = spriteRenderer.material;
    }

    // Update is called once per frame
    void Update()
    {
        constructionTimer -= Time.deltaTime;

        constructionMaterial.SetFloat("_Progress", GetConstrucitonTimerNormalized());

        if (constructionTimer <= 0f)
        {
            Instantiate(buildingType.prefabTransform, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        
    }

    private void SetBuildingType(BuildingTypeSO buildingType)
    {
        this.buildingType = buildingType;

        constructionTimerMax = buildingType.constructionTimerMax;
        constructionTimer = constructionTimerMax;

        // picking tthe correct image
        spriteRenderer.sprite = buildingType.sprite;

        // dynamically natch the building being placed
        boxCollider.offset = buildingType.prefabTransform.GetComponent<BoxCollider2D>().offset;
        boxCollider.size = buildingType.prefabTransform.GetComponent<BoxCollider2D>().size;

        // prevent spam placement
        buildingTypeHolder.buildingType = buildingType;
    }

    public float GetConstrucitonTimerNormalized()
    {
        return 1 - constructionTimer / constructionTimerMax;
    }

}
