using UnityEngine;

public class CollectableKey : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerKey inventarioDoJogador = other.GetComponentInParent<PlayerKey>();

            if (inventarioDoJogador != null && !inventarioDoJogador.temChave)
            {
                inventarioDoJogador.ColetarChave();
                Destroy(gameObject);
            }
        }
    }
}
