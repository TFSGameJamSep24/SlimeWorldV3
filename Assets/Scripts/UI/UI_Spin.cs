using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Spin : MonoBehaviour
{
    [SerializeField] private float spinRate;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * spinRate * Time.deltaTime);
    }
}
