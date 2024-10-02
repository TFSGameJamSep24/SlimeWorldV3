using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravityObject : MonoBehaviour
{
    private Rigidbody rb;
    public bool isPlayer;

    [SerializeField] private float rotateTime = 2;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.freezeRotation = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        GravitySource.instance.OnApplyGravity += ApplyGravity;
    }

    private void OnDestroy()
    {
        GravitySource.instance.OnApplyGravity -= ApplyGravity;
    }

    private void ApplyGravity(float value)
    {
        Vector3 directionToPlanet = (GravitySource.instance.transform.position - transform.position).normalized;

        rb.AddForce(directionToPlanet * value * rb.mass);

        if (!isPlayer)
        {
            return;
        }


        Quaternion worldDirection = Quaternion.FromToRotation(-transform.up, directionToPlanet) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, worldDirection, rotateTime * Time.deltaTime);
    }
}