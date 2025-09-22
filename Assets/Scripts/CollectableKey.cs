using UnityEngine;

public class ChaveColetavel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerKey inventarioDoJogador = other.GetComponent<PlayerKey>();

            if (inventarioDoJogador != null)
            {
                inventarioDoJogador.ColetarChave();

                Destroy(gameObject);
            }
        }
    }
}
