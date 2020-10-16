using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilsClass
{
    // A Utility Class for General useful items

    public static Vector3 GetRandomDirection()
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    public static float GetAngleFromVector(Vector3 vector3)
    {
        // gets Rotation in Radians
        float radians = Mathf.Atan2(vector3.y, vector3.x);
        // convert to degrees
        float degrees = radians * Mathf.Rad2Deg;

        return degrees;
    }
    
}
