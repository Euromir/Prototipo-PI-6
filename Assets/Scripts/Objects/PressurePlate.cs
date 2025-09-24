using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine.InputSystem; // Adicione esta linha

public class PressurePlate : MonoBehaviour
{
    public event Action<PressurePlate> OnStateChanged;

    public enum ActivationAction { Deactivate, Destroy }

    [Header("Configuração da Plataforma")]
    public float requiredWeight = 2f;
    public bool isTrap = false;

    [Header("Ação Individual")]
    [Tooltip("Este objeto será afetado apenas por esta placa. Caso seja necessarias multiplas interações, use o multi plate puzzle")]
    public GameObject targetObject;
    public ActivationAction actionToPerform;

    private float _currentWeight = 0f;
    public bool _isActivated { get; private set; } = false;

    private List<PlayerWeight> _weightsOnPlate = new List<PlayerWeight>();

    // Referência direta ao PlayerManager
    private PlayerManager _playerManager;

    private void Awake()
    {
        // Encontra o PlayerManager na cena quando o jogo começa
        _playerManager = FindObjectOfType<PlayerManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerWeight playerWeight = other.GetComponentInParent<PlayerWeight>();
        if (playerWeight != null && !_weightsOnPlate.Contains(playerWeight))
        {
            _weightsOnPlate.Add(playerWeight);

            PlayerResizer playerResizer = playerWeight.GetComponent<PlayerResizer>();
            if (playerResizer != null)
            {
                playerResizer.OnWeightOrSizeChanged += RecalculateTotalWeight;
            }
            RecalculateTotalWeight();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerWeight playerWeight = other.GetComponentInParent<PlayerWeight>();
        if (playerWeight != null && _weightsOnPlate.Contains(playerWeight))
        {
            PlayerResizer playerResizer = playerWeight.GetComponent<PlayerResizer>();
            if (playerResizer != null)
            {
                playerResizer.OnWeightOrSizeChanged -= RecalculateTotalWeight;
            }

            _weightsOnPlate.Remove(playerWeight);
            RecalculateTotalWeight();
        }
    }

    private void RecalculateTotalWeight()
    {
        _weightsOnPlate = _weightsOnPlate.Where(p => p != null).ToList();

        _currentWeight = 0f;
        foreach (PlayerWeight weightComponent in _weightsOnPlate)
        {
            _currentWeight += weightComponent.Weight;
        }

        CheckWeight();
    }

    private void CheckWeight()
    {
        bool previousState = _isActivated;

        // Lógica da armadilha modificada
        if (_currentWeight >= requiredWeight && isTrap)
        {
            if (_playerManager != null)
            {
                // Para cada jogador na plataforma, pegue seu PlayerInput e mande para o PlayerManager
                foreach (PlayerWeight playerOnPlate in _weightsOnPlate)
                {
                    PlayerInput playerInput = playerOnPlate.GetComponentInParent<PlayerInput>();
                    if (playerInput != null)
                    {
                        _playerManager.RespawnPlayer(playerInput);
                    }
                }
            }
            return;
        }

        if (_currentWeight >= requiredWeight)
        {
            _isActivated = true;
            PerformAction();
        }
        else
        {
            _isActivated = false;
            ReverseAction();
        }

        if (_isActivated != previousState)
        {
            OnStateChanged?.Invoke(this);
        }
    }

    private void PerformAction()
    {
        if (targetObject == null) return;
        switch (actionToPerform)
        {
            case ActivationAction.Deactivate: targetObject.SetActive(false); break;
            case ActivationAction.Destroy: Destroy(targetObject); break;
        }
    }

    private void ReverseAction()
    {
        if (targetObject == null) return;
        switch (actionToPerform)
        {
            case ActivationAction.Deactivate: targetObject.SetActive(true); break;
            case ActivationAction.Destroy: break;
        }
    }
}
