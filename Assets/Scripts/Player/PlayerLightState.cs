using UnityEngine;

public class PlayerLightState : MonoBehaviour
{
    [Header("Componentes do Jogador")]
    [Tooltip("A luz que será ligada e desligada no jogador.")]
    public Light playerLightObject;
    public Light secondaryLightObject;
    public Collider lightCollider;
    public GameObject Interactor; // O GameObject que contém o script Interactor

    [Header("Estado da Luz")]
    public bool isLightOn = true;

    // NOVO: Adicionamos variáveis para controlar o raio
    [Header("Configurações do Raio de Interação")]
    public float radiusWhenOn = 1.0f; // Raio quando a luz está LIGADA
    public float radiusWhenOff = 0.0f; // Raio quando a luz está DESLIGADA

    // NOVO: Variável para guardar a referência ao script Interactor
    private Interactor interactorComponent;

    // NOVO: Usamos Awake para pegar a referência do componente
    void Awake()
    {
        // Pega o componente 'Interactor' do GameObject que você referenciou
        if (Interactor != null)
        {
            interactorComponent = Interactor.GetComponent<Interactor>();
        }
        else
        {
            Debug.LogError("O GameObject do Interactor não foi atribuído no Inspector!", this.gameObject);
        }
    }

    void Start()
    {
        // Mantém a lógica original
        if (playerLightObject != null)
        {
            playerLightObject.enabled = isLightOn;
        }

        // NOVO: Define o raio inicial baseado no estado da luz
        UpdateInteractorRadius();
    }

    public void ToggleLight()
    {
        isLightOn = !isLightOn;

        playerLightObject.enabled = isLightOn;
        secondaryLightObject.enabled = isLightOn;

        lightCollider.enabled = isLightOn;

        UpdateInteractorRadius();
    }

    // NOVO: Criamos um método para manter o código organizado
    private void UpdateInteractorRadius()
    {
        if (interactorComponent != null)
        {
            // Se a luz está ligada, usa o raio 'On', senão, usa o raio 'Off'
            float newRadius = isLightOn ? radiusWhenOn : radiusWhenOff;
            interactorComponent.SetRadius(newRadius);
            Debug.Log($"O raio do interactor foi definido para: {newRadius}");
        }
    }
}
