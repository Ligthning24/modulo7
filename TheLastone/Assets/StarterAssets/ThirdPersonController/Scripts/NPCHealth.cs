using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    // Referencia al script StarterAssetsInputs para efectos visuales
    private StarterAssets.StarterAssetsInputs starterAssetsInputs;

    void Start()
    {
        currentHealth = maxHealth;
        starterAssetsInputs = GetComponent<StarterAssets.StarterAssetsInputs>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        ShowDamage(damage);

        // Activar el efecto visual de explosión si el script starterAssetsInputs está disponible
        if (starterAssetsInputs != null && starterAssetsInputs.healingEffect != null)
        {
            starterAssetsInputs.healingEffect.transform.position = transform.position;
            starterAssetsInputs.healingEffect.Play();
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void ShowDamage(int damage)
    {
        GameObject damageText = new GameObject("DamageText");
        TextMesh textMesh = damageText.AddComponent<TextMesh>();
        textMesh.text = damage.ToString();
        textMesh.characterSize = 0.3f;
        textMesh.color = Color.red;
        textMesh.fontStyle = FontStyle.Bold;

        damageText.transform.position = transform.position + new Vector3(0, 2, 0);
        Destroy(damageText, 1.5f); // Eliminar el texto después de 1.5 segundos
    }

    void Die()
    {
        // Implementar comportamiento al morir
        Debug.Log("El NPC ha muerto");
        Destroy(gameObject); // Destruir el NPC cuando muera
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fireball"))
        {
            TakeDamage(20);
        }
    }
}

