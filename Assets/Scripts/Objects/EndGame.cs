using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class EndGame : MonoBehaviour
{
    public string sceneToLoad;
    private int requiredPlayerCount = 4;
    private List<GameObject> playersInTrigger = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !playersInTrigger.Contains(other.gameObject))
        {
            playersInTrigger.Add(other.gameObject);
            CheckPlayerCountAndLoadScene();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playersInTrigger.Remove(other.gameObject);
        }
    }

    private void CheckPlayerCountAndLoadScene()
    {
        if (playersInTrigger.Count == requiredPlayerCount)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
