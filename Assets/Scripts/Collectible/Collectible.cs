using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [Header("Points Properties")]
    [SerializeField][Range(0, 10)] private int points;

    [Header("Audio Properties")]
    [SerializeField] private AudioClip collectSound;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Collect()
    {
        // play animations
        anim.Play("Collected");

        GetComponent<Rigidbody>().isKinematic = true;

        if (collectSound) AudioManager.instance.PlaySFX(collectSound);
    }

    public int GetPoints()
    {
        return points;
    }

    public void Collected()
    {
        gameObject.SetActive(false);
    }
}