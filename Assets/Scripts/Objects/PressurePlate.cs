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

    private List<PlayerResizer> _playersOnPlate = new List<PlayerResizer>();

    private void OnTriggerEnter(Collider other)
    {
        PlayerResizer player = other.GetComponentInParent<PlayerResizer>();
        if (player != null && !_playersOnPlate.Contains(player))
        {
            _playersOnPlate.Add(player);
            player.OnWeightOrSizeChanged += RecalculateTotalWeight;
            RecalculateTotalWeight();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerResizer player = other.GetComponentInParent<PlayerResizer>();
        if (player != null && _playersOnPlate.Contains(player))
        {
            player.OnWeightOrSizeChanged -= RecalculateTotalWeight;
            _playersOnPlate.Remove(player);
            RecalculateTotalWeight();
        }
    }

    private void RecalculateTotalWeight()
    {
        _playersOnPlate = _playersOnPlate.Where(p => p != null).ToList();

        _currentWeight = 0f;
        foreach (PlayerResizer player in _playersOnPlate)
        {
            _currentWeight += player.GetComponent<PlayerWeight>().Weight;
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
