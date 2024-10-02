using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Blower : MonoBehaviour
{
    [Header("Blow Angle Properties")]
    [SerializeField] private Transform blowerPoint;
    [SerializeField] private float blowerRange = 5;
    [SerializeField] private float maxBlowerAngle;      // Find the angle between object and forward and compare to this float
    [SerializeField] private LayerMask objectsLayer;

    [Header("Blow Strength Properties")]
    [SerializeField] private float blowStrength = 10;
    [SerializeField] private float initialPushBackForce = 10;
    [SerializeField] private float pushBackForce = 10;
    private Rigidbody rb;

    [Header("Blower VFX Properties")]
    [SerializeField] private VFX_Parent vfx;

    [Header("Blower Audio Properties")]
    [SerializeField] private AudioClip blowerSFX;
    private AudioSource blowerAudioSource = null;

    private bool isActive = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) ToggleBlower(); 

        if (!isActive) return;

        ActivateBlower();
    }

    private void ToggleBlower()
    {
        isActive = !isActive;

        if (isActive)
        {
            rb.AddForce(-blowerPoint.forward * initialPushBackForce);
            vfx.PlayEffects();

            if (blowerAudioSource == null)
            {
                if (blowerSFX != null) blowerAudioSource = AudioManager.instance.PlaySFX(blowerSFX, true);
            }

            else blowerAudioSource.Play();
        }

        else if (!isActive)
        {
            vfx.StopEffects();

            if (blowerAudioSource != null) blowerAudioSource.Stop();
        }
    }

    private void ActivateBlower()
    {
        RaycastHit[] blownObjects = Physics.SphereCastAll(blowerPoint.position, blowerRange, blowerPoint.forward, blowerRange, objectsLayer);

        foreach (RaycastHit objectToBlow in blownObjects)
        {
            if (Vector3.Angle((objectToBlow.transform.position - transform.position).normalized, blowerPoint.forward) <= maxBlowerAngle)
            {
                BlowAway(objectToBlow.collider.gameObject);
            }
        }

        rb.AddForce(-blowerPoint.forward * pushBackForce * Time.deltaTime);
    }

    private void BlowAway(GameObject slime)
    {
        /*Rigidbody slimeRB = slime.GetComponent<Rigidbody>();

        slimeRB.AddForce((slime.transform.position - transform.position).normalized * blowStrength * Time.deltaTime);*/

        SlimeBehaviour slimeBehavior = slime.GetComponent<SlimeBehaviour>();
        if (slimeBehavior != null)
        {
            slimeBehavior.Damage();
        }

        Rigidbody slimeRB = slime.GetComponent<Rigidbody>();

        if (rb != null)  
        {
            Vector3 forceDirection = (slime.transform.position - transform.position).normalized;
            slimeRB.AddForce(forceDirection * blowStrength * Time.deltaTime);
        }
    }
}