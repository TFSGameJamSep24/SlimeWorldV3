using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Camera : MonoBehaviour
{
    [SerializeField] private Transform gimbal;
    [SerializeField] private float xSensitivity = 10;

    // Update is called once per frame
    void Update()
    {
        gimbal.localRotation *= Quaternion.Euler(0, -xSensitivity * Input.GetAxis("Mouse X"), 0);
    }
}
