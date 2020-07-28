using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBarRotation : MonoBehaviour {
    public Camera cameraToLookAt;
    void Update()
    {
        if (cameraToLookAt) {
            Vector3 v = cameraToLookAt.transform.position - transform.position;
            v.x = v.z = 0.0f;
            transform.LookAt(cameraToLookAt.transform.position - v);
            transform.Rotate(0, 180, 0);
        }
    }
}
