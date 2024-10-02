using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeManager : MonoBehaviour
{
    public static SlimeManager Instance { get; private set; }

    private Dictionary<string, int> collectedSlimes = new Dictionary<string, int>()
    {
        { "docile", 0 },
        { "shy", 0 },
        { "angry", 0 }
    };

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CollectSlime(string slimeType)
    {
        if (collectedSlimes.ContainsKey(slimeType))
        {
            collectedSlimes[slimeType]++;
            UIManager.Instance.UpdateSlimeCount(slimeType, collectedSlimes[slimeType]);
        }
    }

    public int GetSlimeCount(string slimeType)
    {
        return collectedSlimes.ContainsKey(slimeType) ? collectedSlimes[slimeType] : 0;
    }
}
