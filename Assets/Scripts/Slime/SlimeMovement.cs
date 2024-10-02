using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class SlimeMovement : MonoBehaviour
{

    [Header("Slime Movement Properties")]
    [SerializeField] private float speed = 2f;
    //[SerializeField] private float squishAmount = 0.5f;
    [SerializeField] private float squishSpeed = 0.5f;
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private float maxVelocity = 5f;

    private Vector3 originalScale;
    private Rigidbody rb;
    private Animator anim;
    private Coroutine wobbleCoroutine;
    private Vector3 randomDirection;


    private void Start()
    {
        originalScale = transform.localScale;
        rb = GetComponent<Rigidbody>();
        rb.drag = 2f; //Might not need, testing
        anim = GetComponent<Animator>();
        StartCoroutine(ChangeDirectionRoutine());
    }

    private void Update()
    {
        
        //Calculate movement direction relative to gravity
        Vector3 gravityDirection = (GravitySource.instance.transform.position - transform.position).normalized;
        float gravityStrength = 8f; //Adjust this as needed

        Vector3 right = Vector3.Cross(gravityDirection, transform.forward).normalized;
        Vector3 forward = Vector3.Cross(right, gravityDirection).normalized;

        Vector3 movement = (transform.right * randomDirection.x + transform.forward * randomDirection.z).normalized * speed * Time.deltaTime;

        rb.AddForce(movement);

        //rb.velocity = movement + rb.velocity.y * gravityDirection;

        rb.AddForce(gravityDirection * gravityStrength, ForceMode.Acceleration);

        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement, -gravityDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            /*if (wobbleCoroutine == null)
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
            }*/

            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime * squishSpeed);
        }

        if (rb.velocity.magnitude > maxVelocity)
        {
            rb.velocity = rb.velocity.normalized * maxVelocity;
        }
    }

    public void ApplyBlowerForce(Vector3 force)
    {
        rb.AddForce(force);

        Debug.Log(rb.velocity.magnitude);

        if (rb.velocity.magnitude > maxVelocity)
        {
            anim.SetTrigger("Pop");
        }

    }

    public void PopSlime()
    {
        gameObject.SetActive(false);

        float popRadius = 10f;
        Collider[] colliders = Physics.OverlapSphere(transform.position, popRadius);

        foreach (Collider collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (rb != null && rb != this.rb)
            {
                Vector3 direction = (rb.transform.position - transform.position).normalized;
                float forceAmount = 50f;
                rb.AddForce(direction * forceAmount);
            }
        
        }
        Debug.Log("Slime popped");
        Destroy(gameObject);
    }


    private IEnumerator ChangeDirectionRoutine()
    {
        while (true)
        {
            randomDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
            yield return new WaitForSeconds(Random.Range(5f, 10f));
        }
    }

    /* private IEnumerator WobbleEffect()
     {
         while (true)
         {
             float squishFactor = 1 - Mathf.Sin(Time.time * squishSpeed) * squishAmount;
             Vector3 newScale = new Vector3(originalScale.x * squishFactor, originalScale.y, originalScale.z * squishFactor);
             transform.localScale = Vector3.Lerp(transform.localScale, newScale, Time.deltaTime * squishSpeed); 

             yield return null;
         }
     }*/

}
