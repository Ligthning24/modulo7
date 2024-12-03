using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bolafuego : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    public GameObject efectoImpacto;
    public float damage = 20f;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }
    // Update is called once per frame
    void Update()
    {
    }
    public void OnCollisionEnter(Collision collision)
    {
        // Verificar si el objeto con el que colisionó tiene un componente "StarterAssetsInputs"
        StarterAssetsInputs starterAssetsInputs = collision.gameObject.GetComponent<StarterAssetsInputs>();
        if (starterAssetsInputs != null)
        {
            // Aumentar opacidad del efecto de sangre al ser golpeado por la bola de fuego
            starterAssetsInputs.BloodFromFireball();
        }
        // Generar el efecto de impacto
        if (efectoImpacto != null)
        {
            Destroy(Instantiate(efectoImpacto, transform.position, transform.rotation), 2.0f);
        }
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto golpeado tiene el script CharacterHealth
        CharacterHealth health = other.GetComponent<CharacterHealth>();
        if (health != null)
        {
            // Llama al método TakeDamage, pasando la posición del impacto
            health.TakeDamage(damage, transform.position);
        }
        Destroy(this.gameObject); // Destruir la Bola de Fuego
    }
}