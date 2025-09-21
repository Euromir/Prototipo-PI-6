using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject Door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.CompareTag("Player"))
        {
            Door.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
