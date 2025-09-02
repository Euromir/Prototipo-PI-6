using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityHolder : MonoBehaviour
{
    [Tooltip("A habilidade ativa do personagem.")]
    public Ability CurrentAbility;

    [Header("Configurações de Mira")]
    public float RaycastDistance = 1f;
    public Color HighlightColor = Color.red;

    private float _cooldown;
    private IHighlightable _lastHighlightedTarget;

    private void Update()
    {
        if (CurrentAbility is ITargetingAbility targetingAbility)
        {
            LayerMask targetLayer = targetingAbility.GetTargetLayer();
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, RaycastDistance, targetLayer))
            {
                IHighlightable currentTarget = hit.collider.GetComponent<IHighlightable>();
                if (currentTarget != null)
                {
                    if (currentTarget != _lastHighlightedTarget)
                    {
                        ResetLastTarget();
                        _lastHighlightedTarget = currentTarget;
                        _lastHighlightedTarget.Highlight(HighlightColor);
                    }
                }
                else
                {
                    ResetLastTarget();
                }
            }
            else
            {
                ResetLastTarget();
            }
        }
        else
        {
            ResetLastTarget();
        }
    }

    private void ResetLastTarget()
    {
        if (_lastHighlightedTarget != null)
        {
            _lastHighlightedTarget.ResetColor();
            _lastHighlightedTarget = null;
        }
    }

    public void OnActivateAbility(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (CurrentAbility != null && Time.time >= _cooldown)
            {
                CurrentAbility.Activate(this.gameObject);
                _cooldown = Time.time + CurrentAbility.CooldownTime;
            }
        }
    }

    public void StartAbilityCoroutine(System.Collections.IEnumerator routine)
    {
        StartCoroutine(routine);
    }
}
