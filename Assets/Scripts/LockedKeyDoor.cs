using UnityEngine;

public class LockedKeyDoor : MonoBehaviour
{
    [Header("Partes da Porta")]
    [Tooltip("Arraste aqui o GameObject que tem o colisor sólido da porta.")]
    public GameObject corpoSolidoDaPorta;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerKey inventarioDoJogador = other.GetComponentInParent<PlayerKey>();

            if (inventarioDoJogador != null && inventarioDoJogador.temChave)
            {
                inventarioDoJogador.UsarChave();

                if (corpoSolidoDaPorta != null)
                {
                    corpoSolidoDaPorta.SetActive(false);
                }

                gameObject.SetActive(false);
            }
        }
    }
}
