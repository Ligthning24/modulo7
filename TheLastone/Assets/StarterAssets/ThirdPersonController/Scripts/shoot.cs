using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // Necesario para el New Input System

public class shoot : MonoBehaviour
{
    public GameObject Bolafuego;   // Prefab de la bola de fuego
    public GameObject salida;     // Punto de salida de la bola de fuego

    private InputAction fireAction; // Acci�n de disparo

    void Awake()
    {
        // Obt�n la acci�n "Fire" desde el componente PlayerInput
        var playerInput = GetComponent<PlayerInput>();
        fireAction = playerInput.actions["Fire"]; // Aseg�rate de que "Fire" est� configurado en el mapa Player
    }

    void Update()
    {
        // Revisa si la acci�n "Fire" fue activada
        if (fireAction != null && fireAction.triggered)
        {
            Fire();
        }
    }

    void Fire()
    {
        if (Bolafuego != null && salida != null)
        {
            // Instancia la bola de fuego en la posici�n y rotaci�n del punto de salida
            Instantiate(Bolafuego, salida.transform.position, salida.transform.rotation);
            Debug.Log("Disparo realizado");
        }
    }
}

