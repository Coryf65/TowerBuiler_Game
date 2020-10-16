using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ResourceType")]
public class ResourceTypeSO : ScriptableObject
{
    public string nameString;
    public string nameShort;
    public Sprite sprite;
    // colors for the tooltip    
    [ColorUsage(false)]
    public Color colorRGBA;
    
    [System.NonSerialized]
    public string colorHex;


    public void OnValidate()
    {
        if (colorRGBA != null)
        {
            // converting the RGB into Hex
            colorHex = ColorUtility.ToHtmlStringRGB(colorRGBA);
        }
    }

}
