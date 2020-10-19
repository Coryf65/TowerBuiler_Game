using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingManager : MonoBehaviour
{

    // using a singleton to access setActiveBuilding() from other classes
    public static BuildingManager Instance { get; private set; }

    //custom event
    public event EventHandler<OnActiveBuildingTypeChangedEventArgs> OnActiveBuildingTypeChanged;

    // creating a custom EventHandler, to send extra info along with the args
    public class OnActiveBuildingTypeChangedEventArgs : EventArgs
    {
        // extra fields we are passing in
        public BuildingTypeSO activeBuildingType;
    }

    [SerializeField] private Building hqBuilding;

    // initialize the camera one time
    private Camera mainCamera;

    // we want to use [SerializedField] to open to the Unity Editor, not Public accessor
    // could use a list to hold each type, but we will use ScriptableObjects
    private BuildingTypeSO activeBuildingType = null;
    private BuildingTypeListSO buildingList;

    private void Awake()
    {
        Instance = this;

        buildingList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name);
        activeBuildingType = buildingList.list[0];
    }

    private void Start()
    {
        mainCamera = Camera.main;        
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (activeBuildingType.nameString != "Arrow")
            {
                if (CanPlaceBuilding(activeBuildingType, MouseHelper.GetMouseWorldPosition(), out string errorMessage))
                {
                    if (ResourceManager.TheResourceManager.CanAfford(activeBuildingType.ConstructionResourceCost))
                    {
                        ResourceManager.TheResourceManager.SpendResources(activeBuildingType.ConstructionResourceCost);
                        // create an instance on mouse position with no rotation
                        //Instantiate(activeBuildingType.prefabTransform, MouseHelper.GetMouseWorldPosition(), Quaternion.identity);
                        BuildingConstructor.Create(MouseHelper.GetMouseWorldPosition(), activeBuildingType);
                    } else
                    {
                        ToolTipUI.Instance.Show("Cannot Afford: " + activeBuildingType.GetConstructionCost(), new ToolTipUI.ToolTipTimer { timer = 2f });
                    }
                } else
                {
                    ToolTipUI.Instance.Show(errorMessage, new ToolTipUI.ToolTipTimer { timer = 2f });
                }
            }
            
        }

    }    

    public void SetActiveBuildingType(BuildingTypeSO buildingType)
    {
        activeBuildingType = buildingType;
        // call the event, and set the references
        OnActiveBuildingTypeChanged?.Invoke(this, new OnActiveBuildingTypeChangedEventArgs { activeBuildingType = activeBuildingType });
    }

    public BuildingTypeSO GetActiveBuildingType()
    {
        return activeBuildingType;
    }


    private bool CanPlaceBuilding(BuildingTypeSO buildingType, Vector3 position, out string errorReason)
    {
        BoxCollider2D boxCollider2D = buildingType.prefabTransform.GetComponent<BoxCollider2D>();
        Collider2D[] collider2Ds =  Physics2D.OverlapBoxAll(position + (Vector3)boxCollider2D.offset, boxCollider2D.size, 0);        
        bool isAreaClear = collider2Ds.Length == 0;
        errorReason = "";

        if (!isAreaClear)
        {
            errorReason = "Area is not clear!";
            return false;
        }

        // displaying an overlaying circle
        collider2Ds = Physics2D.OverlapCircleAll(position, buildingType.minConstructuonRadius);

        foreach (Collider2D collider2D in collider2Ds)
        {
            // colliders inside the construction radius
            BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder != null)
            {
                // has a building type holder
                if (buildingTypeHolder.buildingType == buildingType)
                {
                    errorReason = "Too close to a building of the same type!";
                    // Building of the same type already placed in our construction radius
                    return false;   
                }
            }
        }

        // max building placement range, might change this .....
        float maxConstructionRadius = 30f;
        collider2Ds = Physics2D.OverlapCircleAll(position, maxConstructionRadius);
        foreach (Collider2D collider2D in collider2Ds)
        {
            // colliders inside the construction radius
            BuildingTypeHolder buildingTypeHolder = collider2D.GetComponent<BuildingTypeHolder>();
            if (buildingTypeHolder != null)
            {                
                // There is a building nearby
                return true;
            }
        }

        return true;
    }

    public Building GetHQBuilding()
    {
        return hqBuilding;
    }
}
