using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraHandler : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] private CinemachineVirtualCamera cinemachineCamera;

    [Header("Camera Settings")]
    [SerializeField] private float zoomSpeed = 5f;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float cameraMoveSpeed = 30f;
    [SerializeField] private float minOrthographicSize = 10f;
    [SerializeField] private float maxOrthographicSize = 30f;
    [Tooltip("Boundry in Pixels of the screens edge to active the edge scrolling")]
    [SerializeField] private float edgeScrollingSize = 20f; // pixels

    private float orthographicSize;
    private float targetOrthographicSize;
    private bool edgeScrolling;
    private BuildingTypeSO arrow;
    private BuildingTypeListSO buildingList;

    public static CameraHandler Instance { get; private set; }

    private void Awake()
    {
        Instance = this;               
    }

    private void Start()
    {
        buildingList = Resources.Load<BuildingTypeListSO>(typeof(BuildingTypeListSO).Name); 
        arrow = buildingList.list[0];

        orthographicSize = cinemachineCamera.m_Lens.OrthographicSize;
        targetOrthographicSize = orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleZoom();
    }

    private void HandleZoom()
    {
        // zoom in Scolling mouse wheel
        targetOrthographicSize -= Input.mouseScrollDelta.y * moveSpeed;
        // sets a min and max size
        targetOrthographicSize = Mathf.Clamp(targetOrthographicSize, minOrthographicSize, maxOrthographicSize);

        // adding smoothing on zooming by changing ortho to targetOrtho
        orthographicSize = Mathf.Lerp(orthographicSize, targetOrthographicSize, zoomSpeed * Time.deltaTime);
        cinemachineCamera.m_Lens.OrthographicSize = orthographicSize;
    }

    private void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OptionsUI.Instance.ToggleVisible();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            BuildingManager.Instance.SetActiveBuildingType(buildingType: arrow);
        }
        
        if (edgeScrolling)
        {
            // make edge map scrolling           
            if (Input.mousePosition.x > Screen.width - edgeScrollingSize)
            {
                x = 1f; // move right
            }
            else if (Input.mousePosition.x < edgeScrollingSize)
            {
                x = -1f; // move left
            }

            if (Input.mousePosition.y > Screen.height - edgeScrollingSize)
            {
                y = 1f; // move up
            }
            else if (Input.mousePosition.y < edgeScrollingSize)
            {
                y = -1f; // move down
            }
        }        

        Vector3 moveDir = new Vector3(x, y).normalized;

        transform.position += moveDir * cameraMoveSpeed * Time.deltaTime;
    }

    public void SetEdgeScrolling(bool edgeScrolling)
    {
        this.edgeScrolling = edgeScrolling;
    }

    public bool GetEdgeScrolling()
    {
        return edgeScrolling;
    }
}
