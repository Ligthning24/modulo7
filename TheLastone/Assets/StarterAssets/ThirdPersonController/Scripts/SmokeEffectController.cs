using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeEffectController : MonoBehaviour
{
    private ParticleSystem SmokeEffect;

    private void Awake()
    {
        SmokeEffect = GetComponent<ParticleSystem>();

        if (SmokeEffect == null)
        {
            Debug.LogError("No se encontró el sistema de partículas en el prefab de SmokeEffect.");
        }
    }

    public void ActivateEffect()
    {
        if (SmokeEffect != null && !SmokeEffect.isPlaying)
        {
            SmokeEffect.Play();
        }
    }

    public void DeactivateEffect()
    {
        if (SmokeEffect != null && SmokeEffect.isPlaying)
        {
            SmokeEffect.Stop();
        }
    }
}

