using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [Header("Configurações de Interação")]
    [Tooltip("O raio da esfera de detecção a partir da posição do jogador.")]
    [SerializeField] private float _interactRadius = 2.5f;
    [Tooltip("Define quais camadas (Layers) contêm objetos interativos.")]
    [SerializeField] private LayerMask _interactableLayer;

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, _interactRadius, _interactableLayer);

            if (colliders.Length > 0)
            {
                IInteractable interactable = colliders[0].GetComponent<IInteractable>();
                if (interactable != null)
                {
                    interactable.Interact(this.gameObject);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _interactRadius);
    }
}
