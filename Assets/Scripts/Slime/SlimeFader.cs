using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeFader : MonoBehaviour
{
    public Material fadeMaterial;
    private SlimeBehaviour slimeBehaviour;

    private void Awake()
    {
        PrimeFader();

        slimeBehaviour = GetComponent<SlimeBehaviour>();
    }

    private void Update()
    {
        Fade(slimeBehaviour.GetHPRatio());
    }

    private void PrimeFader()
    {
        MeshRenderer mr = GetComponentInChildren<MeshRenderer>();

        fadeMaterial = mr.materials[1];

        Color fade = fadeMaterial.color;
        fade.a = 0;
        fadeMaterial.color = fade;
        mr.materials[1].color = fadeMaterial.color;
    }

    public void Fade(float ratio)
    {
        Color fade = fadeMaterial.color;
        fade.a = 1 - ratio;
        fadeMaterial.color = fade;
    }
}
