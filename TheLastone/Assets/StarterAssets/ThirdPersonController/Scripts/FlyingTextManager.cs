using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem; // Para el nuevo Input System

public class FlyingTextManager : MonoBehaviour
{
    public GameObject FlyingTextPrefab;
    public float flyDistance = 4f;
    public float speed = 1f;

    public void SpawnText(Vector3 spawnPoint, string text)
    {
        if (FlyingTextPrefab != null)
        {
            GameObject spawnedText = Instantiate(FlyingTextPrefab, spawnPoint, Quaternion.identity);
            spawnedText.GetComponent<FlyingText>().SetupText(text);
            StartCoroutine(Move(spawnedText));
        }
        else
        {
            Debug.LogWarning("FlyingTextPrefab no está asignado.");
        }
    }

    private IEnumerator Move(GameObject textObj)
    {
        float targetY = textObj.transform.position.y + flyDistance;

        while (textObj != null && textObj.transform.position.y < targetY)
        {
            textObj.transform.position += Vector3.up * speed * Time.deltaTime;
            yield return null;
        }

        Destroy(textObj);
    }
}


