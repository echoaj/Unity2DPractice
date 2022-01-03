using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;

    public Vector3 offset;

    [Range(0, 1)]
    public float smoothSpeed = 0.125f;

    // Late update is called after update 
    // Therefore the camera only moves after the character moves
    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        // Linear Enterperlation: process of smoothly going from one point to another.
        // Lerp's 3rd parameter is any float form 0 to 1.  If 0 value will be the 1st parameter
        // If 1 the value will be 2nd parameters.  If in between, value will be in between those numbers
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);            
        transform.position = smoothedPosition;

        transform.LookAt(target);
    }

}
