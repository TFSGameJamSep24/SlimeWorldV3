using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [Header("Points Properties")]
    [SerializeField] [Range(0, 10)] private int points;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Collect()
    {
        // play animations

        // Temp
        StartCoroutine(Collected());
        // Temp
    }

    IEnumerator Collected()
    {
        yield return new WaitForSeconds(2);

        if (gameObject.activeSelf) gameObject.SetActive(false);
    }

    public int GetPoints()
    {
        return points;
    }
}