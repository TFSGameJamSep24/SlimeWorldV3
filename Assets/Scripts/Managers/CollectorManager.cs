using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class CollectorManager : MonoBehaviour
{
    public static CollectorManager instance;

    [Header("Point collector")]
    [SerializeField] private List<Collectible> collectedStuff;

    [Header("Audio Properties")]
    [SerializeField] private AudioClip collectSound;

    public delegate void Collect(int value);
    public event Collect OnCollect;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Collectible collectCheck = other.gameObject.GetComponent<Collectible>();

        if (collectCheck != null)
        {
            //  Add object to list and update listeners
            collectedStuff.Add(collectCheck);
            OnCollect?.Invoke(collectCheck.GetPoints());
            collectCheck.Collect();

            if (collectSound != null) AudioManager.instance.PlaySFX(collectSound);
        }
    }

    public int GetTotalPoints()
    {
        int total = 0;

        foreach (Collectible collectible in collectedStuff)
        {
            total += collectible.GetPoints();
        }

        return total;
    }
}