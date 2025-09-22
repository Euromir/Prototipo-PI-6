using UnityEngine;

public class PlayerKey : MonoBehaviour
{
    [Header("Estado")]
    public bool temChave = false;

    [Header("Visual")]
    [Tooltip("O objeto da chave que é filho do jogador e aparecerá na cabeça dele.")]
    [SerializeField] private GameObject chaveNaCabeca;

    void Start()
    {
        if (chaveNaCabeca != null)
        {
            chaveNaCabeca.SetActive(false);
        }
    }

    public void ColetarChave()
    {
        temChave = true;

        if (chaveNaCabeca != null)
        {
            chaveNaCabeca.SetActive(true);
        }
    }
}
