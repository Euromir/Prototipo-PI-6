using UnityEngine;

[RequireComponent(typeof(PlayerWeight))]
public class PressurePlate : MonoBehaviour
{
    public enum ActivationAction { Deactivate, Destroy }

    [Header("Configuração da Plataforma")]
    [Tooltip("O peso total necessário para ativar a plataforma.")]
    public float requiredWeight = 2f;

    [Header("Ação")]
    [Tooltip("O objeto que será afetado quando a plataforma for ativada.")]
    public GameObject targetObject;

    [Tooltip("A ação a ser executada no objeto alvo.")]
    public ActivationAction actionToPerform;

    private float _currentWeight = 0f;
    private bool _isActivated = false;

    private void OnTriggerEnter(Collider other)
    {
        PlayerWeight player = other.GetComponent<PlayerWeight>();
        if (player != null)
        {
            _currentWeight += player.Weight;
            CheckWeight();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerWeight player = other.GetComponent<PlayerWeight>();
        if (player != null)
        {
            _currentWeight -= player.Weight;
            CheckWeight();
        }
    }

    private void CheckWeight()
    {
        if (!_isActivated && _currentWeight >= requiredWeight)
        {
            _isActivated = true;
            Debug.Log("Plataforma ativada!");
            PerformAction();
        }
    }

    private void PerformAction()
    {
        if (targetObject == null)
        {
            Debug.LogWarning("Nenhum objeto alvo foi definido para a plataforma de pressão.");
            return;
        }

        switch (actionToPerform)
        {
            case ActivationAction.Deactivate:
                targetObject.SetActive(false);
                break;
            case ActivationAction.Destroy:
                Destroy(targetObject);
                break;
        }
    }
}
