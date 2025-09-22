using UnityEngine;

public class LockedKeyDoor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerKey inventarioDoJogador = other.GetComponent<PlayerKey>();

            if (inventarioDoJogador != null && inventarioDoJogador.temChave)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
