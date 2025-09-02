using UnityEngine;
using System;

[RequireComponent(typeof(PlayerWeight))]
public class PlayerResizer : MonoBehaviour
{
    public event Action OnWeightOrSizeChanged;

    [Header("Configuração de Tamanho")]
    [Tooltip("A escala do personagem quando estiver pequeno.")]
    public Vector3 smallScale = new Vector3(0.5f, 0.5f, 0.5f);

    [Header("Configuração de Peso")]
    [Tooltip("O peso do personagem quando estiver em tamanho normal (grande).")]
    public float normalWeight = 2f;
    [Tooltip("O peso do personagem quando estiver pequeno.")]
    public float smallWeight = 0f;

    private Vector3 _normalScale;
    private PlayerWeight _playerWeight;
    private bool _isNormalSize = true;

    void Awake()
    {
        _normalScale = transform.localScale;
        _playerWeight = GetComponent<PlayerWeight>();
        if (_playerWeight != null)
        {
            _playerWeight.Weight = normalWeight;
        }
    }

    public void ToggleSize()
    {
        _isNormalSize = !_isNormalSize;

        if (_isNormalSize)
        {
            transform.localScale = _normalScale;
            _playerWeight.Weight = normalWeight;
        }
        else
        {
            transform.localScale = smallScale;
            _playerWeight.Weight = smallWeight;
        }

        OnWeightOrSizeChanged?.Invoke();
    }
}
