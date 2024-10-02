using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySource : MonoBehaviour
{
    public static GravitySource instance;

    [SerializeField] [Range(0.1f, 10f)] private float gravityScale;

    public delegate void ApplyGravity(float value);
    public ApplyGravity OnApplyGravity;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        OnApplyGravity?.Invoke(gravityScale * 9.81f);
    }
}