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
            Debug.LogError("No se encontr� el componente FlyingTextManager en el jugador.");
        }
    }

    // M�todo actualizado con dos par�metros: da�o y posici�n
    public void TakeDamage(float damageAmount, Vector3 impactPosition)
    {
        currentHealth -= damageAmount;

        if (flyingTextManager != null)
        {
            // Mostrar el da�o visualmente en la posici�n del impacto.
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
        // Aqu� podr�as a�adir l�gica para la muerte del jugador, como activar una animaci�n o reiniciar el nivel.
    }
}
