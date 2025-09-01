using UnityEngine;
using UnityEngine.InputSystem;

public class AbilityHolder : MonoBehaviour
{
    [Tooltip("A habilidade ativa do personagem.")]
    public Ability currentAbility;

    private float _cooldown;

    public void OnActivateAbility(InputAction.CallbackContext context)
    {

        if (context.performed)
        {
            if (currentAbility != null && Time.time >= _cooldown)
            {
                currentAbility.Activate(this.gameObject);
                _cooldown = Time.time + currentAbility.CooldownTime;
            }
        }
    }

    public void StartAbilityCoroutine(System.Collections.IEnumerator routine)
    {
        StartCoroutine(routine);
    }
}
