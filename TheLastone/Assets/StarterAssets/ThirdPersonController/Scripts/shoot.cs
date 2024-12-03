using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // Necesario para el New Input System

public class shoot : MonoBehaviour
{
    public GameObject Bolafuego;   // Prefab de la bola de fuego
    public GameObject salida;     // Punto de salida de la bola de fuego

    private InputAction fireAction; // Acción de disparo

    void Awake()
    {
        // Obtén la acción "Fire" desde el componente PlayerInput
        var playerInput = GetComponent<PlayerInput>();
        fireAction = playerInput.actions["Fire"]; // Asegúrate de que "Fire" esté configurado en el mapa Player
    }

    void Update()
    {
        // Revisa si la acción "Fire" fue activada
        if (fireAction != null && fireAction.triggered)
        {
            Fire();
        }
    }

    void Fire()
    {
        if (Bolafuego != null && salida != null)
        {
            // Instancia la bola de fuego en la posición y rotación del punto de salida
            Instantiate(Bolafuego, salida.transform.position, salida.transform.rotation);
            Debug.Log("Disparo realizado");
        }
    }
}

