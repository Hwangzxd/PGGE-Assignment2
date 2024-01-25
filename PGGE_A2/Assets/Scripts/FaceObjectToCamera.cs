using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceObjectToCamera : MonoBehaviour
{
    void Update()
    {
        // make the player tag follow the main camera
        transform.LookAt(Camera.main.transform);
    }
}
