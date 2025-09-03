using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class EndGame : MonoBehaviour
{
    public string sceneToLoad = "Main_Menu";
    public int requiredPlayerCount = 4;

    private List<GameObject> playersInTrigger = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody playerRigidbody = other.attachedRigidbody;

        if (playerRigidbody != null && playerRigidbody.CompareTag("Player"))
        {
            GameObject playerObject = playerRigidbody.gameObject;

            if (!playersInTrigger.Contains(playerObject))
            {
                playersInTrigger.Add(playerObject);
                CheckPlayerCount();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody playerRigidbody = other.attachedRigidbody;

        if (playerRigidbody != null && playerRigidbody.CompareTag("Player"))
        {
            GameObject playerObject = playerRigidbody.gameObject;
            playersInTrigger.Remove(playerObject);
        }
    }

    private void CheckPlayerCount()
    {
        if (playersInTrigger.Count == requiredPlayerCount)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
