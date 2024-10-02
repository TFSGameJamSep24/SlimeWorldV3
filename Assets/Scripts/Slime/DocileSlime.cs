using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class DocileSlime : MonoBehaviour
{

    [Header("Slime Movement Properties")]
    [SerializeField] private float speed = 2f;
    [SerializeField] private float squishAmount = 0.05f;
    [SerializeField] private float squishSpeed = 0.5f;
    [SerializeField] private float rotationSpeed = 2f;

    private Vector3 originalScale;
    private Rigidbody rb;
    private Coroutine wobbleCoroutine;

    private void Start()
    {
        originalScale = transform.localScale;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //Need to set up AI movement
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //Calculate movement direction relative to gravity
        Vector3 gravityDirection = (GravitySource.instance.transform.position - transform.position).normalized;
        Vector3 right = Vector3.Cross(gravityDirection, transform.forward).normalized;
        Vector3 forward = Vector3.Cross(right, gravityDirection).normalized;

        Vector3 movement = (right * horizontal + forward * vertical).normalized * speed;

        rb.velocity = movement + rb.velocity.y * gravityDirection;

        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement, gravityDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            if (wobbleCoroutine == null)
            {
                wobbleCoroutine = StartCoroutine(WobbleEffect());
            }
        }
        else
        {
            if (wobbleCoroutine != null)
            {
                StopCoroutine(wobbleCoroutine);
                wobbleCoroutine = null;
            }

            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime * squishSpeed);
        }
    }
    private IEnumerator WobbleEffect()
    {
        while (true)
        {
            float squishFactor = 1 - Mathf.Sin(Time.time * squishSpeed) * squishAmount;
            Vector3 newScale = new Vector3(originalScale.x * squishFactor, originalScale.y, originalScale.z * squishFactor);
            transform.localScale = Vector3.Lerp(transform.localScale, newScale, Time.deltaTime * squishSpeed); 

            yield return null;
        }
    }

}
