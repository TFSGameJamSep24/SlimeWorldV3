using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX_Parent : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> particleEffects;

    private void Awake()
    {
        if (particleEffects.Count == 0)
        {
            ParticleSystem[] particleSystems = GetComponentsInChildren<ParticleSystem>();

            foreach (ParticleSystem particleSystem in particleSystems)
            {
                particleEffects.Add(particleSystem);
            }
        }

        StopEffects();
    }

    public void StopEffects()
    {
        foreach (ParticleSystem particle in particleEffects)
        {
            particle.Stop();
        }
    }

    public void PlayEffects()
    {
        foreach(ParticleSystem particle in particleEffects) particle.Play();
    }
}
