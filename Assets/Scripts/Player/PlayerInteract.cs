using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [Header("Configura��es de Intera��o")]
    [Tooltip("O raio da esfera de detec��o a partir da posi��o do jogador.")]
    [SerializeField] private float _interactRadius = 2.5f;
    [Tooltip("Define quais camadas (Layers) cont�m objetos interativos.")]
    [SerializeField] private LayerMask _interactableLayer;

    private int _playerID;

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, _interactRadius, _interactableLayer);

            if (colliders.Length > 0)
            {
                Collider targetCollider = colliders[0];
                IInteractable interactable = targetCollider.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    interactable.Interact(this.gameObject);

                    Vector3 interactPosition = targetCollider.transform.position;
                    PlayerEventSystem.InvokePlayerInteracted(_playerID, interactPosition);
                }
            }
        }
    }
}
