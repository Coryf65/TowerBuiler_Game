using UnityEngine;

public static class MouseHelper
{

    private static Camera mainCamera;

    /// <summary>
    ///  Gets the position of our mouse, from the camera with the TAG "main camera"
    /// </summary>
    /// <returns>World Position Vector3</returns>
    public static Vector3 GetMouseWorldPosition()
    {
        // if not set, set it!
        if (mainCamera == null) mainCamera = Camera.main;

        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0f;

        return mouseWorldPosition;
    }
}

