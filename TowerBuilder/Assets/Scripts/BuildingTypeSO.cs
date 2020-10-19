using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="ScriptableObjects/BuildingType")]
public class BuildingTypeSO : ScriptableObject
{
    // a scriptable object are containers for data 
    [Header("GameObjects")]
    public Transform prefabTransform;
    public ResourceGeneratorData resourceGenData;

    [Header("Building Attributes")]
    public string nameString;
    public Sprite sprite;
    public float minConstructuonRadius;
    [Range(0, 1000)]
    public int maxHP;
    public bool hasResourceGenData;
    
    [Header("Construction Costs")]
    public ResourceAmount[] ConstructionResourceCost;
    [Tooltip("How Long it will take to construct this building")]
    public float constructionTimerMax;

    public string GetConstructionCost()
    {
        string cost = "";

        if (ConstructionResourceCost != null)
        {
            foreach (ResourceAmount resourceAmount in ConstructionResourceCost)
            {
                cost += "<color=#" + resourceAmount.resourceType.colorHex + ">" + resourceAmount.resourceType.nameShort + ": " + resourceAmount.amount + "</color> ";
            }
        }
      
        return cost;
    }

}
