using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAnchorController : MonoBehaviour
{
    [SerializeField] private List<Transform> anchorPoints;
    [SerializeField] Transform gravitySource;

    private void Start()
    {
        if (gravitySource == null) 
        {
            gravitySource = GravitySource.instance.transform;
        }

        if (anchorPoints == null || anchorPoints.Count == 0)
        {
            Debug.LogWarning("Anchor point is not assigned to the controller");
        }
        
    }

    private void Update()
    {
        foreach (Transform anchor in anchorPoints)
        {
            Vector3 gravityDirection = (gravitySource.position - anchor.position).normalized;
            anchor.up = -gravityDirection;
        }

    }

}
