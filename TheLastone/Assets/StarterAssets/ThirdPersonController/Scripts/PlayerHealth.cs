using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
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

        // Activar el efecto visual de daño
        if (starterAssetsInputs != null)
        {
            starterAssetsInputs.BloodFromFireball();  // Aumentar la opacidad del efecto de sangre
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Asegura que la salud no exceda el máximo
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
        Debug.Log("El jugador ha muerto");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fireball"))
        {
            TakeDamage(20);
        }
    }
}
