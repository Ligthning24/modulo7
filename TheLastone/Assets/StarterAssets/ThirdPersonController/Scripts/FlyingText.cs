using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingText : MonoBehaviour
{
    private TextMesh textMesh;

    private void Awake()
    {
        // Buscar el componente TextMesh si no se ha asignado previamente.
        textMesh = GetComponent<TextMesh>();

        if (textMesh == null)
        {
            Debug.LogError("No se encontró el componente TextMesh en el prefab FlyingText.");
        }
    }

    public void SetupText(string text)
    {
        if (textMesh != null)
        {
            textMesh.text = text;
        }
    }

    private void Update()
    {
        if (Camera.main != null)
        {
            // Orientar el texto para que siempre mire hacia la cámara.
            transform.LookAt(transform.position - Camera.main.transform.position, Camera.main.transform.up);
        }
    }
}

