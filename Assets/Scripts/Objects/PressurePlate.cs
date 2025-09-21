using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PressurePlate : MonoBehaviour
{
    public enum ActivationAction { Deactivate, Destroy }

    [Header("Configuração da Plataforma")]
    public float requiredWeight = 2f;
    public bool isTrap = false;

    [Header("Ação")]
    public GameObject targetObject;
    public ActivationAction actionToPerform;

    private float _currentWeight = 0f;
    public bool _isActivated = false;
    private MultiStepsPuzzle LockedDoor;
    private bool Kill;

    private List<PlayerWeight> _weightsOnPlate = new List<PlayerWeight>();

    private void Start()
    {
        LockedDoor = GetComponentInParent<MultiStepsPuzzle>();
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

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.root.CompareTag("Player") && Kill)
        {
            other.transform.root.gameObject.SetActive(false);
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
        if (_currentWeight >= requiredWeight && !_isActivated && !isTrap)
        {
            _isActivated = true;
            PerformAction();
        }
        else if (_currentWeight < requiredWeight && _isActivated && !isTrap)
        {
            _isActivated = false;
            ReverseAction();
        }

        if (_currentWeight >= requiredWeight && isTrap)
        {
            Kill = true;
        }
        else if (_currentWeight >= requiredWeight && isTrap)
        {
            Kill = false;
        }

        if (LockedDoor != null)
        {
            LockedDoor.CheckTheDoor.Invoke();
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
