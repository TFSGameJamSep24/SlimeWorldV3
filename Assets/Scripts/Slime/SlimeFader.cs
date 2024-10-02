using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeFader : MonoBehaviour
{
    public Material fadeMaterial;

    private void Awake()
    {
        PrimeFader();
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

    public void Fade()
    {
        Debug.Log("Fading");

        Color fade = fadeMaterial.color;
        fade.a += 0.002f;
        fadeMaterial.color = fade;
    }
}
