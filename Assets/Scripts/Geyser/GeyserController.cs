using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeyserController : MonoBehaviour
{

    [SerializeField] private ParticleSystem geyserParticles;
    [SerializeField] private Collider geyserCollider;
    [SerializeField] private float geyserStrength;
    [SerializeField] private float eruptionDuration = 10f;
    [SerializeField] private float eruptionInterval = 15f;

    private void Start()
    {
        StartCoroutine(GeyserCycle());
    }

    private IEnumerator GeyserCycle()
    {
        while (true)
        {
            geyserParticles.Play();
            geyserCollider.enabled = true;
            yield return new WaitForSeconds(eruptionDuration);

            geyserParticles.Stop();
            geyserCollider.enabled = false;
            yield return new WaitForSeconds(eruptionInterval);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Push player up & back
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 pushForce = (Vector3.up + -transform.forward) * geyserStrength;
                rb.AddForce(pushForce, ForceMode.Impulse);
            }
        }
        //Pop the slime
        if (other.CompareTag("Slime"))
        {
            SlimeBehaviour slime = other.GetComponent<SlimeBehaviour>();
            if (slime != null)
            {
                slime.TriggerPop();
            }
        }
    }
}
