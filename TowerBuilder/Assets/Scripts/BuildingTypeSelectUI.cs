using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTypeSelectUI : MonoBehaviour
{
    // the unselect button
    [SerializeField] private Sprite arrowSprite;

    private Dictionary<BuildingTypeSO, Transform> btnTransformDictionary;
    private float offestAmount = 0f;

    void Awake()
    {
        // hide our template
        Transform btnTemplate = transform.Find("btnTemplate");
        btnTemplate.gameObject.SetActive(false);

        // get the list of items to display
        BuildingTypeListSO buildingList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
        // init our Dictionary
        btnTransformDictionary = new Dictionary<BuildingTypeSO, Transform>();

        // Setting a new Unselect Button


        foreach (BuildingTypeSO buildingType in buildingList.list)
        {
            Transform btnTansform = Instantiate(btnTemplate, transform);
            btnTansform.gameObject.SetActive(true);
           
            btnTansform.GetComponent<RectTransform>().anchoredPosition = new Vector2(offestAmount, 0);
            // find our itemImage
            btnTansform.Find("itemImage").GetComponent<Image>().sprite = buildingType.sprite;
            
            // resizing the cursor
            if (buildingType.sprite.name == "Cursor")
            {
                btnTansform.Find("itemImage").GetComponent<RectTransform>().sizeDelta = new Vector2(0, -30);
            }

            //Debug.Log(buildingType.sprite.name);

            // positioning
            offestAmount += 130f;

            // waiting for a click event and lambda function, inline version of subscribing
            btnTansform.GetComponent<Button>().onClick.AddListener(() => {
                BuildingManager.Instance.SetActiveBuildingType(buildingType);
            });

            MouseEvents mouseEvents = btnTansform.GetComponent<MouseEvents>();
            // subscribing to the events
            mouseEvents.OnMouseEnter += (object sender, EventArgs eventArgs) =>
            {
                ToolTipUI.Instance.Show(buildingType.nameString + "\n" + buildingType.GetConstructionCost());
            };
            mouseEvents.OnMouseExit += (object sender, EventArgs eventArgs) =>
            {
                ToolTipUI.Instance.Hide();
            };

            // assgin in Dictionary
            btnTransformDictionary[buildingType] = btnTansform;
        }
    }

    private void Start()
    {
        BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActiveBuildingTypeChanged;
        UpdateActiveBuildingTypeButton();
    }

    // moving updating on Events helps performance greatly
    private void BuildingManager_OnActiveBuildingTypeChanged(object sender, BuildingManager.OnActiveBuildingTypeChangedEventArgs e)
    {
        UpdateActiveBuildingTypeButton();
    }

    private void UpdateActiveBuildingTypeButton()
    {
        foreach (BuildingTypeSO buildingType in btnTransformDictionary.Keys)
        {
            // find and hide ALL selected game objects
            Transform btnTransform = btnTransformDictionary[buildingType];
            btnTransform.Find("selected").gameObject.SetActive(false);
        }
        // now get the active one and display that
        BuildingTypeSO activeBuildingType = BuildingManager.Instance.GetActiveBuildingType();
        btnTransformDictionary[activeBuildingType].Find("selected").gameObject.SetActive(true);
    }
}
