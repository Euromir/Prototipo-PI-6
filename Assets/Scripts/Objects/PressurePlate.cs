using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PressurePlate : MonoBehaviour
{
    public enum ActivationAction { Deactivate, Destroy }

    [Header("Configuração da Plataforma")]
    public float requiredWeight = 2f;

    [Header("Ação")]
    public GameObject targetObject;
    public ActivationAction actionToPerform;

    private float _currentWeight = 0f;
    private bool _isActivated = false;

    private List<PlayerWeight> _weightsOnPlate = new List<PlayerWeight>();

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
        if (_currentWeight >= requiredWeight && !_isActivated)
        {
            _isActivated = true;
            PerformAction();
        }
        else if (_currentWeight < requiredWeight && _isActivated)
        {
            _isActivated = false;
            ReverseAction();
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
