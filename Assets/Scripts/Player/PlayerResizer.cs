using UnityEngine;

public class PlayerResizer : MonoBehaviour
{
    [Header("Configurações de Tamanho")]
    [Tooltip("A escala do jogador quando estiver grande.")]
    public Vector3 BigScale = new Vector3(2f, 2f, 2f);

    [Tooltip("A escala do jogador quando estiver pequeno.")]
    public Vector3 SmallScale = new Vector3(0.5f, 0.5f, 0.5f);

    private Vector3 _originalScale;

    // Usaremos um número para controlar o estado:
    // 0 = Normal, 1 = Grande, 2 = Pequeno
    private int _currentState = 0;

    private void Awake()
    {
        // Guarda a escala inicial do jogador
        _originalScale = transform.localScale;
    }

    /// <summary>
    /// Alterna o tamanho do jogador entre Normal, Grande e Pequeno, em ciclo.
    /// </summary>
    public void ToggleSize()
    {
        // Avança para o próximo estado, e usa o operador % para ciclar de volta a 0 após o 2
        _currentState = (_currentState + 1) % 3;

        // Usa um switch para aplicar a escala correta baseada no estado atual
        switch (_currentState)
        {
            case 0: // Volta ao Normal
                transform.localScale = _originalScale;
                Debug.Log("Jogador voltou ao tamanho normal!");
                break;
            case 1: // Fica Grande
                transform.localScale = BigScale;
                Debug.Log("Jogador ficou grande!");
                break;
            case 2: // Fica Pequeno
                transform.localScale = SmallScale;
                Debug.Log("Jogador ficou pequeno!");
                break;
        }
    }
}