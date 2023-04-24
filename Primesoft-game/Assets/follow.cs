using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow : MonoBehaviour
{
    public Transform target;  // The player's Transform component
    public float smoothTime = 0.3f;  // The delay or smoothing factor

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        // Calculate the desired camera position based on the player's position
        Vector3 targetPosition = target.position;
        targetPosition.z = transform.position.z;  // Maintain the camera's Z position

        // Smoothly move the camera towards the target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

}
