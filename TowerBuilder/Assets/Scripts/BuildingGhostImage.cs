using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingGhostImage : MonoBehaviour
{
    private GameObject spriteGameObject;
    private ResourceNearbyOverlay resourceNearbyOverlay;

    private void Awake()
    {
        spriteGameObject = transform.Find("sprite").gameObject;
        resourceNearbyOverlay = transform.Find("ResourceNearbyOverlay").GetComponent<ResourceNearbyOverlay>();
        Hide();
    }

    private void Start()
    {
        //subscribing to the event from building manger
        BuildingManager.Instance.OnActiveBuildingTypeChanged += BuildingManager_OnActiveBuildingTypeChanged;
    }

    private void BuildingManager_OnActiveBuildingTypeChanged(object sender, BuildingManager.OnActiveBuildingTypeChangedEventArgs eventArgs)
    {
        if (eventArgs.activeBuildingType == null || eventArgs.activeBuildingType.nameString == "Arrow")
        {
            Hide();
            resourceNearbyOverlay.Hide();
        }
        else
        {
            Show(eventArgs.activeBuildingType.sprite);
            // Check if there should be an overlay for this building type
            if (eventArgs.activeBuildingType.hasResourceGenData)
            {
                resourceNearbyOverlay.Show(eventArgs.activeBuildingType.resourceGenData);
            } else
            {
                resourceNearbyOverlay.Hide();
            }
        }
    }

    private void Update()
    {
        // following the mouse cursor
        transform.position = MouseHelper.GetMouseWorldPosition();
    }

    private void Show(Sprite ghostSprite)
    {
        spriteGameObject.SetActive(true);

        // display ghost image for all but the cursor
        spriteGameObject.GetComponent<SpriteRenderer>().sprite = (ghostSprite.name != "Cursor") ? ghostSprite : null;
    }

    private void Hide()
    {
        spriteGameObject.SetActive(false);
    }
}
