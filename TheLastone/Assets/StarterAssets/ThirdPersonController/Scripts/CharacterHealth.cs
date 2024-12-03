using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    private FlyingTextManager flyingTextManager;

    private void Start()
    {
        currentHealth = maxHealth;

        // Buscar el componente FlyingTextManager en el propio jugador.
        flyingTextManager = GetComponent<FlyingTextManager>();

        if (flyingTextManager == null)
        {
            Debug.LogError("No se encontró el componente FlyingTextManager en el jugador.");
        }
    }

    // Método actualizado con dos parámetros: daño y posición
    public void TakeDamage(float damageAmount, Vector3 impactPosition)
    {
        currentHealth -= damageAmount;

        if (flyingTextManager != null)
        {
            // Mostrar el daño visualmente en la posición del impacto.
            flyingTextManager.SpawnText(impactPosition + Vector3.up * 1.5f, damageAmount.ToString());
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} ha muerto.");
        // Aquí podrías añadir lógica para la muerte del jugador, como activar una animación o reiniciar el nivel.
    }
}
