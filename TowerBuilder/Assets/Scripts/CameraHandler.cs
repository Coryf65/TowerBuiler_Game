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
    
    private float orthographicSize;
    private float targetOrthographicSize;

    private void Start()
    {
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

        Vector3 moveDir = new Vector3(x, y).normalized;

        transform.position += moveDir * cameraMoveSpeed * Time.deltaTime;
    }
}
