using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeWobble : MonoBehaviour
{
    [SerializeField] private Transform[] anchorPoints;
    [SerializeField] private float wobbleAmount = 0.1f;
    [SerializeField] private float wobbleSpeed = 1f;

    private Vector3[] originalPositions;

    private void Start()
    {
        originalPositions = new Vector3[anchorPoints.Length];
        for (int i = 0; i < anchorPoints.Length; i++)
        {
            originalPositions[i] = anchorPoints[i].localPosition;
        }
    }

    private void Update()
    {
        for (int i = 0; i < anchorPoints.Length; i++)
        {
            Vector3 originalPosition = originalPositions[i];
            float wobble = Mathf.Sin(Time.time * wobbleSpeed + i) * wobbleAmount;
            anchorPoints[i].localPosition = new Vector3(originalPosition.x, originalPosition.y + wobble, originalPosition.z);
        }
    }
}
